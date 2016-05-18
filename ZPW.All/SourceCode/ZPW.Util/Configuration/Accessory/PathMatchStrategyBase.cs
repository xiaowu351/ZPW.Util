using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ZPW.Util.Accessory;
using ZPW.Util.Configuration.Element;
using ZPW.Util.Core;

namespace ZPW.Util.Configuration.Accessory
{
	/// <summary>
	///  路径匹配算法实现基类
	/// </summary>
	internal abstract class PathMatchStrategyBase : IStrategy<IDictionary<string, string>, string>
	{
		/// <summary>
		/// 定义获取应用类型路径，供子类赋值使用
		/// </summary>
		protected string path;
		/// <summary>
		/// 待计算的数据
		/// </summary>
		protected IDictionary<string, string> candidates;

		/// <summary>
		/// 筛选文件，返回匹配的文件
		/// </summary>
		/// <param name="data">待筛选的文件集合</param>
		/// <returns>返回匹配的文件，如果没有找到则返回string.empty</returns>
		public abstract string Calculate(IDictionary<string, string> data);

		/// <summary>
		/// 待筛选的文件路径
		/// </summary>
		public IDictionary<string, string> Candidates
		{
			get { return candidates; }
		}

		/// <summary>
		/// 判断某个路径是否为目录
		/// </summary>
		/// <param name="folderPath">路径</param>
		/// <returns></returns>
		protected bool IsDirectory(string folderPath)
		{
			return string.IsNullOrWhiteSpace(Path.GetFileName(folderPath));
		}

		/// <summary>
		/// 转换成小写
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		protected string ToLowerTrim(string filePath)
		{
			return filePath.ToLower().ToString(CultureInfo.InvariantCulture).Trim();
		}

		/// <summary>
		///  依照路径类型（文件/目录）和格式筛选路径
		/// </summary>
		/// <param name="instances"></param>
		/// <param name="isDirectory">是否为目录</param>
		/// <returns></returns>
		protected IDictionary<string, string> FileterPath(MetaInstanceConfigurationElementCollection instances, bool isDirectory)
		{
			if (instances == null || instances.Count <= 0)
				return null;
			IDictionary<string, string> result = new Dictionary<string, string>();

			//Uri pathAbsolute = null;

			foreach (MetaInstanceConfigurationElement instance in instances)
			{
				if (instance == null || instance.Mappings == null || instance.Mappings.Count <= 0 || instance.InstanceMode != CoreHelper.EnvironmentHelper.Mode)
					continue;

				string metaConfig = ToLowerTrim(instance.Path);

				foreach (MetaApplicationsConfigurationElement mapping in instance.Mappings)
				{
					string applicationPath = mapping.Application;
					if (isDirectory ^ IsDirectory(applicationPath))
						continue;

					if (CoreHelper.EnvironmentHelper.Mode == EnumInstanceMode.Web)
					{
						//if (pathAbsolute == null)
						//	pathAbsolute = new Uri(path, UriKind.Absolute);
						Uri appAbsolute = new Uri(applicationPath, UriKind.RelativeOrAbsolute);
						if (appAbsolute.IsAbsoluteUri)
						{
							//if (pathAbsolute.Scheme == appAbsolute.Scheme
							//	&& pathAbsolute.Port == appAbsolute.Port
							//	&& pathAbsolute.UserInfo == appAbsolute.UserInfo 
							//	&& pathAbsolute.Host == appAbsolute.Host 
							//	&& pathAbsolute.HostNameType == appAbsolute.HostNameType
							//	)
							{
								applicationPath = ToLowerTrim(appAbsolute.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped));
							}
						}
					}
					else
					{
						applicationPath = ToLowerTrim(Path.GetFullPath(applicationPath));
					}

					result.Add(applicationPath, metaConfig);
				}
			}
			return result;
		}
	}
}
