using System;
using System.Collections.Generic;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 流程保存数据结构
    /// </summary>
    public class ProcessSaveData
    {
        public DateTime SaveTime { get; set; }
        public string ProcessName { get; set; }
        public List<DeviceSaveData> Devices { get; set; } = new List<DeviceSaveData>();
        public List<ConnectionSaveData> Connections { get; set; } = new List<ConnectionSaveData>();
    }

    /// <summary>
    /// 设备保存数据结构
    /// </summary>
    public class DeviceSaveData
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Unit { get; set; }
        public double TargetValue { get; set; }
        public string ConditionExpression { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    /// <summary>
    /// 连接保存数据结构
    /// </summary>
    public class ConnectionSaveData
    {
        public string ConnectionId { get; set; }
        public string SourceDeviceId { get; set; }
        public string TargetDeviceId { get; set; }
        public string SourcePort { get; set; }
        public string TargetPort { get; set; }
        public bool IsActive { get; set; }
    }
}