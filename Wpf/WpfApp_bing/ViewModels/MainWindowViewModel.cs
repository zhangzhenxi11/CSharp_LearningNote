using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using WpfApp_bing.Models;

namespace WpfApp_bing.ViewModels
{
    /// <summary>
    /// 主窗口ViewModel - 使用Prism MVVM模式
    /// 展示了相比传统WPF方式的优势
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        #region 私有字段
        private StudentPrism _currentStudent;
        private string _inputText = string.Empty;
        #endregion

        #region 公共属性
        
        /// <summary>
        /// 当前学生对象
        /// </summary>
        public StudentPrism CurrentStudent
        {
            get => _currentStudent;
            set => SetProperty(ref _currentStudent, value);
        }

        /// <summary>
        /// 输入文本 - 演示双向绑定
        /// </summary>
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        /// <summary>
        /// 显示文本 - 根据学生信息动态生成
        /// </summary>
        public string DisplayText => $"学生信息：{CurrentStudent?.Name} - 年龄：{CurrentStudent?.Age}";

        #endregion

        #region 命令属性

        /// <summary>
        /// 添加年龄命令 - 替代传统的事件处理
        /// </summary>
        public ICommand AddAgeCommand { get; }

        /// <summary>
        /// 更新姓名命令
        /// </summary>
        public ICommand UpdateNameCommand { get; }

        /// <summary>
        /// 重置数据命令
        /// </summary>
        public ICommand ResetDataCommand { get; }

        #endregion

        #region 构造函数

        public MainWindowViewModel()
        {
            // 初始化数据
            CurrentStudent = new StudentPrism 
            { 
                Name = "张三", 
                Age = 20,
                Email = "zhangsan@example.com"
            };

            // 初始化命令 - Prism的DelegateCommand提供了强大的命令支持
            AddAgeCommand = new DelegateCommand(ExecuteAddAge, CanExecuteAddAge);
            UpdateNameCommand = new DelegateCommand(ExecuteUpdateName);
            ResetDataCommand = new DelegateCommand(ExecuteResetData);

            // 监听学生属性变化，自动更新显示文本
            CurrentStudent.PropertyChanged += (s, e) => RaisePropertyChanged(nameof(DisplayText));
        }

        #endregion

        #region 命令实现

        /// <summary>
        /// 执行添加年龄操作
        /// </summary>
        private void ExecuteAddAge()
        {
            CurrentStudent.Age++;
            // 不需要手动更新UI，Binding会自动处理
        }

        /// <summary>
        /// 判断是否可以执行添加年龄操作
        /// </summary>
        private bool CanExecuteAddAge()
        {
            return CurrentStudent?.Age < 100; // 年龄上限100
        }

        /// <summary>
        /// 执行更新姓名操作
        /// </summary>
        private void ExecuteUpdateName()
        {
            if (!string.IsNullOrWhiteSpace(InputText))
            {
                CurrentStudent.Name = InputText;
                InputText = string.Empty; // 清空输入框
            }
        }

        /// <summary>
        /// 执行重置数据操作
        /// </summary>
        private void ExecuteResetData()
        {
            CurrentStudent.Name = "新学生";
            CurrentStudent.Age = 18;
            CurrentStudent.Email = "newstudent@example.com";
            InputText = string.Empty;
        }

        #endregion
    }
}

/*
Prism MVVM模式优势对比：

1. 传统WPF方式（MainWindow.xaml.cs）：
   - 业务逻辑和UI代码混合在CodeBehind中
   - 事件处理方式，代码耦合度高
   - 难以进行单元测试
   - 代码复用性差

2. Prism MVVM方式（MainWindowViewModel.cs）：
   - 完全分离视图和业务逻辑
   - 使用Command模式替代事件处理
   - 支持单元测试
   - 代码结构清晰，易于维护
   - 属性变更自动通知UI更新
   - 提供了CanExecute功能，可以动态控制命令是否可用
*/