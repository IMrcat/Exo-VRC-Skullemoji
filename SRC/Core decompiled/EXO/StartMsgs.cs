using System;
using EXO.LogTools;
using HexedTools.HookUtils;

namespace EXO
{
	// Token: 0x02000034 RID: 52
	internal class StartMsgs
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000996C File Offset: 0x00007B6C
		internal static void Watermark()
		{
			Logs.WriteOut("", 15);
			Logs.WriteOut("", 15);
			Logs.WriteOut("", 15);
			CLog.L("[============================================================================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 32);
			CLog.R("                                                                              ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 33);
			CLog.R("                                   Cyconi", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 34);
			CLog.R("       [-]  $$$$\\       $$$$$$$$\\ $$\\   $$\\  $$$$$$\\        $$$$\\   [-] ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 35);
			CLog.R("       [-]  $$  _|      $$  _____|$$ |  $$ |$$  __$$\\       \\_$$ |  [-]     ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 36);
			CLog.R("       [-]  $$ |        $$ |      \\$$\\ $$  |$$ /  $$ |        $$ |  [-]     ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 37);
			CLog.R("       [-]  $$ |        $$$$$\\     \\$$$$  / $$ |  $$ |        $$ |  [-]     ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 38);
			CLog.R("       [-]  $$ |        $$  __|    $$  $$<  $$ |  $$ |        $$ |  [-]       ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 39);
			CLog.R("       [-]  $$ |        $$ |      $$  /\\$$\\ $$ |  $$ |        $$ |  [-]     ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 40);
			CLog.R("       [-]  $$$$\\       $$$$$$$$\\ $$ /  $$ | $$$$$$  |      $$$$ |  [-]     ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 41);
			CLog.R("       [-]  \\____|      \\________|\\__|  \\__| \\______/       \\____|  [-] ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 42);
			CLog.R("                                    v" + AppStart.release, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 43);
			CLog.R(StartMsgs.RngMsg() ?? "", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 44);
			CLog.L("[============================================================================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 45);
			Logs.WriteOut("", 15);
			Logs.WriteOut("", 15);
			Logs.WriteOut("", 15);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009AE8 File Offset: 0x00007CE8
		internal static string RngMsg()
		{
			string[] words = new string[] { "                    No longer a Demon, no less than a God", "                      You've got the Devil in your eyes", "                         I see the Devil in your eyes", "                               WCv2 - Buy Now!", "                              Demon of Deities...", "                             Deal with the Devil", "                           I'm a Demon among Deities...", "                              Blood of the Gods", "                              Welcome to Godhood" };
			Random random = new Random();
			int randomIndex = random.Next(words.Length);
			return words[randomIndex];
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009B64 File Offset: 0x00007D64
		internal static void Authorized(string username, string license)
		{
			bool flag = string.IsNullOrEmpty(license);
			if (flag)
			{
				license = "Lifetime";
			}
			Logs.WriteOut("", 15);
			CLog.R("[====== EXO Engine ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 80);
			CLog.L("", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 81);
			CLog.L("        Authorized        ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 82);
			CLog.L("      Welcome " + username + "  ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 83);
			CLog.L("     License " + license + "    ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 84);
			CLog.L("", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 85);
			CLog.R("[====== EXO Engine ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 86);
			Logs.WriteOut("", 15);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009C38 File Offset: 0x00007E38
		internal static void UnAuthorized()
		{
			Logs.WriteOut("", 15);
			CLog.R("[====== EXO Engine ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 92);
			CLog.L("", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 93);
			CLog.L("      Not Authorized      ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 94);
			CLog.L("", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 95);
			CLog.R("[====== EXO Engine ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\StartMsgs.cs", 96);
			Logs.WriteOut("", 15);
		}
	}
}
