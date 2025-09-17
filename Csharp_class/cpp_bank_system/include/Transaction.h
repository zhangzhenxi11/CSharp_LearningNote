#ifndef TRANSACTION_H
#define TRANSACTION_H

#include <string>
#include <chrono>
#include <iomanip>
#include <sstream>

// C++版本的Transaction类
// 对应C#项目中的Transaction类
class Transaction {
private:
    double amount;
    std::chrono::system_clock::time_point date;
    std::string notes;

public:
    // 构造函数
    Transaction(double amount, const std::chrono::system_clock::time_point& date, const std::string& notes);
    
    // 获取器方法（对应C#的属性）
    double getAmount() const;
    std::chrono::system_clock::time_point getDate() const;
    std::string getNotes() const;
    
    // 获取格式化的日期字符串
    std::string getFormattedDate() const;
};

#endif // TRANSACTION_H