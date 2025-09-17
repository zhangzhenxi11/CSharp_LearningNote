﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Aga.Diagrams
{
	/// <summary>
	/// DebuggingConverter.cs - 调试转换器
	/// 实现IValueConverter接口，用于调试数据绑定
	/// 不做任何转换，直接返回原值，便于断点调试
	/// 使用单例模式提供全局访问
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class DebuggingConverter : IValueConverter
	{
		public static DebuggingConverter Instance
		{
			get { return new DebuggingConverter(); }
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}
	}
}
