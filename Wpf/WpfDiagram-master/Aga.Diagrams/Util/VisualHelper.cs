﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Aga.Diagrams
{
	/// <summary>
	/// VisualHelper.cs - 视觉树帮助类
	/// 提供WPF视觉树操作的帮助方法
	/// 主要用于查找父级控件，类似于jQuery的parent()方法
	/// 使用泛型支持类型安全的查找操作
	/// </summary>
	public static class VisualHelper
	{
		public static T FindParent<T>(this DependencyObject value) where T : DependencyObject
		{
			DependencyObject parent = value;
			while (parent != null && !(parent is T))
				parent = VisualTreeHelper.GetParent(parent);
			return parent as T;
		}

		/*public static Point GetWindowPosition(this System.Windows.Input.MouseEventArgs e, DependencyObject relativeTo)
		{
			var parentWindow = Window.GetWindow(relativeTo);
			return e.GetPosition(parentWindow);
		}*/

		/*public static Point ClientToScreen(this UIElement value, Point point)
		{
			var parentWindow = Window.GetWindow(value);
			return value.TranslatePoint(point, parentWindow);
		}*/
	}
}
