using Prism.Mvvm;

namespace WpfApp_bing.Models
{
    /// <summary>
    /// 使用Prism框架优化后的Student模型
    /// 对比原始版本，代码更简洁，维护性更好
    /// </summary>
    public class StudentPrism : BindableBase
    {
        private string _name = string.Empty;
        
        /// <summary>
        /// 学生姓名 - Prism版本
        /// 使用SetProperty方法，自动处理属性变更通知
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _age;
        
        /// <summary>
        /// 学生年龄
        /// </summary>
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _email = string.Empty;
        
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
    }
}

/*
Prism框架优势对比分析：

1. 原始WPF方式（Student.cs）：
   - 需要手动实现INotifyPropertyChanged
   - 每个属性都要写大量样板代码
   - 容易出错（忘记触发事件或属性名写错）
   
2. Prism框架方式（StudentPrism.cs）：
   - 继承BindableBase，自动获得属性通知功能
   - 使用SetProperty方法，一行代码完成属性设置和通知
   - 编译时检查，避免属性名错误
   - 代码更简洁，可读性更好
*/