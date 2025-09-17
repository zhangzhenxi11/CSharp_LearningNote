﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//IPort.cs - 端口接口，定义链接管理、位置计算、边缘点获取
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// IPort.cs - 端口接口
	/// 定义节点上的连接点的基本功能
	/// 提供链接管理、位置计算、边缘点获取等操作
	/// 支持鼠标悬停检测和位置更新通知
	/// </summary>
	public interface IPort
	{
		ICollection<ILink> Links { get; }
		Point Center { get; }

		bool IsNear(Point point);
		Point GetEdgePoint(Point target);
		void UpdatePosition();
	}
}
