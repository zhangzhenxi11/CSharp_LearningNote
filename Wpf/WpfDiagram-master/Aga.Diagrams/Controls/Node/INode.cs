﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

//INode.cs - 节点接口，定义端口集合
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// INode.cs - 节点接口
	/// 定义所有节点类型必须实现的基本功能
	/// 提供端口集合的访问，用于链接管理
	/// 简化节点类型的统一接口设计
	/// </summary>
	public interface INode
	{
		IEnumerable<IPort> Ports { get; }
	}
}
