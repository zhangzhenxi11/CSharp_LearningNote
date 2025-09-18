using System;
using System.Collections.Generic;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 流程状态枚举
    /// </summary>
    public enum ProcessStatus
    {
        Stopped,        // 停止
        Running,        // 运行中
        Paused,         // 暂停
        Error,          // 错误
        Completed       // 完成
    }

    /// <summary>
    /// 流程连接信息
    /// </summary>
    public class ProcessConnection
    {
        public string ConnectionId { get; set; }
        public SemiconductorDevice SourceDevice { get; set; }
        public SemiconductorDevice TargetDevice { get; set; }
        public string SourcePort { get; set; }
        public string TargetPort { get; set; }
        public bool IsActive { get; set; }
        public double FlowRate { get; set; }
        public DateTime LastDataTime { get; set; }
    }

    /// <summary>
    /// 配方数据结构
    /// </summary>
    public class RecipeData
    {
        public List<DeviceData> Devices { get; set; } = new List<DeviceData>();
        public List<ConnectionData> Connections { get; set; } = new List<ConnectionData>();
    }

    public class DeviceData
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Unit { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ConnectionData
    {
        public string ConnectionId { get; set; }
        public string SourceDeviceId { get; set; }
        public string TargetDeviceId { get; set; }
        public string SourcePort { get; set; }
        public string TargetPort { get; set; }
    }
}