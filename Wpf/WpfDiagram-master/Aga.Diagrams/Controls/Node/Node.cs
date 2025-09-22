﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Aga.Diagrams.Adorners;

//Node.cs - 节点实现类，支持内容显示、调整大小、端口管理
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// 节点控件实现类 - 图表中可连接的核心元素
	/// 
	/// 【继承关系】：Node -> DiagramItem -> ContentControl
	/// 【接口实现】：INode - 提供节点特有的端口功能
	/// 
	/// 【核心功能】：
	/// 1. 内容显示：通过Content属性显示任意对象
	/// 2. 端口管理：维护端口集合，支持连接功能
	/// 3. 尺寸调整：支持用户拖拽调整节点大小
	/// 4. 位置管理：自动更新端口位置坐标
	/// 5. 选择显示：提供自定义的选择装饰器
	/// 
	/// 【WPF技术特点】：
	/// - 依赖属性：Content和CanResize属性
	/// - 样式重写：通过DefaultStyleKeyProperty定义外观
	/// - Canvas布局：使用Canvas.Left/Top定位
	/// - 可视化树遍历：计算边界和子元素位置
	/// 
	/// 【设计模式应用】：
	/// - 组合模式：包含端口集合，统一管理
	/// - 观察者模式：位置变化时通知端口更新
	/// - 模板方法模式：CreateSelectionAdorner由子类定义
	/// 
	/// 【学习要点】：
	/// - 静态构造函数：用于样式注册和元数据设置
	/// - 依赖属性系统：WPF数据绑定和属性系统的核心
	/// - 接口实现：显式接口实现和隐式接口实现
	/// - 集合类型：ICollection vs IEnumerable的区别
	/// </summary>
	public class Node : DiagramItem, INode
	{
		/// <summary>
		/// 静态构造函数 - 注册WPF样式和元数据
		/// 
		/// 【执行时机】：在任何Node实例创建之前执行一次
		/// 【核心功能】：重写默认样式键，指向Node类型
		/// 【技术详解】：
		/// - OverrideMetadata：重写父类的属性元数据
		/// - DefaultStyleKeyProperty：WPF样式系统的核心属性
		/// - typeof(Node)：告诉WPF查找Node类型的默认样式
		/// 
		/// 【作用意义】：
		/// 1. 允许在资源字典中定义Node的外观样式
		/// 2. 支持主题切换和样式继承
		/// 3. 提供一致的视觉体验和品牌认知
		/// </summary>
		static Node()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
		}

		#region Properties

		#region Content Property

		public object Content 
		{
			get { return (bool)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content",
									   typeof(object),
									   typeof(Node));

		#endregion

		#region CanResize Property

		public bool CanResize
		{
			get { return (bool)GetValue(CanResizeProperty); }
			set { SetValue(CanResizeProperty, value); }
		}

		public static readonly DependencyProperty CanResizeProperty =
			DependencyProperty.Register("CanResize",
									   typeof(bool),
									   typeof(Node),
									   new FrameworkPropertyMetadata(true));

		#endregion

		private List<IPort> _ports = new List<IPort>();
		public ICollection<IPort> Ports { get { return _ports; } }

		public override Rect Bounds
		{
			get
			{
				//var itemRect = VisualTreeHelper.GetDescendantBounds(item);
				//return item.TransformToAncestor(this).TransformBounds(itemRect);
				var x = Canvas.GetLeft(this);
				var y = Canvas.GetTop(this);
				return new Rect(x, y, ActualWidth, ActualHeight);
			}
		}

		#endregion

		public Node()
		{
		}

		public void UpdatePosition()
		{
			foreach (var p in Ports)
				p.UpdatePosition();
		}

		protected override Adorner CreateSelectionAdorner()
		{
			return new SelectionAdorner(this, new SelectionFrame());
		}

		#region INode Members

		IEnumerable<IPort> INode.Ports
		{
			get { return _ports; }
		}

		#endregion
	}
}
