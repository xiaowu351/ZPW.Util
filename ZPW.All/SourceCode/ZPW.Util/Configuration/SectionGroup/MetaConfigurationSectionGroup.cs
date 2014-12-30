using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ZPW.Util.Configuration.Section;

namespace ZPW.Util.Configuration.SectionGroup
{
	/// <summary>
	/// 自定义SectionGroup配置项
	/// </summary>
	internal sealed class MetaConfigurationSectionGroup : ConfigurationSectionGroup
	{ 
		/// <summary>
		/// 源配置映射配置项
		/// </summary>
		public MetaSourceMappingsConfigurationSection SourceMappings
		{
			get { return base.Sections[MetaSourceMappingsConfigurationSection.Name] as MetaSourceMappingsConfigurationSection; }
		}

	}
}
