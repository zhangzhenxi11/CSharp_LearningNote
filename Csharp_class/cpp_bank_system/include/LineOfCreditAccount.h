#ifndef LINEOFCREDITACCOUNT_H
#define LINEOFCREDITACCOUNT_H

#include "BankAccount.h"

// 信用账户类（对应C#的LineOfCreditAccount）
// 信用账户：
// 1.余额可以为负，但不能大于信用限额的绝对值
// 2.如果月末余额不为0，每个月都会产生利息
// 3.将在超过信用限额的每次提款后收取费用
class LineOfCreditAccount : public BankAccount {
public:
    // 构造函数
    LineOfCreditAccount(const std::string& name, double initialBalance, double creditLimit);
    
    // 重写月末交易处理方法
    void performMonthEndTransactions() override;

protected:
    // 重写透支检查方法（体现模板方法模式的核心）
    // 当账户透支时，返回费用交易；如果取款未超出限额，则返回nullptr
    std::unique_ptr<Transaction> checkWithdrawalLimit(bool isOverdrawn) override;
};

#endif // LINEOFCREDITACCOUNT_H