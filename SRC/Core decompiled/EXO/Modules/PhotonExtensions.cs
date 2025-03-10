using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDK3.Components;

namespace EXO.Modules
{
	// Token: 0x02000072 RID: 114
	internal class PhotonExtensions
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00015FC5 File Offset: 0x000141C5
		public static void OpRaiseEvent<[IsUnmanaged] T>(byte code, T[] customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions) where T : struct, ValueType
		{
			PhotonExtensions.OpRaiseEvent(code, new global::Il2CppSystem.Object(new Il2CppStructArray<T>(customObject).Pointer), RaiseEventOptions, sendOptions);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00015FE0 File Offset: 0x000141E0
		public static void OpRaiseEvent<S, T>(byte code, Dictionary<S, T> customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			Dictionary<S, T> il2enm = new Dictionary<S, T>();
			customObject.ForEach(delegate(KeyValuePair<S, T> x)
			{
				il2enm.Add(x.Key, x.Value);
			});
			PhotonExtensions.OpRaiseEvent(code, il2enm, RaiseEventOptions, sendOptions);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00016024 File Offset: 0x00014224
		public static void OpRaiseEvent(byte code, global::Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			bool flag = customObject != null;
			if (flag)
			{
				PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject, RaiseEventOptions, sendOptions);
				return;
			}
			throw new NullReferenceException("YOUR DUMBASS ALMOST GOT BANNED, AGAIN!");
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00016054 File Offset: 0x00014254
		internal static byte[] Vector3ToBytes(Vector3 vector3)
		{
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector3.z), 0, array, 8, 4);
			return array;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000160AE File Offset: 0x000142AE
		internal static void SyncObject(VRCPickup Pickup, Vector3 Pos, Vector3 Rot)
		{
			SyncPhysics.Method_Internal_Static_Void_VRCObjectSync_Vector3_Quaternion_PDM_0(Pickup.GetComponent<VRCObjectSync>(), Pos, Quaternion.Euler(Rot));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000160C4 File Offset: 0x000142C4
		internal static void EditMovmentData(ref byte[] EventData, Vector3 Poz, Quaternion rotation, byte Fps = 0, short Ping = -1, bool check = true)
		{
			if (check)
			{
				bool flag = !PhotonExtensions.CheckIsValid(EventData, null);
				if (flag)
				{
					throw new Exception("WARNING : BAD PAYLOAD SKIPPING!!");
				}
			}
			byte[] src = QuantizedSerialization.Method_Public_Static_Il2CppStructArray_1_Byte_Quaternion_EnumNPublicSealedvaNoHaZe6vZeZeUnique_0(rotation, QuantizedSerialization.EnumNPublicSealedvaNoHaZe6vZeZeUnique.ZeroToOne10Bit);
			Buffer.BlockCopy(src, 0, EventData, 50, 5);
			Buffer.BlockCopy(PhotonExtensions.Vector3ToBytes(Poz), 0, EventData, 38, 12);
			PhotonExtensions.EditSync(ref EventData, Fps, Ping);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001612C File Offset: 0x0001432C
		internal static void EditSync(ref byte[] EventData, byte Fps = 0, short Ping = -1)
		{
			bool flag = Fps > 235;
			if (flag)
			{
				Fps = 225;
			}
			bool flag2 = Ping != -1;
			if (flag2)
			{
				Buffer.BlockCopy(BitConverter.GetBytes(Ping), 0, EventData, 55, 2);
			}
			bool flag3 = Fps > 0;
			if (flag3)
			{
				EventData[59] = Fps;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00016178 File Offset: 0x00014378
		internal static int ReadActor(byte[] buffer)
		{
			bool flag = buffer == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = buffer.Length < 4;
				if (flag2)
				{
					num = 0;
				}
				else
				{
					num = int.Parse(BitConverter.ToInt32(buffer, 0).ToString().Replace("00001", string.Empty));
				}
			}
			return num;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000161C8 File Offset: 0x000143C8
		internal static Quaternion GetRot(byte[] EventData, int offset = 33)
		{
			byte[] array = new byte[5];
			Buffer.BlockCopy(EventData, offset, array, 0, 5);
			Quaternion Blank = Quaternion.identity;
			QuantizedSerialization.Method_Private_Static_Void_Il2CppStructArray_1_Byte_byref_Quaternion_PDM_2(array, ref Blank);
			return Blank;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00016204 File Offset: 0x00014404
		internal static void Scan(byte[] array, int ac)
		{
			global::VRC.Player lpt = PlayerWrapper.GetByActorID(ac);
			bool flag = lpt == null;
			if (!flag)
			{
				bool flag2 = lpt == PlayerWrapper.LocalPlayer || (PhotonExtensions.MovmentOffset != 0 && PhotonExtensions.RotOffset != 0 && PhotonExtensions.FpsOffset != 0 && PhotonExtensions.PingOffset != 0);
				if (!flag2)
				{
					int offset = 37;
					double Rx = Math.Round((double)lpt.transform.position.x, 1);
					double Ry = Math.Round((double)lpt.transform.position.y, 1);
					while (Math.Round((double)BitConverter.ToSingle(array, offset), 1) != Rx)
					{
						offset++;
					}
					bool flag3 = Math.Round((double)BitConverter.ToSingle(array, offset + 4), 1) != Ry;
					if (flag3)
					{
						CLog.D("Failed?", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 116);
					}
					else
					{
						PhotonExtensions.MovmentOffset = offset - 1;
						PhotonExtensions.RotOffset = PhotonExtensions.MovmentOffset + 12;
						while (BitConverter.ToInt16(array, offset) != lpt.GetPing())
						{
							offset++;
						}
						PhotonExtensions.PingOffset = offset - 1;
						while (array[offset] != lpt.GetFrames(true))
						{
							offset++;
						}
						PhotonExtensions.FpsOffset = offset;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(52, 4);
						defaultInterpolatedStringHandler.AppendLiteral("Got Momvnet offsets (Poz : ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.MovmentOffset);
						defaultInterpolatedStringHandler.AppendLiteral(" | Rot : ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.RotOffset);
						defaultInterpolatedStringHandler.AppendLiteral(" Ping : ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.PingOffset);
						defaultInterpolatedStringHandler.AppendLiteral(" Fps : ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.FpsOffset);
						defaultInterpolatedStringHandler.AppendLiteral(")");
						CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 126);
					}
				}
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000163E0 File Offset: 0x000145E0
		internal static ValueTuple<int, int, int, int> _scan(byte[] array, int ac)
		{
			global::VRC.Player byActorID = PlayerWrapper.GetByActorID(ac);
			if (byActorID == null)
			{
				throw new NullReferenceException("Player By Actor is Null!");
			}
			global::VRC.Player lpt = byActorID;
			int offset = 37;
			double Rx = Math.Round((double)lpt.transform.position.x, 1);
			double Ry = Math.Round((double)lpt.transform.position.y, 1);
			while (Math.Round((double)BitConverter.ToSingle(array, offset), 1) != Rx)
			{
				offset++;
			}
			bool flag = Math.Round((double)BitConverter.ToSingle(array, offset + 4), 1) != Ry;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (flag)
			{
				defaultInterpolatedStringHandler..ctor(65, 5);
				defaultInterpolatedStringHandler.AppendLiteral("Movment Y Value Diff While X Match [Offset ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
				defaultInterpolatedStringHandler.AppendLiteral(" - RX ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)lpt.transform.position.x, 1));
				defaultInterpolatedStringHandler.AppendLiteral(" = ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)BitConverter.ToSingle(array, offset)));
				defaultInterpolatedStringHandler.AppendLiteral(" BUT RY ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)lpt.transform.position.y, 1));
				defaultInterpolatedStringHandler.AppendLiteral(" != ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)BitConverter.ToSingle(array, offset + 4), 1));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				throw new ArithmeticException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			int MovmentOffset = offset;
			int RotOffset = MovmentOffset + 12;
			while (BitConverter.ToInt16(array, offset) != lpt.GetPing())
			{
				offset++;
			}
			int PingOffset = offset;
			while (array[offset] != lpt.GetFrames(true))
			{
				offset++;
			}
			int FpsOffset = offset;
			defaultInterpolatedStringHandler..ctor(52, 4);
			defaultInterpolatedStringHandler.AppendLiteral("Got Momvnet offsets (Poz : ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(MovmentOffset);
			defaultInterpolatedStringHandler.AppendLiteral(" | Rot : ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RotOffset);
			defaultInterpolatedStringHandler.AppendLiteral(" Ping : ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(PingOffset);
			defaultInterpolatedStringHandler.AppendLiteral(" Fps : ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(FpsOffset);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 159);
			return new ValueTuple<int, int, int, int>(MovmentOffset, RotOffset, PingOffset, FpsOffset);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016638 File Offset: 0x00014838
		internal static Vector3 GetVector(byte[] EventData, int ofse = 38)
		{
			float x = BitConverter.ToSingle(EventData, ofse);
			float y = BitConverter.ToSingle(EventData, ofse + 4);
			float z = BitConverter.ToSingle(EventData, ofse + 8);
			return new Vector3(x, y, z);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016670 File Offset: 0x00014870
		internal static bool CheckIsValid(byte[] eData, global::VRC.Player plr = null)
		{
			if (plr == null)
			{
				plr = PlayerWrapper.GetByActorID(PhotonExtensions.ReadActor(eData));
			}
			bool flag = plr == null;
			if (flag)
			{
				throw new Exception("Null Player?");
			}
			double Rx = Math.Round((double)plr.transform.position.x, 1);
			double Ry = Math.Round((double)plr.transform.position.y, 1);
			bool flag2 = Math.Round((double)BitConverter.ToSingle(eData, 42), 1) != Ry || Math.Round((double)BitConverter.ToSingle(eData, 38), 1) != Rx;
			bool flag3;
			if (flag2)
			{
				CLog.D("Failed1", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 179);
				flag3 = false;
			}
			else
			{
				bool flag4 = eData[59] != plr.GetFrames(true);
				if (flag4)
				{
					flag3 = false;
				}
				else
				{
					bool flag5 = BitConverter.ToInt16(eData, 55) != plr.GetPing();
					flag3 = !flag5;
				}
			}
			return flag3;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00016760 File Offset: 0x00014960
		public static void CreatePortal(string InstanceID, Vector3 Position, float Rotation)
		{
			bool flag = InstanceID == null;
			if (!flag)
			{
				byte b = 70;
				Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
				dictionary.Add(0, 0);
				dictionary.Add(5, InstanceID);
				dictionary.Add(6, PhotonExtensions.Vector3ToBytes(Position));
				dictionary.Add(7, Rotation);
				PhotonExtensions.OpRaiseEvent<byte, object>(b, dictionary, new RaiseEventOptions(), SendOptions.SendReliable);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000167C2 File Offset: 0x000149C2
		internal static byte GetFps(byte[] EventData, int offset = 59)
		{
			return EventData[offset];
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000167C7 File Offset: 0x000149C7
		internal static short GetPing(byte[] EventData, int offset = 55)
		{
			return BitConverter.ToInt16(EventData, offset);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000167D0 File Offset: 0x000149D0
		internal static void SendEmoji(int Number)
		{
			byte b = 71;
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(0, 0);
			dictionary.Add(2, Number);
			PhotonExtensions.OpRaiseEvent<byte, object>(b, dictionary, new RaiseEventOptions
			{
				field_Public_EventCaching_0 = EventCaching.DoNotCache,
				field_Public_ReceiverGroup_0 = ReceiverGroup.All
			}, default(SendOptions));
		}

		// Token: 0x040001EF RID: 495
		private static int MovmentOffset;

		// Token: 0x040001F0 RID: 496
		private static int RotOffset;

		// Token: 0x040001F1 RID: 497
		private static int FpsOffset;

		// Token: 0x040001F2 RID: 498
		private static int PingOffset;

		// Token: 0x02000152 RID: 338
		internal enum OffSets
		{
			// Token: 0x040005F2 RID: 1522
			Movment = 21,
			// Token: 0x040005F3 RID: 1523
			Rot = 33,
			// Token: 0x040005F4 RID: 1524
			Movment2 = 38,
			// Token: 0x040005F5 RID: 1525
			Rot2 = 50,
			// Token: 0x040005F6 RID: 1526
			Ping = 55,
			// Token: 0x040005F7 RID: 1527
			Fps = 59
		}
	}
}
