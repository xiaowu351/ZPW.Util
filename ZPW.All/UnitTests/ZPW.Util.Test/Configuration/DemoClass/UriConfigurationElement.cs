using System;
using System.Configuration;
using ZPW.Util.Configuration;
using ZPW.Util.Core;


namespace ZPW.Util.Test.Configuration.DemoClass
{
	 
	/// <summary>
	/// 继承基类，基类中包含name节点和description节点
	/// 此类增加了uri节点
	/// </summary>
	public class UriConfigurationElement:NamedConfigurationElement
	{
		/// <summary>
		/// Uri地址字符串
		/// </summary>
		[ConfigurationProperty("uri")]
		private string UriString
		{
			get { return (string) this["uri"]; }
		}

		/// <summary>
		/// 配置的Uri
		/// </summary>
		public Uri Uri
		{
			get
			{
				return CoreHelper.UriHelper.AbsoluteUri(UriString);
			}
		}
	}

	/// <summary>
	/// Uri 配置项集合
	/// </summary>
	public class UriConfigurationElementCollection : NamedConfigurationElementCollection<UriConfigurationElement>
	{
		
	}
}
