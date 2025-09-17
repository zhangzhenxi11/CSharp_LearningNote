using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*信用帐户：
1.余额可以为负，但不能大于信用限额的绝对值。
2.如果月末余额不为 0，每个月都会产生利息。
3.将在超过信用限额的每次提款后收取费用。

*/
namespace Csharp_class
{
    internal class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name,initialBalance, -creditLimit)
        {
        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                // Negate the balance to get a positive interest charge:
                var interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "按月收取利息");
            }
        }
        //当帐户透支时，该重写将返回费用交易。 如果取款未超出限额，则该方法将返回 null 交易。
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn? new Transaction(-20, DateTime.Now, "申请透支费") : default;

    }
}
