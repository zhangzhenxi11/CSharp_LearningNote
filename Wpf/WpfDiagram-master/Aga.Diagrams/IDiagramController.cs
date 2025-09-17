﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Controls;
using System.Windows;

/*
控制器接口
IDiagramController.cs - 图表控制器接口，定义了更新元素边界、更新链接、执行命令等核心操作
*/
namespace Aga.Diagrams
{
	/// <summary>
	/// IDiagramController.cs - 图表控制器接口
	/// 定义图表编辑器的核心控制逻辑接口
	/// 处理元素位置和尺寸变更、链接更新等操作
	/// 提供命令执行和可执行性验证功能
	/// 作为视图和数据模型之间的桥梁
	/// </summary>
	public interface IDiagramController
	{
		/// <summary>
		/// Is called when user move/resize an item
		/// </summary>
		/// <param name="items">Selected items</param>
		/// <param name="bounds">New item bounds</param>
		void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds);

		/// <summary>
		/// Is called when user create a link between items
		/// </summary>
		/// <param name="initialState">the state of the link before user action</param>
		/// <param name="link">Link in the current state</param>
		void UpdateLink(LinkInfo initialState, ILink link);

		void ExecuteCommand(System.Windows.Input.ICommand command, object parameter);

		bool CanExecuteCommand(System.Windows.Input.ICommand command, object parameter);
	}
}
