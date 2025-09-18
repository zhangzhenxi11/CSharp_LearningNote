using System;
using System.Threading.Tasks;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 设备控制服务实现 - 模拟设备控制，生产环境需连接实际设备驱动
    /// </summary>
    public static class DeviceControlService
    {
        private static readonly Random _random = new Random();

        public static async Task ExecuteCommandAsync(string deviceId, string command)
        {
            try
            {
                Console.WriteLine($"执行设备命令: {deviceId} - {command}");
                
                // 模拟命令执行时间
                await Task.Delay(_random.Next(50, 200));
                
                // 模拟偶发错误
                if (_random.NextDouble() < 0.05) // 5%的错误概率
                {
                    throw new InvalidOperationException($"设备 {deviceId} 命令执行失败: {command}");
                }
                
                Console.WriteLine($"设备命令执行成功: {deviceId} - {command}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设备命令执行失败: {deviceId} - {command}: {ex.Message}");
                throw;
            }
        }

        public static async Task SetParameterAsync(string deviceId, double value, string unit)
        {
            try
            {
                Console.WriteLine($"设置设备参数: {deviceId} = {value} {unit}");
                
                // 模拟参数设置时间
                await Task.Delay(_random.Next(20, 100));
                
                Console.WriteLine($"设备参数设置成功: {deviceId} = {value} {unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设备参数设置失败: {deviceId}: {ex.Message}");
                throw;
            }
        }

        public static async Task<double> ReadCurrentValueAsync(string deviceId)
        {
            try
            {
                // 模拟读取时间
                await Task.Delay(_random.Next(10, 50));
                
                // 生成模拟数据
                var baseValue = _random.NextDouble() * 100;
                var noise = (_random.NextDouble() - 0.5) * 10;
                var currentValue = Math.Max(0, baseValue + noise);
                
                return Math.Round(currentValue, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取设备数据失败: {deviceId}: {ex.Message}");
                throw;
            }
        }
    }
}