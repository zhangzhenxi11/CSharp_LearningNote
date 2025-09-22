using Prism.Mvvm;
using System.ComponentModel;

namespace WpfApp_bing.ViewModels
{
    /// <summary>
    /// Prism框架基础ViewModel，简化属性通知机制
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {
        // Prism的BindableBase已经实现了INotifyPropertyChanged
        // 提供了SetProperty方法，大大简化了属性设置
    }
}