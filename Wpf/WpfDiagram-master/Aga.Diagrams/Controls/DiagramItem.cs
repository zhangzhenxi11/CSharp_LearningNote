﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Aga.Diagrams.Adorners;

/*DiagramItem.cs - 图表元素基类，提供选择状态、移动能力、选择装饰器等基础功能*/
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// DiagramItem.cs - 图表元素基类
	/// 所有图表中显示的元素的抽象基类（节点、链接等）
	/// 提供选择状态管理、移动能力控制、装饰器支持
	/// 实现INotifyPropertyChanged接口支持数据绑定
	/// 管理元素的选中、主选择状态和边界信息
	/// </summary>
	public abstract class DiagramItem : Control
	{
		#region Properties

		private Adorner SelectionAdorner { get; set; }

		public object ModelElement { get; set; }

		#region IsSelected Property

		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			internal set { SetValue(IsSelectedProperty, value); }
		}

		internal static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsSelectedChanged)));

		private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue)
			{
				d.ClearValue(IsPrimarySelectionProperty);
				(d as DiagramItem).HideSelectionAdorner();
			}
			else
				(d as DiagramItem).ShowSelectionAdorner();
		}

		protected virtual void IsSelectedChanged()
		{
		}

		#endregion

		#region IsPrimarySelection Property

		public bool IsPrimarySelection
		{
			get { return (bool)GetValue(IsPrimarySelectionProperty); }
			internal set { SetValue(IsPrimarySelectionProperty, value); }
		}

		internal static readonly DependencyProperty IsPrimarySelectionProperty =
			DependencyProperty.Register("IsPrimarySelection",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(false));

		#endregion

		#region CanMove Property

		public bool CanMove
		{
			get { return (bool)GetValue(CanMoveProperty); }
			set { SetValue(CanMoveProperty, value); }
		}

		public static readonly DependencyProperty CanMoveProperty =
			DependencyProperty.Register("CanMove",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(true));

		#endregion

		#region CanSelect Property

		public bool CanSelect
		{
			get { return (bool)GetValue(CanSelectProperty); }
			set { SetValue(CanSelectProperty, value); }
		}

		public static readonly DependencyProperty CanSelectProperty =
			DependencyProperty.Register("CanSelect",
									   typeof(bool),
									   typeof(DiagramItem),
									   new FrameworkPropertyMetadata(true));

		#endregion

		public abstract Rect Bounds { get; }

		#endregion

		protected void HideSelectionAdorner()
		{
			if (SelectionAdorner != null)
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
				if (adornerLayer != null)
					adornerLayer.Remove(SelectionAdorner);
				SelectionAdorner = null;
			}
		}

		protected void ShowSelectionAdorner()
		{
			var adornerLayer = AdornerLayer.GetAdornerLayer(this);
			if (adornerLayer != null)
			{
				SelectionAdorner = CreateSelectionAdorner();
				SelectionAdorner.Visibility = Visibility.Visible;
				adornerLayer.Add(SelectionAdorner);
			}
		}

		protected abstract Adorner CreateSelectionAdorner();
	}
}
