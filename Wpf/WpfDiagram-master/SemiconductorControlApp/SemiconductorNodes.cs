using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Aga.Diagrams.Controls;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 半导体设备节点枚举
    /// </summary>
    public enum DeviceType
    {
        Pump,           // 泵
        Valve,          // 阀门
        Sensor,         // 传感器
        Heater,         // 加热器
        Chamber,        // 反应腔
        Controller      // 控制器
    }

    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum DeviceStatus
    {
        Idle,           // 空闲
        Running,        // 运行中
        Error,          // 错误
        Maintenance     // 维护
    }

    /// <summary>
    /// 半导体设备节点数据模型
    /// </summary>
    public class SemiconductorDevice : INotifyPropertyChanged
    {
        private string _deviceId;
        private string _deviceName;
        private DeviceType _deviceType;
        private DeviceStatus _status;
        private double _currentValue;
        private double _targetValue;
        private string _unit;
        private DateTime _lastUpdateTime;

        public string DeviceId
        {
            get => _deviceId;
            set { _deviceId = value; OnPropertyChanged(); }
        }

        public string DeviceName
        {
            get => _deviceName;
            set { _deviceName = value; OnPropertyChanged(); }
        }

        public DeviceType DeviceType
        {
            get => _deviceType;
            set { _deviceType = value; OnPropertyChanged(); }
        }

        public DeviceStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public double CurrentValue
        {
            get => _currentValue;
            set { _currentValue = value; OnPropertyChanged(); }
        }

        public double TargetValue
        {
            get => _targetValue;
            set { _targetValue = value; OnPropertyChanged(); }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        public DateTime LastUpdateTime
        {
            get => _lastUpdateTime;
            set { _lastUpdateTime = value; OnPropertyChanged(); }
        }

        // 设备控制命令
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SetParameterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// 半导体设备节点控件
    /// </summary>
    public class SemiconductorNode : Node
    {
        static SemiconductorNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SemiconductorNode), 
                new FrameworkPropertyMetadata(typeof(SemiconductorNode)));
        }

        #region 依赖属性

        public static readonly DependencyProperty DeviceProperty =
            DependencyProperty.Register("Device", typeof(SemiconductorDevice), 
                typeof(SemiconductorNode), new PropertyMetadata(null, OnDeviceChanged));

        public SemiconductorDevice Device
        {
            get => (SemiconductorDevice)GetValue(DeviceProperty);
            set => SetValue(DeviceProperty, value);
        }

        private static void OnDeviceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = d as SemiconductorNode;
            var device = e.NewValue as SemiconductorDevice;
            
            if (device != null && node != null)
            {
                // 绑定设备数据到节点
                node.ModelElement = device;
                node.SetupCommands(device);
                node.SetupPorts(device);
            }
        }

        #endregion

        private void SetupCommands(SemiconductorDevice device)
        {
            // 设置设备控制命令
            device.StartCommand = new RelayCommand(() => ExecuteDeviceCommand("START", device));
            device.StopCommand = new RelayCommand(() => ExecuteDeviceCommand("STOP", device));
            device.ResetCommand = new RelayCommand(() => ExecuteDeviceCommand("RESET", device));
            device.SetParameterCommand = new RelayCommand<double>(value => 
                ExecuteSetParameter(device, value));
        }

        private void SetupPorts(SemiconductorDevice device)
        {
            // 根据设备类型设置端口
            Ports.Clear();
            
            switch (device.DeviceType)
            {
                case DeviceType.Pump:
                    // 泵：输入端口（控制信号）+ 输出端口（流量输出）
                    AddPort("Control_In", VerticalAlignment.Top, HorizontalAlignment.Center, true, false);
                    AddPort("Flow_Out", VerticalAlignment.Bottom, HorizontalAlignment.Center, false, true);
                    break;
                    
                case DeviceType.Valve:
                    // 阀门：输入端口（控制信号）+ 流体输入/输出端口
                    AddPort("Control_In", VerticalAlignment.Top, HorizontalAlignment.Center, true, false);
                    AddPort("Fluid_In", VerticalAlignment.Center, HorizontalAlignment.Left, true, false);
                    AddPort("Fluid_Out", VerticalAlignment.Center, HorizontalAlignment.Right, false, true);
                    break;
                    
                case DeviceType.Sensor:
                    // 传感器：数据输出端口
                    AddPort("Data_Out", VerticalAlignment.Bottom, HorizontalAlignment.Center, false, true);
                    break;
                    
                case DeviceType.Chamber:
                    // 反应腔：多个输入输出端口
                    AddPort("Gas_In", VerticalAlignment.Top, HorizontalAlignment.Left, true, false);
                    AddPort("Gas_Out", VerticalAlignment.Top, HorizontalAlignment.Right, false, true);
                    AddPort("Control_In", VerticalAlignment.Top, HorizontalAlignment.Center, true, false);
                    AddPort("Status_Out", VerticalAlignment.Bottom, HorizontalAlignment.Center, false, true);
                    break;
            }
        }

        private void AddPort(string portName, VerticalAlignment vAlign, HorizontalAlignment hAlign, 
            bool canAcceptIncoming, bool canAcceptOutgoing)
        {
            var port = new EllipsePort
            {
                Name = portName,
                Width = 12,
                Height = 12,
                Margin = new Thickness(-6),
                VerticalAlignment = vAlign,
                HorizontalAlignment = hAlign,
                CanAcceptIncomingLinks = canAcceptIncoming,
                CanAcceptOutgoingLinks = canAcceptOutgoing,
                CanCreateLink = true,
                Tag = portName
            };
            
            Ports.Add(port);
        }

        private async void ExecuteDeviceCommand(string command, SemiconductorDevice device)
        {
            try
            {
                // 调用设备控制API
                await DeviceControlService.ExecuteCommandAsync(device.DeviceId, command);
                
                // 记录到数据库
                await DatabaseService.LogDeviceOperationAsync(device.DeviceId, command, DateTime.Now);
                
                // 更新设备状态
                device.Status = command == "START" ? DeviceStatus.Running : DeviceStatus.Idle;
                device.LastUpdateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                // 错误处理
                device.Status = DeviceStatus.Error;
                await DatabaseService.LogErrorAsync(device.DeviceId, ex.Message, DateTime.Now);
            }
        }

        private async void ExecuteSetParameter(SemiconductorDevice device, double value)
        {
            try
            {
                // 设置设备参数
                await DeviceControlService.SetParameterAsync(device.DeviceId, value, device.Unit);
                
                // 更新目标值
                device.TargetValue = value;
                device.LastUpdateTime = DateTime.Now;
                
                // 记录参数变更
                await DatabaseService.LogParameterChangeAsync(device.DeviceId, 
                    "TargetValue", value.ToString(), DateTime.Now);
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync(device.DeviceId, ex.Message, DateTime.Now);
            }
        }
    }
}