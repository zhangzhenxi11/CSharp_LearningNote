﻿﻿﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Aga.Diagrams.Controls;
using System.Windows.Documents;
using System.Windows.Controls;
using Aga.Diagrams.Adorners;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 链接创建和编辑工具 - 实现图表中节点间的连接功能
	/// 
	/// 【设计模式】：策略模式 + 命令模式 + 状态机模式
	/// 【核心功能】：
	/// 1. 创建新链接：从端口拖拽到另一个端口
	/// 2. 编辑现有链接：移动链接的端点或控制点
	/// 3. 端口吸附：自动吸附到附近的可连接端口
	/// 4. 连接验证：检查端口是否可以建立连接
	/// 5. 撤销支持：保存初始状态支持撤销操作
	/// 
	/// 【操作流程】：
	/// 1. BeginDrag/BeginDragNewLink - 启动链接操作
	/// 2. DragTo - 实时更新链接位置和目标
	/// 3. CanDrop - 检查是否可以放置
	/// 4. EndDrag - 结束操作，提交或取消更改
	/// 
	/// 【技术特点】：
	/// - 使用LinkInfo保存初始状态
	/// - 通过LinkAdorner提供视觉反馈
	/// - 支持多种链接类型（SegmentLink等）
	/// - 智能端口匹配和距离计算
	/// 
	/// 【学习要点】：
	/// - LINQ查询语法：SelectMany, Where, OrderBy, FirstOrDefault
	/// - 几何计算：GeometryHelper.Length距离计算
	/// - 状态管理：_isNewLink标记区分新建和编辑
	/// - 可空类型：Point?的使用和判断
	/// </summary>
	public class LinkTool: ILinkTool
	{
		/// <summary>
		/// 关联的图表视图控件 - 提供UI上下文和容器访问
		/// </summary>
		protected DiagramView View { get; private set; }
		
		/// <summary>
		/// 图表控制器 - 提供业务逻辑操作接口
		/// </summary>
		protected IDiagramController Controller { get { return View.Controller; } }
		
		/// <summary>
		/// 拖拽起始点 - 记录链接操作的开始坐标
		/// 【作用】：计算拖拽向量和目标位置
		/// </summary>
		protected Point DragStart { get; set; }
		
		/// <summary>
		/// 当前编辑的链接对象 - 正在创建或修改的链接
		/// 【类型】：ILink接口，支持多种链接实现
		/// </summary>
		protected ILink Link { get; set; }
		
		/// <summary>
		/// 链接拖拽点类型 - 指定正在操作的是源端还是目标端
		/// 【枚举值】：
		/// - LinkThumbKind.Source：操作链接的源端点
		/// - LinkThumbKind.Target：操作链接的目标端点
		/// </summary>
		protected LinkThumbKind Thumb { get; set; }
		
		/// <summary>
		/// 链接初始状态 - 用于撤销和恢复操作
		/// 【作用】：
		/// 1. 支持撤销操作（doCommit=false时恢复）
		/// 2. 为控制器提供初始状态参考
		/// 3. 用于命令模式的历史记录
		/// </summary>
		protected LinkInfo InitialState { get; set; }
		
		/// <summary>
		/// 链接装饰器 - 提供链接操作时的视觉反馈
		/// 【功能】：
		/// 1. 显示链接的实时预览
		/// 2. 显示可连接端口的高亮
		/// 3. 提供鼠标样式反馈
		/// </summary>
		protected LinkAdorner Adorner { get; set; }
		
		/// <summary>
		/// 新链接标记 - 区分是创建新链接还是编辑现有链接
		/// 【作用】：
		/// 1. 决定取消操作时的行为（删除 vs 恢复）
		/// 2. 影响链接的生命周期管理
		/// 3. 控制链接的初始化方式
		/// </summary>
		private bool _isNewLink;

		/// <summary>
		/// 构造函数 - 初始化链接工具并关联图表视图
		/// </summary>
		/// <param name="view">要关联的图表视图控件</param>
		public LinkTool(DiagramView view)
		{
			View = view;
		}

		/// <summary>
		/// 开始拖拽现有链接 - 公共接口重载，默认为编辑现有链接
		/// 
		/// 【用途】：编辑已存在的链接，移动其端点或控制点
		/// 【参数说明】：
		/// - start：拖拽开始的坐标位置
		/// - link：要编辑的链接对象
		/// - thumb：指定操作的是源端还是目标端
		/// </summary>
		public void BeginDrag(Point start, ILink link, LinkThumbKind thumb)
		{
			BeginDrag(start, link, thumb, false);
		}

		protected virtual void BeginDrag(Point start, ILink link, LinkThumbKind thumb, bool isNew)
		{
			_isNewLink = isNew;
			DragStart = start;
			Link = link;
			Thumb = thumb;
			InitialState = new LinkInfo(link);
			Adorner = CreateAdorner();
			View.DragAdorner = Adorner;
		}

		public virtual void DragTo(Vector vector)
		{
			vector = UpdateVector(vector);
			var point = DragStart + vector;
			var port = View.Children.OfType<INode>().SelectMany(p => p.Ports)
				.Where(p => p.IsNear(point) && CanLinkTo(p))
				.OrderBy(p => GeometryHelper.Length(p.Center, point))
				.FirstOrDefault();

			UpdateLink(point, port);

			Adorner.Port = port;
			Link.UpdatePath();
		}

		protected virtual void UpdateLink(Point point, IPort port)
		{
			if (Thumb == LinkThumbKind.Source)
			{
				Link.Source = port;
				Link.SourcePoint = port == null ? point : (Point?)null;
			}
			else
			{
				Link.Target = port;
				Link.TargetPoint = port == null ? point : (Point?)null;
			}
		}

		protected virtual bool CanLinkTo(IPort port)
		{
			var pb = port as PortBase;
			if (pb != null)
			{
				if (Thumb == LinkThumbKind.Source)
					return pb.CanAcceptOutgoingLinks;
				else
					return pb.CanAcceptIncomingLinks;
			}
			else
				return true;
		}

		public virtual bool CanDrop()
		{
			return Adorner.Port != null;
		}

		public virtual void EndDrag(bool doCommit)
		{
			if (doCommit)
			{
				Controller.UpdateLink(InitialState, Link);
			}
			else
			{
				if (_isNewLink)
					View.Children.Remove((Control)Link);
				else
					InitialState.UpdateLink(Link);
			}
			Link.UpdatePath();
			Link = null;
			Adorner = null;
		}

		public virtual void BeginDragNewLink(Point start, IPort port)
		{
			var link = CreateNewLink(port);
			if (link != null && link is Control)
			{
				var thumb = (link.Source != null) ? LinkThumbKind.Target : LinkThumbKind.Source;
				View.Children.Add((Control)link);
				BeginDrag(start, link, thumb, true);
			}
		}

		protected virtual ILink CreateNewLink(IPort port)
		{
			var link = new SegmentLink();
			BindNewLinkToPort(port, link);
			return link;
		}

		protected virtual void BindNewLinkToPort(IPort port, LinkBase link)
		{
			link.EndCap = true;
			var portBase = port as PortBase;
			if (portBase != null)
			{
				if (portBase.CanAcceptIncomingLinks && !portBase.CanAcceptOutgoingLinks)
					link.Target = port;
				else
					link.Source = port;
			}
			else
				link.Source = port;
		}

		protected virtual LinkAdorner CreateAdorner()
		{
			return new LinkAdorner(View, DragStart) { Cursor = Cursors.Cross };
		}

		protected virtual Vector UpdateVector(Vector vector)
		{
			return vector;
		}
	}
}
