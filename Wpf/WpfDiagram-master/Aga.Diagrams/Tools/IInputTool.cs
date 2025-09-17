﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// IInputTool.cs - 输入工具接口
	/// 定义基本的鼠标和键盘事件处理方法
	/// 为图表交互提供统一的输入处理接口
	/// 支持鼠标点击、移动、释放和键盘事件
	/// </summary>
	public interface IInputTool
	{
		void OnMouseDown(MouseButtonEventArgs e);
		void OnMouseMove(MouseEventArgs e);
		void OnMouseUp(MouseButtonEventArgs e);

		void OnPreviewKeyDown(KeyEventArgs e);
	}
}
