using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Configuration;
using ZPW.Util.Test.Configuration.DemoClass;

namespace ZPW.Util.Test.Configuration
{
	[TestClass]
	public class ConfigurationTest
	{
		[TestMethod]
		[Description("测试读取自定义配置项值")]
		public void TestMethod1()
		{
			RegisterServersConfigurationSection registerServers = (RegisterServersConfigurationSection)ConfigurationBroker.GetSection("registerServers");
			Assert.AreEqual(1,registerServers.Servers.Count);
		}
	}
}
