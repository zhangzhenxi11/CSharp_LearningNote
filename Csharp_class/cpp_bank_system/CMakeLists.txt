cmake_minimum_required(VERSION 3.16)

# 项目名称和版本
project(CppBankSystem VERSION 1.0.0 LANGUAGES CXX)

# 设置C++标准
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# 包含头文件目录
include_directories(include)

# 收集源文件
file(GLOB_RECURSE SOURCES 
    "src/*.cpp"
)

# 收集头文件
file(GLOB_RECURSE HEADERS 
    "include/*.h"
)

# 创建可执行文件
add_executable(bank_system ${SOURCES} ${HEADERS})

# 设置编译器特定的选项
if(MSVC)
    # Visual Studio编译器选项
    target_compile_options(bank_system PRIVATE /W4)
    # 设置源文件编码为UTF-8
    target_compile_options(bank_system PRIVATE /utf-8)
    # 设置执行字符集为UTF-8
    target_compile_options(bank_system PRIVATE /execution-charset:utf-8)
    # 设置源字符集为UTF-8
    target_compile_options(bank_system PRIVATE /source-charset:utf-8)
    # 添加预处理器定义以支持中文控制台输出
    target_compile_definitions(bank_system PRIVATE _CRT_SECURE_NO_WARNINGS)
    target_compile_definitions(bank_system PRIVATE UNICODE _UNICODE)
elseif(CMAKE_CXX_COMPILER_ID STREQUAL "GNU" OR CMAKE_CXX_COMPILER_ID STREQUAL "Clang")
    # GCC或Clang编译器选项
    target_compile_options(bank_system PRIVATE -Wall -Wextra -O2)
    # 设置输入输出编码为UTF-8
    target_compile_options(bank_system PRIVATE -finput-charset=UTF-8 -fexec-charset=UTF-8)
endif()

# Windows下设置控制台代码页
if(WIN32)
    # 在Windows下添加运行时设置控制台代码页的代码
    target_compile_definitions(bank_system PRIVATE WIN32_LEAN_AND_MEAN)
endif()

# 设置输出目录
set_target_properties(bank_system PROPERTIES
    RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin
)

# 打印构建信息
message(STATUS "项目名称: ${PROJECT_NAME}")
message(STATUS "项目版本: ${PROJECT_VERSION}")
message(STATUS "C++标准: C++${CMAKE_CXX_STANDARD}")
message(STATUS "编译器ID: ${CMAKE_CXX_COMPILER_ID}")
message(STATUS "构建类型: ${CMAKE_BUILD_TYPE}")

# 安装规则（可选）
install(TARGETS bank_system
    RUNTIME DESTINATION bin
)