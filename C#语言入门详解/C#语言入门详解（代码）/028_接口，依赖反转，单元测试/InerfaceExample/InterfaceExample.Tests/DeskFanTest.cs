using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static InerfaceExample.Program;
using Moq;

namespace InterfaceExample.Tests {
    [TestClass]
    public class DeskFanTest {//引用被测试项目
        [TestMethod]    //特征、特性、属性
        public void PowerLowerThanZero_OK() {//一个test case就是一个方法
            var fan = new DeskFan2(new PowerSupplyLowerThanZero());
            var expected = "Won't work.";
            var actual = fan.Work();
            Assert.AreEqual(expected, actual);    //AreEqual
        }

        [TestMethod]
        public void PowerHigherThan200_Warning() {
            var fan = new DeskFan2(new PowerSupplyHigherThan200());
            var expected = "Warning!";
            var actual = fan.Work();
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void PowerLowerThanZero_OK2() {
            var mock = new Mock<IPowerSupply>();    //使用Mock简化单元测试
            mock.Setup(ps => ps.GetPower()).Returns(() => 0);
            var fan = new DeskFan2(mock.Object);
            var expected = "Won't work.";
            var actual = fan.Work();
            Assert.AreEqual(expected, actual);    //AreEqual
        }

        [TestMethod]
        public void PowerHigherThan200_Warning2() {
            var mock = new Mock<IPowerSupply>();
            mock.Setup(ps => ps.GetPower()).Returns(() => 220);
            var fan = new DeskFan2(mock.Object);
            var expected = "Warning!";
            var actual = fan.Work();
            Assert.AreEqual(expected, actual);
        }
    }
    class PowerSupplyLowerThanZero : IPowerSupply {
        public int GetPower() {
            return 0;
        }
    }
    class PowerSupplyHigherThan200 : IPowerSupply {
        public int GetPower() {
            return 220;
        }
    }
}
