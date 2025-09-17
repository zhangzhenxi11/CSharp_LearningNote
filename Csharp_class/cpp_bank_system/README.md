# C++ 银行账户管理系统

这是从C#项目重写的C++版本银行账户管理系统。

## 项目结构

```
cpp_bank_system/
├── include/                 # 头文件目录
│   ├── Transaction.h        # 交易类
│   ├── BankAccount.h        # 银行账户基类
│   ├── InterestEarningAccount.h   # 红利账户
│   ├── GiftCardAccount.h    # 礼品卡账户
│   └── LineOfCreditAccount.h      # 信用账户
├── src/                     # 源文件目录
│   ├── Transaction.cpp
│   ├── BankAccount.cpp
│   ├── InterestEarningAccount.cpp
│   ├── GiftCardAccount.cpp
│   ├── LineOfCreditAccount.cpp
│   ├── main.cpp            # 主程序（包含中文）
│   └── main_simple.cpp     # 简化版主程序（纯英文）
├── CMakeLists.txt          # CMake构建文件
└── README.md               # 本文件
```

## 在Visual Studio中解决中文乱码问题

### 方法1：使用CMake构建（推荐）

1. 在Visual Studio中打开文件夹：`文件` → `打开` → `文件夹`，选择`cpp_bank_system`目录
2. CMakeLists.txt已经包含了正确的UTF-8编码设置
3. 直接构建和运行项目

### 方法2：手动设置项目属性

如果创建新的Visual Studio项目，需要设置以下属性：

1. **项目属性设置**：
   - 右键项目 → 属性
   - C/C++ → 命令行 → 添加选项：`/utf-8 /execution-charset:utf-8 /source-charset:utf-8`

2. **确保源文件保存为UTF-8编码**：
   - 文件 → 高级保存选项 → 编码：`Unicode (UTF-8 带签名) - 代码页 65001`

3. **设置控制台代码页**：
   - 项目已在代码中自动设置控制台UTF-8编码

### 方法3：使用简化版本

如果仍有编码问题，可以使用 `main_simple.cpp`，它使用纯英文输出：

```cpp
// 在CMakeLists.txt中修改主文件
// 将 src/main.cpp 改为 src/main_simple.cpp
```

## 构建和运行

### 使用CMake构建

```bash
mkdir build
cd build
cmake ..
cmake --build .
```

### 运行程序

```bash
# Windows
.\bin\bank_system.exe

# 或者在Visual Studio中直接运行
```

## 核心设计模式

1. **继承和多态性**：BankAccount基类，多个派生类
2. **模板方法模式**：`checkWithdrawalLimit`方法的重写机制
3. **访问控制**：protected成员确保封装性

## 类功能说明

- **BankAccount**：基础银行账户，不允许透支
- **InterestEarningAccount**：红利账户，余额>500时月末获得5%利息
- **GiftCardAccount**：礼品卡账户，支持每月自动充值
- **LineOfCreditAccount**：信用账户，允许透支但收取费用

## 编译要求

- C++17 或更高版本
- CMake 3.16 或更高版本
- 支持UTF-8的编译器（Visual Studio 2019+ / GCC 7+ / Clang 5+）