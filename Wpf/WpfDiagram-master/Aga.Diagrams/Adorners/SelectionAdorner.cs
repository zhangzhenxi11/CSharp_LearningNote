using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using Aga.Diagrams.Controls;
using System.Windows.Controls;
/// <summary>
/// SelectionAdorner.cs - 选择状态装饰器
/// 为被选中的图表元素显示选择框和拖拽手柄
/// 使用VisualCollection管理子控件，提供用户交互界面
/// 继承自WPF Adorner，在元素上层渲染选择效果
/// </summary>
namespace Aga.Diagrams.Adorners
{
	/// <summary>
	/// 选择装饰器 - 显示元素选中状态的视觉效果
	/// </summary>
	public class SelectionAdorner : Adorner
	{
		private VisualCollection _visuals;
		private Control _control;

		protected override int VisualChildrenCount
		{
			get { return _visuals.Count; }
		}

		public SelectionAdorner(DiagramItem item, Control control)
			: base(item)
		{
			_control = control;
			_control.DataContext = item;
			_visuals = new VisualCollection(this);
			_visuals.Add(_control);
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			_control.Arrange(new Rect(finalSize));
			return finalSize;
		}

		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}
	}
}
