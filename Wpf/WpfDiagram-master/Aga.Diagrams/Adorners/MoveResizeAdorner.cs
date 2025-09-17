using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Adorners
{
	/// <summary>
	/// MoveResizeAdorner.cs - 移动和调整大小装饰器
	/// 在元素被拖拽或调整大小时提供视觉反馈
	/// 处理移动和尺寸调整操作的拖拽事件
	/// 与DragTool协作完成实际的位置和尺寸变更
	/// </summary>
	public class MoveResizeAdorner : DragAdorner
	{
		public MoveResizeAdorner(DiagramView view, Point start)
			: base(view, start)
		{
		}

		protected override bool DoDrag()
		{
			View.DragTool.DragTo(End - Start);
			return View.DragTool.CanDrop();
		}

		protected override void EndDrag()
		{
			View.DragTool.EndDrag(DoCommit);
		}
	}
}
