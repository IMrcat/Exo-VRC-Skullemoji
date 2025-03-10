using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.MenuOverrides;
using EXO.LogTools;
using UnityEngine;

namespace EXO.Modules
{
	// Token: 0x0200006D RID: 109
	internal class LogReader : ModsModule
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00014B1C File Offset: 0x00012D1C
		internal static void Init()
		{
			bool flag = !Config.cfg.QMConsole;
			if (!flag)
			{
				bool flag2 = !File.Exists(LogReader.filePath);
				if (flag2)
				{
					CoroutineManager.RunCoroutine(LogReader.WaitForFile());
				}
				else
				{
					LogReader.SetupWatcher();
				}
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014B61 File Offset: 0x00012D61
		private static IEnumerator WaitForFile()
		{
			while (!File.Exists(LogReader.filePath))
			{
				yield return new WaitForSeconds(0.1f);
			}
			LogReader.SetupWatcher();
			yield break;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014B6C File Offset: 0x00012D6C
		private static void SetupWatcher()
		{
			bool flag = LogReader.watcher != null;
			if (flag)
			{
				LogReader.watcher.Dispose();
				LogReader.watcher = null;
			}
			LogReader.watcher = new FileSystemWatcher
			{
				Path = Path.GetDirectoryName(LogReader.filePath),
				Filter = Path.GetFileName(LogReader.filePath),
				NotifyFilter = 27,
				EnableRaisingEvents = true
			};
			FileSystemWatcher fileSystemWatcher = LogReader.watcher;
			FileSystemEventHandler fileSystemEventHandler;
			if ((fileSystemEventHandler = LogReader.<>O.<0>__OnChanged) == null)
			{
				fileSystemEventHandler = (LogReader.<>O.<0>__OnChanged = new FileSystemEventHandler(LogReader.OnChanged));
			}
			fileSystemWatcher.Changed += fileSystemEventHandler;
			FileSystemWatcher fileSystemWatcher2 = LogReader.watcher;
			FileSystemEventHandler fileSystemEventHandler2;
			if ((fileSystemEventHandler2 = LogReader.<>O.<0>__OnChanged) == null)
			{
				fileSystemEventHandler2 = (LogReader.<>O.<0>__OnChanged = new FileSystemEventHandler(LogReader.OnChanged));
			}
			fileSystemWatcher2.Created += fileSystemEventHandler2;
			CLog.L("File System Watcher setup complete.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogReader.cs", 61);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00014C35 File Offset: 0x00012E35
		private static void OnChanged(object sender, FileSystemEventArgs e)
		{
			CoroutineManager.RunCoroutine(LogReader.HandleFileChange());
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014C42 File Offset: 0x00012E42
		private static IEnumerator HandleFileChange()
		{
			yield return new WaitForSeconds(0.1f);
			string newText = LogReader.ReadNewTextFromFile(LogReader.filePath);
			bool flag = !string.IsNullOrEmpty(newText);
			if (flag)
			{
				QMConsole.LogToQM(newText);
			}
			yield break;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00014C4C File Offset: 0x00012E4C
		private static string ReadNewTextFromFile(string filePath)
		{
			string newText = "";
			for (;;)
			{
				try
				{
					long newLength = new FileInfo(filePath).Length;
					bool flag = newLength > LogReader.lastLength;
					if (flag)
					{
						using (FileStream fs = new FileStream(filePath, 3, 1, 3))
						{
							fs.Seek(LogReader.lastLength, 0);
							byte[] buffer = new byte[newLength - LogReader.lastLength];
							fs.Read(buffer, 0, buffer.Length);
							newText = Encoding.Default.GetString(buffer);
							newText = LogReader.ConvertToUnityRichText(newText);
							LogReader.lastLength = newLength;
						}
					}
					break;
				}
				catch (IOException ex)
				{
					CLog.E("Error reading file: " + ex.Message, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogReader.cs", 97);
				}
			}
			return newText;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00014D30 File Offset: 0x00012F30
		internal static string ConvertToUnityRichText(string ansiText)
		{
			string result = LogReaderRegex.AnsiColorRegex().Replace(ansiText, delegate(Match m)
			{
				int r = int.Parse(m.Groups[1].Value);
				int g = int.Parse(m.Groups[2].Value);
				int b = int.Parse(m.Groups[3].Value);
				string text = ((m.Index > 0) ? "</color>" : "");
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(9, 3);
				defaultInterpolatedStringHandler.AppendLiteral("<color=#");
				defaultInterpolatedStringHandler.AppendFormatted<int>(r, "X2");
				defaultInterpolatedStringHandler.AppendFormatted<int>(g, "X2");
				defaultInterpolatedStringHandler.AppendFormatted<int>(b, "X2");
				defaultInterpolatedStringHandler.AppendLiteral(">");
				return text + defaultInterpolatedStringHandler.ToStringAndClear();
			});
			return result + "</color>";
		}

		// Token: 0x040001DE RID: 478
		private static long lastLength = 0L;

		// Token: 0x040001DF RID: 479
		private static readonly string filePath = "C:\\HexedTools\\Log.txt";

		// Token: 0x040001E0 RID: 480
		private static FileSystemWatcher watcher;

		// Token: 0x040001E1 RID: 481
		private static readonly Regex AnsiColorRegex = new Regex("\\x1b\\[38;2;(\\d+);(\\d+);(\\d+)m", 8);

		// Token: 0x02000145 RID: 325
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040005CC RID: 1484
			public static FileSystemEventHandler <0>__OnChanged;
		}
	}
}
