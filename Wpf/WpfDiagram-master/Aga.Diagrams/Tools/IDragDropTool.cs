﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// IDragDropTool.cs - 拖放工具接口
	/// 定义WPF拖放操作的事件处理接口
	/// 支持从外部应用拖拽元素到图表中
	/// 提供拖拽进入、移动、离开、放置等事件处理
	/// </summary>
	public interface IDragDropTool
	{
		void OnDragEnter(System.Windows.DragEventArgs e);
		void OnDragOver(System.Windows.DragEventArgs e);
		void OnDragLeave(System.Windows.DragEventArgs e);
		void OnDrop(System.Windows.DragEventArgs e);
	}
}
