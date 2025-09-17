﻿using System;
using System.Windows;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// IMoveResizeTool.cs - 移动调整工具接口
	/// 定义元素移动和尺寸调整的操作接口
	/// 支持拖拽开始、进行中、结束的完整生命周期
	/// 提供放置验证和提交控制功能
	/// </summary>
	public interface IMoveResizeTool
	{
		void BeginDrag(Point start, DiagramItem item, DragThumbKinds kind);
		void DragTo(Vector vector);
		bool CanDrop();
		void EndDrag(bool doCommit);
	}
}
