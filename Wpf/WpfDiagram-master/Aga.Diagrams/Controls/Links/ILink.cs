using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//ILink.cs - 链接接口，定义源点、目标点、路径更新等基本功能
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// ILink.cs - 链接接口
	/// 定义所有链接类型必须实现的基本功能
	/// 提供源端口、目标端口、路径点管理
	/// 支持路径更新和几何计算
	/// </summary>
	public interface ILink
	{
		IPort Source { get; set; }
		IPort Target { get; set; }
		Point? SourcePoint { get; set; }
		Point? TargetPoint { get; set; }

		void UpdatePath();
	}
}
