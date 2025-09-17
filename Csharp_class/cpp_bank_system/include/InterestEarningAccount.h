#ifndef INTERESTEARNINGACCOUNT_H
#define INTERESTEARNINGACCOUNT_H

#include "BankAccount.h"

// 红利账户类（对应C#的InterestEarningAccount）
// 将获得月末余额2%的额度
class InterestEarningAccount : public BankAccount {
public:
    // 构造函数
    InterestEarningAccount(const std::string& name, double initialBalance);
    
    // 重写虚方法（对应C#的override方法）
    void performMonthEndTransactions() override;
};

#endif // INTERESTEARNINGACCOUNT_H