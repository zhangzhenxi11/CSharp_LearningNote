﻿using System;
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
	/// PortBase.cs - 端口基类
	/// 节点上的连接点实现，管理入出链接
	/// 提供链接管理、位置计算、灵敏度设置等功能
	/// 支持鼠标交互创建新链接
	/// 抽象类，需要子类实现具体的几何计算
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
