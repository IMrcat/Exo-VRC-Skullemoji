using System;
using System.Runtime.CompilerServices;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;

namespace EXO_Engine
{
	// Token: 0x02000006 RID: 6
	[NullableContext(1)]
	[Nullable(0)]
	internal class DiscordClient
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002890 File Offset: 0x00000A90
		internal static void Init()
		{
			DiscordClient.presence = new DiscordRpcClient("983952959010381844", -1, null, true, null)
			{
				Logger = new ConsoleLogger(LogLevel.Trace, true)
			};
			DiscordClient.presence.OnReady += delegate(object sender, ReadyMessage msg)
			{
				DiscordClient.DiscordAccount = msg.User;
			};
			DiscordClient.presence.OnJoinRequested += delegate(object sender, JoinRequestMessage msg)
			{
				CLog.L("Discord User " + msg.User.Username + " has Requested to join", ConsoleColor.White, ConsoleColor.DarkRed);
			};
			DiscordClient.presence.Initialize();
		}

		// Token: 0x0400000B RID: 11
		internal static DiscordRpcClient presence;

		// Token: 0x0400000C RID: 12
		internal static User DiscordAccount;
	}
}
