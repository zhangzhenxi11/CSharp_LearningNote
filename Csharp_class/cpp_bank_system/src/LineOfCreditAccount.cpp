#include "../include/LineOfCreditAccount.h"

// 构造函数实现
LineOfCreditAccount::LineOfCreditAccount(const std::string& name, double initialBalance, double creditLimit)
    : BankAccount(name, initialBalance, -creditLimit) {  // 将信用限额设为负的最小余额
}

// 重写月末交易处理方法
void LineOfCreditAccount::performMonthEndTransactions() {
    if (getBalance() < 0) {
        // 将余额取负值以获得正的利息费用
        double interest = -getBalance() * 0.07;  // 7%利息
        makeWithdrawal(interest, std::chrono::system_clock::now(), "按月收取利息");
    }
}

// 重写透支检查方法（体现模板方法模式和多态性）
// 这里完全重写了基类的行为：不抛出异常，而是收取费用
std::unique_ptr<Transaction> LineOfCreditAccount::checkWithdrawalLimit(bool isOverdrawn) {
    if (isOverdrawn) {
        // 返回透支费交易，而不是抛出异常
        return std::make_unique<Transaction>(-20.0, std::chrono::system_clock::now(), "申请透支费");
    } else {
        return nullptr;  // 对应C#的default
    }
}