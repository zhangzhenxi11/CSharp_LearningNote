using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Tools;
using Aga.Diagrams;
using System.Windows;

namespace TestApp.Flowchart
{
	/// <summary>
	/// CustomMoveResizeTool.cs - 自定义移动调整工具
	/// 继承自MoveResizeTool，为流程图提供专用的移动功能
	/// 重写CanDrop方法增加网格位置冲突检测
	/// 防止多个节点放置在同一个网格位置
	/// 提供更严格的位置验证规则
	/// </summary>
	class CustomMoveResizeTool: MoveResizeTool
	{
		private FlowchartModel _model;
 
		public CustomMoveResizeTool(DiagramView view, FlowchartModel model)
			: base(view)
		{
			_model = model;
		}

		public override bool CanDrop()
		{
			foreach(var item in DragItems)
			{
				var column = (int)(item.Bounds.X / View.GridCellSize.Width);
				var row = (int)(item.Bounds.Y / View.GridCellSize.Height);
				if (_model.Nodes.Where(p => !IsDragged(p) && p.Row == row && p.Column == column).Count() != 0)
					return false;
			}
			return true;
		}

		private bool IsDragged(FlowNode node)
		{
			return DragItems.Where(p => p.ModelElement == node).Count() > 0;
		}
	}
}
