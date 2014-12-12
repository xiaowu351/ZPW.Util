using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace ZPW.Util.Core
{
	public static partial class CoreHelper
	{
		/// <summary>
		/// 提供Uri相关处理的函数。
		/// </summary>
		public static class UriHelper
		{
			/// <summary>
			/// 分析Url,得到所有的参数集合
			/// </summary>
			/// <param name="url">Uri类型的Url，绝对路径或者相对路径</param>
			/// <returns>NameValueCollection参数集合</returns>
			public static NameValueCollection GetUriParamCollection(Uri url)
			{
				ExceptionHelper.TrueThrow<ArgumentNullException>(url == null, "url");
				return GetUriParamCollection(url.ToString());
			}

			/// <summary>
			/// 从Url中获取参数的集合
			/// </summary>
			/// <param name="uriValue">url地址字符串</param>
			/// <returns>NameValueCollection参数集合</returns>
			public static NameValueCollection GetUriParamCollection(string uriValue)
			{
				ExceptionHelper.CheckStringIsNullOrEmpty(uriValue, "uriValue");

				NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);

				string bookmarkString = GetBookmarkStringInUrl(uriValue);
				if (false == string.IsNullOrWhiteSpace(bookmarkString))
				{
					uriValue = uriValue.Remove(uriValue.Length - bookmarkString.Length, bookmarkString.Length);
				}
				string query = uriValue;

				int startIndex = query.IndexOf("?", StringComparison.Ordinal);
				if (startIndex >= 0)
				{
					query = query.Substring(startIndex + 1);
				}

				if (false == string.IsNullOrWhiteSpace(query))
				{
					string[] parts = query.Split('&');
					for (int i = 0; i < parts.Length; i++)
					{
						int equalsSignIndex = parts[i].IndexOf("=", StringComparison.Ordinal);
						string paramName = string.Empty;
						string paramValue;
						if (equalsSignIndex >= 0)
						{
							paramName = HttpUtility.UrlDecode(parts[i].Substring(0, equalsSignIndex));
							paramValue = HttpUtility.UrlDecode(parts[i].Substring(equalsSignIndex + 1));
						}
						else
						{
							paramValue = HttpUtility.UrlDecode(parts[i]);
						}
						AddValueToCollection(paramName, paramValue, result);
					}
				}
				return result;
			}



			/// <summary>
			/// 得到url中的最后书签部分，“#”后面的部分。通常放在url最后
			/// </summary>
			/// <param name="queryString">http://localhost/zhoupingwu?test=1#littleTurtle</param>
			/// <returns>littleTurtle</returns>
			public static string GetBookmarkStringInUrl(string queryString)
			{
				ExceptionHelper.CheckStringIsNullOrEmpty(queryString, "queryString");

				int bookmarkStart = -1;
				for (int i = queryString.Length - 1; i >= 0; i--)
				{
					if (queryString[i] == '#')
						bookmarkStart = i;
					else if (queryString[i] == '&' || queryString[i] == '?')
						break;
				}
				string result = string.Empty;
				if (bookmarkStart > 0)
					result = queryString.Substring(bookmarkStart);
				return result;
			}


			/// <summary>
			/// 添加paramName键的值到result中。如果键已存在，则会追加到该键后面以“,”隔开。
			/// </summary>
			/// <param name="paramName">键</param>
			/// <param name="paramValue">值</param>
			/// <param name="result">键值集合</param>
			private static void AddValueToCollection(string paramName, string paramValue, NameValueCollection result)
			{
				string oriValue = result[paramName];
				if (oriValue == null)
				{
					result.Add(paramName, paramValue);
				}
				else
				{
					string rValue = oriValue;
					if (oriValue.Length > 0)
						rValue += ",";
					rValue += paramValue;
					result[paramName] = rValue;
				}
			}
		}
	}

}
