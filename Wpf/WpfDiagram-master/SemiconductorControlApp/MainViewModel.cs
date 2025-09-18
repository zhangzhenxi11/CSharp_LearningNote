using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Windows;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 主窗口ViewModel - 展示WPF高级Binding机制
    /// 实现复杂的数据绑定、命令绑定、集合绑定等特性
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly RecipeService _recipeService;
        private readonly DispatcherTimer _statusTimer;

        // 私有字段，用于属性的后备存储
        private ProcessStatus _processStatus = ProcessStatus.Stopped;
        private string _currentRecipe = "默认配方";
        private DateTime _processStartTime;
        private TimeSpan _processElapsedTime;
        private bool _showGrid = true;
        private double _zoomLevel = 1.0;
        private SemiconductorDevice _selectedDevice;
        private string _statusMessage = "系统就绪";
        private string _systemLogs = "";
        private DateTime _currentTime = DateTime.Now;

        public MainViewModel()
        {
            _recipeService = new RecipeService();

            // 初始化集合 - 展示ObservableCollection的数据绑定
            Devices = new ObservableCollection<SemiconductorDevice>();
            Connections = new ObservableCollection<ProcessConnection>();
            AvailableRecipes = new ObservableCollection<string>();

            // 初始化命令 - 展示命令绑定机制
            InitializeCommands();

            // 启动状态更新定时器 - 展示实时数据绑定
            _statusTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();

            // 加载初始数据
            _ = LoadInitialDataAsync();
        }

        #region 属性 - 展示各种数据绑定模式

        /// <summary>
        /// 流程状态 - 展示枚举绑定和转换器使用
        /// </summary>
        public ProcessStatus ProcessStatus
        {
            get => _processStatus;
            set
            {
                if (_processStatus != value)
                {
                    _processStatus = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsProcessRunning));
                    OnPropertyChanged(nameof(CanStartProcess));
                    OnPropertyChanged(nameof(CanStopProcess));
                    
                    // 级联更新其他属性 - 展示属性间的联动绑定
                    UpdateStatusMessage();
                }
            }
        }

        /// <summary>
        /// 当前配方名称 - 展示字符串绑定
        /// </summary>
        public string CurrentRecipe
        {
            get => _currentRecipe;
            set
            {
                if (_currentRecipe != value)
                {
                    _currentRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 流程开始时间 - 展示DateTime绑定和格式化
        /// </summary>
        public DateTime ProcessStartTime
        {
            get => _processStartTime;
            set
            {
                if (_processStartTime != value)
                {
                    _processStartTime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 流程运行时间 - 展示TimeSpan绑定和格式化显示
        /// </summary>
        public TimeSpan ProcessElapsedTime
        {
            get => _processElapsedTime;
            set
            {
                if (_processElapsedTime != value)
                {
                    _processElapsedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 是否显示网格 - 展示布尔绑定
        /// </summary>
        public bool ShowGrid
        {
            get => _showGrid;
            set
            {
                if (_showGrid != value)
                {
                    _showGrid = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 缩放级别 - 展示数值绑定和双向绑定
        /// </summary>
        public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                var clampedValue = Math.Max(0.1, Math.Min(5.0, value));
                if (Math.Abs(_zoomLevel - clampedValue) > 0.001)
                {
                    _zoomLevel = clampedValue;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的设备 - 展示复杂对象绑定
        /// </summary>
        public SemiconductorDevice SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasSelectedDevice));
                }
            }
        }

        /// <summary>
        /// 状态消息 - 展示动态文本绑定
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 系统日志 - 展示多行文本绑定
        /// </summary>
        public string SystemLogs
        {
            get => _systemLogs;
            set
            {
                if (_systemLogs != value)
                {
                    _systemLogs = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 当前时间 - 展示实时更新绑定
        /// </summary>
        public DateTime CurrentTime
        {
            get => _currentTime;
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 设备集合 - 展示ObservableCollection绑定
        /// </summary>
        public ObservableCollection<SemiconductorDevice> Devices { get; }

        /// <summary>
        /// 连接集合 - 展示集合的动态绑定
        /// </summary>
        public ObservableCollection<ProcessConnection> Connections { get; }

        /// <summary>
        /// 可用配方列表 - 展示下拉框数据源绑定
        /// </summary>
        public ObservableCollection<string> AvailableRecipes { get; }

        // 计算属性 - 展示依赖属性的自动更新
        public bool IsProcessRunning => ProcessStatus == ProcessStatus.Running;
        public bool CanStartProcess => ProcessStatus == ProcessStatus.Stopped || ProcessStatus == ProcessStatus.Paused;
        public bool CanStopProcess => ProcessStatus != ProcessStatus.Stopped;
        public bool HasSelectedDevice => SelectedDevice != null;
        public int ConnectedDeviceCount => Devices.Count(d => d.Status != DeviceStatus.Error);
        public int ActiveConnectionCount => Connections.Count(c => c.IsActive);

        #endregion

        #region 命令 - 展示ICommand绑定机制

        public ICommand StartProcessCommand { get; private set; }
        public ICommand StopProcessCommand { get; private set; }
        public ICommand PauseProcessCommand { get; private set; }
        public ICommand LoadRecipeCommand { get; private set; }
        public ICommand SaveConfigurationCommand { get; private set; }
        public ICommand ExportDataCommand { get; private set; }
        public ICommand AddDeviceCommand { get; private set; }
        public ICommand RemoveDeviceCommand { get; private set; }
        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }
        public ICommand ZoomToFitCommand { get; private set; }
        public ICommand NewProcessCommand { get; private set; }
        public ICommand SaveProcessCommand { get; private set; }
        public ICommand LoadProcessCommand { get; private set; }

        private void InitializeCommands()
        {
            // 流程控制命令 - 展示带参数验证的命令绑定
            StartProcessCommand = new AsyncRelayCommand(StartProcessAsync, () => CanStartProcess);
            StopProcessCommand = new AsyncRelayCommand(StopProcessAsync, () => CanStopProcess);
            PauseProcessCommand = new AsyncRelayCommand(PauseProcessAsync, () => IsProcessRunning);

            // 配方管理命令 - 展示参数化命令
            LoadRecipeCommand = new AsyncRelayCommand<string>(LoadRecipeAsync);
            SaveConfigurationCommand = new AsyncRelayCommand(SaveConfigurationAsync);
            ExportDataCommand = new AsyncRelayCommand(ExportDataAsync);

            // 设备管理命令 - 展示集合操作命令
            AddDeviceCommand = new RelayCommand<string>(AddDevice);
            RemoveDeviceCommand = new RelayCommand(() => RemoveDevice(SelectedDevice), () => HasSelectedDevice);

            // 视图控制命令 - 展示UI交互命令
            ZoomInCommand = new RelayCommand(() => ZoomLevel *= 1.2);
            ZoomOutCommand = new RelayCommand(() => ZoomLevel /= 1.2);
            ZoomToFitCommand = new RelayCommand(ZoomToFit);
            NewProcessCommand = new AsyncRelayCommand(NewProcessAsync);
            SaveProcessCommand = new AsyncRelayCommand(SaveProcessAsync);
            LoadProcessCommand = new AsyncRelayCommand(LoadProcessAsync);
        }

        #endregion

        #region 命令实现 - 展示异步命令和业务逻辑绑定

        private async Task StartProcessAsync()
        {
            try
            {
                ProcessStatus = ProcessStatus.Running;
                ProcessStartTime = DateTime.Now;
                StatusMessage = "正在启动流程...";
                
                AddLogMessage("开始执行流程");

                // 验证设备连接
                foreach (var device in Devices)
                {
                    if (device.Status == DeviceStatus.Error)
                    {
                        throw new InvalidOperationException($"设备 {device.DeviceName} 处于错误状态");
                    }
                }

                // 初始化设备
                foreach (var device in Devices)
                {
                    await DeviceControlService.ExecuteCommandAsync(device.DeviceId, "INITIALIZE");
                    device.Status = DeviceStatus.Idle;
                    AddLogMessage($"设备 {device.DeviceName} 初始化完成");
                }

                // 激活连接
                foreach (var connection in Connections)
                {
                    connection.IsActive = true;
                    AddLogMessage($"连接 {connection.ConnectionId} 已激活");
                }

                StatusMessage = "流程运行中";
                AddLogMessage("流程启动成功");

                // 记录到数据库
                await DatabaseService.LogProcessEventAsync("ProcessStarted", CurrentRecipe, DateTime.Now);
            }
            catch (Exception ex)
            {
                ProcessStatus = ProcessStatus.Error;
                StatusMessage = $"启动失败: {ex.Message}";
                AddLogMessage($"错误: {ex.Message}");
                Console.WriteLine($"流程启动失败: {ex.Message}");
            }
        }

        private async Task StopProcessAsync()
        {
            try
            {
                StatusMessage = "正在停止流程...";
                AddLogMessage("停止流程");

                // 停止所有设备
                foreach (var device in Devices)
                {
                    await DeviceControlService.ExecuteCommandAsync(device.DeviceId, "STOP");
                    device.Status = DeviceStatus.Idle;
                    AddLogMessage($"设备 {device.DeviceName} 已停止");
                }

                // 停用连接
                foreach (var connection in Connections)
                {
                    connection.IsActive = false;
                }

                ProcessStatus = ProcessStatus.Stopped;
                StatusMessage = "流程已停止";
                AddLogMessage("流程停止完成");

                await DatabaseService.LogProcessEventAsync("ProcessStopped", CurrentRecipe, DateTime.Now);
            }
            catch (Exception ex)
            {
                StatusMessage = $"停止失败: {ex.Message}";
                AddLogMessage($"错误: {ex.Message}");
                Console.WriteLine($"流程停止失败: {ex.Message}");
            }
        }

        private async Task PauseProcessAsync()
        {
            ProcessStatus = ProcessStatus.Paused;
            StatusMessage = "流程已暂停";
            AddLogMessage("流程暂停");
            await DatabaseService.LogProcessEventAsync("ProcessPaused", CurrentRecipe, DateTime.Now);
        }

        private async Task LoadRecipeAsync(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName)) return;

            try
            {
                StatusMessage = $"正在加载配方: {recipeName}";
                AddLogMessage($"加载配方: {recipeName}");

                var recipeData = await _recipeService.LoadRecipeAsync(recipeName);
                
                // 清空当前数据
                Devices.Clear();
                Connections.Clear();

                // 加载设备
                foreach (var deviceData in recipeData.Devices)
                {
                    var device = new SemiconductorDevice
                    {
                        DeviceId = deviceData.DeviceId,
                        DeviceName = deviceData.DeviceName,
                        DeviceType = deviceData.DeviceType,
                        Status = DeviceStatus.Idle,
                        Unit = deviceData.Unit ?? ""
                    };
                    
                    Devices.Add(device);
                }

                // 加载连接
                foreach (var connectionData in recipeData.Connections)
                {
                    var connection = new ProcessConnection
                    {
                        ConnectionId = connectionData.ConnectionId,
                        SourceDevice = Devices.FirstOrDefault(d => d.DeviceId == connectionData.SourceDeviceId),
                        TargetDevice = Devices.FirstOrDefault(d => d.DeviceId == connectionData.TargetDeviceId),
                        SourcePort = connectionData.SourcePort,
                        TargetPort = connectionData.TargetPort
                    };
                    
                    Connections.Add(connection);
                }

                CurrentRecipe = recipeName;
                StatusMessage = $"配方 {recipeName} 加载完成";
                AddLogMessage($"配方加载完成，包含 {Devices.Count} 个设备，{Connections.Count} 个连接");
            }
            catch (Exception ex)
            {
                StatusMessage = $"配方加载失败: {ex.Message}";
                AddLogMessage($"配方加载错误: {ex.Message}");
                Console.WriteLine($"配方加载失败: {ex.Message}");
            }
        }

        private async Task SaveConfigurationAsync()
        {
            try
            {
                StatusMessage = "正在保存配置...";
                
                var configData = new RecipeData
                {
                    Devices = Devices.Select(d => new DeviceData
                    {
                        DeviceId = d.DeviceId,
                        DeviceName = d.DeviceName,
                        DeviceType = d.DeviceType,
                        Unit = d.Unit,
                        X = 100, // 实际应用中从UI获取位置
                        Y = 100
                    }).ToList(),
                    
                    Connections = Connections.Select(c => new ConnectionData
                    {
                        ConnectionId = c.ConnectionId,
                        SourceDeviceId = c.SourceDevice?.DeviceId,
                        TargetDeviceId = c.TargetDevice?.DeviceId,
                        SourcePort = c.SourcePort,
                        TargetPort = c.TargetPort
                    }).ToList()
                };

                await _recipeService.SaveRecipeAsync(CurrentRecipe, configData);
                StatusMessage = "配置保存成功";
                AddLogMessage("配置保存完成");
            }
            catch (Exception ex)
            {
                StatusMessage = $"保存失败: {ex.Message}";
                AddLogMessage($"保存错误: {ex.Message}");
                Console.WriteLine($"配置保存失败: {ex.Message}");
            }
        }

        private async Task ExportDataAsync()
        {
            try
            {
                StatusMessage = "正在导出数据...";
                AddLogMessage("开始导出数据");
                
                var exportData = await DatabaseService.ExportProcessDataAsync(
                    ProcessStartTime, DateTime.Now);
                
                StatusMessage = "数据导出完成";
                AddLogMessage("数据导出完成");
            }
            catch (Exception ex)
            {
                StatusMessage = $"导出失败: {ex.Message}";
                AddLogMessage($"导出错误: {ex.Message}");
                Console.WriteLine($"数据导出失败: {ex.Message}");
            }
        }

        private async Task NewProcessAsync()
        {
            if (ProcessStatus != ProcessStatus.Stopped)
            {
                StatusMessage = "请先停止当前流程";
                return;
            }

            Devices.Clear();
            Connections.Clear();
            CurrentRecipe = "新建流程";
            StatusMessage = "已创建新流程";
            AddLogMessage("创建新流程");
        }

        /// <summary>
        /// 保存流程
        /// </summary>
        private async Task SaveProcessAsync()
        {
            try
            {
                // 获取主窗口的控制器
                var mainWindow = Application.Current.MainWindow as SemiconductorMainWindow;
                var controller = mainWindow?.GetProcessController();
                
                if (controller != null)
                {
                    var result = await controller.SaveProcessToFileAsync();
                    if (result)
                    {
                        StatusMessage = "流程保存成功";
                        AddLogMessage("流程保存成功");
                    }
                    else
                    {
                        StatusMessage = "流程保存失败";
                        AddLogMessage("流程保存失败或已取消");
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"保存失败: {ex.Message}";
                AddLogMessage($"保存错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载流程
        /// </summary>
        private async Task LoadProcessAsync()
        {
            try
            {
                // 获取主窗口的控制器
                var mainWindow = Application.Current.MainWindow as SemiconductorMainWindow;
                var controller = mainWindow?.GetProcessController();
                
                if (controller != null)
                {
                    var result = await controller.LoadProcessFromFileAsync();
                    if (result)
                    {
                        StatusMessage = "流程加载成功";
                        AddLogMessage("流程加载成功");
                    }
                    else
                    {
                        StatusMessage = "流程加载失败";
                        AddLogMessage("流程加载失败或已取消");
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"加载失败: {ex.Message}";
                AddLogMessage($"加载错误: {ex.Message}");
            }
        }

        private void AddDevice(string deviceType)
        {
            if (!Enum.TryParse<DeviceType>(deviceType, out var type))
                return;

            var deviceId = $"{type}_{DateTime.Now:HHmmss}";
            var device = new SemiconductorDevice
            {
                DeviceId = deviceId,
                DeviceName = $"{GetDeviceTypeName(type)}_{Devices.Count + 1}",
                DeviceType = type,
                Status = DeviceStatus.Idle,
                Unit = GetDefaultUnit(type)
            };

            Devices.Add(device);
            SelectedDevice = device;
            AddLogMessage($"添加设备: {device.DeviceName}");
        }

        /// <summary>
        /// 在指定位置创建设备
        /// </summary>
        public SemiconductorDevice CreateDeviceAtPosition(string deviceType, double x, double y)
        {
            try
            {
                if (!Enum.TryParse<DeviceType>(deviceType, out var type))
                {
                    Console.WriteLine($"无效的设备类型: {deviceType}");
                    return null;
                }

                var deviceId = $"{deviceType.ToUpper()}{Devices.Count + 1:D2}";
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

                Devices.Add(device);
                SelectedDevice = device;
                
                // 更新状态
                OnPropertyChanged(nameof(ConnectedDeviceCount));
                AddLogMessage($"添加设备: {device.DeviceName} [{device.DeviceId}] 在位置 ({x:F0}, {y:F0})");
                
                return device;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建设备失败: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 执行设备命令
        /// </summary>
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
                AddLogMessage($"设备命令: {device.DeviceName} - {command}");
            }
            catch (Exception ex)
            {
                device.Status = DeviceStatus.Error;
                AddLogMessage($"设备命令失败: {device.DeviceName} - {command}: {ex.Message}");
            }
        }

        /// <summary>
        /// 设置设备参数
        /// </summary>
        private async void ExecuteSetParameter(SemiconductorDevice device, double value)
        {
            try
            {
                await DeviceControlService.SetParameterAsync(device.DeviceId, value, device.Unit);
                device.TargetValue = value;
                device.LastUpdateTime = DateTime.Now;
                AddLogMessage($"参数设置: {device.DeviceName} = {value} {device.Unit}");
            }
            catch (Exception ex)
            {
                AddLogMessage($"参数设置失败: {device.DeviceName}: {ex.Message}");
            }
        }

        private void RemoveDevice(SemiconductorDevice device)
        {
            if (device == null) return;

            // 移除相关连接
            var relatedConnections = Connections
                .Where(c => c.SourceDevice == device || c.TargetDevice == device)
                .ToList();
            
            foreach (var connection in relatedConnections)
            {
                Connections.Remove(connection);
            }

            Devices.Remove(device);
            if (SelectedDevice == device)
                SelectedDevice = null;
            
            AddLogMessage($"移除设备: {device.DeviceName}");
        }

        private void ZoomToFit()
        {
            ZoomLevel = 1.0;
            AddLogMessage("缩放到适合大小");
        }

        #endregion

        #region 辅助方法

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            // 更新当前时间
            CurrentTime = DateTime.Now;

            // 更新运行时间
            if (ProcessStatus == ProcessStatus.Running)
            {
                ProcessElapsedTime = DateTime.Now - ProcessStartTime;
            }

            // 更新设备状态（模拟实时数据）
            UpdateDeviceStatus();

            // 触发计算属性更新
            OnPropertyChanged(nameof(ConnectedDeviceCount));
            OnPropertyChanged(nameof(ActiveConnectionCount));
        }

        private void UpdateDeviceStatus()
        {
            var random = new Random();
            foreach (var device in Devices)
            {
                // 模拟实时数据更新
                if (device.Status == DeviceStatus.Running)
                {
                    device.CurrentValue = device.TargetValue + (random.NextDouble() - 0.5) * 5;
                    device.LastUpdateTime = DateTime.Now;
                }
            }
        }

        private void UpdateStatusMessage()
        {
            StatusMessage = ProcessStatus switch
            {
                ProcessStatus.Stopped => "系统就绪",
                ProcessStatus.Running => "流程运行中",
                ProcessStatus.Paused => "流程已暂停",
                ProcessStatus.Error => "系统错误",
                ProcessStatus.Completed => "流程完成",
                _ => "未知状态"
            };
        }

        private void AddLogMessage(string message)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var logEntry = $"[{timestamp}] {message}\n";
            SystemLogs = logEntry + SystemLogs;
            
            // 限制日志长度
            if (SystemLogs.Length > 5000)
            {
                SystemLogs = SystemLogs.Substring(0, 5000);
            }
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // 加载可用配方
                var recipes = await _recipeService.GetAvailableRecipesAsync();
                foreach (var recipe in recipes)
                {
                    AvailableRecipes.Add(recipe);
                }

                if (AvailableRecipes.Count > 0)
                {
                    await LoadRecipeAsync(AvailableRecipes[0]);
                }
                else
                {
                    // 创建示例设备
                    CreateSampleDevices();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"初始数据加载失败: {ex.Message}");
                CreateSampleDevices();
            }
        }

        private void CreateSampleDevices()
        {
            // 创建示例设备用于演示
            var devices = new[]
            {
                new SemiconductorDevice { DeviceId = "PUMP01", DeviceName = "主泵", DeviceType = DeviceType.Pump, Status = DeviceStatus.Idle, Unit = "L/min", TargetValue = 10 },
                new SemiconductorDevice { DeviceId = "VALVE01", DeviceName = "进气阀", DeviceType = DeviceType.Valve, Status = DeviceStatus.Idle, Unit = "%", TargetValue = 50 },
                new SemiconductorDevice { DeviceId = "SENSOR01", DeviceName = "温度传感器", DeviceType = DeviceType.Sensor, Status = DeviceStatus.Idle, Unit = "°C", TargetValue = 25 },
                new SemiconductorDevice { DeviceId = "HEATER01", DeviceName = "加热器", DeviceType = DeviceType.Heater, Status = DeviceStatus.Idle, Unit = "°C", TargetValue = 200 },
                new SemiconductorDevice { DeviceId = "CHAMBER01", DeviceName = "反应腔", DeviceType = DeviceType.Chamber, Status = DeviceStatus.Idle, Unit = "Torr", TargetValue = 0.1 }
            };

            foreach (var device in devices)
            {
                Devices.Add(device);
            }

            AvailableRecipes.Add("示例配方");
            CurrentRecipe = "示例配方";
            AddLogMessage("已加载示例设备配置");
        }

        private string GetDeviceTypeName(DeviceType type) => type switch
        {
            DeviceType.Pump => "泵",
            DeviceType.Valve => "阀门",
            DeviceType.Sensor => "传感器", 
            DeviceType.Heater => "加热器",
            DeviceType.Chamber => "反应腔",
            DeviceType.Controller => "控制器",
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
            _ => ""
        };

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

        #region INotifyPropertyChanged 实现

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}