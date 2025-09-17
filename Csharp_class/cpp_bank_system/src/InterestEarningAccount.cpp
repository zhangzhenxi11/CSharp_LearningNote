#include "../include/InterestEarningAccount.h"

// 构造函数实现
InterestEarningAccount::InterestEarningAccount(const std::string& name, double initialBalance)
    : BankAccount(name, initialBalance) {
}

// 重写月末交易处理方法
void InterestEarningAccount::performMonthEndTransactions() {
    if (getBalance() > 500.0) {
        double interest = getBalance() * 0.05;  // 5%利息（对应C#代码中的0.05m）
        makeDeposit(interest, std::chrono::system_clock::now(), "按月申请利息");
    }
}