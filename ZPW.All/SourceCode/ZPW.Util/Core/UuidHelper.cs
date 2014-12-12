using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ZPW.Util.Core
{
	/// <summary>
	/// 核心操作类
	/// </summary>
	public static partial class CoreHelper
	{
		/// <summary>
		/// 辅助生成连续的UUID类
		/// </summary>
		public static class UuidHelper
		{
			

			/// <summary>
			/// 生成连续的UUID，底层调用了Windows API UuidCreateSequential。 
			/// </summary>
			/// <returns>本机生成连续的Guid</returns>
			/// <remarks>
			/// 经测试发现：UuidCreateSequential，在多CPU并发状态下，有可能会产生重复数据，因此这个方法进行的并发控制，延迟1毫秒。另外，UuidCreateSequential的生成和网络连接有关（网卡个数），如果电脑上插上了Windows Mobile的手机，会产生新的网络连接，导致UuidCreateSequential出错，此时，这个方法将使用传统的Guid来替代Uuid.
			/// 2014.12.05测试发现，UuidCreateSequential此API函数与网络连接数无关，并且可以生成连续的GUID
			/// <code source="..\TestResults\ZPW.Util.Test\Core\UuidHelperTest.cs"  lang="cs" title="检查字符串参数是否为null或空串，如果是，则抛出异常"/>
			/// </remarks>
			public static Guid NewGuid()
			{
				Guid result;
				lock (typeof(UuidHelper))
				{
					int hr = APIHelper.UuidCreateSequential(out result);
					//if (hr == 0)
					//	result = Guid.NewGuid();

					Thread.Sleep(1);
				}
				return result;
			}

			/// <summary>
			/// 生成连续的Uuid，底层调用了Windows API UuidCreateSequential
			/// </summary>
			/// <returns>生成连续的Guid</returns>
			public static string NewUuidString()
			{
				Guid result = NewGuid();

				byte[] guidBytes = result.ToByteArray();
				for (int i = 0; i < 8; i++)
				{
					byte t = guidBytes[15 - i];
					guidBytes[15 - i] = guidBytes[i];
					guidBytes[i] = t;
				}

				return new Guid(guidBytes).ToString();
			}
		}
	}
}
