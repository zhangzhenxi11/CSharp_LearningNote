#include "../include/BankAccount.h"
#include "../include/InterestEarningAccount.h"
#include "../include/GiftCardAccount.h"
#include "../include/LineOfCreditAccount.h"
#include <iostream>
#include <memory>
#include <chrono>

// Windows下需要的头文件和函数
#ifdef _WIN32
#include <windows.h>
#endif

// C++版本的主程序（对应C#的Program.cs）
class Program {
public:
    static void main() {
        std::cout << "=== C++ Bank Account Management System ===" << std::endl << std::endl;
        
        // 验证多态性 - 礼品卡账户测试
        std::cout << "1. Gift Card Account Test:" << std::endl;
        auto giftCard = std::make_unique<GiftCardAccount>("gift card", 100, 50);
        giftCard->makeWithdrawal(20, std::chrono::system_clock::now(), "Expensive coffee");
        giftCard->makeWithdrawal(50, std::chrono::system_clock::now(), "Buy groceries");
        giftCard->performMonthEndTransactions();
        // 可以进行额外存款
        giftCard->makeDeposit(27.50, std::chrono::system_clock::now(), "Add some extra spending");
        std::cout << giftCard->getAccountHistory() << std::endl;
        
        // 储蓄账户（红利账户）测试
        std::cout << "2. Interest Earning Account Test:" << std::endl;
        auto savings = std::make_unique<InterestEarningAccount>("savings account", 0);
        savings->makeDeposit(750, std::chrono::system_clock::now(), "Save some money");
        savings->makeDeposit(1250, std::chrono::system_clock::now(), "Add more savings");
        savings->makeWithdrawal(250, std::chrono::system_clock::now(), "Need to pay monthly bills");
        savings->performMonthEndTransactions();
        std::cout << savings->getAccountHistory() << std::endl;
        
        // 信用账户测试
        std::cout << "3. Line of Credit Account Test:" << std::endl;
        auto lineOfCredit = std::make_unique<LineOfCreditAccount>("line of credit", 0, 2000);
        // 测试借款是否过多
        lineOfCredit->makeWithdrawal(1000, std::chrono::system_clock::now(), "Monthly advance withdrawal");
        lineOfCredit->makeDeposit(50, std::chrono::system_clock::now(), "Small repayment");
        lineOfCredit->makeWithdrawal(5000, std::chrono::system_clock::now(), "Emergency repair fund");
        lineOfCredit->makeDeposit(150, std::chrono::system_clock::now(), "Partial repair");
        lineOfCredit->performMonthEndTransactions();
        std::cout << lineOfCredit->getAccountHistory() << std::endl;
        
        // 基本功能测试
        std::cout << "4. Basic Bank Account Test:" << std::endl;
        test_basic_functionality();
    }
    
private:
    // 基本功能测试（对应C#的test_1方法）
    static void test_basic_functionality() {
        auto account = std::make_unique<BankAccount>("Mir zhang", 1000);
        std::cout << "Account " << account->getNumber() 
                  << " was created for " << account->getOwner() 
                  << " with " << account->getBalance() 
                  << " initial balance." << std::endl;
                  
        account->makeWithdrawal(500, std::chrono::system_clock::now(), "Rent payment");
        std::cout << "Balance after withdrawal: " << account->getBalance() << std::endl;
        
        account->makeDeposit(100, std::chrono::system_clock::now(), "Friend paid me back");
        std::cout << "Balance after deposit: " << account->getBalance() << std::endl;
        
        // 测试初始余额必须为正数
        try {
            auto invalidAccount = std::make_unique<BankAccount>("invalid", -55);
        } catch (const std::exception& e) {
            std::cout << "Exception caught creating account with negative balance" << std::endl;
            std::cout << e.what() << std::endl;
        }
        
        // 测试负余额
        try {
            account->makeWithdrawal(750, std::chrono::system_clock::now(), "Attempt to overdraw");
        } catch (const std::exception& e) {
            std::cout << "Exception caught trying to overdraw" << std::endl;
            std::cout << e.what() << std::endl;
        }
        
        std::cout << account->getAccountHistory() << std::endl;
    }
};

// 程序入口点
int main() {
    // 设置Windows控制台编码为UTF-8
#ifdef _WIN32
    // 设置控制台代码页为UTF-8
    SetConsoleOutputCP(CP_UTF8);
    SetConsoleCP(CP_UTF8);
#endif
    
    try {
        Program::main();
    } catch (const std::exception& e) {
        std::cerr << "Program exception: " << e.what() << std::endl;
        return 1;
    }
    
    // 等待用户按键（可选）
    std::cout << "Press Enter to exit..." << std::endl;
    std::cin.get();
    
    return 0;
}