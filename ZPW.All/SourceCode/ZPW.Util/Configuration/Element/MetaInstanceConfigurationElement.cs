using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using ZPW.Util.Configuration.Accessory;
using ZPW.Util.Core;

namespace ZPW.Util.Configuration.Element
{
	/// <summary>
	/// sourceMappings 实例配置元素
	/// </summary>
	class MetaInstanceConfigurationElement : NamedConfigurationElement
	{

		/// <summary>
		/// 全局配置节点名称path和文件名
		/// </summary>
		private const string pathItem = "path";
		/// <summary>
		/// 配置节点名称mode，不可以重复
		/// </summary>
		private const string modeItem = "mode";

		/// <summary>
		/// 子配置节点项使用的名称
		/// </summary>
		public const string appsItem = "applications";
		/// <summary>
		/// 配置文件路径Path
		/// </summary>
		[ConfigurationProperty(pathItem, IsRequired = true)]
		public string Path
		{
			get { return (string)base[pathItem]; }
		}

		/// <summary>
		/// 配置应用的类型Mode
		/// </summary>
		[ConfigurationProperty(modeItem, IsRequired = true)]
		private string Mode
		{
			get { return (string)base[modeItem]; }
		}

		/// <summary>
		/// 当前应用类型Mode
		/// </summary>
		public EnumInstanceMode InstanceMode
		{
			get
			{
				switch (Mode.ToLower(CultureInfo.InvariantCulture).Trim())
				{
					case "web":
						return EnumInstanceMode.Web;
					case "win":
						return EnumInstanceMode.Windows;
					case "wcf":
						return EnumInstanceMode.WCF;
					default:
						throw new NotSupportedException("未定义的应用程序的枚举类型！");

				}
			}
		}


		/// <summary>
		/// 当前实例的配置文件映射
		/// </summary>
		[ConfigurationProperty(appsItem)]
		public MetaApplicationsConfigurationElementCollection Mappings
		{
			get { return base[appsItem] as MetaApplicationsConfigurationElementCollection; }
		}

	}

	/// <summary>
	/// sourceMappings配置元素集合
	/// </summary>
	class MetaInstanceConfigurationElementCollection :
	   NamedConfigurationElementCollection<MetaInstanceConfigurationElement>
	{
		/// <summary>
		/// 返回值是匹配成功的全局配置文件和文件名称。（即配置项的path的属性值）
		/// 如果未找到匹配的全局配置文件，则返回值为""
		/// </summary>
		/// <returns></returns>
		public string MatchedPath()
		{
			string  path = string.Empty;
			StrategyContext context = new StrategyContext();
			//对于Windows应用，如果有匹配的assembly内容，则直接使用文件内容匹配
			if (EnvironmentHelper.Mode == EnumInstanceMode.Windows)
			{
				context.Strategy = new FileNameMatchStrategy(this);
				path = context.DoAction();
				if (false == string.IsNullOrWhiteSpace(path))
					return path;
			}

			//如果非Windows应用，或者Windows应用中没有匹配的assembly指定，则采用目录匹配算法
			context.Strategy = new DirectoryMatchStrategy(this);
			path = context.DoAction();
			return path;
		}
	}
}
