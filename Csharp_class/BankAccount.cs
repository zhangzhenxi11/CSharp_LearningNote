using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
1. 用一个 10 位数唯一标识银行帐户。
2. 用字符串存储一个或多个所有者名称。
3. 可以检索余额。
4. 接受存款。
5. 接受取款。
6. 初始余额必须是正数。
7. 取款后的余额不能是负数。
*/

namespace Csharp_class
{
    public class BankAccount
    {
        private static int accountNumberSeed = 1234567890;//数据成员 ：银行账户

        private List<Transaction> allTransactions = new List<Transaction>();

        private readonly decimal minimumBalance; //规定最小余额的只读字段

        //新构造函数会执行现有构造函数完成的所有操作
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

        //构造函数
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            this.minimumBalance = minimumBalance;

            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        //属性
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }
        public virtual void PerformMonthEndTransactions() { }

        //方法
        //：初始余额必须为正数，且取款后的余额不能是负数。
        //存款
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "存款金额必须为正");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }
        //取款
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "存款金额必须为正");
            }
            //if (Balance - amount < minimumBalance) //将硬编码的 0 更改为 minimumBalance
            //{
            //    throw new InvalidOperationException("没有足够的资金来取款");
            //}
            //var withdrawal = new Transaction(-amount, date, note);
            //allTransactions.Add(withdrawal);
            var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);

            if (overdraftTransaction != null)
                allTransactions.Add(overdraftTransaction);
        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("没有足够的资金来取款");
            }
            else
            {
                return default;
            }
        }


        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            return report.ToString();
        }
    }
}
