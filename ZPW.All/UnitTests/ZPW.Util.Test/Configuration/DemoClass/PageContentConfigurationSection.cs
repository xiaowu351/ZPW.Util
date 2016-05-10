using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ZPW.Util.Configuration.Section;

namespace ZPW.Util.Test.Configuration.DemoClass
{
	class PageContentConfigurationSection : ConfigurationSectionBase<PageContentConfigurationSection>
	{
		/// <summary>
		/// 构造函数，Sections名称为pageContent
		/// </summary>
		public PageContentConfigurationSection():base("pageContent"){}

		/// <summary>
		/// 此Section下的Element集合
		/// </summary>
		[ConfigurationProperty("startScripts")]
		public UriConfigurationElementCollection StartScripts
		{
			get { return base["startScripts"] as UriConfigurationElementCollection; }
		}
	}
}
