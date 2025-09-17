﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//RectPort.cs - 矩形端口
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// RectPort.cs - 矩形端口
	/// 实现矩形状的连接端口
	/// 提供矩形边缘点计算和鼠标悬停检测
	/// 使用GeometryHelper进行矩形几何计算
	/// </summary>
	public class RectPort : PortBase
	{
		static RectPort()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(RectPort), new FrameworkPropertyMetadata(typeof(RectPort)));
		}

		public override Point GetEdgePoint(Point target)
		{
			var rect = new Rect(Center.X - ActualWidth / 2, Center.Y - ActualHeight / 2, ActualWidth, ActualHeight);
			return GeometryHelper.RectLineIntersection(rect, target);
		}

		public override bool IsNear(Point point)
		{
			var rect = new Rect(Center.X - ActualWidth / 2, Center.Y - ActualHeight / 2, ActualWidth, ActualHeight);
			rect.Inflate(Sensitivity, Sensitivity);
			return GeometryHelper.RectContains(rect, point);
		}
	}
}
