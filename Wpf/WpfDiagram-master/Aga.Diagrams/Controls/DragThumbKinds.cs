﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//DragThumbKinds.cs - 拖拽类型枚举，定义各种拖拽方向（上下左右、角落、中心）
namespace Aga.Diagrams
{
	/// <summary>
	/// DragThumbKinds.cs - 拖拽手柄类型枚举
	/// 定义元素拖拽和调整大小的方向和类型
	/// 使用Flags特性支持位运算组合（如TopLeft = Top | Left）
	/// 包含上下左右、四个角落和中心移动等类型
	/// </summary>
	[Flags]
	public enum DragThumbKinds
	{
		None = 0,
		Top = 1, 
		Left = 2, 
		Bottom = 4, 
		Right = 8,
		Center = 16,
		TopLeft = Top | Left,
		TopRight = Top | Right,
		BottomLeft = Bottom | Left,
		BottomRight = Bottom | Right
	}
}
