using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// LinkThumb.cs - 链接拖拽手柄
	/// 为链接提供拖拽手柄功能，用于重新连接
	/// 处理鼠标点击事件，启动链接编辑模式
	/// 根据LinkThumbKind区分源端和目标端手柄
	/// 与LinkTool协作完成链接的拖拽重连操作
	/// </summary>
	public class LinkThumb: Control
	{
		public LinkThumbKind Kind { get; set; }
		protected Point? MouseDownPoint { get; set; }

		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				var link = this.DataContext as LinkBase;
				var view = VisualHelper.FindParent<DiagramView>(link);
				if (link != null && view != null)
				{
					MouseDownPoint = e.GetPosition(view);
					view.LinkTool.BeginDrag(MouseDownPoint.Value, link, this.Kind);
					e.Handled = true;
				}
			}
		}
	}
}
