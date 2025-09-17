#ifndef GIFTCARDACCOUNT_H
#define GIFTCARDACCOUNT_H

#include "BankAccount.h"

// 礼品卡账户类（对应C#的GiftCardAccount）
// 每月最后一天，可以充值一次指定的金额
class GiftCardAccount : public BankAccount {
private:
    double monthlyDeposit;

public:
    // 构造函数，monthlyDeposit有默认值0（对应C#的默认参数）
    GiftCardAccount(const std::string& name, double initialBalance, double monthlyDeposit = 0.0);
    
    // 重写虚方法以添加每月存款
    void performMonthEndTransactions() override;
};

#endif // GIFTCARDACCOUNT_H