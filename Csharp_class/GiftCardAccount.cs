using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
礼品卡帐户：
每月最后一天，可以充值一次指定的金额。
 */
namespace Csharp_class
{
    public class GiftCardAccount : BankAccount
    {
        private decimal _monthlyDeposit = 0m;
        //构造函数为 monthlyDeposit 值提供默认值，因此调用方可以省略 0 以表示不进行每月存款。
        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

        //重写该方法以添加每月存款
        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "增加每月存款");
            }
        }

    }
}
