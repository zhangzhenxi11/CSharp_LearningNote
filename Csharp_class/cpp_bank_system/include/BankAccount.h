#ifndef BANKACCOUNT_H
#define BANKACCOUNT_H

#include "Transaction.h"
#include <vector>
#include <memory>
#include <string>
#include <chrono>
#include <stdexcept>
#include <sstream>
#include <iomanip>

// C++版本的BankAccount基类
// 对应C#项目中的BankAccount类
class BankAccount {
private:
    static int accountNumberSeed;
    std::vector<std::unique_ptr<Transaction>> allTransactions;
    double minimumBalance;  // 规定最小余额的字段

public:
    std::string number;
    std::string owner;

    // 构造函数
    BankAccount(const std::string& name, double initialBalance);
    BankAccount(const std::string& name, double initialBalance, double minimumBalance);
    
    // 虚析构函数（对于基类很重要）
    virtual ~BankAccount() = default;

    // 获取器方法
    std::string getNumber() const;
    std::string getOwner() const;
    void setOwner(const std::string& owner);
    double getBalance() const;
    
    // 虚方法，允许派生类重写（对应C#的virtual方法）
    virtual void performMonthEndTransactions();
    
    // 公共方法
    void makeDeposit(double amount, const std::chrono::system_clock::time_point& date, const std::string& note);
    void makeWithdrawal(double amount, const std::chrono::system_clock::time_point& date, const std::string& note);
    std::string getAccountHistory() const;

protected:
    // 受保护的虚方法，允许派生类重写（对应C#的protected virtual方法）
    // 这里体现了模板方法模式和多态性设计规范
    virtual std::unique_ptr<Transaction> checkWithdrawalLimit(bool isOverdrawn);
};

#endif // BANKACCOUNT_H