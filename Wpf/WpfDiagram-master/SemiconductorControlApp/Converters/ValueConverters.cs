using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 设备状态到颜色的转换器 - 展示WPF值转换器的应用
    /// </summary>
    public class DeviceStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DeviceStatus status)
            {
                return status switch
                {
                    DeviceStatus.Idle => Brushes.Gray,
                    DeviceStatus.Running => Brushes.LimeGreen,
                    DeviceStatus.Error => Brushes.Red,
                    DeviceStatus.Maintenance => Brushes.Orange,
                    _ => Brushes.Gray
                };
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 流程状态到字符串的转换器
    /// </summary>
    public class ProcessStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProcessStatus status)
            {
                return status switch
                {
                    ProcessStatus.Stopped => "已停止",
                    ProcessStatus.Running => "运行中",
                    ProcessStatus.Paused => "已暂停",
                    ProcessStatus.Error => "错误",
                    ProcessStatus.Completed => "已完成",
                    _ => "未知"
                };
            }
            return "未知";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 布尔到可见性的转换器
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }

    /// <summary>
    /// 数值到字符串的格式化转换器
    /// </summary>
    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                var format = parameter?.ToString() ?? "F2";
                return doubleValue.ToString(format);
            }
            return value?.ToString() ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString(), out double result))
            {
                return result;
            }
            return 0.0;
        }
    }

    /// <summary>
    /// 设备类型到图标的转换器
    /// </summary>
    public class DeviceTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DeviceType type)
            {
                return type switch
                {
                    DeviceType.Pump => "🔄",
                    DeviceType.Valve => "🚰",
                    DeviceType.Sensor => "📊",
                    DeviceType.Heater => "🔥",
                    DeviceType.Chamber => "⚗️",
                    DeviceType.Controller => "🎛️",
                    _ => "❓"
                };
            }
            return "❓";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 多值转换器示例 - 将多个值组合成一个字符串
    /// </summary>
    public class MultiValueToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length >= 2)
            {
                var deviceName = values[0]?.ToString() ?? "";
                var currentValue = values[1];
                var unit = values.Length > 2 ? values[2]?.ToString() ?? "" : "";
                
                if (currentValue is double doubleValue)
                {
                    return $"{deviceName}: {doubleValue:F2} {unit}";
                }
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}