using System.Collections.Generic;
using System.Configuration;
using ZPW.Util.Core;
using ZPW.Util.Properties;

namespace ZPW.Util.Configuration
{
	/// <summary>
	/// 以名字为键值的配置元素集合
	/// </summary>
	public abstract class NamedConfigurationElementCollection<T> : ConfigurationElementCollection
		where T : NamedConfigurationElement, new()
	{
		/// <summary>
		/// 按照序号获取指定的配置元素
		/// </summary>
		/// <param name="index">序号</param>
		/// <returns>配置元素</returns>
		public virtual T this[int index]
		{
			get { return (T)BaseGet(index); }
		}

		/// <summary>
		/// 按照名称获取长度指定的配置元素
		/// </summary>
		/// <param name="name">名称</param>
		/// <returns>配置元素</returns>
		public new T this[string name]
		{
			get { return (T)BaseGet(name); }
		}

		/// <summary>
		/// 是否包含指定的配置元素
		/// </summary>
		/// <param name="name">配置元素名称</param>
		/// <returns>包含返回true，否则false</returns>
		public bool ContainsKey(string name)
		{
			return BaseGet(name) != null;
		}

		/// <summary>
		/// 通过name在字典内查找数据。如果name不存在，则抛出异常。
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		protected virtual T InnerGet(string name)
		{
			object element = BaseGet(name);
			CoreHelper.ExceptionHelper.TrueThrow<KeyNotFoundException>(element == null, Resource.CanNotFindNamedConfigElement, name);
			return (T)element;
		}

		/// <summary>
		/// 生成新的配置元素实例
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		/// <summary>
		/// 得到元素的Key值
		/// </summary>
		/// <param name="element">配置元素</param>
		/// <returns>配置元素所对应的配置元素</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((T)element).Name;
		}
	}
}
