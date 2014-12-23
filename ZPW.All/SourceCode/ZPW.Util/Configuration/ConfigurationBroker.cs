using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using ZPW.Util.Core;

namespace ZPW.Util.Configuration
{
	/// <summary>
	/// 管理所有本地配置文件和映射配置文件
	/// 远程配置文件映射由ConfigurationFileMap和ConfigurationManager.OpenMappedMachineConfiguration处理
	/// 约束：
	/// <list type="bullet">
	///    <item>
	///     映射文件必须以ConfigurationSectionGroup和ConfigurationSection开始
	///    </item>
	/// </list>
	/// </summary>
	public static class ConfigurationBroker
	{
		#region 私有变量
		/// <summary>
		/// 外部文件配置节点
		/// </summary>
		private const string MetaConfigurationItem = "ZPW.All.MetaConfiguration";

		/// <summary>
		/// 外部节点Section节点名称
		/// </summary>
		private const string MetaConfigurationSectionGroupItem = "ZPW.All.framework.metaConfig";

		/// <summary>
		/// .net framework 机器配置文件
		/// </summary>
		private static readonly string MachineConfigurationFile = ConfigurationManager.OpenMachineConfiguration().FilePath;
		/// <summary>
		/// 当前应用程序域使用的配置文件
		/// </summary>
		private static readonly string LocalConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
		/// <summary>
		/// 全局配置文件
		/// </summary>
		private static readonly string GlobalConfigurationFile = ConfigurationBroker.MachineConfigurationFile;

		/// <summary>
		/// meta配置文件位置枚举
		/// </summary>
		private enum MetaConfigurationPosition
		{
			/// <summary>
			/// 本地文件
			/// </summary>
			LocalFile,
			/// <summary>
			/// Meta文件
			/// </summary>
			MetaFile
		}

		/// <summary>
		/// 内部类，用于存放、传递machine、local、meta、global配置文件的地址和meta文件位置（枚举）
		/// </summary>
		private class ConfigFilesSetting
		{
			private MetaConfigurationPosition metaFilePosition = MetaConfigurationPosition.LocalFile;

			public MetaConfigurationPosition MetaFilePosition
			{
				get { return this.metaFilePosition; }
				set { this.metaFilePosition = value; }
			}

			public string GlobalConfigurationFile { get; set; }
			public string MetaConfigurationFile { get; set; }
			public string LocalConfigurationFile { get; set; }
			public string MachineConfigurationFile { get; set; }
			/// <summary>
			/// 构造函数
			/// </summary>
			public ConfigFilesSetting()
			{
				GlobalConfigurationFile = string.Empty;
				MetaConfigurationFile = string.Empty;
				LocalConfigurationFile = string.Empty;
				MachineConfigurationFile = string.Empty;

			}
		}
		#endregion
		/// <summary>
		/// 构造函数
		/// </summary>
		static ConfigurationBroker()
		{

		}
		#region 私有方法

		/// <summary>
		/// 加载所有的配置文件
		/// </summary>
		/// <param name="globalConfigurationFile">全局配置文件</param>
		/// <returns>ConfigFilesSetting对象</returns>
		private static ConfigFilesSetting loadFilesSetting(string globalConfigurationFile)
		{
			bool globalEmpty = string.IsNullOrWhiteSpace(globalConfigurationFile);
			ConfigFilesSetting settings = new ConfigFilesSetting()
			{
				MachineConfigurationFile = ConfigurationBroker.MachineConfigurationFile,
				GlobalConfigurationFile = globalEmpty ? ConfigurationBroker.GlobalConfigurationFile : globalConfigurationFile,
				LocalConfigurationFile = ConfigurationBroker.LocalConfigurationFile
			};
			if (globalEmpty == false)
				return settings;

			//此处待增加外部指定配置文件
			return settings;

		}

		/// <summary>
		/// 获取标准Web应用程序的配置信息，合并Web.config和 global配置文件
		/// </summary>
		/// <param name="machineConfigPath">global配置文件地址</param>
		/// <returns>Web.config和global配置文件合并后的Configuration对象</returns>
		private static System.Configuration.Configuration GetStandardWebConfiguration(string machineConfigPath)
		{
			System.Configuration.Configuration config;
			WebConfigurationFileMap webFileMap = new WebConfigurationFileMap()
			{
				 MachineConfigFilename = machineConfigPath
			};
			VirtualDirectoryMapping vDirMap = new VirtualDirectoryMapping(HttpContext.Current.Request.PhysicalApplicationPath,true);

			webFileMap.VirtualDirectories.Add("/",vDirMap);
			config = WebConfigurationManager.OpenMappedWebConfiguration(webFileMap, "/",
				System.Web.Hosting.HostingEnvironment.SiteName);

			return config;
		}

		/// <summary>
		/// 获取标准的Winform应用程序的配置信息，合并App.config和global配置文件
		/// </summary>
		/// <param name="machineConfigPath">global配置文件地址</param>
		/// <param name="localConfigPath">本地应用程序配置文件地址</param>
		/// <returns>App.config和global配置文件合并后的Configuration对象</returns>
		private static System.Configuration.Configuration getStandardExeConfiguration(string machineConfigPath,
			string localConfigPath)
		{
			System.Configuration.Configuration config;
			ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap()
			{
				MachineConfigFilename = machineConfigPath,
				ExeConfigFilename = localConfigPath
			};
			config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

			return config;
		}

		/// <summary>
		/// 获取最终的local和global合并后的Configuration
		/// </summary>
		/// <param name="fileSettings">ConfigFilesSetting对象</param>
		/// <returns>local和global合并后的Configuration对象</returns>
		private static System.Configuration.Configuration getFinalConfiguration(ConfigFilesSetting fileSettings)
		{
			System.Configuration.Configuration config = EnvironmentHelper.Mode == EnumInstanceMode.Web
				? ConfigurationBroker.GetStandardWebConfiguration(fileSettings.GlobalConfigurationFile)
				: ConfigurationBroker.getStandardExeConfiguration(fileSettings.GlobalConfigurationFile,
					fileSettings.LocalConfigurationFile);

			return config;
		}

		/// <summary>
		/// 从SectionGroup中读取Section，在Section写在group里时使用
		/// </summary>
		/// <param name="sectionName">section name</param>
		/// <param name="groups">sectionGroup</param>
		/// <returns>ConfigurationSection对象</returns>
		private static ConfigurationSection GetSectionFromGroups(string sectionName, ConfigurationSectionGroupCollection groups)
		{
			ConfigurationSection section = null;
			try
			{
				foreach (ConfigurationSectionGroup group in groups)
				{
					section = group.SectionGroups.Count > 0
						? GetSectionFromGroups(sectionName, group.SectionGroups)
						: group.Sections[sectionName];
					if (section != null)
						break;
				}
			}
			catch (FileNotFoundException ex)
			{
				if (ex.Source != "System.Configuration")
					throw;
			}
			return section;
		}
		#endregion
		#region 公开方法
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sectionName"></param>
		/// <returns></returns>
		public static ConfigurationSection GetSection(string sectionName)
		{
			ConfigurationSection section = new AnonymousIdentificationSection();
			//实际中应该加上缓存，本次不加

			ConfigFilesSetting settings = loadFilesSetting(null);

			System.Configuration.Configuration config = getFinalConfiguration(settings);
			section =  config.GetSection(sectionName);

			if (section == null || section is DefaultSection)
				section = GetSectionFromGroups(sectionName, config.SectionGroups);

			 
			return section;
		}

		
		#endregion
	}


}
