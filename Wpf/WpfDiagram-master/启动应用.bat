@echo off
echo ====================================
echo 半导体控制系统 - 快速启动脚本
echo ====================================
echo.

REM 检查.NET 6.0是否安装
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo 错误: 未找到.NET 6.0运行时
    echo 请从以下地址下载并安装:
    echo https://dotnet.microsoft.com/download/dotnet/6.0
    pause
    exit /b 1
)

echo 检测到.NET版本:
dotnet --version

echo.
echo 正在恢复项目依赖...
dotnet restore

if %errorlevel% neq 0 (
    echo 错误: 依赖恢复失败
    pause
    exit /b 1
)

echo.
echo 正在编译项目...
dotnet build

if %errorlevel% neq 0 (
    echo 错误: 编译失败
    pause
    exit /b 1
)

echo.
echo 正在启动半导体控制系统...
echo 如需退出，请关闭应用程序窗口
echo.

dotnet run --project SemiconductorControl.csproj

echo.
echo 应用程序已退出
pause