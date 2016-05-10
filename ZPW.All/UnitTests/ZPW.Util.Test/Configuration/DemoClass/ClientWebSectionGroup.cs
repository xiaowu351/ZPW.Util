using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ZPW.Util.Configuration.Section;

namespace ZPW.Util.Test.Configuration.DemoClass
{
    class ClientWebSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>
		/// 配置页面内容节点
		/// </summary>
		[ConfigurationProperty("pageContent")]
	    public PageContentConfigurationSection PageContent
	    {
			get {return base.Sections["pageContent"] as PageContentConfigurationSection; }
	    }
	}
}
