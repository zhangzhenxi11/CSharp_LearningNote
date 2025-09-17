﻿using System;
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
	/// Node.cs - 节点控件实现类
	/// 图表中可连接的节点元素，继承自DiagramItem
	/// 支持内容显示、端口管理、尺寸调整
	/// 实现INode接口，提供端口集合的访问
	/// 管理节点的位置更新和边界计算
	/// </summary>
	public class Node : DiagramItem, INode
	{
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
