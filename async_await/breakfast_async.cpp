#include <iostream>
#include <chrono>
#include <coroutine>
#include <future>
#include <thread>
#include <vector>
#include <string>

// C++20 协程支持 - 类似于 C# 的 async/await
using namespace std::chrono_literals;

// 简单的协程任务类型 - 对应 C# 的 Task<T>
template<typename T>
struct Task {
    // 协程 Promise 类型 - 编译器生成状态机需要的核心组件
    struct promise_type {
        T value;                    // 存储协程返回值
        std::exception_ptr exception; // 存储异常信息
        
        // 协程开始时的行为 - 对应 C# 编译器生成的状态机初始化
        Task get_return_object() {
            return Task{std::coroutine_handle<promise_type>::from_promise(*this)};
        }
        
        // 协程启动方式 - suspend_never 表示立即开始执行
        std::suspend_never initial_suspend() { return {}; }
        
        // 协程结束时的行为 - suspend_always 表示协程结束后暂停，等待外部获取结果
        std::suspend_always final_suspend() noexcept { return {}; }
        
        // 设置返回值 - 对应 C# 的 return 语句
        void return_value(T val) {
            value = std::move(val);
        }
        
        // 异常处理 - 对应 C# 的异常传播机制
        void unhandled_exception() {
            exception = std::current_exception();
        }
    };
    
    // 协程句柄 - 类似于 C# Task 的内部状态管理
    std::coroutine_handle<promise_type> coro;
    
    // 构造函数
    Task(std::coroutine_handle<promise_type> h) : coro(h) {}
    
    // 析构函数 - 清理协程资源
    ~Task() {
        if (coro) {
            coro.destroy();
        }
    }
    
    // 移动构造函数 - 确保协程句柄唯一性
    Task(Task&& other) noexcept : coro(std::exchange(other.coro, {})) {}
    Task& operator=(Task&& other) noexcept {
        if (this != &other) {
            if (coro) coro.destroy();
            coro = std::exchange(other.coro, {});
        }
        return *this;
    }
    
    // 禁用拷贝构造 - 协程不能被复制
    Task(const Task&) = delete;
    Task& operator=(const Task&) = delete;
    
    // 获取协程执行结果 - 对应 C# 的 Task.Result
    T get_result() {
        if (!coro.done()) {
            // 如果协程未完成，这里可以实现等待逻辑
            // 简化版本：直接返回默认值
        }
        
        if (coro.promise().exception) {
            std::rethrow_exception(coro.promise().exception);
        }
        
        return std::move(coro.promise().value);
    }
    
    // 检查协程是否完成 - 对应 C# 的 Task.IsCompleted
    bool is_done() const {
        return coro && coro.done();
    }
};

// 异步延时函数 - 对应 C# 的 Task.Delay()
struct DelayAwaiter {
    std::chrono::milliseconds duration;
    
    // 是否立即完成 - false 表示需要暂停协程
    bool await_ready() const { return duration.count() <= 0; }
    
    // 暂停协程并设置恢复条件 - 核心的任务调度机制
    void await_suspend(std::coroutine_handle<> handle) {
        // 在新线程中等待，然后恢复协程 - 类似 C# 的任务调度器
        std::thread([handle, this]() {
            std::this_thread::sleep_for(duration);
            handle.resume(); // 恢复协程执行 - 对应 C# 状态机的状态切换
        }).detach();
    }
    
    // 协程恢复时的返回值
    void await_resume() {}
};

// 创建延时等待器 - 对应 C# 的 await Task.Delay()
DelayAwaiter delay(std::chrono::milliseconds ms) {
    return DelayAwaiter{ms};
}

// 食物类定义 - 对应 C# 的内部类
class Coffee {};
class Egg {};
class HashBrown {};
class Toast {};
class Juice {};

// 同步操作 - 倒咖啡（立即完成，不需要等待）
Coffee pour_coffee() {
    std::cout << "Pouring coffee" << std::endl;
    return Coffee{};
}

// 异步操作 - 煎蛋（需要等待时间）
Task<Egg> fry_eggs_async(int count) {
    std::cout << "Warming the egg pan..." << std::endl;
    co_await delay(3000ms); // 等待3秒 - 对应 C# 的 await Task.Delay(3000)
    
    std::cout << "Cracking " << count << " eggs" << std::endl;
    std::cout << "Cooking the eggs..." << std::endl;
    co_await delay(3000ms); // 再等待3秒
    
    std::cout << "Put eggs on plate" << std::endl;
    co_return Egg{}; // 返回结果 - 对应 C# 的 return
}

// 异步操作 - 煎土豆饼
Task<HashBrown> fry_hash_browns_async(int patties) {
    std::cout << "Putting " << patties << " hash brown patties in the pan" << std::endl;
    std::cout << "Cooking first side of hash browns..." << std::endl;
    co_await delay(3000ms);
    
    for (int i = 0; i < patties; ++i) {
        std::cout << "Flipping a hash brown patty" << std::endl;
    }
    
    std::cout << "Cooking the second side of hash browns..." << std::endl;
    co_await delay(3000ms);
    std::cout << "Put hash browns on plate" << std::endl;
    
    co_return HashBrown{};
}

// 异步操作 - 烤面包
Task<Toast> toast_bread_async(int slices) {
    for (int i = 0; i < slices; ++i) {
        std::cout << "Putting a slice of bread in the toaster" << std::endl;
    }
    
    std::cout << "Start toasting..." << std::endl;
    co_await delay(3000ms);
    std::cout << "Remove toast from toaster" << std::endl;
    
    co_return Toast{};
}

// 同步操作 - 涂黄油
void apply_butter(const Toast& toast) {
    std::cout << "Putting butter on the toast" << std::endl;
}

// 同步操作 - 涂果酱
void apply_jam(const Toast& toast) {
    std::cout << "Putting jam on the toast" << std::endl;
}

// 异步组合操作 - 制作涂有黄油和果酱的吐司
Task<Toast> make_toast_with_butter_and_jam_async(int count) {
    auto toast = co_await toast_bread_async(count); // 等待烤面包完成
    apply_butter(toast);  // 同步操作
    apply_jam(toast);     // 同步操作
    co_return toast;
}

// 同步操作 - 倒橙汁
Juice pour_orange_juice() {
    std::cout << "Pouring orange juice" << std::endl;
    return Juice{};
}

// 主函数 - 协调所有异步操作
Task<int> make_breakfast_async() {
    // 立即完成的操作
    auto coffee = pour_coffee();
    std::cout << "Coffee is ready" << std::endl;
    
    // 启动多个异步任务 - 对应 C# 的任务并发启动
    auto eggs_task = fry_eggs_async(2);
    auto hash_browns_task = fry_hash_browns_async(3);
    auto toast_task = make_toast_with_butter_and_jam_async(2);
    
    // 等待所有任务完成 - 简化版本，实际中可以实现类似 C# Task.WhenAny 的逻辑
    std::cout << "Starting all cooking tasks concurrently..." << std::endl;
    
    // 等待各个任务完成（简化实现）
    auto eggs = co_await std::move(eggs_task);
    std::cout << "Eggs are ready" << std::endl;
    
    auto hash_browns = co_await std::move(hash_browns_task);
    std::cout << "Hash browns are ready" << std::endl;
    
    auto toast = co_await std::move(toast_task);
    std::cout << "Toast is ready" << std::endl;
    
    // 最后的同步操作
    auto orange_juice = pour_orange_juice();
    std::cout << "Orange juice is ready" << std::endl;
    
    std::cout << "Breakfast is ready!" << std::endl;
    co_return 0;
}

// 程序入口点
int main() {
    std::cout << "=== C++ 异步早餐制作程序 ===" << std::endl;
    std::cout << "演示 C++20 协程 (coroutines) 的使用" << std::endl;
    std::cout << "原理与 C# 的 async/await 相同" << std::endl;
    std::cout << "============================" << std::endl;
    
    // 启动异步主函数
    auto breakfast_task = make_breakfast_async();
    
    // 简单的等待实现（实际应用中会更复杂）
    while (!breakfast_task.is_done()) {
        std::this_thread::sleep_for(100ms);
    }
    
    auto result = breakfast_task.get_result();
    std::cout << "Program completed with result: " << result << std::endl;
    
    return 0;
}

/*
编译说明：
需要支持 C++20 的编译器，如：
- GCC 10+ 或 Clang 10+
- Visual Studio 2019 16.8+ 或 Visual Studio 2022

编译命令：
g++ -std=c++20 -fcoroutines breakfast_async.cpp -o breakfast_async
或
clang++ -std=c++20 -stdlib=libc++ breakfast_async.cpp -o breakfast_async

主要差异总结：
1. C# 使用 async/await 关键字，C++ 使用 co_await/co_return
2. C# 有内置的 Task<T> 类型，C++ 需要自定义协程类型
3. C# 的任务调度器更成熟，C++ 需要手动实现更多细节
4. 两者的底层原理相同：都是编译器生成状态机，实现协作式多任务

核心相同点：
- 都不会阻塞线程
- 都通过状态机实现任务切换
- 都支持异步操作的组合
- 都能提高程序的并发性能
*/