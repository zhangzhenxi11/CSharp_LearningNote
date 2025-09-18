using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Aga.Diagrams;
using Aga.Diagrams.Controls;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 半导体流程控制器
    /// </summary>
    public class SemiconductorProcessController : IDiagramController, INotifyPropertyChanged
    {
        private readonly DiagramView _diagramView;
        private ProcessStatus _processStatus;
        private string _currentRecipe;
        private DateTime _processStartTime;
        private TimeSpan _processElapsedTime;
        private ObservableCollection<SemiconductorDevice> _devices;
        private ObservableCollection<ProcessConnection> _connections;
        private System.Timers.Timer _statusUpdateTimer;

        public SemiconductorProcessController(DiagramView diagramView)
        {
            _diagramView = diagramView;
            _devices = new ObservableCollection<SemiconductorDevice>();
            _connections = new ObservableCollection<ProcessConnection>();
            
            InitializeCommands();
            StartStatusUpdateTimer();
        }

        #region 属性

        public ProcessStatus ProcessStatus
        {
            get => _processStatus;
            set { _processStatus = value; OnPropertyChanged(); }
        }

        public string CurrentRecipe
        {
            get => _currentRecipe;
            set { _currentRecipe = value; OnPropertyChanged(); }
        }

        public DateTime ProcessStartTime
        {
            get => _processStartTime;
            set { _processStartTime = value; OnPropertyChanged(); }
        }

        public TimeSpan ProcessElapsedTime
        {
            get => _processElapsedTime;
            set { _processElapsedTime = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SemiconductorDevice> Devices => _devices;
        public ObservableCollection<ProcessConnection> Connections => _connections;

        #endregion

        #region 命令

        public ICommand StartProcessCommand { get; private set; }
        public ICommand StopProcessCommand { get; private set; }
        public ICommand PauseProcessCommand { get; private set; }
        public ICommand LoadRecipeCommand { get; private set; }
        public ICommand SaveConfigurationCommand { get; private set; }
        public ICommand ExportDataCommand { get; private set; }

        private void InitializeCommands()
        {
            StartProcessCommand = new RelayCommand(async () => await StartProcessAsync(), 
                () => ProcessStatus == ProcessStatus.Stopped || ProcessStatus == ProcessStatus.Paused);
            
            StopProcessCommand = new RelayCommand(async () => await StopProcessAsync(),
                () => ProcessStatus != ProcessStatus.Stopped);
                
            PauseProcessCommand = new RelayCommand(async () => await PauseProcessAsync(),
                () => ProcessStatus == ProcessStatus.Running);
                
            LoadRecipeCommand = new RelayCommand<string>(async recipe => await LoadRecipeAsync(recipe));
            SaveConfigurationCommand = new RelayCommand(async () => await SaveConfigurationAsync());
            ExportDataCommand = new RelayCommand(async () => await ExportDataAsync());
        }

        #endregion

        #region 流程控制方法

        public async Task StartProcessAsync()
        {
            try
            {
                ProcessStatus = ProcessStatus.Running;
                ProcessStartTime = DateTime.Now;
                
                // 1. 验证设备连接
                if (!await ValidateDeviceConnectionsAsync())
                {
                    ProcessStatus = ProcessStatus.Error;
                    return;
                }

                // 2. 初始化所有设备
                foreach (var device in _devices)
                {
                    device.Status = DeviceStatus.Idle;
                    await DeviceControlService.ExecuteCommandAsync(device.DeviceId, "INITIALIZE");
                }

                // 3. 执行流程配方
                await ExecuteRecipeAsync();

                // 4. 记录流程开始
                await DatabaseService.LogProcessEventAsync("ProcessStarted", CurrentRecipe, DateTime.Now);
            }
            catch (Exception ex)
            {
                ProcessStatus = ProcessStatus.Error;
                await DatabaseService.LogErrorAsync("ProcessController", ex.Message, DateTime.Now);
            }
        }

        public async Task StopProcessAsync()
        {
            try
            {
                ProcessStatus = ProcessStatus.Stopped;
                
                // 1. 停止所有设备
                foreach (var device in _devices)
                {
                    await DeviceControlService.ExecuteCommandAsync(device.DeviceId, "STOP");
                    device.Status = DeviceStatus.Idle;
                }

                // 2. 关闭所有连接
                foreach (var connection in _connections)
                {
                    connection.IsActive = false;
                }

                // 3. 记录流程结束
                await DatabaseService.LogProcessEventAsync("ProcessStopped", CurrentRecipe, DateTime.Now);
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync("ProcessController", ex.Message, DateTime.Now);
            }
        }

        public async Task PauseProcessAsync()
        {
            ProcessStatus = ProcessStatus.Paused;
            await DatabaseService.LogProcessEventAsync("ProcessPaused", CurrentRecipe, DateTime.Now);
        }

        private async Task<bool> ValidateDeviceConnectionsAsync()
        {
            // 验证所有设备连接是否正常
            foreach (var device in _devices)
            {
                if (device.Status == DeviceStatus.Error)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task ExecuteRecipeAsync()
        {
            // 这里实现具体的配方执行逻辑
            // 可以根据流程图中的连接关系按序执行
            await Task.Run(async () =>
            {
                // 示例：按设备顺序启动
                foreach (var device in _devices.OrderBy(d => d.DeviceId))
                {
                    if (ProcessStatus != ProcessStatus.Running) break;
                    
                    await DeviceControlService.ExecuteCommandAsync(device.DeviceId, "START");
                    device.Status = DeviceStatus.Running;
                    
                    // 等待设备稳定
                    await Task.Delay(1000);
                }
            });
        }

        #endregion

        #region 设备管理

        /// <summary>
        /// 在指定位置添加设备
        /// </summary>
        public SemiconductorDevice AddDeviceAtPosition(string deviceType, double x, double y)
        {
            try
            {
                if (!Enum.TryParse<DeviceType>(deviceType, out var type))
                {
                    Console.WriteLine($"无效的设备类型: {deviceType}");
                    return null;
                }

                var deviceId = $"{deviceType.ToUpper()}{_devices.Count + 1:D2}";
                var device = new SemiconductorDevice
                {
                    DeviceId = deviceId,
                    DeviceName = GetDeviceDisplayName(type),
                    DeviceType = type,
                    Status = DeviceStatus.Idle,
                    Unit = GetDefaultUnit(type),
                    TargetValue = GetDefaultTargetValue(type),
                    CurrentValue = 0,
                    LastUpdateTime = DateTime.Now,
                    ConditionExpression = type == DeviceType.Condition ? "Value > 50" : null
                };

                // 设置命令
                device.StartCommand = new RelayCommand(() => ExecuteDeviceCommand("START", device));
                device.StopCommand = new RelayCommand(() => ExecuteDeviceCommand("STOP", device));
                device.ResetCommand = new RelayCommand(() => ExecuteDeviceCommand("RESET", device));
                device.SetParameterCommand = new RelayCommand<double>(value => ExecuteSetParameter(device, value));

                // 创建可视化节点
                var node = new SemiconductorNode
                {
                    Device = device,  // 先设置Device属性触发端口创建
                    Width = 120,
                    Height = 80
                };

                // 设置节点位置
                node.SetValue(System.Windows.Controls.Canvas.LeftProperty, x - 60);
                node.SetValue(System.Windows.Controls.Canvas.TopProperty, y - 40);

                // 添加到集合和视图
                _devices.Add(device);
                _diagramView.Children.Add(node);
                
                // 强制刷新端口设置（确保端口正确显示）
                node.SetupPorts(device);
                
                Console.WriteLine($"添加设备: {device.DeviceName} [{device.DeviceId}] 在位置 ({x:F0}, {y:F0})，端口数量: {node.Ports.Count}");
                
                return device;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建设备失败: {ex.Message}");
                return null;
            }
        }

        private async void ExecuteDeviceCommand(string command, SemiconductorDevice device)
        {
            try
            {
                await DeviceControlService.ExecuteCommandAsync(device.DeviceId, command);
                
                // 更新设备状态
                device.Status = command switch
                {
                    "START" => DeviceStatus.Running,
                    "STOP" => DeviceStatus.Idle,
                    "RESET" => DeviceStatus.Idle,
                    _ => device.Status
                };
                
                device.LastUpdateTime = DateTime.Now;
                Console.WriteLine($"设备命令: {device.DeviceName} - {command}");
            }
            catch (Exception ex)
            {
                device.Status = DeviceStatus.Error;
                Console.WriteLine($"设备命令失败: {device.DeviceName} - {command}: {ex.Message}");
            }
        }
        
        private async void ExecuteSetParameter(SemiconductorDevice device, double value)
        {
            try
            {
                await DeviceControlService.SetParameterAsync(device.DeviceId, value, device.Unit);
                device.TargetValue = value;
                device.LastUpdateTime = DateTime.Now;
                Console.WriteLine($"参数设置: {device.DeviceName} = {value} {device.Unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"参数设置失败: {device.DeviceName}: {ex.Message}");
            }
        }

        private string GetDeviceDisplayName(DeviceType type) => type switch
        {
            DeviceType.Pump => "泵",
            DeviceType.Valve => "阀门",
            DeviceType.Sensor => "传感器",
            DeviceType.Heater => "加热器",
            DeviceType.Chamber => "反应腔",
            DeviceType.Controller => "控制器",
            DeviceType.Condition => "条件节点",
            _ => "未知设备"
        };

        private string GetDefaultUnit(DeviceType type) => type switch
        {
            DeviceType.Pump => "L/min",
            DeviceType.Valve => "%",
            DeviceType.Sensor => "°C",
            DeviceType.Heater => "°C", 
            DeviceType.Chamber => "Torr",
            DeviceType.Controller => "",
            DeviceType.Condition => "",
            _ => ""
        };

        private double GetDefaultTargetValue(DeviceType type) => type switch
        {
            DeviceType.Pump => 10.0,
            DeviceType.Valve => 50.0,
            DeviceType.Sensor => 25.0,
            DeviceType.Heater => 200.0,
            DeviceType.Chamber => 0.1,
            DeviceType.Controller => 0.0,
            DeviceType.Condition => 0.0,
            _ => 0.0
        };

        #endregion

        #region 配方和配置管理

        public async Task LoadRecipeAsync(string recipeName)
        {
            try
            {
                // 从数据库加载配方
                var recipeData = await DatabaseService.LoadRecipeAsync(recipeName);
                
                // 清空当前配置
                _devices.Clear();
                _connections.Clear();
                _diagramView.Children.Clear();

                // 重建流程图
                foreach (var deviceData in recipeData.Devices)
                {
                    var device = new SemiconductorDevice
                    {
                        DeviceId = deviceData.DeviceId,
                        DeviceName = deviceData.DeviceName,
                        DeviceType = deviceData.DeviceType,
                        Status = DeviceStatus.Idle
                    };
                    
                    var node = new SemiconductorNode
                    {
                        Device = device,
                        Width = 120,
                        Height = 80
                    };
                    
                    // 设置节点位置
                    node.SetValue(System.Windows.Controls.Canvas.LeftProperty, deviceData.X);
                    node.SetValue(System.Windows.Controls.Canvas.TopProperty, deviceData.Y);
                    
                    _devices.Add(device);
                    _diagramView.Children.Add(node);
                }

                // 重建连接
                foreach (var connectionData in recipeData.Connections)
                {
                    var connection = new ProcessConnection
                    {
                        ConnectionId = connectionData.ConnectionId,
                        SourceDevice = _devices.First(d => d.DeviceId == connectionData.SourceDeviceId),
                        TargetDevice = _devices.First(d => d.DeviceId == connectionData.TargetDeviceId),
                        SourcePort = connectionData.SourcePort,
                        TargetPort = connectionData.TargetPort
                    };
                    
                    _connections.Add(connection);
                    
                    // 创建视觉连接线
                    // 这里需要根据实际的端口位置创建Link
                }

                CurrentRecipe = recipeName;
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync("RecipeLoader", ex.Message, DateTime.Now);
            }
        }

        public async Task SaveConfigurationAsync()
        {
            try
            {
                var configData = new
                {
                    Devices = _devices.Select(d => new
                    {
                        d.DeviceId,
                        d.DeviceName,
                        d.DeviceType,
                        X = (double)(_diagramView.Items.FirstOrDefault(i => (i as SemiconductorNode)?.Device == d)?.GetValue(System.Windows.Controls.Canvas.LeftProperty) ?? 0),
                        Y = (double)(_diagramView.Items.FirstOrDefault(i => (i as SemiconductorNode)?.Device == d)?.GetValue(System.Windows.Controls.Canvas.TopProperty) ?? 0)
                    }),
                    Connections = _connections.Select(c => new
                    {
                        c.ConnectionId,
                        SourceDeviceId = c.SourceDevice.DeviceId,
                        TargetDeviceId = c.TargetDevice.DeviceId,
                        c.SourcePort,
                        c.TargetPort
                    }),
                    RecipeName = CurrentRecipe
                };

                await DatabaseService.SaveConfigurationAsync(configData);
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync("ConfigurationSaver", ex.Message, DateTime.Now);
            }
        }

        public async Task ExportDataAsync()
        {
            try
            {
                // 导出历史数据
                var exportData = await DatabaseService.ExportProcessDataAsync(
                    ProcessStartTime, DateTime.Now);
                
                // 保存到文件或发送到其他系统
                // 这里可以实现具体的导出逻辑
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync("DataExporter", ex.Message, DateTime.Now);
            }
        }

        #endregion

        #region 实时数据更新

        private void StartStatusUpdateTimer()
        {
            _statusUpdateTimer = new System.Timers.Timer(1000); // 每秒更新
            _statusUpdateTimer.Elapsed += async (s, e) => await UpdateDeviceStatusAsync();
            _statusUpdateTimer.Start();
        }

        private async Task UpdateDeviceStatusAsync()
        {
            try
            {
                foreach (var device in _devices)
                {
                    // 从实际设备读取当前值
                    var currentValue = await DeviceControlService.ReadCurrentValueAsync(device.DeviceId);
                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        device.CurrentValue = currentValue;
                        device.LastUpdateTime = DateTime.Now;
                    });

                    // 记录数据到数据库
                    await DatabaseService.LogDeviceDataAsync(device.DeviceId, currentValue, DateTime.Now);
                }

                // 更新流程运行时间
                if (ProcessStatus == ProcessStatus.Running)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ProcessElapsedTime = DateTime.Now - ProcessStartTime;
                    });
                }
            }
            catch (Exception ex)
            {
                await DatabaseService.LogErrorAsync("StatusUpdater", ex.Message, DateTime.Now);
            }
        }

        #endregion

        #region IDiagramController 实现

        public void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds)
        {
            // 更新设备位置
            for (int i = 0; i < items.Length; i++)
            {
                var node = items[i] as SemiconductorNode;
                if (node?.Device != null)
                {
                    // 记录位置变更到数据库
                    _ = DatabaseService.LogPositionChangeAsync(
                        node.Device.DeviceId, bounds[i].X, bounds[i].Y, DateTime.Now);
                }
            }
        }

        public void UpdateLink(LinkInfo initialState, ILink link)
        {
            // 更新连接信息
            if (link is LinkBase linkBase && linkBase.ModelElement is ProcessConnection connection)
            {
                // 记录连接变更
                _ = DatabaseService.LogConnectionChangeAsync(
                    connection.ConnectionId, "LinkUpdated", DateTime.Now);
            }
        }

        public void ExecuteCommand(ICommand command, object parameter)
        {
            // 处理图表相关命令
            if (command == ApplicationCommands.Delete && _diagramView.Selection.Count > 0)
            {
                // 删除选中的设备或连接
                foreach (var item in _diagramView.Selection.ToList())
                {
                    if (item is SemiconductorNode node)
                    {
                        _devices.Remove(node.Device);
                        _diagramView.Children.Remove(node);
                    }
                }
            }
        }

        public bool CanExecuteCommand(ICommand command, object parameter)
        {
            if (command == ApplicationCommands.Delete)
                return _diagramView.Selection.Count > 0 && ProcessStatus == ProcessStatus.Stopped;
            
            return false;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _statusUpdateTimer?.Stop();
            _statusUpdateTimer?.Dispose();
        }
    }
}