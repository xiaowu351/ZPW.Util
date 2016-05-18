using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace ZPW.Util.Core
{
    /// <summary>
    /// 核心帮助类
    /// </summary>
    public static partial class CoreHelper {
        /// <summary>
        /// Windows API 帮助类
        /// </summary>
        public static class APIHelper
		{
			/// <summary>
			/// 生成连续的UUID
			/// </summary>
			/// <param name="guid">返回结果guid</param>
			/// <returns>返回结果：0表示失败</returns>
			[DllImport("rpcrt4.dll", SetLastError = true)]
			public static extern int UuidCreateSequential(out Guid guid);

			/// <summary>
			/// 销毁句柄
			/// </summary>
			/// <param name="hWndPtr">句柄对象</param>
			/// <returns>成功返回true，否则false</returns>
			[DllImport("kernel32.dll")]
			public static extern bool CloseHandle(IntPtr hWndPtr);

			/// <summary>
			/// 读取或者写指定路径下的文件，如果文件读取失败，则返回IntPtr(-1)对象
			/// </summary>
			/// <param name="lpPathName">文件全路径名</param>
			/// <param name="iReadWrite">读或者写整数值</param>
			/// <returns>返回0表示成功。其他任何值都代表一个错误代码</returns>
			[DllImport("kernel32.dll")]
			public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

			/// <summary>
			/// 这个函数可以创建或打开一个对象的句柄，凭借此句柄就可以控制这些对象：控制台对象、通信资源对象、目录对象(只能打开)、磁盘设备对象、文件对象、邮槽对象、管道对象
			/// </summary>
			/// <param name="lpFileName">文件或者文件夹</param>
			/// <param name="dwDesiredAccess">访问模式</param>
			/// <param name="dwShareMode">共享模式</param>
			/// <param name="lpSecurityAttributes">安全属性，即销毁方式</param>
			/// <param name="dwCreationDisposition">怎么创建</param>
			/// <param name="dwFlagsAndAttributes">文件属性</param>
			/// <param name="hTemplateFile">模版文件句柄</param>
			/// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.</returns>
			/// <remarks>
			/// 详细参数详解见：http://msdn.microsoft.com/en-us/library/windows/desktop/aa363858(v=vs.85).aspx
			/// </remarks>
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		}
	}
}
