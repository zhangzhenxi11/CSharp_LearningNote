using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TestApp.Flowchart
{
	/// <summary>
	/// Link.cs - 流程图链接模型
	/// 定义流程图中节点之间的连接关系
	/// 包含源节点、目标节点和对应的端口信息
	/// 支持链接文本标签和数据绑定
	/// 实现INotifyPropertyChanged支持属性变更通知
	/// </summary>
	class Link: INotifyPropertyChanged
	{
		[Browsable(false)]
		public FlowNode Source { get; private set; }
		[Browsable(false)]
		public PortKinds SourcePort { get; private set; }
		[Browsable(false)]
		public FlowNode Target { get; private set; }
		[Browsable(false)]
		public PortKinds TargetPort { get; private set; }

		private string _text;
		public string Text
		{
			get { return _text; }
			set 
			{ 
				_text = value;
				OnPropertyChanged("Text");
			}
		}

		public Link(FlowNode source, PortKinds sourcePort, FlowNode target, PortKinds targetPort)
		{
			Source = source;
			SourcePort = sourcePort;
			Target = target;
			TargetPort = targetPort;
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
	/// PortKinds - 端口方向枚举
	/// 定义节点上端口的四个方向：上、下、左、右
	/// </summary>
	enum PortKinds { Top, Bottom, Left, Right }
}
