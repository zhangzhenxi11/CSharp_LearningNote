﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Aga.Diagrams.Adorners;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 输入处理工具类 - 实现图表编辑器的基础交互功能
	/// 
	/// 【设计模式】：策略模式的具体实现类
	/// 【核心功能】：
	/// 1. 处理鼠标事件（点击、拖拽、移动）
	/// 2. 处理键盘事件（Esc键取消操作）
	/// 3. 管理元素选择状态（单选、多选、框选）
	/// 4. 启动其他专用工具（拖拽工具、橡皮筋选择）
	/// 
	/// 【交互逻辑】：
	/// - 鼠标按下：记录起始点和目标元素
	/// - 鼠标移动：判断是否启动拖拽或框选
	/// - 鼠标抬起：完成元素选择操作
	/// - Esc键：取消当前操作
	/// 
	/// 【多选支持】：
	/// - Ctrl+点击：切换元素选择状态
	/// - Shift+点击：追加元素到选择集合
	/// - 普通点击：单选或清空选择
	/// 
	/// 【学习要点】：
	/// - 事件冒泡处理：e.Handled = true阻止事件继续传播
	/// - 可空类型使用：Point? MouseDownPoint
	/// - 键盘修饰符检测：Keyboard.Modifiers
	/// - 依赖对象查找：FindParent<T>扩展方法
	/// </summary>
	public class InputTool : IInputTool
	{
		/// <summary>
		/// 关联的图表视图控件 - 提供UI上下文和状态访问
		/// 【设计特点】：protected访问级别，允许子类访问但限制外部访问
		/// </summary>
		protected DiagramView View { get; private set; }
		
		/// <summary>
		/// 图表控制器属性 - 提供业务逻辑操作接口
		/// 【设计模式】：代理模式，通过View.Controller获取控制器实例
		/// 【作用】：将UI操作转发给业务逻辑层处理
		/// </summary>
		protected IDiagramController Controller { get { return View.Controller; } }
		
		/// <summary>
		/// 鼠标按下时的坐标点 - 用于计算拖拽距离和方向
		/// 【可空类型】：Point? 表示可能没有值的坐标
		/// 【用途】：
		/// 1. 判断是否有未完成的鼠标操作
		/// 2. 计算拖拽的起始位置
		/// 3. 检测最小拖拽距离阈值
		/// </summary>
		protected Point? MouseDownPoint { get; set; }
		
		/// <summary>
		/// 鼠标按下时的目标元素 - 记录点击的图表元素
		/// 【作用】：
		/// 1. 区分点击元素还是空白区域
		/// 2. 为拖拽操作提供目标对象
		/// 3. 支持复杂的选择逻辑
		/// </summary>
		protected DiagramItem MouseDownItem { get; set; }

		/// <summary>
		/// 构造函数 - 初始化输入工具并关联图表视图
		/// 
		/// 【参数验证】：通常需要检查view参数是否为null
		/// 【初始化】：建立工具与视图的双向关联
		/// </summary>
		/// <param name="view">要关联的图表视图控件</param>
		public InputTool(DiagramView view)
		{
			View = view;
		}

		/// <summary>
		/// 处理鼠标按下事件 - 记录操作起始状态
		/// 
		/// 【核心逻辑】：
		/// 1. 查找鼠标下的图表元素（使用FindParent扩展方法）
		/// 2. 记录鼠标按下的坐标位置
		/// 3. 标记事件已处理，阻止事件冒泡
		/// 
		/// 【技术要点】：
		/// - e.OriginalSource：获取最初触发事件的UI元素
		/// - FindParent<T>：在可视化树中向上查找指定类型的父元素
		/// - e.GetPosition(View)：获取相对于View控件的鼠标坐标
		/// - virtual修饰符：允许子类重写此方法实现特定逻辑
		/// </summary>
		/// <param name="e">鼠标按钮事件参数</param>
		public virtual void OnMouseDown(MouseButtonEventArgs e)
		{
			MouseDownItem = (e.OriginalSource as DependencyObject).FindParent<DiagramItem>();
			MouseDownPoint = e.GetPosition(View);
			e.Handled = true;
		}

		/// <summary>
		/// 处理鼠标移动事件 - 检测并启动拖拽或框选操作
		/// 
		/// 【触发条件】：左键按下且有记录的起始点
		/// 【操作分支】：
		/// 1. 有目标元素：启动拖拽工具（DragTool.BeginDrag）
		/// 2. 无目标元素：清空选择并启动橡皮筋框选
		/// 
		/// 【技术细节】：
		/// - MouseButtonState.Pressed：检查左键是否仍处于按下状态
		/// - HasValue：检查可空类型是否有值
		/// - DragThumbKinds.Center：指定拖拽类型为中心拖拽（移动）
		/// - 操作完成后清空状态变量，避免重复触发
		/// 
		/// 【设计模式应用】：
		/// - 状态机模式：通过MouseDownPoint状态控制行为
		/// - 工厂模式：CreateRubberbandAdorner创建装饰器
		/// </summary>
		/// <param name="e">鼠标移动事件参数</param>
		public virtual void OnMouseMove(MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && MouseDownPoint.HasValue)
			{
				if (MouseDownItem != null)
				{
					View.DragTool.BeginDrag(MouseDownPoint.Value, MouseDownItem, DragThumbKinds.Center);
				}
				else
				{
					View.Selection.Clear();
					View.DragAdorner = CreateRubberbandAdorner();
				}
				MouseDownItem = null;
				MouseDownPoint = null;
			}
			e.Handled = true;
		}

		/// <summary>
		/// 处理鼠标抬起事件 - 完成元素选择操作
		/// 
		/// 【执行条件】：必须有记录的鼠标按下点（MouseDownPoint != null）
		/// 【核心流程】：
		/// 1. 查找鼠标抬起位置的图表元素
		/// 2. 调用SelectItem方法处理选择逻辑
		/// 3. 标记事件已处理
		/// 
		/// 【技术说明】：
		/// - e.Source vs e.OriginalSource：这里使用Source获取直接事件源
		/// - 选择逻辑委托给SelectItem方法，支持多种选择模式
		/// - 如果MouseDownPoint为null，说明已经被其他操作消费，直接返回
		/// 
		/// 【交互设计】：支持点击选择和框选后的元素选择
		/// </summary>
		/// <param name="e">鼠标按钮事件参数</param>
		public virtual void OnMouseUp(MouseButtonEventArgs e)
		{
			if (MouseDownPoint == null)
				return;

			var item = (e.Source as DependencyObject).FindParent<DiagramItem>();
			SelectItem(item);
			e.Handled = true;
		}

		/// <summary>
		/// 处理键盘按键预览事件 - 实现Esc键取消当前操作
		/// 
		/// 【Esc键功能】：
		/// 1. 如果有活动的拖拽装饰器且捕获了鼠标，释放鼠标捕获
		/// 2. 否则清空当前选择
		/// 
		/// 【技术要点】：
		/// - PreviewKeyDown：键盘事件的预览阶段，优先级更高
		/// - IsMouseCaptured：检查控件是否捕获了鼠标输入
		/// - ReleaseMouseCapture()：释放鼠标捕获，恢复正常鼠标处理
		/// 
		/// 【用户体验】：提供通用的"取消"操作，符合Windows界面标准
		/// 【扩展性】：virtual修饰符允许子类添加更多取消逻辑
		/// </summary>
		/// <param name="e">键盘事件参数</param>
		public virtual void OnPreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				e.Handled = true;
				if (View.DragAdorner != null && View.DragAdorner.IsMouseCaptured)
					View.DragAdorner.ReleaseMouseCapture();
				else
					View.Selection.Clear();
			}
		}

		/// <summary>
		/// 选择图表元素 - 实现多种选择模式的核心逻辑
		/// 
		/// 【选择模式详解】：
		/// 1. Ctrl+点击：切换模式
		///    - 已选中的元素：取消选择（Remove）
		///    - 未选中的元素：添加到选择（Add）
		/// 2. Shift+点击：追加模式
		///    - 直接添加元素到现有选择，不影响其他已选元素
		/// 3. 普通点击：替换模式
		///    - 有元素：清空现有选择并选中新元素（Set）
		///    - 无元素：清空所有选择（Clear）
		/// 
		/// 【安全检查】：
		/// - item != null：确保点击了有效元素
		/// - item.CanSelect：检查元素是否允许被选择
		/// 
		/// 【技术实现】：
		/// - Keyboard.Modifiers：获取当前按下的修饰键
		/// - ModifierKeys枚举：Control、Shift、Alt等修饰键
		/// - Selection对象：管理选择集合的专用类
		/// 
		/// 【设计原则】：符合Windows标准的多选交互模式
		/// </summary>
		/// <param name="item">要处理选择的图表元素，可以为null</param>
		protected virtual void SelectItem(DiagramItem item)
		{
			var sel = View.Selection;
			if (Keyboard.Modifiers == ModifierKeys.Control)
			{
				if (item != null && item.CanSelect)
				{
					if (item.IsSelected)
						sel.Remove(item);
					else
						sel.Add(item);
				}
			}
			else if (Keyboard.Modifiers == ModifierKeys.Shift)
			{
				if (item != null && item.CanSelect)
					sel.Add(item);
			}
			else
			{
				if (item != null && item.CanSelect)
					sel.Set(item);
				else
					sel.Clear();
			}
		}

		/// <summary>
		/// 创建橡皮筋选择装饰器 - 工厂方法模式的应用
		/// 
		/// 【设计模式】：工厂方法模式
		/// 【功能】：创建用于框选操作的视觉装饰器
		/// 【参数】：使用MouseDownPoint.Value作为框选起始点
		/// 
		/// 【技术细节】：
		/// - RubberbandAdorner：实现框选矩形的绘制和交互
		/// - MouseDownPoint.Value：可空类型的值提取，调用前已确保HasValue
		/// - virtual修饰符：允许子类创建自定义的橡皮筋装饰器
		/// 
		/// 【使用场景】：鼠标在空白区域拖拽时启动框选功能
		/// 【视觉效果】：显示虚线矩形框，实时跟随鼠标移动
		/// </summary>
		/// <returns>橡皮筋选择装饰器实例</returns>
		protected virtual Adorner CreateRubberbandAdorner()
		{
			return new RubberbandAdorner(View, MouseDownPoint.Value);
		}
	}
}
