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

/// <summary>
/// RubberbandAdorner.cs - 橡皮筋选择装饰器
/// 实现拖拽框选多个元素的功能，类似文件管理器中的框选
/// 显示一个透明的矩形框，框住的所有元素都会被选中
/// 支持实时视觉反馈和多选操作
/// </summary>
namespace Aga.Diagrams.Adorners
{
	/// <summary>
	/// 橡皮筋选择装饰器 - 实现拖拽框选功能
	/// </summary>
	class RubberbandAdorner : DragAdorner
	{
		private Pen _pen;

		public RubberbandAdorner(DiagramView view, Point start)
			: base(view, start)
		{
			_pen = new Pen(Brushes.Black, 2);
		}

		protected override bool DoDrag()
		{
			InvalidateVisual();
			return true;
		}

		protected override void EndDrag()
		{
			if (DoCommit)
			{
				var rect = new Rect(Start, End);
				var items = View.Items.Where(p => p.CanSelect && rect.Contains(p.Bounds));
				View.Selection.SetRange(items);
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			dc.DrawRectangle(Brushes.Transparent, _pen, new Rect(Start, End));
		}
	}
}
