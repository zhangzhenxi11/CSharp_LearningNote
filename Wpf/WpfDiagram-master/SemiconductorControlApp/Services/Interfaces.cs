using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 数据库服务接口
    /// </summary>
    public interface IDatabaseService
    {
        Task LogDeviceOperationAsync(string deviceId, string operation, DateTime timestamp);
        Task LogParameterChangeAsync(string deviceId, string parameter, string value, DateTime timestamp);
        Task LogErrorAsync(string deviceId, string error, DateTime timestamp);
        Task LogProcessEventAsync(string eventType, string recipe, DateTime timestamp);
        Task LogDeviceDataAsync(string deviceId, double value, DateTime timestamp);
        Task<object> ExportProcessDataAsync(DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 设备控制服务接口
    /// </summary>
    public interface IDeviceControlService
    {
        Task ExecuteCommandAsync(string deviceId, string command);
        Task SetParameterAsync(string deviceId, double value, string unit);
        Task<double> ReadCurrentValueAsync(string deviceId);
    }

    /// <summary>
    /// 配方服务接口
    /// </summary>
    public interface IRecipeService
    {
        Task<RecipeData> LoadRecipeAsync(string recipeName);
        Task SaveRecipeAsync(string recipeName, RecipeData data);
        Task<List<string>> GetAvailableRecipesAsync();
        Task DeleteRecipeAsync(string recipeName);
    }
}