#include "../include/Transaction.h"

// 构造函数实现
Transaction::Transaction(double amount, const std::chrono::system_clock::time_point& date, const std::string& notes)
    : amount(amount), date(date), notes(notes) {
}

// 获取金额
double Transaction::getAmount() const {
    return amount;
}

// 获取日期
std::chrono::system_clock::time_point Transaction::getDate() const {
    return date;
}

// 获取备注
std::string Transaction::getNotes() const {
    return notes;
}

// 获取格式化的日期字符串（对应C#的ToShortDateString()）
std::string Transaction::getFormattedDate() const {
    auto time_t = std::chrono::system_clock::to_time_t(date);
    std::tm* tm = std::localtime(&time_t);
    
    std::ostringstream oss;
    oss << std::put_time(tm, "%Y/%m/%d");
    return oss.str();
}