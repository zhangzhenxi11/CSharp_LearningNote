using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Tools;
using Aga.Diagrams.Controls;
using System.Windows;
using Aga.Diagrams;
using System.Windows.Controls;

namespace TestApp.Flowchart
{
	/// <summary>
	/// CustomLinkTool.cs - 自定义链接工具
	/// 继承自LinkTool，为流程图提供专用的链接功能
	/// 重写CreateNewLink方法使用OrthogonalLink替代默认链接
	/// 保证流程图中的所有链接都使用正交风格
	/// </summary>
	class CustomLinkTool: LinkTool
	{
		public CustomLinkTool(DiagramView view)
			: base(view)
		{
		}

		protected override ILink CreateNewLink(IPort port)
		{
			var link = new OrthogonalLink();
			BindNewLinkToPort(port, link);
			return link;
		}

		protected override void UpdateLink(Point point, IPort port)
		{
			base.UpdateLink(point, port);
			var link = Link as OrthogonalLink;
		}
	}
}
