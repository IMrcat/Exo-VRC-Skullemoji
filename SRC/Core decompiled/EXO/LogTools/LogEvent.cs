using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using VRC;
using WebSocketSharp;

namespace EXO.LogTools
{
	// Token: 0x02000088 RID: 136
	internal class LogEvent
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x0001C230 File Offset: 0x0001A430
		internal static void HandleEventLog(EventData __0)
		{
			byte code = __0.Code;
			byte b = code;
			if (b <= 12)
			{
				switch (b)
				{
				case 1:
				{
					bool flag = LogEvent.e1;
					if (flag)
					{
						LogEvent.PrintEvent(__0, "Uspeak");
					}
					break;
				}
				case 2:
				case 3:
				case 5:
					break;
				case 4:
				{
					bool flag2 = LogEvent.e4;
					if (flag2)
					{
						LogEvent.PrintEvent(__0, "Synced Event");
					}
					break;
				}
				case 6:
				{
					bool flag3 = LogEvent.e6;
					if (flag3)
					{
						LogEvent.PrintEvent(__0, "RPC");
					}
					break;
				}
				case 7:
				{
					bool flag4 = LogEvent.e7;
					if (flag4)
					{
						LogEvent.PrintEvent(__0, "Unreliable Serialz");
					}
					break;
				}
				default:
					if (b == 12)
					{
						bool flag5 = LogEvent.e12;
						if (flag5)
						{
							LogEvent.PrintEvent(__0, null);
						}
					}
					break;
				}
			}
			else if (b != 209)
			{
				if (b == 210)
				{
					bool flag6 = LogEvent.e210;
					if (flag6)
					{
						LogEvent.PrintEvent(__0, "Ownership transfer");
					}
				}
			}
			else
			{
				bool flag7 = LogEvent.e209;
				if (flag7)
				{
					LogEvent.PrintEvent(__0, "Ownership request");
				}
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001C348 File Offset: 0x0001A548
		private static void PrintEvent(EventData __0, string eventName = null)
		{
			Player player = PlayerWrapper.GetByActorID(__0.sender);
			byte[] eventData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);
			CLog.Tag();
			string text = "[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 59);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 1);
			defaultInterpolatedStringHandler.AppendLiteral("E");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(__0.Code);
			text.WriteToConsole(defaultInterpolatedStringHandler.ToStringAndClear(), 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 60).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 61);
			bool flag = eventName == null;
			if (flag)
			{
				"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 65).WriteToConsole(eventName, 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 66).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 67);
			}
			"Sender".WriteToConsole(13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 70).WriteToConsole(" - ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 71).WriteLineToConsole(player.DisplayName(), 13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 72);
			CLog.Tag();
			string text2 = "[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 76);
			defaultInterpolatedStringHandler..ctor(1, 1);
			defaultInterpolatedStringHandler.AppendLiteral("E");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(__0.Code);
			text2.WriteToConsole(defaultInterpolatedStringHandler.ToStringAndClear(), 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 77).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 78);
			bool flag2 = eventName.IsNullOrEmpty();
			if (flag2)
			{
				"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 82).WriteToConsole(eventName, 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 83).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 84);
			}
			"Base64 Message:".WriteLineToConsole(13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 87).WriteLineToConsole(Convert.ToBase64String(eventData), 7, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\LogEvent.cs", 88);
			GC.Collect();
		}

		// Token: 0x04000279 RID: 633
		internal static bool e1;

		// Token: 0x0400027A RID: 634
		internal static bool e4;

		// Token: 0x0400027B RID: 635
		internal static bool e6;

		// Token: 0x0400027C RID: 636
		internal static bool e7;

		// Token: 0x0400027D RID: 637
		internal static bool e12;

		// Token: 0x0400027E RID: 638
		internal static bool e209;

		// Token: 0x0400027F RID: 639
		internal static bool e210;
	}
}
