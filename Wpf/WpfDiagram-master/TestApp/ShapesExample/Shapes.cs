using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace TestApp.ShapesExample
{
	/// <summary>
	/// ShapeBase.cs - 图形基类
	/// 定义所有图形的公共属性：位置、尺寸、链接关系
	/// 实现INotifyPropertyChanged支持数据绑定
	/// 作为具体图形类型的基础，提供统一的接口
	/// </summary>
	class ShapeBase: INotifyPropertyChanged
	{
		private Point _location;
		public Point Location 
		{
			get { return _location; }
			set 
			{ 
				_location = value;
				OnPropertyChanged("Location");
			}
		}

		private Size _size;
		public Size Size
		{
			get { return _size; }
			set
			{
				_size = value;
				OnPropertyChanged("Size");
			}
		}

		private List<ShapeBase> _links = new List<ShapeBase>();
		[Browsable(false)]
		public List<ShapeBase> Links
		{
			get { return _links; }
		}

		public override string ToString()
		{
			return GetType().Name;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	/// <summary>
	/// RectangleShape.cs - 矩形图形
	/// 继承自ShapeBase，表示矩形元素
	/// </summary>
	class RectangleShape : ShapeBase
	{
	}

	/// <summary>
	/// EllipseShape.cs - 椭圆图形
	/// 继承自ShapeBase，表示椭圆元素
	/// </summary>
	class EllipseShape : ShapeBase
	{
	}
}
