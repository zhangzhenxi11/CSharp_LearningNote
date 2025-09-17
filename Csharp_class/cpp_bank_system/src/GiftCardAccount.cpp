#include "../include/GiftCardAccount.h"

// 构造函数实现
GiftCardAccount::GiftCardAccount(const std::string& name, double initialBalance, double monthlyDeposit)
    : BankAccount(name, initialBalance), monthlyDeposit(monthlyDeposit) {
}

// 重写月末交易处理方法以添加每月存款
void GiftCardAccount::performMonthEndTransactions() {
    if (monthlyDeposit != 0.0) {
        makeDeposit(monthlyDeposit, std::chrono::system_clock::now(), "增加每月存款");
    }
}