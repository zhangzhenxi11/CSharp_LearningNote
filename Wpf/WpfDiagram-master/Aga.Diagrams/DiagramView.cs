﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using Aga.Diagrams.Controls;
using Aga.Diagrams.Tools;
using System.Windows.Media;
using System.Windows.Input;

/*
DiagramView.cs - 核心图表画布控件，继承自Canvas，提供网格显示、缩放、选择、拖拽等基础功能
学习要点：
1. WPF依赖属性的定义和使用模式
2. Canvas绘制和自定义渲染的实现方法
3. 工具模式（Tool Pattern）的应用
4. WPF命令系统的集成方式
5. Adorner装饰器系统的使用
*/
namespace Aga.Diagrams
{
	/// <summary>
	/// DiagramView.cs - 图表视图主控件
	/// 继承自WPF Canvas，为图表编辑器提供核心画布功能
	/// 支持网格显示、缩放、拖拽、选择、链接等操作
	/// 管理各种工具（InputTool、DragTool、LinkTool等）
	/// 提供命令绑定和事件处理机制
	/// 支持装饰器系统和鼠标交互
	/// 
	/// 设计模式应用：
	/// - 策略模式：不同的工具实现不同的交互策略
	/// - 装饰器模式：Adorner为控件添加动态视觉效果
	/// - 命令模式：通过WPF命令系统处理用户操作
	/// </summary>
	public class DiagramView : Canvas
	{
		/// <summary>
		/// 网格绘制画笔，用于DrawGrid方法绘制背景网格
		/// 随缩放比例动态调整线条粗细，保持视觉一致性
		/// </summary>
		private Pen _gridPen;

		/// <summary>
		/// 静态构造函数 - WPF控件的标准初始化模式
		/// 设置默认样式键，告诉WPF在themes/generic.xaml中查找对应的控件模板
		/// 这是WPF自定义控件的标准做法，用于主题和样式支持
		/// </summary>
		static DiagramView()
		{
			// 重写默认样式键，指向DiagramView类型
			// 这样WPF会自动应用themes/DiagramView.xaml中定义的控件模板
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(DiagramView), new FrameworkPropertyMetadata(typeof(DiagramView)));
		}

		#region Properties - WPF依赖属性的定义区域

		#region GridCellSize - 网格单元格大小属性

		/// <summary>
		/// 网格单元格大小的依赖属性定义
		/// 使用DependencyProperty实现WPF数据绑定、样式和动画支持
		/// 默认值为10x10像素，用于背景网格的绘制间距
		/// </summary>
		public static readonly DependencyProperty GridCellSizeProperty =
			DependencyProperty.Register("GridCellSize",    // 属性名称
									   typeof(Size),           // 属性类型
									   typeof(DiagramView),    // 所有者类型
									   new FrameworkPropertyMetadata(new Size(10, 10))); // 元数据和默认值

		/// <summary>
		/// 网格单元格大小属性的CLR封装
		/// 提供C#代码中的标准属性访问方式，内部调用GetValue/SetValue
		/// 返回Size结构，表示网格的宽度和高度
		/// </summary>
		public Size GridCellSize
		{
			get { return (Size)GetValue(GridCellSizeProperty); }
			set { SetValue(GridCellSizeProperty, value); }
		}

		#endregion

		#region ShowGrid - 是否显示网格属性

		/// <summary>
		/// 控制是否显示背景网格的依赖属性
		/// 布尔类型，默认为false（不显示网格）
		/// 当值变为true时，OnRender方法会调用DrawGrid绘制网格线
		/// </summary>
		public static readonly DependencyProperty ShowGridProperty =
			DependencyProperty.Register("ShowGrid",
									   typeof(bool),
									   typeof(DiagramView),
									   new FrameworkPropertyMetadata(false));

		/// <summary>
		/// 是否显示网格属性的CLR封装
		/// 用于在XAML中或代码中控制背景网格的显示/隐藏
		/// </summary>
		public bool ShowGrid
		{
			get { return (bool)GetValue(ShowGridProperty); }
			set { SetValue(ShowGridProperty, value); }
		}

		#endregion

		#region DocumentSize - 文档大小属性

		/// <summary>
		/// 图表文档大小的依赖属性
		/// 定义了整个图表的可编辑区域大小，默认为2000x1500像素
		/// 在MeasureOverride中用作最大测量尺寸，影响滚动条显示
		/// </summary>
		public static readonly DependencyProperty DocumentSizeProperty =
			DependencyProperty.Register("DocumentSize",
									   typeof(Size),
									   typeof(DiagramView),
									   new FrameworkPropertyMetadata(new Size(2000, 1500)));

		/// <summary>
		/// 文档大小属性的CLR封装
		/// 表示整个图表的理论上的最大编辑区域，超出此范围可能需要滚动
		/// </summary>
		public Size DocumentSize
		{
			get { return (Size)GetValue(DocumentSizeProperty); }
			set { SetValue(DocumentSizeProperty, value); }
		}

		#endregion

		#region Zoom - 缩放比例属性

		/// <summary>
		/// 缩放比例的依赖属性，支持属性变更回调
		/// 默认值为1.0（100%），支持任意正数值
		/// 当值变更时触发OnZoomChanged回调，自动应用缩放变换
		/// </summary>
		public static readonly DependencyProperty ZoomProperty =
			DependencyProperty.Register("Zoom",
									   typeof(double),
									   typeof(DiagramView),
									   new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnZoomChanged)));

		/// <summary>
		/// 缩放变更回调函数 - 处理缩放比例变化的副作用
		/// 当Zoom属性变更时自动调用，实现以下功能：
		/// 1. 重新创建网格画笔（调整线条粗细）
		/// 2. 应用缩放变换（ScaleTransform）到LayoutTransform
		/// 3. 当缩放为1.0时清除变换以优化性能
		/// </summary>
		private static void OnZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var view = d as DiagramView;
			var zoom = (double)e.NewValue;
			
			// 重新创建网格画笔，根据缩放比例调整线条粗细
			view._gridPen = view.CreateGridPen();
			
			// 应用缩放变换：当缩放为1.0时不应用变换以优化性能
			if (Math.Abs(zoom - 1) < 0.0001)
				view.LayoutTransform = null;
			else
				// 创建缩放变换，同时作用于X和Y轴
				view.LayoutTransform = new ScaleTransform(zoom, zoom);
		}

		/// <summary>
		/// 缩放比例属性的CLR封装
		/// 控制整个图表的缩放级别，1.0为原始大小，2.0为200%放大
		/// 通过LayoutTransform实现，影响所有子元素的显示大小
		/// </summary>
		public double Zoom
		{
			get { return (double)GetValue(ZoomProperty); }
			set { SetValue(ZoomProperty, value); }
		}

		#endregion

		/// <summary>
		/// 选择管理器 - 管理当前选中的图表元素集合
		/// 支持单选和多选，提供选中状态的统一管理
		/// 实现INotifyPropertyChanged，支持数据绑定和视图更新
		/// </summary>
		public Selection Selection { get; private set; }

		/// <summary>
		/// 图表控制器接口 - 实现业务逻辑和数据操作
		/// 由外部设置，处理命令执行、元素更新等高级操作
		/// 实现业务逻辑与视图层的分离，遵循MVVM模式
		/// </summary>
		public IDiagramController Controller { get; set; }

		/// <summary>
		/// 当前画布上的所有图表元素集合
		/// 使用LINQ过滤Children集合，只返回DiagramItem类型的子元素
		/// 包括Node、Link等所有继承自DiagramItem的元素
		/// </summary>
		public IEnumerable<DiagramItem> Items
		{
			get { return Children.OfType<DiagramItem>(); }
		}

		/// <summary>
		/// 输入工具接口 - 处理鼠标和键盘输入事件
		/// 负责分发输入事件给具体的工具（拖拽、链接等）
		/// 实现策略模式，根据上下文决定当前活动的工具
		/// </summary>
		private IInputTool _inputTool;
		public IInputTool InputTool
		{
			get { return _inputTool; }
			set
			{
				// 参数验证：输入工具不能为空，是核心组件
				if (value == null) 
					throw new ArgumentNullException("value");
				_inputTool = value;
			}
		}

		/// <summary>
		/// 移动和缩放工具接口 - 处理元素的拖拽移动和尺寸调整
		/// 实现元素的位置变更、大小调整等交互操作
		/// 通过Adorner提供视觉反馈，如选择框、拖拽手柄等
		/// </summary>
		private IMoveResizeTool _dragTool;
		public IMoveResizeTool DragTool
		{
			get { return _dragTool; }
			set
			{
				// 参数验证：拖拽工具不能为空
				if (value == null)
					throw new ArgumentNullException("value");
				_dragTool = value;
			}
		}

		/// <summary>
		/// 链接工具接口 - 处理节点间的连接操作
		/// 实现从源端口到目标端口的连线绘制
		/// 支持连线预览、连接验证、连线重建等功能
		/// </summary>
		private ILinkTool _linkTool;
		public ILinkTool LinkTool
		{
			get { return _linkTool; }
			set
			{
				// 参数验证：链接工具不能为空
				if (value == null)
					throw new ArgumentNullException("value");
				_linkTool = value;
			}
		}

		/// <summary>
		/// 拖放工具接口 - 处理从外部拖放元素到画布的操作
		/// 可选工具，用于支持从工具箱或其他控件拖放元素
		/// 实现拖放创建新节点、复制元素等功能
		/// </summary>
		public IDragDropTool DragDropTool { get; set; }

		/// <summary>
		/// 当前活动的拖拽装饰器 - 用于拖拽过程中的视觉反馈
		/// 自动管理Adorner的添加和移除，在AdornerLayer中显示
		/// 包括选择框、拖拽预览、连线预览等装饰器
		/// </summary>
		private Adorner _dragAdorner;
		public Adorner DragAdorner
		{
			get { return _dragAdorner; }
			set
			{
				// 只有当新值与当前值不同时才更新
				if (_dragAdorner != value)
				{
					// 获取当前控件的AdornerLayer
					var adornerLayer = AdornerLayer.GetAdornerLayer(this);
					
					// 移除旧的装饰器
					if (_dragAdorner != null)
						adornerLayer.Remove(_dragAdorner);
						
					_dragAdorner = value;
					
					// 添加新的装饰器
					if (_dragAdorner != null)
						adornerLayer.Add(_dragAdorner);
				}
			}
		}

		/// <summary>
		/// 判断当前是否在拖拽状态
		/// 通过检查DragAdorner是否为null来判断
		/// 用于在其他操作中判断当前状态
		/// </summary>
		public bool IsDragging { get { return DragAdorner != null; } }

		#endregion

		/// <summary>
		/// DiagramView构造函数 - 初始化图表控件的所有核心组件
		/// 设置默认工具、初始化选择管理器、绑定命令等
		/// 这是学习WPF控件初始化模式的经典示例
		/// </summary>
		public DiagramView()
		{
			// 初始化网格画笔，用于背景网格的绘制
			_gridPen = CreateGridPen();
			
			// 创建选择管理器实例，管理元素的选中状态
			Selection = new Selection();
			
			// 设置默认工具：输入工具、拖拽工具、链接工具
			InputTool = new InputTool(this);      // 处理基本输入事件
			DragTool = new MoveResizeTool(this);  // 处理元素移动和缩放
			LinkTool = new LinkTool(this);        // 处理节点连接
			
			// 绑定标准命令（复制、粘贴、删除等）
			BindCommands();
			
			// 设置为可获取焦点，以便接收键盘输入
			Focusable = true;

			// 订阅布局更新事件，用于同步节点位置
			this.LayoutUpdated += new EventHandler(DiagramView_LayoutUpdated);
		}

		/// <summary>
		/// 布局更新事件处理器 - 同步节点位置信息
		/// 当控件布局发生变化时，通知所有节点更新其位置
		/// 这对于Canvas中的元素位置管理非常重要
		/// </summary>
		void DiagramView_LayoutUpdated(object sender, EventArgs e)
		{
			// 遍历所有Node类型的子元素，更新其位置信息
			foreach (var n in this.Children.OfType<Node>())
				n.UpdatePosition();
		}

		/// <summary>
		/// 根据模型元素查找对应的视图元素
		/// 实现模型与视图的双向绑定，是MVVM模式的关键方法
		/// 用于从业务对象快速定位到对应的UI元素
		/// </summary>
		/// <param name="modelElement">模型对象，通常是业务实体</param>
		/// <returns>对应的图表元素，找不到返回null</returns>
		public DiagramItem FindItem(object modelElement)
		{
			// 使用LINQ在所有Items中查找第一个ModelElement匹配的元素
			return Items.FirstOrDefault(p => p.ModelElement == modelElement);
		}

		/// <summary>
		/// 重写鼠标按下事件处理 - 实现事件分发机制
		/// 将鼠标事件委托给InputTool处理，实现职责分离
		/// 同时设置焦点以支持键盘输入
		/// </summary>
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			// 将事件交给输入工具处理，实现策略模式
			InputTool.OnMouseDown(e);
			
			// 调用基类方法，保持WPF事件链的完整性
			base.OnMouseDown(e);
			
			// 设置焦点到当前控件，以便接收键盘输入
			Focus();
		}

		/// <summary>
		/// 重写鼠标移动事件处理 - 处理拖拽和悬停操作
		/// 支持拖拽预览、悬停高亮等交互效果
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			// 将鼠标移动事件交给输入工具处理
			InputTool.OnMouseMove(e);
			base.OnMouseMove(e);
		}

		/// <summary>
		/// 重写鼠标释放事件处理 - 完成拖拽、选择等操作
		/// 结束各种正在进行的交互操作
		/// </summary>
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			InputTool.OnMouseUp(e);
			base.OnMouseUp(e);
		}

		/// <summary>
		/// 重写键盘按下事件处理 - 支持快捷键和特殊操作
		/// 如Delete键删除、Ctrl+C复制等快捷键操作
		/// </summary>
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			// 先交给输入工具处理，再调用基类方法
			InputTool.OnPreviewKeyDown(e);
			base.OnPreviewKeyDown(e);
		}

		/// <summary>
		/// 重写拖放进入事件处理 - 支持从外部拖放元素到画布
		/// 用于从工具箱拖放新节点到图表中
		/// </summary>
		protected override void OnDragEnter(DragEventArgs e)
		{
			// 如果设置了拖放工具，则交给它处理
			if (DragDropTool != null)
				DragDropTool.OnDragEnter(e);
			base.OnDragEnter(e);
		}

		/// <summary>
		/// 重写拖放离开事件处理 - 清除拖放预览效果
		/// </summary>
		protected override void OnDragLeave(DragEventArgs e)
		{
			if (DragDropTool != null)
				DragDropTool.OnDragLeave(e);
			base.OnDragLeave(e);
		}

		/// <summary>
		/// 重写拖放悬停事件处理 - 提供拖放过程中的视觉反馈
		/// </summary>
		protected override void OnDragOver(DragEventArgs e)
		{
			if (DragDropTool != null)
				DragDropTool.OnDragOver(e);
			base.OnDragOver(e);
		}

		/// <summary>
		/// 重写拖放释放事件处理 - 完成元素的创建和放置
		/// 这是拖放操作的最终步骤，实际创建新节点
		/// </summary>
		protected override void OnDrop(DragEventArgs e)
		{
			if (DragDropTool != null)
				DragDropTool.OnDrop(e);
			base.OnDrop(e);
		}

		/// <summary>
		/// 重写测量方法 - 设置控件的最大尺寸
		/// 使用DocumentSize作为最大尺寸，影响滚动条的显示
		/// 这是WPF自定义控件布局系统的关键方法
		/// </summary>
		protected override Size MeasureOverride(Size constraint)
		{
			// 先调用基类Measure，传入DocumentSize作为约束
			base.MeasureOverride(DocumentSize);
			
			// 返回DocumentSize作为需要的尺寸，影响ScrollViewer行为
			return DocumentSize;
		}

		/// <summary>
		/// 重写渲染方法 - 绘制背景和网格
		/// 实现自定义的画布背景效果，是WPF自定义绘制的核心
		/// </summary>
		protected override void OnRender(DrawingContext dc)
		{
			// 创建背景矩形，覆盖整个控件区域
			var rect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);
			
			// 绘制背景颜色，使用Background属性的值
			dc.DrawRectangle(Background, null, rect);
			
			// 如果启用了网格显示且网格尺寸有效，则绘制网格
			if (ShowGrid && GridCellSize.Width > 0 && GridCellSize.Height > 0)
				DrawGrid(dc, rect);
		}

		/// <summary>
		/// 绘制网格线的虚函数 - 可被子类重写以定制网格样式
		/// 使用循环绘制水平和垂直网格线，实现网格背景效果
		/// </summary>
		/// <param name="dc">绘制上下文，用于执行绘制操作</param>
		/// <param name="rect">绘制区域范围</param>
		protected virtual void DrawGrid(DrawingContext dc, Rect rect)
		{
			// 使用 .5 像素偏移强制 WPF 绘制单像素线条（避免模糊）
			// 绘制水平网格线
			for (var i = 0.5; i < rect.Height; i += GridCellSize.Height)
				dc.DrawLine(_gridPen, new Point(0, i), new Point(rect.Width, i));
				
			// 绘制垂直网格线
			for (var i = 0.5; i < rect.Width; i += GridCellSize.Width)
				dc.DrawLine(_gridPen, new Point(i, 0), new Point(i, rect.Height));
		}

		/// <summary>
		/// 创建网格画笔的虚函数 - 可被子类重写以定制网格样式
		/// 根据当前缩放比例调整线条粗细，保持视觉一致性
		/// </summary>
		/// <returns>用于绘制网格的画笔对象</returns>
		protected virtual Pen CreateGridPen()
		{
			// 创建浅灰色画笔，线条粗细随缩放反比调整
			// 缩放越大，线条越细，保持视觉上的一致性
			return new Pen(Brushes.LightGray, (1 / Zoom));
		}

		/// <summary>
		/// 绑定WPF标准命令的私有方法
		/// 实现复制、粘贴、删除等常用操作的快捷键支持
		/// </summary>
		private void BindCommands()
		{
			// 添加命令绑定，将标准命令与本控件的处理方法关联
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, ExecuteCommand, CanExecuteCommand));
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, ExecuteCommand, CanExecuteCommand));
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ExecuteCommand, CanExecuteCommand));
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, ExecuteCommand, CanExecuteCommand));
		}

		/// <summary>
		/// 命令执行处理器 - 实际执行用户命令的地方
		/// 先委托给Controller处理，再处理本地特殊逻辑
		/// </summary>
		private void ExecuteCommand(object sender, ExecutedRoutedEventArgs e)
		{
			// 如果设置了Controller，先让Controller处理命令
			if (Controller != null)
				Controller.ExecuteCommand(e.Command, e.Parameter);
				
			// 对于删除命令，额外清空选择状态
			// 这确保删除后界面上不会显示遗留的选择框
			if (e.Command == ApplicationCommands.Delete)
				Selection.Clear();
		}

		/// <summary>
		/// 命令可执行性判断处理器 - 决定命令是否可用
		/// 由Controller决定具体的业务逻辑，如是否有选中元素等
		/// </summary>
		private void CanExecuteCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			// 如果设置了Controller，由Controller决定命令是否可执行
			if (Controller != null)
				e.CanExecute = Controller.CanExecuteCommand(e.Command, e.Parameter);
		}
	}
}
