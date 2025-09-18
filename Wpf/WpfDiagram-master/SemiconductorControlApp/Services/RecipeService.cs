using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace SemiconductorControlApp
{
    /// <summary>
    /// 配方服务实现 - 基于文件系统的配方管理
    /// </summary>
    public class RecipeService
    {
        private readonly string _recipeDirectory;

        public RecipeService()
        {
            _recipeDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recipes");
            
            if (!Directory.Exists(_recipeDirectory))
            {
                Directory.CreateDirectory(_recipeDirectory);
                CreateDefaultRecipes();
            }
        }

        public async Task<RecipeData> LoadRecipeAsync(string recipeName)
        {
            try
            {
                var fileName = Path.Combine(_recipeDirectory, $"{recipeName}.json");
                
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"配方文件不存在: {recipeName}");
                    return CreateDefaultRecipe();
                }

                var jsonContent = await File.ReadAllTextAsync(fileName);
                var recipeData = JsonSerializer.Deserialize<RecipeData>(jsonContent);
                
                Console.WriteLine($"配方加载成功: {recipeName}");
                return recipeData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"配方加载失败: {recipeName}: {ex.Message}");
                return CreateDefaultRecipe();
            }
        }

        public async Task SaveRecipeAsync(string recipeName, RecipeData data)
        {
            try
            {
                var fileName = Path.Combine(_recipeDirectory, $"{recipeName}.json");
                var jsonContent = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                
                await File.WriteAllTextAsync(fileName, jsonContent);
                Console.WriteLine($"配方保存成功: {recipeName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"配方保存失败: {recipeName}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<string>> GetAvailableRecipesAsync()
        {
            try
            {
                var recipes = new List<string>();
                var files = Directory.GetFiles(_recipeDirectory, "*.json");
                
                foreach (var file in files)
                {
                    var recipeName = Path.GetFileNameWithoutExtension(file);
                    recipes.Add(recipeName);
                }
                
                Console.WriteLine($"找到 {recipes.Count} 个配方");
                return recipes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取配方列表失败: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task DeleteRecipeAsync(string recipeName)
        {
            try
            {
                var fileName = Path.Combine(_recipeDirectory, $"{recipeName}.json");
                
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine($"配方删除成功: {recipeName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"配方删除失败: {recipeName}: {ex.Message}");
                throw;
            }
        }

        private void CreateDefaultRecipes()
        {
            try
            {
                // 创建示例配方
                var sampleRecipe = new RecipeData
                {
                    Devices = new List<DeviceData>
                    {
                        new DeviceData
                        {
                            DeviceId = "PUMP01",
                            DeviceName = "主泵",
                            DeviceType = DeviceType.Pump,
                            Unit = "L/min",
                            X = 100,
                            Y = 100
                        },
                        new DeviceData
                        {
                            DeviceId = "VALVE01",
                            DeviceName = "进气阀",
                            DeviceType = DeviceType.Valve,
                            Unit = "%",
                            X = 300,
                            Y = 100
                        },
                        new DeviceData
                        {
                            DeviceId = "CHAMBER01",
                            DeviceName = "反应腔",
                            DeviceType = DeviceType.Chamber,
                            Unit = "Torr",
                            X = 500,
                            Y = 200
                        },
                        new DeviceData
                        {
                            DeviceId = "HEATER01",
                            DeviceName = "加热器",
                            DeviceType = DeviceType.Heater,
                            Unit = "°C",
                            X = 300,
                            Y = 300
                        },
                        new DeviceData
                        {
                            DeviceId = "SENSOR01",
                            DeviceName = "温度传感器",
                            DeviceType = DeviceType.Sensor,
                            Unit = "°C",
                            X = 500,
                            Y = 400
                        }
                    },
                    Connections = new List<ConnectionData>
                    {
                        new ConnectionData
                        {
                            ConnectionId = "CONN01",
                            SourceDeviceId = "PUMP01",
                            TargetDeviceId = "VALVE01",
                            SourcePort = "Flow_Out",
                            TargetPort = "Fluid_In"
                        },
                        new ConnectionData
                        {
                            ConnectionId = "CONN02",
                            SourceDeviceId = "VALVE01",
                            TargetDeviceId = "CHAMBER01",
                            SourcePort = "Fluid_Out",
                            TargetPort = "Gas_In"
                        },
                        new ConnectionData
                        {
                            ConnectionId = "CONN03",
                            SourceDeviceId = "HEATER01",
                            TargetDeviceId = "CHAMBER01",
                            SourcePort = "Heat_Out",
                            TargetPort = "Control_In"
                        },
                        new ConnectionData
                        {
                            ConnectionId = "CONN04",
                            SourceDeviceId = "CHAMBER01",
                            TargetDeviceId = "SENSOR01",
                            SourcePort = "Status_Out",
                            TargetPort = "Data_In"
                        }
                    }
                };

                var fileName = Path.Combine(_recipeDirectory, "示例配方.json");
                var jsonContent = JsonSerializer.Serialize(sampleRecipe, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, jsonContent);
                
                Console.WriteLine("默认配方创建完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建默认配方失败: {ex.Message}");
            }
        }

        private RecipeData CreateDefaultRecipe()
        {
            return new RecipeData
            {
                Devices = new List<DeviceData>(),
                Connections = new List<ConnectionData>()
            };
        }
    }
}