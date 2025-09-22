using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_bing
{
    public class Student:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name 
        {
            get { return name; }
            set {
                name = value;

                //激发事件,来通知Binging，Name变化了！Binging才能把变化传递给UI.  
                
                if (this.PropertyChanged != null)
                { 
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }    
        } 
    }
}
/*
mfc：这个机制很麻烦，后面prism框架简化后，会好很多。

对比Qt,Qt内部内置了事件触发机制，逻辑对UI的改变，直接通过信号槽，
直接通过控件接口实现值变化。
 
*/
