using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
//DragAdorner.cs - 拖拽装饰器基类
namespace Aga.Diagrams.Adorners
{
	/// <summary>
	/// DragAdorner.cs - 拖拽操作装饰器基类
	/// 为图表元素提供拖拽时的视觉反馈和鼠标捕获功能
	/// 继承自WPF的Adorner类，用于在拖拽过程中显示临时的视觉效果
	/// 提供拖拽开始、进行中、结束的完整生命周期管理
	/// </summary>


	public abstract class DragAdorner: Adorner
	{
		public DiagramView View { get; private set; } //画布控件属性
        protected bool DoCommit { get; set; }
		private bool CanDrop { get; set; }
		protected Point Start { get; set; }
		protected Point End { get; set; }

		protected DragAdorner(DiagramView view, Point start): base(view)
		{
			View = view;
			End = Start = start;
			this.Loaded += OnLoaded; //public event RoutedEventHandler Loaded; 路由事件 += 事件处理器
        }

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			DoCommit = false;
			CaptureMouse();
		}

		protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			End = e.GetPosition(View);
			CanDrop = DoDrag();
			Mouse.OverrideCursor = CanDrop ? Cursor : Cursors.No;
		}

		protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.IsMouseCaptured)
			{
				DoCommit = CanDrop;
				this.ReleaseMouseCapture();
			}
		}

		protected override void OnLostMouseCapture(MouseEventArgs e)
		{
			View.DragAdorner = null;
			Mouse.OverrideCursor = null;
			EndDrag();
		}

		/// <summary>
		/// Returns true if drop is possible at this location
		/// </summary>
		protected abstract bool DoDrag();
		protected abstract void EndDrag();
	}
}
