using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ZPW.Util.Core
{
	/// <summary>
	/// 核心帮助类
	/// </summary>
	public static partial class CoreHelper
	{
		/// <summary>
		///与版本无关的XML序列化转化类
		/// </summary>
		public static class SerializationHelper
		{
			/// <summary>
			/// 将对象object转换成JSON格式字符串
			/// </summary>
			/// <param name="obj">待转对象</param>
			/// <returns>对象的JSON字符串形式</returns>
			public static string JsonSerialize(object obj)
			{
				return JsonConvert.SerializeObject(obj);
			}

			/// <summary>
			/// 将json格式字符串转换成指定类型T对象
			/// </summary>
			/// <typeparam name="T">泛型T</typeparam>
			/// <param name="json">待转json字符串</param>
			/// <returns>返回实际T类型对象</returns>
			public static T JsonDeserialize<T>(string json)
			{
				if (string.IsNullOrWhiteSpace(json))
				{
					return default(T);
				}
				return JsonConvert.DeserializeObject<T>(json);
			}

			/// <summary>
			/// 将json格式字符串转换成object对象
			/// </summary>
			/// <param name="json">待转json字符串</param>
			/// <returns>返回Object对象</returns>
			public static object JsonDeserialize(string json)
			{
				if (string.IsNullOrWhiteSpace(json))
				{
					return null;
				}
				return JsonConvert.DeserializeObject(json);
			}

			/// <summary>
			/// 将json格式字符串转成object对象
			/// </summary>
			/// <param name="json">待转json字符串</param>
			/// <param name="type">指定type类型</param>
			/// <returns>返回Object对象</returns>
			public static object JsonDesrialize(string json, Type type)
			{
				if (type == null)
				{
					return null;
				}
				if (string.IsNullOrWhiteSpace(json))
				{
					return null;
				}
				return JsonConvert.DeserializeObject(json, type);
			}

			/// <summary>
			/// 将object对象XML序列化成字符串格式
			/// </summary>
			/// <param name="obj">待序列化obj对象</param>
			/// <returns>Xml序列化字符串</returns>
			public static string XmlSerialize(object obj)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				using (XmlWriter writer = XmlWriter.Create(memoryStream, new XmlWriterSettings()
				{
					OmitXmlDeclaration = false,
					Encoding = Encoding.UTF8
				}))
				{
					XmlSerializer xs = new XmlSerializer(obj.GetType());
					XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
					ns.Add(string.Empty, string.Empty);
					xs.Serialize(writer, obj, ns);
					var xml = Encoding.UTF8.GetString(memoryStream.GetBuffer());
					return xml.TrimEnd('\0');
				}
			}
			/// <summary>
			/// XML反序列化对象
			/// </summary>
			/// <typeparam name="T">泛型T</typeparam>
			/// <param name="xml">序列化字符串</param>
			/// <returns>返回序列化 T 对象</returns>
			public static T XmlDeserialize<T>(string xml)
			{
				return XmlDeserialize(xml, typeof (T));
			}

			/// <summary>
			/// XML反序列化对象
			/// </summary>
			/// <param name="xml">序列化字符串</param>
			/// <param name="type">type类型</param>
			/// <returns>返回序列化对象</returns>
			public static dynamic XmlDeserialize(string xml, Type type)
			{
				using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
				using (XmlReader reader = XmlReader.Create(memoryStream))
				 
				{
					XmlSerializer xs = new XmlSerializer(type);
					return xs.Deserialize(reader);
				}
			}
		}
	}
}
