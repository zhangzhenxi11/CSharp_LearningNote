using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadDelegateUsingExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Operation opt1 = new Operation();
            Operation opt2 = new Operation();
            Operation opt3 = new Operation();
            opt3.InnerOperation = opt2;
            opt2.InnerOperation = opt1;
            opt3.Operate(new object(), null, null);
            //问题1：如果传入的两个参数为null，失败和成功的效果是什么？答：内层的操作会调用外层的回调！
            //问题2：如果传入的两个参数不为null，会出现什么情况？答：所有默认callback都被"穿透性"屏蔽
        }
    }
    class Operation
    {
        public Action DefaultSuccessCallback { get; set; }
        public Action DefaultFailureCallback { get; set; }
        public Operation InnerOperation { get; set; }
        public object Operate(object input, Action successCallback, Action failureCallback)
        {
            if (successCallback == null)
            {
                successCallback = this.DefaultSuccessCallback;
            }
            if (failureCallback == null)
            {
                failureCallback = this.DefaultFailureCallback;
            }
            object result = null;
            try
            {
                result = this.InnerOperation.Operate(input, successCallback, failureCallback);
                //Do something here
            }
            catch
            {
                failureCallback.Invoke();
            }
            successCallback.Invoke();
            return result;

        }
    }
}