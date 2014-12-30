
using System.Configuration;
using ZPW.Util.Configuration.Element;

namespace ZPW.Util.Configuration.Section
{
	/// <summary>
	/// ConfigurationSection
	/// </summary>
	 class MetaSourceMappingsConfigurationSection : ConfigurationSectionBase<MetaSourceMappingsConfigurationSection>
	{
		/// <summary>
		/// Section配置节点名称
		/// </summary>
		public const string Name = "sourceMappings";

		private MetaSourceMappingsConfigurationSection()
			: base(Name)
		{
		}

		/// <summary>
		/// 所有实例的源映射元素集合
		/// </summary>
		[ConfigurationProperty("instances")]
		 public MetaInstanceConfigurationElementCollection Instances
		{
			get { return base["instances"] as MetaInstanceConfigurationElementCollection; }
		}
	}
}
