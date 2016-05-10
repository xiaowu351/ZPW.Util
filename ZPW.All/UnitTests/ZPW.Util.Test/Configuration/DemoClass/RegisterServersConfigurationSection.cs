using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ZPW.Util.Configuration.Section;

namespace ZPW.Util.Test.Configuration.DemoClass
{
	public class RegisterServersConfigurationSection : ConfigurationSectionBase<RegisterServersConfigurationSection>
	{
		private RegisterServersConfigurationSection()
			: base("registerServers")
		{
		}

		/// <summary>
		/// 配置项集合
		/// </summary>
		[ConfigurationProperty("items", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public UriConfigurationElementCollection Servers
		{
			get { return (UriConfigurationElementCollection)this["items"]; }
		}
	}
}
