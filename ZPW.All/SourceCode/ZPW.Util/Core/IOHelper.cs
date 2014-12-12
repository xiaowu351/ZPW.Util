using System;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace ZPW.Util.Core
{

	public static partial class CoreHelper
	{
		/// <summary>
		/// Windows API 帮助类
		/// </summary>
		public static class IOHelper
		{
			#region Const定义
			private const uint GENERIC_READ = 0x80000000;
			private const uint GENERIC_WRITE = 0x40000000;
			private const uint FILE_SHARE_NONE = 0x00000000;
			private const uint FILE_SHARE_READ = 0x00000001;
			private const uint FILE_SHARE_WRITE = 0x00000002;
			private const uint FILE_SHARE_DELETE = 0x00000004;
			private const uint CREATE_NEW = 1;
			private const uint CREATE_ALWAYS = 2;
			private const uint OPEN_EXISTING = 3;
			private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

			private const int OF_READWRITE = 2;
			private const int OF_SHARE_DENY_NONE = 0x40;

			/// <summary>
			/// 错误句柄
			/// </summary>
			private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
			#endregion

			/// <summary>
			/// 检查文件夹是否被占用
			/// </summary>
			/// <param name="directoryInfo">待检查文件夹</param>
			/// <returns>占用返回true，否则返回false</returns>
			public static bool CheckDirectoryIsUsed(DirectoryInfo directoryInfo)
			{
				string fileName;
				return CheckDirectoryIsUsed(directoryInfo, out fileName);
			}

			/// <summary>
			/// 检查文件夹是否被占用，返回占用的文件名
			/// </summary>
			/// <param name="directoryInfo">文件夹</param>
			/// <param name="fileName">返回被占用的文件名</param>
			/// <returns>文件夹占用返回true,否则返回false</returns>
			public static bool CheckDirectoryIsUsed(DirectoryInfo directoryInfo, out string fileName)
			{
				fileName = string.Empty;
				if (null == directoryInfo || false == directoryInfo.Exists)
				{
					return false;
				}

				FileInfo[] files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
				IntPtr hWndPtr;
				foreach (FileInfo file in files)
				{
					hWndPtr = APIHelper._lopen(file.FullName, OF_READWRITE | OF_SHARE_DENY_NONE);
					if (hWndPtr == HFILE_ERROR)
					{
						fileName = file.FullName;
						APIHelper.CloseHandle(hWndPtr);
						return true;
					}
				}
				return false;
			}


			/// <summary>
			/// 独占指定文件夹或者文件。如果文件夹或者文件不存在，则返回null。
			/// 注意：如果文件占用成功，在使用完之后需要调用Close方法来释放资源。或者使用using来释放
			/// </summary>
			/// <param name="driverName">待占用文件</param>
			/// <returns>返回SafeFileHandle对象</returns>
			public static SafeFileHandle OccupyDirFileHandle(string driverName)
			{
				ExceptionHelper.TrueThrow<ArgumentNullException>(string.IsNullOrWhiteSpace(driverName), "文件不能为NULL或者空");
				return APIHelper.CreateFile(driverName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_NONE, IntPtr.Zero, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);

			}

		}
	}
}
