using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
//RelinkControl.cs - 重新连接控制器
namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// RelinkControl.cs - 重新连接控制器
	/// 为被选中的链接提供重新连接的用户界面
	/// 通常包含链接两端的手柄，用于拖拽重连
	/// 继承自Control，通过样式模板定义外观
	/// </summary>
	public class RelinkControl : Control
	{
		static RelinkControl()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(RelinkControl), new FrameworkPropertyMetadata(typeof(RelinkControl)));
		}
	}
}
