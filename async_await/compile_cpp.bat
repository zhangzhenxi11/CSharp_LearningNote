@echo off
chcp 65001 >nul
echo 编译 C++ 异步早餐程序
echo 需要支持 C++20 的编译器
echo.

REM 如果您安装了 Visual Studio 2022
echo 尝试使用 MSVC 编译器...
cl /std:c++20 /EHsc breakfast_async.cpp /Fe:breakfast_async.exe 2>nul
if %errorlevel%==0 (
    echo MSVC 编译成功！
    goto :run
) else (
    echo MSVC 编译失败或未找到编译器
)

echo.
REM 如果您安装了 MinGW-w64 (GCC)
echo 尝试使用 GCC 编译器...
g++ -std=c++20 -fcoroutines breakfast_async.cpp -o breakfast_async.exe 2>nul
if %errorlevel%==0 (
    echo GCC 编译成功！
    goto :run
) else (
    echo GCC 编译失败或未找到编译器
    echo 请确保已安装支持 C++20 的编译器
    goto :end
)

:run
echo.
echo 编译完成，正在运行程序...
echo ==============================
breakfast_async.exe
echo ==============================
echo 程序执行完毕

:end
echo.
echo 按任意键退出...
pause >nul