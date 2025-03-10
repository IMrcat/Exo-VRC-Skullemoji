using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using Transmtn;
using Transmtn.DTO;
using WCv2.Components.WorldPatches;
using WebSocketSharp;

namespace EXO.Patches
{
	// Token: 0x02000041 RID: 65
	internal class PhoneBookPatch : PatchModule
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000BD31 File Offset: 0x00009F31
		public override void LoadPatch()
		{
			MethodInfo method = typeof(PhoneBook).GetMethod("Handle");
			Action<PhoneBook, User, FriendMessageType> action;
			if ((action = PhoneBookPatch.<>O.<0>__APIHandles) == null)
			{
				action = (PhoneBookPatch.<>O.<0>__APIHandles = new Action<PhoneBook, User, FriendMessageType>(PhoneBookPatch.APIHandles));
			}
			PatchHandler.Detour(method, action);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BD68 File Offset: 0x00009F68
		private static void APIHandles(PhoneBook instance, User __0, FriendMessageType __1)
		{
			bool flag = __0 == null || !Config.cfg.LogFriendActivity || __0.displayName.IsNullOrEmpty();
			if (!flag)
			{
				string usersName = __0.displayName;
				try
				{
					switch (__1)
					{
					case FriendMessageType.Add:
						CLog.L(usersName + " added you.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 64);
						break;
					case FriendMessageType.Delete:
						CLog.L(usersName + " has unadded you.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 68);
						break;
					case FriendMessageType.Location:
					{
						bool flag2 = !Config.cfg.LogFriendLocations || __0.location == null;
						if (!flag2)
						{
							Location location = __0.location;
							bool isTraveling = location.isTraveling;
							if (isTraveling)
							{
								CLog.L(usersName + " is traveling to another world.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 78);
							}
							bool isPrivate = location.isPrivate;
							if (isPrivate)
							{
								CLog.L(usersName + " is in a private world.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 81);
							}
							else
							{
								bool flag3 = location.WorldId != null;
								if (flag3)
								{
									CLog.L(usersName + " is in " + WorldWrapper.ApiWorld.name, Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 83);
								}
							}
							bool isWeb = location.isWeb;
							if (isWeb)
							{
								CLog.L(usersName + " is on the web.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 86);
							}
							bool flag4 = !Config.cfg.LogFriendInstanceInfo;
							if (!flag4)
							{
								bool isPrivate2 = location.isPrivate;
								if (isPrivate2)
								{
									CLog.L("Unable to log friend " + usersName + " instance because they are in a private instance.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 92);
								}
								else
								{
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 4);
									defaultInterpolatedStringHandler.AppendFormatted(usersName);
									defaultInterpolatedStringHandler.AppendLiteral("'s instance and world is ");
									defaultInterpolatedStringHandler.AppendFormatted(WorldWrapper.ApiWorld.name);
									defaultInterpolatedStringHandler.AppendLiteral(" | ");
									defaultInterpolatedStringHandler.AppendFormatted(location.WorldId);
									defaultInterpolatedStringHandler.AppendLiteral(":");
									defaultInterpolatedStringHandler.AppendFormatted(location.Instance.Id);
									CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 94);
								}
							}
						}
						break;
					}
					case FriendMessageType.Online:
						CLog.L(usersName + " is Online on " + __0.last_platform, Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 56);
						break;
					case FriendMessageType.Offline:
						CLog.L(usersName + " went Offline.", Config.cfg.LogFriendsToHUD, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PhoneBookPatch.cs", 60);
						break;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x020000FF RID: 255
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000399 RID: 921
			public static Action<PhoneBook, User, FriendMessageType> <0>__APIHandles;
		}
	}
}
