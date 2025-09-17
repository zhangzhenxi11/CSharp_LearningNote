﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// ILinkTool.cs - 链接工具接口
	/// 定义链接创建和编辑的操作接口
	/// 支持从已有链接开始拖拽和从端口创建新链接
	/// 提供端口自动吸附和放置验证功能
	/// </summary>
	public interface ILinkTool
	{
		void BeginDrag(Point start, ILink link, LinkThumbKind thumb);
		void BeginDragNewLink(Point start, IPort port);
		void DragTo(Vector vector);
		bool CanDrop();
		void EndDrag(bool doCommit);
	}
}
