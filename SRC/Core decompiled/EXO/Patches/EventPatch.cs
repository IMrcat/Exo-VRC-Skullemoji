using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CoreRuntime.Manager;
using ExitGames.Client.Photon;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Photon.Realtime;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x0200003B RID: 59
	internal class EventPatch : PatchModule
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000B024 File Offset: 0x00009224
		public override void LoadPatch()
		{
			MethodInfo method = typeof(LoadBalancingClient).GetMethod("OnEvent");
			<>F{00000008}<LoadBalancingClient, EventData, bool> <>F{00000008};
			if ((<>F{00000008} = EventPatch.<>O.<0>__OnEvent) == null)
			{
				<>F{00000008} = (EventPatch.<>O.<0>__OnEvent = new <>F{00000008}<LoadBalancingClient, EventData, bool>(EventPatch.OnEvent));
			}
			PatchHandler.Detour(method, <>F{00000008});
			CoroutineManager.RunCoroutine(EventPatch.AutoClearE1DataList(24f));
			JoinLeavePatch.OnLocalPlayerJoin = (Action<global::VRC.Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerJoin, delegate(global::VRC.Player plr)
			{
				UtilFunc.Delay(5f, delegate
				{
					EventPatch.Ready = true;
				});
			});
			JoinLeavePatch.OnLocalPlayerLeave = (Action<global::VRC.Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerLeave, delegate(global::VRC.Player plr)
			{
				EventPatch.Ready = false;
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private static bool OnEvent(LoadBalancingClient instance, ref EventData eventData)
		{
			bool eventLog = EventPatch.EventLog;
			if (eventLog)
			{
				LogEvent.HandleEventLog(eventData);
			}
			bool flag = !EventPatch.Ready;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = EventPatch.allEventBlock.Contains(eventData.sender) && eventData.Code != 12 && eventData.Code != 1;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = (EventPatch.USpeakCopyAc == eventData.sender || EventPatch.MovmentCopyAc == eventData.sender) && (eventData.Code == 1 || eventData.Code == 12);
					if (flag4)
					{
						byte[] EventData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
						byte[] ServerTime = BitConverter.GetBytes(Networking.GetServerTimeInMilliseconds());
						Buffer.BlockCopy(BitConverter.GetBytes(PlayerWrapper.LocalVRCPlayerAPI.playerId), 0, EventData, 0, 4);
						Buffer.BlockCopy(ServerTime, 0, EventData, 4, 4);
					}
					bool flag5 = eventData.Code == 66 || eventData.Code == 12 || eventData.Code == 2 || eventData.Code == 17 || eventData.Code == 35;
					if (flag5)
					{
						flag2 = true;
					}
					else
					{
						bool flag6 = EventPatch.eventBlocking.ContainsKey(eventData.sender);
						if (flag6)
						{
							ValueTuple<int[], DateTime> val;
							EventPatch.eventBlocking.TryGetValue(eventData.sender, ref val);
							bool flag7 = val.Item2 < DateTime.Now;
							if (flag7)
							{
								CLog.L("Removing " + PlayerWrapper.GetByActorID(eventData.sender).DisplayName() + " From Tempt Event Block..", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\EventPatch.cs", 109);
								EventPatch.eventBlocking.Remove(eventData.sender);
							}
							else
							{
								bool flag8 = Enumerable.Any<int>(val.Item1, (int a) => a == (int)eventData.Code);
								if (flag8)
								{
									return false;
								}
							}
						}
						bool flag9 = EventPatch.E1Blocked.Contains(eventData.sender) && eventData.Code == 1;
						if (flag9)
						{
							flag2 = false;
						}
						else
						{
							bool flag10 = AntiBlock.IsBlocking(eventData);
							flag2 = !flag10;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B36C File Offset: 0x0000956C
		private static IEnumerator AutoClearE1DataList(float del = 24f)
		{
			for (;;)
			{
				yield return new WaitForSeconds(del);
				EventPatch.E1Flagged.Clear();
				EventPatch.flaggedPlayers.Clear();
			}
			yield break;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B384 File Offset: 0x00009584
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static EventPatch()
		{
			int num = 8;
			List<byte> list = new List<byte>(num);
			CollectionsMarshal.SetCount<byte>(list, num);
			Span<byte> span = CollectionsMarshal.AsSpan<byte>(list);
			int num2 = 0;
			*span[num2] = 1;
			num2++;
			*span[num2] = 12;
			num2++;
			*span[num2] = 42;
			num2++;
			*span[num2] = 202;
			num2++;
			*span[num2] = 6;
			num2++;
			*span[num2] = 66;
			num2++;
			*span[num2] = 17;
			num2++;
			*span[num2] = 35;
			num2++;
			EventPatch.WhitelistedECodes = list;
		}

		// Token: 0x04000130 RID: 304
		internal static List<string> E1Flagged = new List<string>();

		// Token: 0x04000131 RID: 305
		internal static List<string> flaggedPlayers = new List<string>();

		// Token: 0x04000132 RID: 306
		internal static List<int> E1Blocked = new List<int>();

		// Token: 0x04000133 RID: 307
		internal static List<int> allEventBlock = new List<int>();

		// Token: 0x04000134 RID: 308
		internal static Dictionary<int, ValueTuple<int[], DateTime>> eventBlocking = new Dictionary<int, ValueTuple<int[], DateTime>>();

		// Token: 0x04000135 RID: 309
		internal static bool Ready;

		// Token: 0x04000136 RID: 310
		internal static bool EventLog;

		// Token: 0x04000137 RID: 311
		internal static bool Noft;

		// Token: 0x04000138 RID: 312
		internal static byte[] dontBother = new byte[] { 12, 1, 66, 35, 17, 2 };

		// Token: 0x04000139 RID: 313
		internal static int panicCount = 0;

		// Token: 0x0400013A RID: 314
		internal static int LastPickup;

		// Token: 0x0400013B RID: 315
		internal static int USpeakCopyAc = -1;

		// Token: 0x0400013C RID: 316
		internal static int MovmentCopyAc = -1;

		// Token: 0x0400013D RID: 317
		private static DateTime PickupNoftTime;

		// Token: 0x0400013E RID: 318
		private static List<byte> WhitelistedECodes;

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000381 RID: 897
			public static <>F{00000008}<LoadBalancingClient, EventData, bool> <0>__OnEvent;
		}
	}
}
