using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using ZPW.Util.Accessory;
using ZPW.Util.Configuration.Element;
using ZPW.Util.Core;
using ZPW.Util.Properties;

namespace ZPW.Util.Configuration.Accessory
{
	/// <summary>
	///  路径匹配算法实现基类
	/// </summary>
	internal  class DirectoryMatchStrategy : PathMatchStrategyBase
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="instances">MetaInstanceConfigurationElementCollection集合对象</param>
		public DirectoryMatchStrategy(MetaInstanceConfigurationElementCollection instances)
		{
			CoreHelper.ExceptionHelper.TrueThrow<NullReferenceException>(instances == null, "配置节点{0}不存在", "sourceMapping/instances");

			base.path = CoreHelper.EnvironmentHelper.Mode == EnumInstanceMode.Web
				? HttpContext.Current.Request.Url.AbsoluteUri
				: AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			base.path = ToLowerTrim(base.path);
			base.candidates = FileterPath(instances,true);
		}

		/// <summary>
		/// 从路径列表里选择与目标路径最贴近的一项
		/// </summary>
		/// <param name="data">路径列表</param>
		/// <returns>最贴近项目</returns>
		public override string Calculate(IDictionary<string, string> data)
		{
			if (data == null || data.Count <= 0 || string.IsNullOrWhiteSpace(base.path))
				return string.Empty;

			int maxMatchLength = -1;
			string metaConfig = string.Empty;

			foreach (var item in data)
			{
				if (base.path.StartsWith(item.Key, StringComparison.Ordinal))
				{
					if (item.Key.Length > maxMatchLength)
					{
						maxMatchLength = item.Key.Length;
						metaConfig = item.Value;
					}
					else
					{
						CoreHelper.ExceptionHelper.TrueThrow<ConfigurationErrorsException>(item.Key.Length == maxMatchLength && !string.Equals(metaConfig,item.Value),Resource.ExceptionConfilitPathDefinition,item.Key ,metaConfig,item.Value);
					}

				}
			}
			return metaConfig;
		} 
	}
}
