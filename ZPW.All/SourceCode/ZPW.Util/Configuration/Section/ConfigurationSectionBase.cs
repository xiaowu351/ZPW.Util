using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using ZPW.Util.Core;

namespace ZPW.Util.Configuration.Section
{
	/// <summary>
	/// 分支节点抽象基类
	/// </summary>
	/// <typeparam name="T">泛型对象</typeparam>
	public abstract class ConfigurationSectionBase<T> : ConfigurationSection where T : ConfigurationSectionBase<T>
	{
		private readonly string  _configNodeName = string.Empty;
		private readonly bool _allowNullValue = false;
		private static readonly Lazy<T> instance = new Lazy<T>(() =>
		{
			var ctors = typeof (T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			CoreHelper.ExceptionHelper.TrueThrow<InvalidOperationException>(ctors.Count()!=1,"类型：{0}必须具有一个构造函数",typeof(T));
			var ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0);
			CoreHelper.ExceptionHelper.TrueThrow<InvalidOperationException>(ctor==null,"类型：{0}必须具有0个参数的构造函数",typeof(T));

			return ctor.Invoke(null) as T;

		});

		/// <summary>
		/// 单例模式
		/// </summary>
		public static T Instance
		{
			get { return instance.Value.GetConfigurationSection(); }
		}

		/// <summary>
		/// 配置节点名称
		/// </summary>
		public string ConfigNodeName
		{
			get { return _configNodeName; }
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configNodeName">配置节点名称</param>
		public ConfigurationSectionBase(string configNodeName):this(configNodeName,false)
		{}

		/// <summary>
		/// 构造函数。
		/// </summary>
		/// <param name="configNodeName">配置节点名称</param>
		/// <param name="allowNullValue">是否允许为null</param>
		public ConfigurationSectionBase(string configNodeName,bool allowNullValue)
		{
			CoreHelper.ExceptionHelper.TrueThrow<ArgumentNullException>(string.IsNullOrWhiteSpace(configNodeName),"{0} 配置节点名称不能为空",configNodeName);
			_configNodeName = configNodeName;
			_allowNullValue = allowNullValue;
		}

		/// <summary>
		/// 获取配置节点对象
		/// </summary>
		/// <returns></returns>
		protected T GetConfigurationSection()
		{
			T config = ConfigurationBroker.GetSection(ConfigNodeName) as T;
			CoreHelper.ExceptionHelper.TrueThrow(config == null && _allowNullValue==false,"找不到配置节点{0}",ConfigNodeName);
			return config;
		}
	}
}
