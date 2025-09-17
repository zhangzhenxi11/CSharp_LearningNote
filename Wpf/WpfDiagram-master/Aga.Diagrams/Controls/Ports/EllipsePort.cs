﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//EllipsePort.cs - 椭圆形端口
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// EllipsePort.cs - 椭圆形端口
	/// 实现椭圆形状的连接端口
	/// 提供椭圆边缘点计算和鼠标悬停检测
	/// 使用GeometryHelper进行椭圆几何计算
	/// </summary>
	public class EllipsePort: PortBase
	{
		static EllipsePort()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(EllipsePort), new FrameworkPropertyMetadata(typeof(EllipsePort)));
		}

		public override Point GetEdgePoint(Point target)
		{
			var a = ActualWidth / 2;
			var b = ActualHeight / 2;
			var p = new Point(target.X - Center.X, target.Y - Center.Y);
			p = GeometryHelper.EllipseLineIntersection(a, b, p);
			return new Point(p.X + Center.X, p.Y + Center.Y);
		}

		public override bool IsNear(Point point)
		{
			var a = ActualWidth / 2 + Sensitivity;
			var b = ActualHeight / 2 + Sensitivity;
			return GeometryHelper.EllipseContains(Center, a, b, point);
		}
	}
}
