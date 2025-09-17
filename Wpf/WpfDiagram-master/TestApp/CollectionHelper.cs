﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
	/// <summary>
	/// CollectionHelper.cs - 集合操作帮助类
	/// 为集合类型提供扩展方法
	/// 实现按条件批量删除元素的功能
	/// 使用LINQ和Func委托提供灵活的查询条件
	/// </summary>
	static class CollectionHelper
	{
		public static void RemoveRange<T>(this ICollection<T> source, Func<T, bool> predicate)
		{
			var arr = source.Where(p => predicate(p)).ToArray();
			foreach (var t in arr)
				source.Remove(t);
		}
	}
}
