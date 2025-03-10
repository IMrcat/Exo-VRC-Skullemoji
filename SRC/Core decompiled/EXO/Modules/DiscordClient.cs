using System;
using System.Runtime.CompilerServices;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.API;
using EXO.Patches;
using EXO.Wrappers;
using VRC;
using VRC.Core;

namespace EXO.Modules
{
	// Token: 0x0200006E RID: 110
	internal class DiscordClient : FunctionModule
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00014DA4 File Offset: 0x00012FA4
		internal static void Init()
		{
			DiscordClient.presence = new DiscordRpcClient("983952959010381844")
			{
				Logger = new ConsoleLogger(LogLevel.Trace, true)
			};
			DiscordClient.presence.OnReady += delegate(object sender, ReadyMessage msg)
			{
				DiscordClient.discordAccount = msg.User;
			};
			DiscordClient.presence.OnJoinRequested += delegate(object sender, JoinRequestMessage msg)
			{
				CLog.L("Discord User " + msg.User.Username + " has Requested to join", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\DiscordClient.cs", 32);
			};
			DiscordClient.presence.Initialize();
			DiscordClient.startTime = Timestamps.Now;
			DiscordClient.richPresence = new RichPresence
			{
				Details = "[" + StartMsgs.RngMsg() + " ]",
				State = "[Loading...]",
				Assets = DiscordClient.assets,
				Timestamps = DiscordClient.startTime,
				Buttons = DiscordClient.buttons
			};
			DiscordClient.presence.SetPresence(DiscordClient.richPresence);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014EA0 File Offset: 0x000130A0
		public override void OnUpdate()
		{
			try
			{
				bool flag = !EventPatch.Ready;
				if (!flag)
				{
					ApiWorld world = WorldWrapper.ApiWorld;
					APIUser apiUser = PlayerWrapper.LocalAPIUser;
					string instanceType = DiscordClient.GetInstanceAccess(WorldWrapper.ApiWorldInstance.type);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
					defaultInterpolatedStringHandler.AppendLiteral("[");
					defaultInterpolatedStringHandler.AppendFormatted(DiscordClient.GetUserStatus(apiUser.statusValue));
					defaultInterpolatedStringHandler.AppendLiteral("] [");
					defaultInterpolatedStringHandler.AppendFormatted<UserRank>(EXO.Modules.API.User.GetRank(apiUser));
					defaultInterpolatedStringHandler.AppendLiteral("] [");
					defaultInterpolatedStringHandler.AppendFormatted(DiscordClient.Platform());
					defaultInterpolatedStringHandler.AppendLiteral("]");
					string details = defaultInterpolatedStringHandler.ToStringAndClear();
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
					defaultInterpolatedStringHandler.AppendLiteral("[");
					defaultInterpolatedStringHandler.AppendFormatted(instanceType);
					defaultInterpolatedStringHandler.AppendLiteral("] [");
					defaultInterpolatedStringHandler.AppendFormatted(world.name);
					defaultInterpolatedStringHandler.AppendLiteral("]");
					string state = defaultInterpolatedStringHandler.ToStringAndClear();
					DiscordClient.party.Size = PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.Count;
					DiscordClient.party.Max = (instanceType.Equals("Loading...") ? 0 : world.capacity);
					DiscordClient.richPresence.Party = DiscordClient.party;
					DiscordClient.richPresence.Details = details;
					DiscordClient.richPresence.State = state;
					DiscordClient.presence.SetPresence(DiscordClient.richPresence);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001503C File Offset: 0x0001323C
		private static string Platform()
		{
			bool flag = !PlayerWrapper.IsInVR();
			string text;
			if (flag)
			{
				text = "Desktop";
			}
			else
			{
				bool flag2 = Player.prop_Player_0.IsFBT();
				if (flag2)
				{
					text = "FBT";
				}
				else
				{
					text = "VR";
				}
			}
			return text;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015080 File Offset: 0x00013280
		private static string GetInstanceAccess(InstanceAccessType instanceType)
		{
			if (!true)
			{
			}
			string text;
			switch (instanceType)
			{
			case InstanceAccessType.Public:
				text = "Public";
				break;
			case InstanceAccessType.FriendsOfGuests:
				text = "Friends+";
				break;
			case InstanceAccessType.FriendsOnly:
				text = "Friends";
				break;
			case InstanceAccessType.InviteOnly:
				text = "Invite";
				break;
			case InstanceAccessType.InvitePlus:
				text = "Invite+";
				break;
			default:
				text = "Loading...";
				break;
			}
			if (!true)
			{
			}
			return text;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000150E8 File Offset: 0x000132E8
		private static string GetUserStatus(APIUser.UserStatus userStatus)
		{
			if (!true)
			{
			}
			string text;
			switch (userStatus)
			{
			case APIUser.UserStatus.Online:
				text = "Online";
				break;
			case APIUser.UserStatus.JoinMe:
				text = "Join Me";
				break;
			case APIUser.UserStatus.AskMe:
				text = "Ask Me";
				break;
			case APIUser.UserStatus.Offline:
				text = "Offline";
				break;
			case APIUser.UserStatus.DoNotDisturb:
				text = "Do Not Disturb";
				break;
			default:
				text = "Unknown";
				break;
			}
			if (!true)
			{
			}
			return text;
		}

		// Token: 0x040001E2 RID: 482
		internal static DiscordRpcClient presence;

		// Token: 0x040001E3 RID: 483
		internal static global::DiscordRPC.User discordAccount;

		// Token: 0x040001E4 RID: 484
		private static RichPresence richPresence;

		// Token: 0x040001E5 RID: 485
		private static Timestamps startTime;

		// Token: 0x040001E6 RID: 486
		private static readonly Button[] buttons = new Button[]
		{
			new Button
			{
				Label = "EXO",
				Url = "https://discord.gg/ymDBSPVERh"
			}
		};

		// Token: 0x040001E7 RID: 487
		private static readonly Assets assets = new Assets
		{
			LargeImageKey = "1pfp",
			LargeImageText = "EXO"
		};

		// Token: 0x040001E8 RID: 488
		private static readonly Party party = new Party
		{
			ID = Guid.NewGuid().ToString()
		};
	}
}
