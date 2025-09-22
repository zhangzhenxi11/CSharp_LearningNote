﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Diagrams.Controls;
using System.Windows;

/*
控制器接口
IDiagramController.cs - 图表控制器接口，定义了更新元素边界、更新链接、执行命令等核心操作
*/
namespace Aga.Diagrams
{
	/// <summary>
	/// 图表控制器接口 - 定义图表编辑操作的核心抽象
	/// 
	/// 【设计模式】：控制器模式（MVC架构中的C层）
	/// 【核心作用】：
	/// 1. 将视图操作转换为业务逻辑调用
	/// 2. 提供统一的图表操作接口
	/// 3. 支持命令模式的可撤销操作
	/// 4. 解耦视图层和数据模型层
	/// 
	/// 【学习要点】：
	/// - 接口隔离原则：只包含图表操作必需的方法
	/// - 命令模式应用：ExecuteCommand和CanExecuteCommand
	/// - 批量操作支持：UpdateItemsBounds使用数组参数
	/// </summary>
	public interface IDiagramController
	{
		/// <summary>
		/// 更新图表元素的边界信息 - 处理移动和缩放操作
		/// 
		/// 【调用时机】：用户拖拽移动或调整元素尺寸时
		/// 【核心功能】：
		/// 1. 批量更新多个选中元素的位置和尺寸
		/// 2. 保持元素间的相对位置关系
		/// 3. 触发相关联的链接重新计算
		/// 4. 支持撤销/重做操作的数据记录
		/// 
		/// 【参数设计】：
		/// - 使用数组支持多选操作
		/// - items和bounds数组长度必须一致
		/// - Rect结构体包含X,Y,Width,Height信息
		/// </summary>
		/// <param name="items">需要更新的图表元素数组</param>
		/// <param name="bounds">对应的新边界矩形数组</param>
		void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds);

		/// <summary>
		/// 更新链接信息 - 处理连接创建和修改操作
		/// 
		/// 【调用时机】：用户创建新链接或修改现有链接时
		/// 【核心功能】：
		/// 1. 记录链接的初始状态（支持撤销操作）
		/// 2. 应用链接的当前状态到数据模型
		/// 3. 验证链接的有效性（如避免循环引用）
		/// 4. 更新相关节点的端口连接状态
		/// 
		/// 【设计思路】：
		/// - initialState保存操作前状态，用于撤销功能
		/// - link参数包含操作后的最终状态
		/// - 支持增量式的链接编辑操作
		/// </summary>
		/// <param name="initialState">链接操作前的初始状态</param>
		/// <param name="link">当前状态的链接对象</param>
		void UpdateLink(LinkInfo initialState, ILink link);

		/// <summary>
		/// 执行WPF命令 - 实现命令模式的核心方法
		/// 
		/// 【设计模式】：命令模式的Execute操作
		/// 【核心功能】：
		/// 1. 统一处理所有图表相关命令（删除、复制、粘贴等）
		/// 2. 支持命令参数的传递和解析
		/// 3. 提供命令执行的统一入口点
		/// 4. 便于添加日志记录和性能监控
		/// 
		/// 【常见命令类型】：
		/// - ApplicationCommands.Delete（删除选中元素）
		/// - ApplicationCommands.Copy（复制操作）
		/// - ApplicationCommands.Paste（粘贴操作）
		/// - 自定义命令（如对齐、分布等）
		/// </summary>
		/// <param name="command">要执行的WPF命令对象</param>
		/// <param name="parameter">命令参数（可为null）</param>
		void ExecuteCommand(System.Windows.Input.ICommand command, object parameter);

		/// <summary>
		/// 检查命令是否可执行 - 实现命令模式的CanExecute查询
		/// 
		/// 【设计模式】：命令模式的CanExecute操作
		/// 【核心功能】：
		/// 1. 根据当前选择状态判断命令可用性
		/// 2. 控制UI元素的启用/禁用状态
		/// 3. 提供命令可执行性的实时反馈
		/// 4. 避免无效操作的执行
		/// 
		/// 【判断逻辑示例】：
		/// - Delete命令：需要有选中的元素
		/// - Copy命令：需要有选中的元素
		/// - Paste命令：需要剪贴板有有效数据
		/// - Undo命令：需要有可撤销的操作历史
		/// 
		/// 【WPF绑定】：通常绑定到MenuItem.IsEnabled或Button.IsEnabled
		/// </summary>
		/// <param name="command">要检查的WPF命令对象</param>
		/// <param name="parameter">命令参数（可为null）</param>
		/// <returns>true表示命令可执行，false表示命令不可执行</returns>
		bool CanExecuteCommand(System.Windows.Input.ICommand command, object parameter);
	}
}
