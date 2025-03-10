using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions;
using EXO.LogTools;
using EXO.Menus.SubMenus;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Photon.Realtime;
using VRC.SDKBase;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000040 RID: 64
	internal class OpEvents : PatchModule
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BA70 File Offset: 0x00009C70
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000BA77 File Offset: 0x00009C77
		internal static bool EventLog { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BA7F File Offset: 0x00009C7F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BA86 File Offset: 0x00009C86
		internal static bool Serlz { get; set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x0000BA90 File Offset: 0x00009C90
		public override void LoadPatch()
		{
			bool hasSerlzNoft = false;
			MethodInfo method = typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0");
			<>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool> <>F{00000048};
			if ((<>F{00000048} = OpEvents.<>O.<0>__OpRaseEvents) == null)
			{
				<>F{00000048} = (OpEvents.<>O.<0>__OpRaseEvents = new <>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool>(OpEvents.OpRaseEvents));
			}
			PatchHandler.Detour(method, <>F{00000048});
			PatchHandler.Detour(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), delegate(LoadBalancingClient instance, byte __0, Object __1, RaiseEventOptions __2, IntPtr __3)
			{
				bool flag = __0 == 12;
				if (flag)
				{
					bool flag2 = !hasSerlzNoft && OpEvents.Serlz;
					if (flag2)
					{
						hasSerlzNoft = true;
						CLog.L("Failed to Block OPEvents, Fake Crash has failed!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\OpEvents.cs", 47);
					}
					else
					{
						bool flag3 = !OpEvents.Serlz;
						if (flag3)
						{
							hasSerlzNoft = false;
						}
					}
				}
			});
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000BB08 File Offset: 0x00009D08
		private static bool OpRaseEvents(LoadBalancingClient instance, ref byte __0, ref Object __1, RaiseEventOptions __2, IntPtr __3)
		{
			int code = (int)__0;
			bool flag = OpEvents.blockLocalUSpeak && __0 == 1;
			bool flag3;
			if (flag)
			{
				bool flag2 = !OpEvents.HasNoft;
				if (flag2)
				{
					CLog.L("For your safety you can't Speak!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\OpEvents.cs", 63);
					OpEvents.HasNoft = true;
					UtilFunc.Delay(10f, delegate
					{
						OpEvents.HasNoft = false;
					});
				}
				flag3 = false;
			}
			else
			{
				bool flag4 = OpEvents.blockLocalMovment && __0 == 12;
				if (flag4)
				{
					flag3 = false;
				}
				else
				{
					byte b = __0;
					byte b2 = b;
					if (b2 != 172)
					{
						if (b2 == 181)
						{
							__0 = 1;
						}
						bool flag5 = __0 == 1 || __0 == 12;
						if (flag5)
						{
							try
							{
								OpEvents.EventData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__1.Pointer);
							}
							catch
							{
								return true;
							}
						}
						OpEvents.IsReady = true;
						bool flag6 = OpEvents.PozFreze && __0 == 12;
						if (flag6)
						{
							if (OpEvents.FrezePoz == null)
							{
								OpEvents.FrezePoz = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__1.Pointer);
							}
						}
						bool flag7 = UtilsMenu.FuckUspeak && __0 == 1;
						if (flag7)
						{
							bool flag8 = OpEvents.lastServTime == Networking.GetServerTimeInMilliseconds();
							if (flag8)
							{
								int newTime = Networking.GetServerTimeInMilliseconds() + (int)PlayerWrapper.LocalPlayer.GetFrames() * new Random().Next(2, 6);
								Buffer.BlockCopy(BitConverter.GetBytes(newTime), 0, OpEvents.EventData, 16, 4);
								OpEvents.lastServTime = newTime;
							}
							else
							{
								int time = Networking.GetServerTimeInMilliseconds();
								Buffer.BlockCopy(BitConverter.GetBytes(time), 0, OpEvents.EventData, 16, 4);
								OpEvents.lastServTime = time;
							}
							__1 = Serialization.MakeObject(OpEvents.EventData);
						}
						bool eventLog = OpEvents.EventLog;
						if (eventLog)
						{
						}
						bool flag9 = OpEvents.Serlz && (__0 == 1 || __0 == 9 || __0 == 12);
						flag3 = !flag9;
					}
					else
					{
						flag3 = false;
					}
				}
			}
			return flag3;
		}

		// Token: 0x0400014C RID: 332
		internal static int lastServTime;

		// Token: 0x0400014D RID: 333
		internal static bool blockLocalUSpeak;

		// Token: 0x0400014E RID: 334
		internal static bool blockLocalMovment;

		// Token: 0x0400014F RID: 335
		internal static bool PozFreze;

		// Token: 0x04000150 RID: 336
		internal static bool IsReady;

		// Token: 0x04000151 RID: 337
		internal static bool HasNoft;

		// Token: 0x04000152 RID: 338
		internal static byte[] FrezePoz;

		// Token: 0x04000153 RID: 339
		internal static byte[] EventData;

		// Token: 0x020000FC RID: 252
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000395 RID: 917
			public static <>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool> <0>__OpRaseEvents;
		}
	}
}
