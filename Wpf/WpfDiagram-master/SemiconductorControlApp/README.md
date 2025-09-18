# 半导体控制系统应用 (SemiconductorControlApp)

## 项目概述

这是一个基于WPF和WpfDiagram框架开发的半导体设备流程控制系统应用，主要用于学习和演示C# WPF的高级数据绑定机制、MVVM架构模式以及自定义控件开发。该项目展示了如何将图形化拖拽控件库应用到实际的工业控制场景中。

### 核心特性

- 🎯 **WPF高级数据绑定**: 展示PropertyChanged、ObservableCollection、命令绑定等机制
- 🏗️ **MVVM架构模式**: 完整的Model-View-ViewModel分离设计
- 🎨 **自定义控件开发**: 半导体设备节点的可视化控件
- ⚡ **异步编程模式**: Task异步模式和UI线程交互
- 🔧 **设备流程控制**: 模拟半导体设备的控制和监控
- 📊 **实时数据绑定**: 动态更新设备状态和参数
- 🎛️ **工业控制界面**: 专业的工业控制系统界面设计

## 技术栈

- **框架**: .NET 6.0 (net6.0-windows)
- **UI框架**: WPF (Windows Presentation Foundation)
- **图形库**: Aga.Diagrams (WpfDiagram)
- **架构模式**: MVVM (Model-View-ViewModel)
- **数据格式**: JSON (System.Text.Json)
- **开发工具**: Visual Studio 2022+ / Qoder IDE

## 项目结构

```
SemiconductorControlApp/
├── 📄 README.md                           # 项目说明文档
├── 📄 SemiconductorControlApp.csproj      # 项目配置文件
├── 📄 appsettings.json                    # 应用配置文件
│
├── 🚀 应用程序入口
│   ├── App.xaml                           # WPF应用程序入口XAML
│   └── App.xaml.cs                        # WPF应用程序入口代码
│
├── 🖼️ 主界面
│   ├── SemiconductorMainWindow.xaml       # 主窗口界面定义 (15.1KB)
│   ├── SemiconductorMainWindow.xaml.cs    # 主窗口逻辑代码 (5.3KB)
│   └── SemiconductorStyles.xaml           # 界面样式定义 (12.8KB)
│
├── 🧠 视图模型层
│   └── MainViewModel.cs                   # 主视图模型 (25.6KB, 700+行)
│
├── 🎮 控制器层
│   ├── SemiconductorProcessController.cs  # 流程控制器 (20.0KB)
│   └── SemiconductorNodes.cs              # 设备节点定义 (11.8KB)
│
├── 📁 Commands/ (命令层)
│   └── RelayCommand.cs                    # 命令模式实现
│
├── 📁 Converters/ (转换器层)
│   └── ValueConverters.cs                # 值转换器集合
│
├── 📁 Models/ (数据模型层)
│   └── DataTypes.cs                      # 数据类型定义
│
├── 📁 Services/ (服务层)
│   ├── Interfaces.cs                     # 服务接口定义
│   ├── DatabaseService.cs                # 数据库服务
│   ├── DeviceControlService.cs           # 设备控制服务
│   └── RecipeService.cs                  # 配方管理服务
│
├── 📁 Properties/
│   └── AssemblyInfo.cs                   # 程序集信息
│
└── 📁 生成输出 (bin/Debug/net6.0-windows/)
    ├── SemiconductorControlApp.exe        # 可执行文件 (151KB)
    ├── SemiconductorControlApp.dll        # 程序集 (113KB)
    ├── Aga.Diagrams.dll                  # 图形库依赖 (64KB)
    ├── Data/                              # 数据目录
    └── Recipes/                           # 配方目录
```

## 核心组件说明

### 1. 主视图模型 (MainViewModel.cs)
- **文件大小**: 25.6KB, 700+行代码
- **主要功能**: 
  - 设备集合管理 (ObservableCollection)
  - 流程状态控制和监控
  - 命令绑定和事件处理
  - 实时数据更新和定时器

### 2. 设备节点系统
- **SemiconductorNodes.cs**: 设备类型定义和节点控件
- **设备类型**: Pump(泵), Valve(阀门), Sensor(传感器), Heater(加热器), Chamber(反应腔), Controller(控制器)
- **节点功能**: 端口连接、状态显示、参数设置

### 3. 流程控制器 (SemiconductorProcessController.cs)
- **功能**: 流程启动/停止/暂停控制
- **数据管理**: 设备连接管理、配方加载
- **监控**: 实时状态更新、数据记录

### 4. 服务层架构
- **DatabaseService**: 数据持久化和日志记录
- **DeviceControlService**: 设备命令执行和参数设置
- **RecipeService**: 配方文件管理和序列化

### 5. WPF绑定机制展示
- **双向数据绑定**: 设备参数的实时同步
- **集合绑定**: 设备列表的动态更新
- **命令绑定**: 按钮和菜单的操作响应
- **值转换器**: 枚举到字符串、设备类型到图标的转换

## 快速开始

### 环境要求
- Windows 10/11
- .NET 6.0 Runtime
- Visual Studio 2022+ (开发)

### 构建和运行

1. **克隆项目** (如果尚未克隆父项目)
```bash
git clone <repository-url>
cd WpfDiagram-master/SemiconductorControlApp
```

2. **编译项目**
```bash
dotnet build
```

3. **运行应用**
```bash
dotnet run
```

或者直接运行生成的可执行文件：
```bash
.\bin\Debug\net6.0-windows\SemiconductorControlApp.exe
```

### 使用说明

1. **设备管理**
   - 从左侧设备库拖拽设备到画布
   - 选择设备查看/修改属性
   - 连接设备端口创建流程

2. **流程控制**
   - 点击"开始流程"启动设备
   - 实时监控设备状态和数据
   - 使用"暂停"/"停止"控制流程

3. **配方管理**
   - 加载预设配方
   - 保存当前配置
   - 导出历史数据

## 学习重点

### WPF数据绑定机制
本项目是学习WPF高级数据绑定的绝佳示例：

1. **属性更改通知** (`INotifyPropertyChanged`)
```csharp
public ProcessStatus ProcessStatus
{
    get => _processStatus;
    set
    {
        if (_processStatus != value)
        {
            _processStatus = value;
            OnPropertyChanged(); // 通知UI更新
        }
    }
}
```

2. **ObservableCollection集合绑定**
```csharp
public ObservableCollection<SemiconductorDevice> Devices { get; }
```

3. **命令绑定** (`ICommand`)
```csharp
public ICommand StartProcessCommand { get; private set; }
```

4. **值转换器应用**
```csharp
// 设备类型到图标的转换
public class DeviceTypeToIconConverter : IValueConverter
```

### MVVM架构模式
- **Model**: DataTypes.cs 中的数据模型
- **View**: XAML界面文件
- **ViewModel**: MainViewModel.cs 业务逻辑

## 开发注意事项

### 编译问题解决经验
在开发过程中可能遇到的常见问题：

1. **命名空间不一致**: 确保所有文件使用 `SemiconductorControlApp` 命名空间
2. **依赖问题**: 避免使用外部NuGet包，优先使用.NET内置功能
3. **XAML绑定错误**: 检查绑定路径和数据上下文设置

### 性能优化建议
1. **大量设备时**: 考虑虚拟化和异步加载
2. **实时更新**: 控制定时器频率，避免UI冻结
3. **内存管理**: 及时清理事件订阅和定时器

## 扩展开发

### 添加新设备类型
1. 在 `DeviceType` 枚举中添加新类型
2. 在 `SemiconductorNodes.cs` 中定义端口配置
3. 在 `SemiconductorStyles.xaml` 中添加样式
4. 在值转换器中添加图标映射

### 集成真实设备
1. 实现 `IDeviceControlService` 接口
2. 替换模拟的 `DeviceControlService`
3. 添加设备驱动程序接口
4. 实现异常处理和设备状态监控

## 相关项目

- **父项目**: [WpfDiagram-master](../README.md) - WPF图形拖拽控件库
- **测试项目**: [TestApp](../TestApp/) - 基础图形控件演示

## 许可证

本项目基于WpfDiagram框架开发，仅用于学习和研究目的。

## 更新日志

### v1.0.0 (2025-01-18)
- ✅ 初始版本发布
- ✅ 完整的半导体设备控制界面
- ✅ WPF高级数据绑定演示
- ✅ MVVM架构实现
- ✅ 编译问题修复和优化

---

**开发目标**: 通过实际项目深入学习C#语法和WPF的Binding机制，为后续学习Prism框架打下基础。