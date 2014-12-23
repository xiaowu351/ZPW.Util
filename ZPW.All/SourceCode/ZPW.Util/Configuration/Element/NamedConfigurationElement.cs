using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ZPW.Util.Configuration
{
	/// <summary>
	/// 以名字为键值的配置项 --叶子节点类
	/// </summary>
	public class NamedConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// name的逻辑名称
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public virtual string Name
		{
			get { return (string)this["name"]; }
		}

		/// <summary>
		/// 描述
		/// </summary>
		[ConfigurationProperty("description",DefaultValue = "")]
		public virtual string Description
		{
			get { return (string)this["description"]; }
		}
	}
}
