using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ZPW.Util.Configuration.Element
{
	/// <summary>
	/// 实体应用映射配置项
	/// </summary>
	class MetaApplicationsConfigurationElement:NamedConfigurationElement
	{
		/// <summary>
		/// 配置应用系统的路径配置节点名称和路径
		/// </summary>
		private const string AppItem = "app";

		/// <summary>
		/// 配置的应用系统的路径，可以是绝对路径，也可以是相对路径
		/// 绝对路径需要指定正确的主机名、IP地址、端口号、用户信息等各字段，匹配要求严格
		/// 建议使用相对路径，配置要求宽松
		/// 如果是配置对所有应用都有效，可以配置app="/",不过匹配规则是找到一个匹配的全局文件就不再进行后的匹配。
		/// </summary>
		[ConfigurationProperty(AppItem,IsRequired = true)]
		public string Application
		{
			get { return base[AppItem] as string; }
		}
	}
}
