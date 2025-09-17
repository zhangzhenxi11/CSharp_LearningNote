﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Controls;
using System.Windows;

/*
	LinkInfo.cs - 链接信息封装类，用于保存和恢复链接状态
*/
namespace Aga.Diagrams
{
	/// <summary>
	/// LinkInfo.cs - 链接信息封装类
	/// 用于保存和恢复链接状态的数据结构
	/// 在链接编辑过程中保存初始状态，支持撤销操作
	/// 包含源端口、目标端口及对应的坐标点信息
	/// 提供从链接创建和更新链接的方法
	/// </summary>
	public class LinkInfo
	{
		public IPort Source { get; set; }
		public IPort Target { get; set; }
		public Point? SourcePoint { get; set; }
		public Point? TargetPoint { get; set; }

		public LinkInfo(ILink link)
		{
			Source = link.Source;
			Target = link.Target;
			SourcePoint = link.SourcePoint;
			TargetPoint = link.TargetPoint;
		}

		public void UpdateLink(ILink link)
		{
			link.Source = Source;
			link.Target = Target;
			link.SourcePoint = SourcePoint;
			link.TargetPoint = TargetPoint;
		}
	}
}
