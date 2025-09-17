using System.Security.Principal;

namespace Csharp_class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //验证多态性
            var giftCard = new GiftCardAccount("gift card", 100, 50);
            giftCard.MakeWithdrawal(20, DateTime.Now, "喝昂贵的咖啡");
            giftCard.MakeWithdrawal(50, DateTime.Now, "买食品杂货");
            giftCard.PerformMonthEndTransactions();
            // can make additional deposits:
            giftCard.MakeDeposit(27.50m, DateTime.Now, "增加一些额外的开支");
            Console.WriteLine(giftCard.GetAccountHistory());

            var savings = new InterestEarningAccount("savings account",0);
            savings.MakeDeposit(750, DateTime.Now, "节省一些钱");
            savings.MakeDeposit(1250, DateTime.Now, "增加更多的储蓄");
            savings.MakeWithdrawal(250, DateTime.Now, "需要支付每月的账单");
            savings.PerformMonthEndTransactions();
            Console.WriteLine(savings.GetAccountHistory());


            var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
            // How much is too much to borrow?
            lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "取出每月预支");
            lineOfCredit.MakeDeposit(50m, DateTime.Now, "小额还款");
            lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "紧急维修基金");
            lineOfCredit.MakeDeposit(150m, DateTime.Now, "部分修复");
            lineOfCredit.PerformMonthEndTransactions();
            Console.WriteLine(lineOfCredit.GetAccountHistory());

        }

        void test_1()
        {
            var account = new BankAccount("Mir zhang", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initialbalance.");
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);
            // Test that the initial balances must be positive.
            try
            {
                var invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
            }

            // Test for a negative balance.
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine(account.GetAccountHistory());
        }
    }
    
}

/*
Date            Amount  Balance Note
2025/9/10       1000    1000    Initial balance
2025/9/10       -500    500     Rent payment
2025/9/10       100     600     Friend paid me back
 
 
*/