﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

//PortBase.cs - 端口基类
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// 端口基类 - 节点上的连接点抽象实现
	/// 
	/// 【设计模式】：模板方法模式 + 策略模式 + 观察者模式
	/// 【继承关系】：PortBase -> Control -> FrameworkElement
	/// 【接口实现】：IPort - 提供端口的标准操作接口
	/// 
	/// 【核心功能】：
	/// 1. 链接管理：维护入出链接集合，自动分类
	/// 2. 位置计算：计算并维护端口在Canvas中的中心坐标
	/// 3. 吸附检测：根据敏感度设置检测鼠标是否靠近
	/// 4. 连接可用性：控制能否接受入/出链接
	/// 5. 交互创建：支持点击创建新链接
	/// 6. 几何计算：抽象方法由子类实现具体形状
	/// 
	/// 【抽象方法】：
	/// - GetEdgePoint：计算端口边缘与目标点的交点
	/// - IsNear：判断指定点是否在敏感区域内
	/// 
	/// 【观察者模式应用】：
	/// - 位置变化时自动通知所有相关链接更新路径
	/// - 通过Links集合维护双向关联关系
	/// 
	/// 【WPF技术特点】：
	/// - 多个依赖属性：支持数据绑定和动态更新
	/// - 事件处理：重写OnPreviewMouseLeftButtonDown实现交互
	/// - 视觉树遍历：使用VisualHelper查找父容器
	/// - 坐标变换：使用TransformToAncestor计算全局坐标
	/// 
	/// 【学习要点】：
	/// - 抽象类设计：公共功能实现 + 抽象方法定义
	/// - LINQ查询：Where过滤器分类入出链接
	/// - 数值验证：double.IsNaN检查无效坐标
	/// - 事件路由：e.Handled控制事件传播
	/// </summary>
	public abstract class PortBase : Control, IPort
	{
		#region Properties

		private List<ILink> _links = new List<ILink>();
		public ICollection<ILink> Links { get { return _links; } }

		public IEnumerable<ILink> IncomingLinks
		{
			get { return Links.Where(p => p.Target == this); }
		}

		public IEnumerable<ILink> OutgoingLinks
		{
			get { return Links.Where(p => p.Source == this); }
		}

		private Point _center;
		public Point Center
		{
			get { return _center; }
			protected set
			{
				if (_center != value && !double.IsNaN(value.X) && !double.IsNaN(value.Y))
				{
					_center = value;
					foreach (var link in Links)
						link.UpdatePath();
				}
			}
		}

		#region Sensitivity Property

		public double Sensitivity
		{
			get { return (double)GetValue(SensitivityProperty); }
			set { SetValue(SensitivityProperty, value); }
		}

		public static readonly DependencyProperty SensitivityProperty =
			DependencyProperty.Register("Sensitivity",
									   typeof(double),
									   typeof(PortBase),
									   new FrameworkPropertyMetadata(5.0));

		#endregion

		#region CanAcceptIncomingLinks Property

		public bool CanAcceptIncomingLinks
		{
			get { return (bool)GetValue(CanAcceptIncomingLinksProperty); }
			set { SetValue(CanAcceptIncomingLinksProperty, value); }
		}

		public static readonly DependencyProperty CanAcceptIncomingLinksProperty =
			DependencyProperty.Register("CanAcceptIncomingLinks",
									   typeof(bool),
									   typeof(PortBase),
									   new FrameworkPropertyMetadata(true));

		#endregion

		#region CanAcceptOutgoingLinks Property

		public bool CanAcceptOutgoingLinks
		{
			get { return (bool)GetValue(CanAcceptOutgoingLinksProperty); }
			set { SetValue(CanAcceptOutgoingLinksProperty, value); }
		}

		public static readonly DependencyProperty CanAcceptOutgoingLinksProperty =
			DependencyProperty.Register("CanAcceptOutgoingLinks",
									   typeof(bool),
									   typeof(PortBase),
									   new FrameworkPropertyMetadata(true));

		#endregion

		#region CanCreateLink Property

		public bool CanCreateLink
		{
			get { return (bool)GetValue(CanCreateLinkProperty); }
			set { SetValue(CanCreateLinkProperty, value); }
		}

		public static readonly DependencyProperty CanCreateLinkProperty =
			DependencyProperty.Register("CanCreateLink",
									   typeof(bool),
									   typeof(PortBase),
									   new FrameworkPropertyMetadata(false));

		#endregion

		#endregion

		protected PortBase()
		{
		}

		public virtual void UpdatePosition()
		{
			var canvas = VisualHelper.FindParent<Canvas>(this);
			if (canvas != null)
				Center = this.TransformToAncestor(canvas).Transform(new Point(this.ActualWidth / 2, this.ActualHeight / 2));
			else
				Center = new Point(Double.NaN, Double.NaN);
		}

		/// <summary>
		/// Calcluate the intersection point of the port bounds and the line between center and target point
		/// </summary>
		public abstract Point GetEdgePoint(Point target);

		/// <summary>
		/// Returns if the specified point is inside port sensivity area
		/// </summary>
		public abstract bool IsNear(Point point);


		protected override void  OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (CanCreateLink)
			{
				var view = VisualHelper.FindParent<DiagramView>(this);
				if (view != null)
				{
					view.LinkTool.BeginDragNewLink(e.GetPosition(view), this);
					e.Handled = true;
				}
			}
			else
				base.OnMouseLeftButtonDown(e);
		}
	}
}
