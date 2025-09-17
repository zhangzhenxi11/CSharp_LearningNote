using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Aga.Diagrams.Tools;
using Aga.Diagrams;
using Aga.Diagrams.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace TestApp.ShapesExample
{
	/// <summary>
	/// ShapesEditor.xaml.cs - 图形编辑器主界面
	/// 提供基本图形（矩形、椭圆）的编辑功能界面
	/// 初始化图表控制器和选择事件处理
	/// 将选中元素的属性绑定到属性视图
	/// </summary>
	/// <summary>
	/// Interaction logic for ShapesEditor.xaml
	/// </summary>
	public partial class ShapesEditor : UserControl
	{
		public ShapesEditor()
		{
			InitializeComponent();

			/*_diagramView.DragTool = new MoveResizeTool(_diagramView) 
			{ 
				MoveGridCell = _diagramView.GridCellSize,
				ResizeGridCell = _diagramView.GridCellSize
			};*/
			_diagramView.Controller = new ShapesController(_diagramView);
			_diagramView.Selection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Selection_PropertyChanged);
		}

		void Selection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var p = _diagramView.Selection.Primary;
			_propertiesView.SelectedObject = p != null ? p.ModelElement : null;
		}
	}
}
