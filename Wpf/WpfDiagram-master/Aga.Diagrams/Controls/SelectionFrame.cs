﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

// SelectionFrame.cs - 选择框控件，用于显示选中状态的视觉框架
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// SelectionFrame.cs - 选择框控件
	/// 用于显示元素选中状态的视觉框架
	/// 通常包含拖拽手柄、边框等用户界面元素
	/// 继承自WPF Control，通过样式模板定义外观
	/// </summary>
	public class SelectionFrame : Control
	{
		static SelectionFrame()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionFrame), new FrameworkPropertyMetadata(typeof(SelectionFrame)));
		}
	}
}
