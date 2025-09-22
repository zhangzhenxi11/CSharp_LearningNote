﻿﻿﻿﻿﻿﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using Aga.Diagrams.Controls;


/*
选择管理
Selection.cs - 选择集合管理器，处理多选、主选择项、选择状态变化通知

学习要点：
1. INotifyPropertyChanged接口的实现和数据绑定原理
2. 集合管理的设计模式（使用Dictionary作为内部存储）
3. 主选择项的概念和管理机制
4. IEnumerable接口的实现和集合遍历支持
5. 封装性设计（internal构造函数）

*/
namespace Aga.Diagrams
{
	/// <summary>
	/// Selection.cs - 选择管理器
	/// 管理图表中元素的选中状态，支持单选和多选
	/// 实现INotifyPropertyChanged支持数据绑定
	/// 管理主选择项（Primary）和选择集合（Items）
	/// 提供添加、删除、清空、设置选择等操作
	/// 实现IEnumerable接口支持集合遍历
	/// 
	/// 设计亮点：
	/// - 使用Dictionary作为内部存储，提高查找效率
	/// - 区分主选择项和普通选择项，支持复杂交互
	/// - 实现属性变更通知，支持WPF数据绑定
	/// </summary>
	public class Selection : INotifyPropertyChanged, IEnumerable<DiagramItem>
	{
		/// <summary>
		/// 主选择项的私有字段
		/// 在多选情况下，主选择项通常是最先选中的元素
		/// </summary>
		private DiagramItem _primary;
		
		/// <summary>
		/// 主选择项属性 - 只读，用于获取当前的主要选中元素
		/// 在多选操作中，主选择项通常用于确定操作的基准元素
		/// 支持数据绑定，属性变更时会触发PropertyChanged事件
		/// </summary>
		public DiagramItem Primary
		{
			get { return _primary; }
		}

		/// <summary>
		/// 内部存储集合 - 使用Dictionary实现高效的查找和存在性检查
		/// Key为DiagramItem，Value更多时候是null（仅使用Key的集合特性）
		/// 这种设计提供了O(1)的查找复杂度
		/// </summary>
		private Dictionary<DiagramItem, object> _items = new Dictionary<DiagramItem, object>();
		
		/// <summary>
		/// 选中元素集合属性 - 返回所有当前选中的元素
		/// 通过Dictionary.Keys实现，提供只读的集合视图
		/// 支持LINQ查询和集合操作
		/// </summary>
		public IEnumerable<DiagramItem> Items
		{
			get { return _items.Keys; }
		}

		/// <summary>
		/// 选中元素数量属性 - 用于快速获取选中项的数量
		/// 常用于条件判断，如是否有选中项、是否为单选等
		/// </summary>
		public int Count
		{
			get { return _items.Count; }
		}

		/// <summary>
		/// 内部构造函数 - 使用internal修饰符限制实例化
		/// 只能由同一程序集内的类创建，通常由DiagramView类实例化
		/// 这种设计确保了Selection对象的生命周期和使用方式可控
		/// </summary>
		internal Selection()
		{
		}

		/// <summary>
		/// 检查指定元素是否已选中
		/// 使用Dictionary的ContainsKey方法，时间复杂度为O(1)
		/// 常用于在添加元素前检查重复性
		/// </summary>
		/// <param name="item">要检查的图表元素</param>
		/// <returns>如果元素已选中返回true，否则返回false</returns>
		public bool Contains(DiagramItem item)
		{
			return _items.ContainsKey(item);
		}

		/// <summary>
		/// 向选择集合中添加元素 - 支持多选操作
		/// 实现以下功能：
		/// 1. 防止重复添加同一元素
		/// 2. 设置元素的选中状态（IsSelected = true）
		/// 3. 管理主选择项（第一个添加的元素成为主选择项）
		/// 4. 触发属性变更通知，支持数据绑定
		/// </summary>
		/// <param name="item">要添加的图表元素</param>
		public void Add(DiagramItem item)
		{
			// 只有当元素不在集合中时才添加
			if (!_items.ContainsKey(item))
			{
				// 判断是否为第一个元素（将成为主选择项）
				bool isPrimary = Count == 0;
				
				// 添加到内部集合
				_items.Add(item, null);
				
				// 设置元素的选中状态
				item.IsSelected = true;
				
				// 设置主选择项标志
				item.IsPrimarySelection = isPrimary;
				
				// 如果是第一个元素，设置为主选择项
				if (isPrimary)
				{
					_primary = item;
					// 触发Primary属性变更通知
					OnPropertyChanged("Primary");
				}
				
				// 触发Items属性变更通知
				OnPropertyChanged("Items");
			}
		}

		/// <summary>
		/// 从选择集合中移除元素 - 支持取消选择操作
		/// 实现以下功能：
		/// 1. 移除元素的选中状态（IsSelected = false）
		/// 2. 从内部集合中移除元素
		/// 3. 重新管理主选择项（如果移除的是主选择项）
		/// 4. 触发属性变更通知
		/// </summary>
		/// <param name="item">要移除的图表元素</param>
		public void Remove(DiagramItem item)
		{
			// 只有当元素在集合中时才移除
			if (_items.ContainsKey(item))
			{
				// 取消元素的选中状态
				item.IsSelected = false;
				
				// 从内部集合中移除
				_items.Remove(item);
			}
			
			// 如果移除的是主选择项，需要重新选择主选择项
			if (_primary == item)
			{
				// 选择第一个剩余元素作为新的主选择项
				_primary = _items.Keys.FirstOrDefault();
				
				// 如果还有其他元素，设置为主选择项
				if (_primary != null)
					_primary.IsPrimarySelection = true;
					
				// 触发Primary属性变更通知
				OnPropertyChanged("Primary");
			}
			
			// 触发Items属性变更通知
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 设置单个元素为选中状态 - 清空现有选择并选中指定元素
		/// 这是一个便捷方法，等价于SetRange(new DiagramItem[] { item })
		/// 常用于单击选择操作
		/// </summary>
		/// <param name="item">要选中的元素</param>
		public void Set(DiagramItem item)
		{
			// 使用SetRange方法实现，传入单元素数组
			SetRange(new DiagramItem[] { item });
		}

		/// <summary>
		/// 设置多个元素为选中状态 - 清空现有选择并选中指定元素集合
		/// 实现以下功能：
		/// 1. 清空所有现有选择
		/// 2. 选中指定的元素集合
		/// 3. 设置第一个元素为主选择项
		/// 4. 触发属性变更通知
		/// 常用于框选操作的结果处理
		/// </summary>
		/// <param name="items">要选中的元素集合</param>
		public void SetRange(IEnumerable<DiagramItem> items)
		{
			// 先清空所有现有选择
			DoClear();
			
			// 标记第一个元素为主选择项
			bool isPrimary = true;
			
			// 遍历所有要选中的元素
			foreach (var item in items)
			{
				// 添加到内部集合
				_items.Add(item, null);
				
				// 设置选中状态
				item.IsSelected = true;
				
				// 如果是第一个元素，设置为主选择项
				if (isPrimary)
				{
					_primary = item;
					item.IsPrimarySelection = true;
					isPrimary = false; // 后续元素不再是主选择项
				}
			}
			
			// 触发属性变更通知
			OnPropertyChanged("Primary");
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 清空所有选择 - 取消所有元素的选中状态
		/// 常用于点击空白区域或ESC键取消选择的操作
		/// 触发属性变更通知，支持数据绑定
		/// </summary>
		public void Clear()
		{
			// 执行实际的清空操作
			DoClear();
			
			// 触发属性变更通知
			OnPropertyChanged("Primary");
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 执行实际清空操作的私有方法 - 不触发属性变更通知
		/// 由Clear()和SetRange()方法调用，避免重复的通知触发
		/// 负责清理所有元素的选中状态和内部集合
		/// </summary>
		private void DoClear()
		{
			// 遍历所有选中元素，取消其选中状态
			foreach (var item in Items)
				item.IsSelected = false;
				
			// 清空内部集合
			_items.Clear();
			
			// 重置主选择项
			_primary = null;
		}

		#region INotifyPropertyChanged Members - 属性变更通知接口实现

		/// <summary>
		/// 属性变更事件 - WPF数据绑定的核心机制
		/// 当Primary或Items属性发生变化时触发，通知UI自动更新
		/// 这是MVVM模式中视图模型层的标准实现
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 触发属性变更通知的受保护方法
		/// 检查事件是否为null以避免NullReferenceException
		/// 使用字符串参数传递属性名，支持编译时检查
		/// </summary>
		/// <param name="name">发生变化的属性名称</param>
		protected void OnPropertyChanged(string name)
		{
			// 检查是否有订阅者在监听这个事件
			if (PropertyChanged != null)
				// 触发事件，传递属性名和变更事件参数
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion

		#region IEnumerable Members - 集合遍历接口实现

		/// <summary>
		/// 非泛型IEnumerable接口实现 - 支持foreach循环遍历
		/// 返回一个非泛型的迭代器，用于foreach语句
		/// 实际委托给Items属性的GetEnumerator方法
		/// </summary>
		/// <returns>用于遍历选中元素的迭代器</returns>
		public IEnumerator GetEnumerator()
		{
			// 委托给Items集合的迭代器
			return Items.GetEnumerator();
		}

		#endregion

		#region IEnumerable<DiagramItem> Members - 泛型集合遍历接口实现

		/// <summary>
		/// 泛型IEnumerable<DiagramItem>接口实现 - 支持强类型的foreach遍历
		/// 返回一个泛型迭代器，提供类型安全的遍历支持
		/// 支持LINQ查询和其他泛型集合操作
		/// </summary>
		/// <returns>用于遍历DiagramItem元素的泛型迭代器</returns>
		IEnumerator<DiagramItem> IEnumerable<DiagramItem>.GetEnumerator()
		{
			// 委托给Items集合的泛型迭代器
			return Items.GetEnumerator();
		}

		#endregion
	}
}
