﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using Aga.Diagrams.Adorners;

// DragThumb.cs - 拖拽手柄控件，处理鼠标拖拽操作

namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// DragThumb.cs - 拖拽手柄控件
	/// 为图表元素提供拖拽手柄功能，支持多种拖拽类型
	/// 处理鼠标点击事件，启动相应的拖拽操作
	/// 与DragThumbKinds枚举配合定义拖拽方向和类型
	/// 通过DataContext获取关联的DiagramItem对象
	/// </summary>
	public class DragThumb: Control
	{
		public DragThumbKinds Kind { get; set; }

		protected Point? MouseDownPoint { get; set; }

		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				var item = this.DataContext as DiagramItem;
				var view = VisualHelper.FindParent<DiagramView>(item);
				if (item != null && view != null)
				{
					MouseDownPoint = e.GetPosition(view);
					view.DragTool.BeginDrag(MouseDownPoint.Value, item, this.Kind);
					e.Handled = true;
				}
			}
		}
	}
}
