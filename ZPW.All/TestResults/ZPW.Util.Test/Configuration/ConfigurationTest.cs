using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Test.Configuration.DemoClass;

namespace ZPW.Util.Test.Configuration
{
	[TestClass]
	public class ConfigurationTest
	{
		private TestContext testContextInstance;

		/// <summary>
		///获取或设置测试上下文，该上下文提供
		///有关当前测试运行及其功能的信息。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		[TestMethod]
		[Description("测试读取zpw.Framework.config自定义全局配置文件中的配置项值")]
		public void ConfigurationSectionBaseTest()
		{
			UriConfigurationElementCollection uriList = RegisterServersConfigurationSection.Instance.Servers;
			foreach (UriConfigurationElement item in uriList)
			{
				Trace.WriteLine(item.Uri);
			}
			Assert.AreEqual(1,uriList.Count);
		}

		[TestMethod]
		[Description("读取app.config文件中自定义SectionGroup配置项中的节点值")]
		public void ReadCustomConfigurationTest()
		{
			UriConfigurationElementCollection jQueryList = PageContentConfigurationSection.Instance.StartScripts;
			foreach (UriConfigurationElement item in jQueryList)
			{
				Trace.WriteLine(item.Uri);
			}
			Assert.AreEqual(2, jQueryList.Count);
		}
	}
}
	;