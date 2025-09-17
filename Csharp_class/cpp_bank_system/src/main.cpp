#include "../include/BankAccount.h"
#include "../include/InterestEarningAccount.h"
#include "../include/GiftCardAccount.h"
#include "../include/LineOfCreditAccount.h"
#include <iostream>
#include <memory>
#include <chrono>
#include <locale>

// Windows下需要的头文件和函数
#ifdef _WIN32
#include <windows.h>
#include <io.h>
#include <fcntl.h>
#endif

// C++版本的主程序（对应C#的Program.cs）
class Program {
public:
    static void main() {
        std::cout << "=== C++版本银行账户管理系统演示 ===" << std::endl << std::endl;
        
        // 验证多态性 - 礼品卡账户测试
        std::cout << "1. 礼品卡账户测试:" << std::endl;
        auto giftCard = std::make_unique<GiftCardAccount>("gift card", 100, 50);
        giftCard->makeWithdrawal(20, std::chrono::system_clock::now(), "喝昂贵的咖啡");
        giftCard->makeWithdrawal(50, std::chrono::system_clock::now(), "买食品杂货");
        giftCard->performMonthEndTransactions();
        // 可以进行额外存款
        giftCard->makeDeposit(27.50, std::chrono::system_clock::now(), "增加一些额外的开支");
        std::cout << giftCard->getAccountHistory() << std::endl;
        
        // 储蓄账户（红利账户）测试
        std::cout << "2. 储蓄账户（红利账户）测试:" << std::endl;
        auto savings = std::make_unique<InterestEarningAccount>("savings account", 0);
        savings->makeDeposit(750, std::chrono::system_clock::now(), "节省一些钱");
        savings->makeDeposit(1250, std::chrono::system_clock::now(), "增加更多的储蓄");
        savings->makeWithdrawal(250, std::chrono::system_clock::now(), "需要支付每月的账单");
        savings->performMonthEndTransactions();
        std::cout << savings->getAccountHistory() << std::endl;
        
        // 信用账户测试
        std::cout << "3. 信用账户测试:" << std::endl;
        auto lineOfCredit = std::make_unique<LineOfCreditAccount>("line of credit", 0, 2000);
        // 测试借款是否过多
        lineOfCredit->makeWithdrawal(1000, std::chrono::system_clock::now(), "取出每月预支");
        lineOfCredit->makeDeposit(50, std::chrono::system_clock::now(), "小额还款");
        lineOfCredit->makeWithdrawal(5000, std::chrono::system_clock::now(), "紧急维修基金");
        lineOfCredit->makeDeposit(150, std::chrono::system_clock::now(), "部分修复");
        lineOfCredit->performMonthEndTransactions();
        std::cout << lineOfCredit->getAccountHistory() << std::endl;
        
        // 基本功能测试
        std::cout << "4. 基本银行账户测试:" << std::endl;
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
    
    // 设置控制台模式以支持ANSI转义序列
    HANDLE hOut = GetStdHandle(STD_OUTPUT_HANDLE);
    DWORD dwMode = 0;
    GetConsoleMode(hOut, &dwMode);
    dwMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
    SetConsoleMode(hOut, dwMode);
    
    // 设置标准输出为二进制模式以支持UTF-8
    _setmode(_fileno(stdout), _O_U8TEXT);
    _setmode(_fileno(stderr), _O_U8TEXT);
#endif
    
    // 设置本地化
    std::locale::global(std::locale(""));
    std::cout.imbue(std::locale());
    
    try {
        Program::main();
    } catch (const std::exception& e) {
        std::cerr << "程序异常: " << e.what() << std::endl;
        return 1;
    }
    
    return 0;
}