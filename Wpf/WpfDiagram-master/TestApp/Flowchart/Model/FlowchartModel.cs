using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TestApp.Flowchart
{
	/// <summary>
	/// FlowchartModel.cs - 流程图数据模型
	/// 管理流程图中的所有节点和链接数据
	/// 使用ObservableCollection支持数据绑定和变更通知
	/// 作为MVVM模式中的Model层，与View和Controller分离
	/// </summary>
	class FlowchartModel
	{
		private ObservableCollection<FlowNode> _nodes = new ObservableCollection<FlowNode>();
		internal ObservableCollection<FlowNode> Nodes
		{
			get { return _nodes; }
		}

		private ObservableCollection<Link> _links = new ObservableCollection<Link>();
		internal ObservableCollection<Link> Links
		{
			get { return _links; }
		}
	}
}
