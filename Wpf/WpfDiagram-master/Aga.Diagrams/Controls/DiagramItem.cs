﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Aga.Diagrams.Adorners;

/*
DiagramItem.cs - 图表元素基类，提供选择状态、移动能力、选择装饰器等基础功能

学习要点：
1. 抽象基类的设计模式和WPF控件继承体系
2. 依赖属性的高级应用（internal setter、属性变更回调）
3. 装饰器模式的实现（Adorner系统）
4. 抽象方法和虚方法的区别
5. WPF控件状态管理的最佳实践

设计模式：
- 模板方法模式：定义算法框架，子类实现具体细节
- 装饰器模式：通过Adorner为元素添加选择装饰
- 状态模式：管理选中、主选择等不同状态
*/
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// DiagramItem.cs - 图表元素基类
	/// 所有图表中显示的元素的抽象基类（节点、链接等）
	/// 提供选择状态管理、移动能力控制、装饰器支持
	/// 实现WPF依赖属性系统支持数据绑定和样式设置
	/// 管理元素的选中、主选择状态和边界信息
	/// 
	/// 继承体系：
	/// Control -> DiagramItem -> Node/Link/Port
	/// 
	/// 核心职责：
	/// 1. 选择状态管理（IsSelected、IsPrimarySelection）
	/// 2. 交互能力控制（CanMove、CanSelect）
	/// 3. 装饰器生命周期管理（显示/隐藏选择装饰器）
	/// 4. 模型绑定支持（ModelElement属性）
	/// 5. 几何边界抽象（Bounds属性）
	/// </summary>
	public abstract class DiagramItem : Control
	{
		#region Properties - 属性定义区域

		/// <summary>
		/// 选择装饰器私有属性 - 管理当前元素的选择视觉效果
		/// 当元素被选中时显示，取消选中时隐藏
		/// 通过CreateSelectionAdorner()抽象方法由子类定制具体样式
		/// </summary>
		private Adorner SelectionAdorner { get; set; }

		/// <summary>
		/// 模型元素属性 - 实现MVVM模式中视图与模型的绑定
		/// 存储与这个视觉元素对应的业务数据对象
		/// 常用于从视图元素反向查找对应的业务对象
		/// 支持任意类型的业务对象，提供最大的灵活性
		/// </summary>
		public object ModelElement { get; set; }

		#region IsSelected Property - 选中状态属性

		/// <summary>
		/// 元素选中状态属性 - 控制元素是否被选中
		/// 使用internal setter限制只能由程序集内部修改
		/// 当值变化时会自动显示或隐藏选择装饰器
		/// 支持数据绑定和样式触发器
		/// </summary>
		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			// internal修饰符确保只有Selection类等内部组件可以修改这个状态
			internal set { SetValue(IsSelectedProperty, value); }
		}

		/// <summary>
		/// IsSelected属性的依赖属性定义
		/// 使用internal修饰符限制访问范围，防止外部直接操作
		/// 设置PropertyChangedCallback实现状态变化的自动处理
		/// </summary>
		internal static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsSelectedChanged)));

		/// <summary>
		/// IsSelected属性变更时的回调函数 - 处理选择状态变化的副作用
		/// 实现以下功能：
		/// 1. 取消选中时清除主选择状态和隐藏装饰器
		/// 2. 选中时显示选择装饰器
		/// 3. 调用虚方法允许子类扩展处理逻辑
		/// </summary>
		private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// 如果取消选中（新值为false）
			if (!(bool)e.NewValue)
			{
				// 清除主选择状态（自动设置为false）
				d.ClearValue(IsPrimarySelectionProperty);
				
				// 隐藏选择装饰器
				(d as DiagramItem).HideSelectionAdorner();
			}
			else
				// 如果选中，显示选择装饰器
				(d as DiagramItem).ShowSelectionAdorner();
		}

		/// <summary>
		/// 虚方法 - 允许子类重写以扩展选择状态变化的处理逻辑
		/// 子类可以在这里添加额外的状态处理，如视觉效果、数据更新等
		/// 默认实现为空，不强制子类实现
		/// </summary>
		protected virtual void IsSelectedChanged()
		{
			// 默认空实现，子类可选择性重写
		}

		#endregion

		#region IsPrimarySelection Property - 主选择状态属性

		/// <summary>
		/// 主选择状态属性 - 标识这个元素是否为多选中的主要元素
		/// 在多选情况下，主选择项经常用作操作的参考基准（如对齐、分布等）
		/// 使用internal setter限制只能由Selection类等内部组件修改
		/// 通常与IsSelected属性配合使用，只有选中的元素才能成为主选择项
		/// </summary>
		public bool IsPrimarySelection
		{
			get { return (bool)GetValue(IsPrimarySelectionProperty); }
			// internal修饰符确保只有Selection管理器可以设置主选择状态
			internal set { SetValue(IsPrimarySelectionProperty, value); }
		}

		/// <summary>
		/// IsPrimarySelection属性的依赖属性定义
		/// 使用internal修饰符限制访问，防止外部直接操作主选择状态
		/// 默认值为false，不设置属性变更回调（由IsSelected管理）
		/// </summary>
		internal static readonly DependencyProperty IsPrimarySelectionProperty =
			DependencyProperty.Register("IsPrimarySelection",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(false));

		#endregion

		#region CanMove Property - 移动能力属性

		/// <summary>
		/// 移动能力属性 - 控制这个元素是否允许被拖拽移动
		/// 用于实现固定元素或限制某些元素的移动
		/// 公共setter允许外部代码设置，提供灵活的控制能力
		/// 默认值为true，大多数元素都允许移动
		/// </summary>
		public bool CanMove
		{
			get { return (bool)GetValue(CanMoveProperty); }
			set { SetValue(CanMoveProperty, value); }
		}

		/// <summary>
		/// CanMove属性的依赖属性定义
		/// 使用公共访问修饰符，允许在XAML中设置和数据绑定
		/// 可以通过样式、触发器等方式动态控制
		/// </summary>
		public static readonly DependencyProperty CanMoveProperty =
			DependencyProperty.Register("CanMove",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(true));

		#endregion

		#region CanSelect Property - 选择能力属性

		/// <summary>
		/// 选择能力属性 - 控制这个元素是否允许被选中
		/// 用于实现不可选中的装饰性元素或禁用某些元素的选择
		/// 在复杂界面中可以有选择性地限制用户交互
		/// 默认值为true，大多数元素都允许选择
		/// </summary>
		public bool CanSelect
		{
			get { return (bool)GetValue(CanSelectProperty); }
			set { SetValue(CanSelectProperty, value); }
		}

		/// <summary>
		/// CanSelect属性的依赖属性定义
		/// 使用公共访问修饰符，支持XAML设置和数据绑定
		/// 可以与业务逻辑结合，实现动态权限控制
		/// </summary>
		public static readonly DependencyProperty CanSelectProperty =
			DependencyProperty.Register("CanSelect",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(true));

		#endregion

		/// <summary>
		/// 抽象属性 - 元素的边界矩形
		/// 由子类实现具体的边界计算逻辑，用于：
		/// 1. 碰撞检测和点击测试
		/// 2. 布局和对齐操作
		/// 3. 选择框的绘制和碰撞检测
		/// 4. 缩放和适应操作
		/// 子类必须实现这个属性以提供准确的边界信息
		/// </summary>
		public abstract Rect Bounds { get; }

		#endregion

		/// <summary>
		/// 隐藏选择装饰器的受保护方法
		/// 在取消选中或元素销毁时调用，清理视觉资源
		/// 安全地从 AdornerLayer 中移除装饰器，防止内存泄漏
		/// </summary>
		protected void HideSelectionAdorner()
		{
			// 检查是否存在选择装饰器
			if (SelectionAdorner != null)
			{
				// 获取当前元素的 AdornerLayer
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
				
				// 检查 AdornerLayer 是否存在（可能在元素销毁时为 null）
				if (adornerLayer != null)
					// 从层中移除装饰器
					adornerLayer.Remove(SelectionAdorner);
					
				// 清空引用，防止内存泄漏
				SelectionAdorner = null;
			}
		}

		/// <summary>
		/// 显示选择装饰器的受保护方法
		/// 在元素被选中时调用，创建并显示视觉反馈
		/// 使用抽象方法允许子类定制装饰器样式
		/// </summary>
		protected void ShowSelectionAdorner()
		{
			// 获取当前元素的 AdornerLayer
			var adornerLayer = AdornerLayer.GetAdornerLayer(this);
			
			// 检查 AdornerLayer 是否存在（在控件模板加载前可能为 null）
			if (adornerLayer != null)
			{
				// 通过抽象方法创建特定类型的选择装饰器
				SelectionAdorner = CreateSelectionAdorner();
				
				// 设置装饰器为可见状态
				SelectionAdorner.Visibility = Visibility.Visible;
				
				// 将装饰器添加到 AdornerLayer 中显示
				adornerLayer.Add(SelectionAdorner);
			}
		}

		/// <summary>
		/// 抽象方法 - 创建选择装饰器
		/// 由子类实现具体的装饰器创建逻辑
		/// 允许不同类型的元素（节点、链接、端口）有不同的选择视觉效果
		/// 
		/// 实现指导：
		/// - Node类可能返回SelectionAdorner（显示边框和缩放手柄）
		/// - Link类可能返回LinkSelectionAdorner（高亮连线）
		/// - Port类可能返回PortSelectionAdorner（高亮端口）
		/// </summary>
		/// <returns>新创建的选择装饰器实例</returns>
		protected abstract Adorner CreateSelectionAdorner();
	}
}
