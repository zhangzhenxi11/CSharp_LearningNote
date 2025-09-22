using System.Windows;

namespace WpfApp_bing.Views
{
    /// <summary>
    /// MainWindowPrism.xaml 的交互逻辑
    /// 注意：在Prism MVVM模式中，CodeBehind应该尽可能保持简洁
    /// 所有业务逻辑都应该在ViewModel中实现
    /// </summary>
    public partial class MainWindowPrism : Window
    {
        public MainWindowPrism()
        {
            InitializeComponent();
            // 在Prism模式中，DataContext通常在XAML中设置
            // 或者通过依赖注入容器自动注入
        }
    }
}

/*
CodeBehind对比分析：

1. 传统WPF方式（MainWindow.xaml.cs）：
   - 包含大量业务逻辑代码
   - 直接操作UI控件
   - 手动创建和管理Binding对象
   - 事件处理代码与UI紧耦合

2. Prism MVVM方式（MainWindowPrism.xaml.cs）：
   - CodeBehind几乎为空，只保留必要的初始化代码
   - 所有业务逻辑都在ViewModel中
   - UI和业务逻辑完全分离
   - 易于测试和维护
*/