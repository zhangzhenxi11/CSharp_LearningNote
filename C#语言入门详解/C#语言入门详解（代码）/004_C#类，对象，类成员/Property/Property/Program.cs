using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DWConfigurationEntities2 proxy = new DWConfigurationEntities2();    //引用数据库DWConfigurationEntities2（跟教程不一样）
            //proxy.compute_node    //proxy可以访问compute_node
            foreach (compute_node p in proxy.compute_node)  //打印compute_node的所有名称
            {
                Console.WriteLine(p.name);
            }
        }
    } 
}
