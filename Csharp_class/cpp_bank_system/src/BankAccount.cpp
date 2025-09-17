#include "../include/BankAccount.h"

// 静态成员初始化
int BankAccount::accountNumberSeed = 1234567890;

// 委托构造函数
BankAccount::BankAccount(const std::string& name, double initialBalance) 
    : BankAccount(name, initialBalance, 0) {
}

// 主构造函数
BankAccount::BankAccount(const std::string& name, double initialBalance, double minimumBalance)
    : minimumBalance(minimumBalance) {
    this->number = std::to_string(accountNumberSeed);
    accountNumberSeed++;
    
    this->owner = name;
    
    if (initialBalance > 0) {
        makeDeposit(initialBalance, std::chrono::system_clock::now(), "Initial balance");
    }
}

// 获取器方法实现
std::string BankAccount::getNumber() const {
    return number;
}

std::string BankAccount::getOwner() const {
    return owner;
}

void BankAccount::setOwner(const std::string& owner) {
    this->owner = owner;
}

double BankAccount::getBalance() const {
    double balance = 0;
    for (const auto& transaction : allTransactions) {
        balance += transaction->getAmount();
    }
    return balance;
}

// 虚方法的默认实现
void BankAccount::performMonthEndTransactions() {
    // 基类的默认实现为空，派生类可以重写
}

// 存款方法
void BankAccount::makeDeposit(double amount, const std::chrono::system_clock::time_point& date, const std::string& note) {
    if (amount <= 0) {
        throw std::invalid_argument("存款金额必须为正");
    }
    auto deposit = std::make_unique<Transaction>(amount, date, note);
    allTransactions.push_back(std::move(deposit));
}

// 取款方法
void BankAccount::makeWithdrawal(double amount, const std::chrono::system_clock::time_point& date, const std::string& note) {
    if (amount <= 0) {
        throw std::invalid_argument("取款金额必须为正");
    }
    
    // 使用模板方法模式，调用可被重写的方法
    auto overdraftTransaction = checkWithdrawalLimit(getBalance() - amount < minimumBalance);
    auto withdrawal = std::make_unique<Transaction>(-amount, date, note);
    allTransactions.push_back(std::move(withdrawal));
    
    if (overdraftTransaction != nullptr) {
        allTransactions.push_back(std::move(overdraftTransaction));
    }
}

// 受保护的虚方法实现（体现模板方法模式）
// 这个方法遵循了虚方法与多态性设计规范和访问修饰符使用规范
std::unique_ptr<Transaction> BankAccount::checkWithdrawalLimit(bool isOverdrawn) {
    if (isOverdrawn) {
        throw std::runtime_error("没有足够的资金来取款");
    } else {
        return nullptr;  // 对应C#的default
    }
}

// 获取账户历史记录
std::string BankAccount::getAccountHistory() const {
    std::ostringstream report;
    double balance = 0;
    report << "Date\t\tAmount\tBalance\tNote\n";
    
    for (const auto& transaction : allTransactions) {
        balance += transaction->getAmount();
        report << transaction->getFormattedDate() << "\t"
               << std::fixed << std::setprecision(2) << transaction->getAmount() << "\t"
               << std::fixed << std::setprecision(2) << balance << "\t"
               << transaction->getNotes() << "\n";
    }
    
    return report.str();
}