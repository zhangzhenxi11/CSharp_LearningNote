using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*红利帐户：
 * 将获得月末余额 2% 的额度
 
 */
namespace Csharp_class
{
    public class InterestEarningAccount:BankAccount
    {

        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {

        }
        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                var interest = Balance * 0.05m;
                MakeDeposit(interest, DateTime.Now, "按月申请利息");
            }
        }
    }
}
