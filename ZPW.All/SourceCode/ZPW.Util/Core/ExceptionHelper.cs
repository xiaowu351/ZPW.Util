using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Web;
using ZPW.Util.Properties;


namespace ZPW.Util.Core
{
	/// <summary>
	/// 核心帮助类
	/// </summary>
	public static partial class CoreHelper
	{
		/// <summary>
		/// Exception工具，提供了TrueThrow和FalseThrow等方法
		/// </summary>
		/// <remarks>
		/// Exception工具，TrueThrow方法判断它的布尔值参数值是否为true，若是则抛出异常；
		/// FalseThrow方法判断它的布尔值参数值是否为false，若是则抛出异常；
		/// </remarks>
		public static class ExceptionHelper
		{

			/// <summary>
			/// 检查字符串参数是否为null或空串，如果是，则抛出异常
			/// </summary>
			/// <param name="data">字符串参数</param>
			/// <param name="paramName">参数名称</param>
			/// <remarks>
			/// 若字符串参数为Null或者空串，抛出ArgumentException异常
			/// <code source="..\TestResults\ZPW.Util.Test\Core\ExceptionHelperTest.cs" region="CheckStringIsNullOrEmpty" lang="cs" title="检查字符串参数是否为null或空串，如果是，则抛出异常"/>
			/// </remarks>
			public static void CheckStringIsNullOrEmpty(string data, string paramName)
			{
				if (string.IsNullOrWhiteSpace(data))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
						ExceptionResource.StringParamCanNotBeNullOrEmpty, paramName));
				}
			}


			/// <summary>
			/// 如果条件表达式parseExpressionResult的结果为true，则抛出message指定的错误信息。
			/// 支持string.Format格式化错误信息
			/// </summary>
			/// <param name="parseExpressionResult">条件表达式</param>
			/// <param name="message">错误信息</param>
			/// <param name="messageParams">错误信息参数</param>
			/// <remarks>如果条件表达式parseExpressionResult的结果为true，则抛出message指定的错误信息。
			/// <code source="..\TestResults\ZPW.Util.Test\Core\ExceptionHelperTest.cs" region="TrueThrowTest" lang="cs" title="通过判断条件表达式的结果值判断是否抛出指定的异常信息"/>
			/// <seealso cref="FalseThrow"/>
			/// </remarks>
			/// <example>
			/// <code>
			/// ExceptionHelper.TrueThrow(name=="zhangsan","对不起，名字不能为{0}",name);
			/// </code>
			/// </example>
			public static void TrueThrow(bool parseExpressionResult, string message, params object[] messageParams)
			{

				TrueThrow<ApplicationException>(parseExpressionResult, message, messageParams);
			}

			/// <summary>
			/// 如果条件表达式parseExpressionResult的结果为true，则抛出message指定的错误信息。
			/// 支持string.Format格式化错误信息
			/// </summary>
			/// <typeparam name="T">异常的类型</typeparam>
			/// <param name="parseExpressionResult">条件表达式</param>
			/// <param name="message">错误信息</param>
			/// <param name="messageParams">错误消息参数</param>
			/// <remarks>如果条件表达式parseExpressionResult的结果为true，则抛出message指定的错误信息。
			/// <code source="..\TestResults\ZPW.Util.Test\Core\ExceptionHelperTest.cs" region="TrueThrowTest" lang="cs" title="通过判断条件表达式的结果值判断是否抛出指定的异常信息"/>
			/// <seealso cref="FalseThrow"/>
			/// </remarks>
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Desgin", "CA1004:GenericMethodsShouldProvideTypeParmeter"
				)]
			public static void TrueThrow<T>(bool parseExpressionResult, string message, params object[] messageParams)
				where T : Exception
			{
				if (!parseExpressionResult)
					return;
				Type exceptionType = typeof(T);

				if (message == null)
					throw new ArgumentNullException("message");

				Object obj = Activator.CreateInstance(exceptionType);

				ConstructorInfo constructorInfo = exceptionType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
					CallingConventions.HasThis, new Type[] { typeof(string) }, null);

				constructorInfo.Invoke(obj, new object[] { string.Format(CultureInfo.InvariantCulture, message, messageParams) });

				throw (Exception)obj;
			}

			/// <summary>
			/// 如果条件表达式parseExpressionResult的结果为false，则抛出message指定的错误信息。
			/// 支持string.Format格式化错误信息
			/// </summary>
			/// <param name="parseExpressionResult">条件表达式</param>
			/// <param name="message">错误信息</param>
			/// <param name="messageParams">错误信息参数</param>
			/// <remarks>如果条件表达式parseExpressionResult的结果为false，则抛出message指定的错误信息。
			/// <code source="..\TestResults\ZPW.Util.Test\Core\ExceptionHelperTest.cs" region="FalseThrowTest" lang="cs" title="通过判断条件表达式的结果值判断是否抛出指定的异常信息"/>
			/// <seealso cref="TrueThrow"/>
			/// </remarks>
			/// <example>
			/// <code>
			/// ExceptionHelper.TrueThrow(name!=="zhangsan","对不起，名字不能为{0}",name);
			/// </code>
			/// </example>
			public static void FalseThrow(bool parseExpressionResult, string message, params object[] messageParams)
			{
				TrueThrow(false == parseExpressionResult, message, messageParams);
			}

			/// <summary>
			/// 如果条件表达式parseExpressionResult的结果为false，则抛出message指定的错误信息。
			/// 支持string.Format格式化错误信息
			/// </summary>
			/// <typeparam name="T">异常的类型</typeparam>
			/// <param name="parseExpressionResult">条件表达式</param>
			/// <param name="message">错误信息</param>
			/// <param name="messageParams">错误信息参数</param>
			/// <remarks>如果条件表达式parseExpressionResult的结果为false，则抛出message指定的错误信息。
			/// <code source="..\TestResults\ZPW.Util.Test\Core\ExceptionHelperTest.cs" region="FalseThrowTest" lang="cs" title="通过判断条件表达式的结果值判断是否抛出指定的异常信息"/>
			/// <seealso cref="TrueThrow"/>
			/// </remarks>
			/// <example>
			/// <code>
			/// ExceptionHelper.TrueThrow&lt;ApplicationException&gt;(name!=="zhangsan","对不起，名字不能为{0}",name);
			/// </code>
			/// </example>
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Desgin", "CA1004:GenericMethodsShouldProvideTypeParmeter"
				)]
			public static void FalseThrow<T>(bool parseExpressionResult, string message, params object[] messageParams)
				where T : Exception
			{
				TrueThrow<T>(false == parseExpressionResult, message, messageParams);
			}

			/// <summary>
			/// 从Exception对象中，获取真正发生错误的错误对象。
			/// </summary>
			/// <param name="ex">Exception对象</param>
			/// <returns>真正发生错误的Exception对象</returns>
			public static Exception GetRealException(Exception ex)
			{
				Exception lasterException = ex;

				while (ex != null && (ex is HttpUnhandledException || ex is HttpException || ex is TargetInvocationException))
				{
					lasterException = ex.InnerException ?? ex;

					ex = ex.InnerException;
				}
				return lasterException;
			}

			/// <summary>
			/// 获取异常的详细信息
			/// </summary>
			/// <param name="e">异常对象</param>
			/// <returns>返回异常的详细信息</returns>
			public static string GetExceptionDetail(Exception e)
			{
				StringBuilder builder = new StringBuilder();

				builder.AppendFormat("在应用程序域\"{0}\"中发生异常！\r\n", AppDomain.CurrentDomain.FriendlyName);

				//带检查
				for (Exception innerExp = e; innerExp != null; innerExp = innerExp.InnerException)
				{
					builder.AppendFormat("-----------------{0}:{1}-----------------\r\n{2}\r\n", innerExp.GetType().Name,
						innerExp.Message, innerExp.StackTrace);
					builder.AppendFormat("{0}\r\n", HandleException(innerExp));
				}
				return builder.ToString();
			}

			/// <summary>
			/// 解析异常类型
			/// </summary>
			/// <param name="innerExp">Exception对象</param>
			/// <returns>返回详细信息</returns>
			private static string HandleException(Exception innerExp)
			{
				StringBuilder builder = new StringBuilder();

				while (innerExp != null)
				{
					//服务端异常
					FaultException<string> faultEx = innerExp as FaultException<string>;
					if (faultEx != null)
					{
						builder.AppendFormat("-------------------{0}：{1}-----------------\r\n", faultEx.GetType().Name, faultEx.Message);
						builder.AppendLine(faultEx.Detail);
						break;
					}

					//SOAP 错误
					FaultException faultEx1 = innerExp as FaultException;
					if (faultEx1 != null)
					{
						builder.AppendLine(faultEx1.ToString());
						break;
					}

					//给进程或操作分配的时间过期时引发的异常
					TimeoutException timeoutEx = innerExp as TimeoutException;
					if (timeoutEx != null)
					{
						builder.AppendLine("连接服务器发生异常，超时！");
						builder.AppendLine(timeoutEx.ToString());
						break;
					}

					//服务或客户端应用程序中的通信错误。
					CommunicationException commuEx = innerExp as CommunicationException;
					if (commuEx != null)
					{
						builder.AppendLine("连接服务器发生异常，通信错误！");
						builder.AppendLine(commuEx.ToString());
						break;
					}

					builder.AppendFormat("-----------------{0} : {1}--------------------\r\n{2}", innerExp.GetType().Name,
						innerExp.Message, innerExp.StackTrace);
					innerExp = innerExp.InnerException;

				}
				return builder.ToString();
			}
		}
	}
}
