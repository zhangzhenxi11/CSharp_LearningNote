using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// LinkThumbKind.cs - 链接手柄类型枚举
	/// 定义链接手柄的两种类型：源端和目标端
	/// 用于区分拖拽操作是针对链接的哪一端
	/// </summary>
	public enum LinkThumbKind
	{
		Source, Target
	}
}
