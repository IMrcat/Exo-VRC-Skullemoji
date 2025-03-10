using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using UnityEngine;
using VRC;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace EXO.Modules.Utilities
{
	// Token: 0x02000078 RID: 120
	internal static class OtherUtils
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000175ED File Offset: 0x000157ED
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x000175F4 File Offset: 0x000157F4
		internal static bool AntiTheft { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000175FC File Offset: 0x000157FC
		internal static GameObject UserInterface
		{
			get
			{
				return GameObject.Find("Canvas_QuickMenu(Clone)").transform.parent.gameObject;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00017628 File Offset: 0x00015828
		internal static List<GameObject> GetChildrenAsList(this Transform transform)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				list.Add(gameObject);
			}
			return list;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00017670 File Offset: 0x00015870
		internal static Transform GetRootObject(this Transform obj)
		{
			Transform root = obj;
			while (root.parent != null)
			{
				root = root.parent;
			}
			return root;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001769E File Offset: 0x0001589E
		internal static string GetObjectPath(this GameObject obj)
		{
			return obj.transform.GetObjectPath();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000176AC File Offset: 0x000158AC
		internal static string GetObjectPath(this Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000176F4 File Offset: 0x000158F4
		internal static T SingleOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			IEnumerable<T> matchingElements = Enumerable.Where<T>(source, predicate);
			bool flag = Enumerable.Count<T>(matchingElements) == 1;
			T t;
			if (flag)
			{
				t = Enumerable.First<T>(matchingElements);
			}
			else
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001772D File Offset: 0x0001592D
		internal static GameObject GetRootObject(this GameObject obj)
		{
			return obj.transform.GetRootObject().gameObject;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001773F File Offset: 0x0001593F
		private static IEnumerator DelayFunc(float del, Action action)
		{
			yield return new WaitForSeconds(del);
			action.Invoke();
			yield break;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00017755 File Offset: 0x00015955
		internal static bool IsJustBad(Vector3 V3)
		{
			return OtherUtils.IsNaN(V3) || OtherUtils.IsNanPos(V3) || OtherUtils.IsMaxPos(V3) || OtherUtils.IsNanBypass(V3);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00017778 File Offset: 0x00015978
		internal static bool IsJustBad(Vector3 V3, bool WeakCheckToo)
		{
			return OtherUtils.IsNaN(V3) || OtherUtils.IsNanPos(V3) || OtherUtils.IsMaxPos(V3) || OtherUtils.BadPoz(V3);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001779C File Offset: 0x0001599C
		internal static bool IsNanBypass(Vector3 v3)
		{
			return OtherUtils.INFBypass.x < v3.x || OtherUtils.INFBypass.y < v3.y || OtherUtils.INFBypass.z < v3.z || OtherUtils.NegINFBypass.x > v3.x || OtherUtils.NegINFBypass.y > v3.y || OtherUtils.NegINFBypass.z > v3.z;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00017818 File Offset: 0x00015A18
		internal static bool IsNaN(Vector3 v3)
		{
			return float.IsNaN(v3.x) || float.IsNaN(v3.y) || float.IsNaN(v3.z);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00017844 File Offset: 0x00015A44
		internal static bool IsNanPos(Vector3 v3)
		{
			return 2.1474836E+09f < v3.x || 2.1474836E+09f < v3.y || 2.1474836E+09f < v3.z || -2.1474836E+09f > v3.x || -2.1474836E+09f > v3.y || -2.1474836E+09f > v3.z;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000178A4 File Offset: 0x00015AA4
		internal static bool IsMaxPos(Vector3 v3)
		{
			return 2.1474836E+09f == v3.x || 2.1474836E+09f == v3.y || 2.1474836E+09f == v3.z || -2.1474836E+09f == v3.x || -2.1474836E+09f == v3.y || -2.1474836E+09f == v3.z;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00017904 File Offset: 0x00015B04
		internal static bool BadPoz(Vector3 v3)
		{
			return 9999f < v3.x || 9999f < v3.y || 9999f < v3.z || -7000f > v3.x || -7000f > v3.y || -7000f > v3.z;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00017964 File Offset: 0x00015B64
		[MethodImpl(256)]
		internal static bool IsInvalid(Quaternion quaternion)
		{
			return float.IsNaN(quaternion.w) || float.IsNaN(quaternion.w) || float.IsNaN(quaternion.x) || float.IsNaN(quaternion.x) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.z) || float.IsNaN(quaternion.z);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000179E0 File Offset: 0x00015BE0
		[MethodImpl(256)]
		internal static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			float num;
			if (flag)
			{
				num = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					num = max;
				}
				else
				{
					num = value;
				}
			}
			return num;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00017A0B File Offset: 0x00015C0B
		internal static Vector3 INFBypass
		{
			get
			{
				return new Vector3(OtherUtils.InfValue, OtherUtils.InfValue, OtherUtils.InfValue);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00017A21 File Offset: 0x00015C21
		internal static Vector3 NegINFBypass
		{
			get
			{
				return new Vector3(-OtherUtils.InfValue, -OtherUtils.InfValue, -OtherUtils.InfValue);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00017A3A File Offset: 0x00015C3A
		internal static Quaternion ROTBypass
		{
			get
			{
				return new Quaternion(OtherUtils.InfValue, OtherUtils.InfValue, OtherUtils.InfValue, OtherUtils.InfValue);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00017A55 File Offset: 0x00015C55
		internal static Quaternion NegROTBypass
		{
			get
			{
				return new Quaternion(-OtherUtils.InfValue, -OtherUtils.InfValue, -OtherUtils.InfValue, -OtherUtils.InfValue);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00017A74 File Offset: 0x00015C74
		internal static double SortNumber(string UnityVersion)
		{
			bool flag = UnityVersion.StartsWith("2019");
			double num;
			if (flag)
			{
				num = 201904310000.0;
			}
			else
			{
				bool flag2 = UnityVersion.StartsWith("2017");
				if (flag2)
				{
					num = 201704150000.0;
				}
				else
				{
					bool flag3 = UnityVersion.StartsWith("2018");
					if (flag3)
					{
						num = 20180420000.0;
					}
					else
					{
						num = 0.0;
					}
				}
			}
			return num;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00017AE0 File Offset: 0x00015CE0
		internal static ValueTuple<string, int> GetItemOrbiter(Il2CppArrayBase<VRCPickup> pickups)
		{
			Dictionary<string, int> cathced = new Dictionary<string, int>();
			foreach (VRCPickup item in pickups)
			{
				Player itemOwner = item.getOwnerOfGameObject();
				bool flag = !cathced.ContainsKey(itemOwner.DisplayName());
				if (flag)
				{
					cathced.Add(itemOwner.DisplayName(), 0);
				}
				int passes;
				cathced.TryGetValue(itemOwner.DisplayName(), ref passes);
				cathced[itemOwner.DisplayName()] = passes + 1;
			}
			KeyValuePair<string, int> highestValue = Enumerable.FirstOrDefault<KeyValuePair<string, int>>(cathced, (KeyValuePair<string, int> x) => x.Value == Enumerable.Max(cathced.Values));
			return new ValueTuple<string, int>(highestValue.Key, highestValue.Value);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal static Dictionary<string, int> GetItemOwnersList(Il2CppArrayBase<VRCPickup> pickups)
		{
			Dictionary<string, int> cathced = new Dictionary<string, int>();
			foreach (VRCPickup item in pickups)
			{
				Player itemOwner = item.getOwnerOfGameObject();
				bool flag = !cathced.ContainsKey(itemOwner.DisplayName());
				if (flag)
				{
					cathced.Add(itemOwner.DisplayName(), 0);
				}
				int passes;
				cathced.TryGetValue(itemOwner.DisplayName(), ref passes);
				cathced[itemOwner.DisplayName()] = passes + 1;
			}
			return cathced;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00017C6C File Offset: 0x00015E6C
		internal static T Random<T>(this T[] array)
		{
			return array[new global::System.Random().Next(array.Length)];
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00017C84 File Offset: 0x00015E84
		internal static void TakeOwnershipIfNecessary(this GameObject gameObject)
		{
			bool flag = gameObject.getOwnerOfGameObject() != PlayerWrapper.LocalPlayer;
			if (flag)
			{
				Networking.SetOwner(Networking.LocalPlayer, gameObject);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00017CB2 File Offset: 0x00015EB2
		internal static Player getOwnerOfGameObject(this VRCPickup pickUp)
		{
			return pickUp.gameObject.getOwnerOfGameObject();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00017CBF File Offset: 0x00015EBF
		internal static Player getOwner(this VRCPickup pickUp)
		{
			return pickUp.gameObject.getOwnerOfGameObject();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00017CCC File Offset: 0x00015ECC
		internal static Player getOwnerOfGameObject(this VRC_Pickup pickUp)
		{
			return pickUp.gameObject.getOwnerOfGameObject();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00017CD9 File Offset: 0x00015ED9
		internal static Player getOwner(this VRC_Pickup pickUp)
		{
			return pickUp.gameObject.getOwnerOfGameObject();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00017CE6 File Offset: 0x00015EE6
		internal static Player getOwnerOfGameObject(this GameObject gameObject)
		{
			return PlayerWrapper.GetByActorID(Networking.GetOwner(gameObject).playerId);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00017CF8 File Offset: 0x00015EF8
		internal static string LogRPC(Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType)
		{
			string text = "[RPC] ";
			text = ((!(sender != null)) ? (text + " INVISABLE sended ") : (text + sender.prop_APIUser_0.displayName + " sended "));
			text = text + vrcBroadcastType.ToString() + " ";
			text = text + vrcEvent.Name + " ";
			text = text + vrcEvent.EventType.ToString() + " ";
			bool flag = vrcEvent.ParameterObject != null;
			if (flag)
			{
				text = text + vrcEvent.ParameterObject.name + " ";
				text = text + vrcEvent.ParameterBool.ToString() + " ";
				text = text + vrcEvent.ParameterBoolOp.ToString() + " ";
				text = text + vrcEvent.ParameterFloat.ToString() + " ";
				text = text + vrcEvent.ParameterInt.ToString() + " ";
				text = text + vrcEvent.ParameterString + " ";
			}
			bool flag2 = vrcEvent.ParameterObjects != null;
			if (flag2)
			{
				for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++)
				{
					text = text + vrcEvent.ParameterObjects[i].name + " ";
				}
			}
			string text2;
			try
			{
				Il2CppReferenceArray<global::Il2CppSystem.Object> il2CppReferenceArray = Networking.DecodeParameters(vrcEvent.ParameterBytes);
				for (int j = 0; j < il2CppReferenceArray.Length; j++)
				{
					text = text + Convert.ToString(il2CppReferenceArray[j]) + " ";
				}
				text2 = text;
			}
			catch
			{
				for (int k = 0; k < vrcEvent.ParameterBytes.Length; k++)
				{
					text = text + vrcEvent.ParameterBytes[k].ToString() + " ";
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00017F28 File Offset: 0x00016128
		internal static string ByteArrayToString(IReadOnlyCollection<byte> ba)
		{
			StringBuilder hex = new StringBuilder(ba.Count * 2);
			foreach (byte b in ba)
			{
				hex.AppendFormat("{0:x2}", b);
			}
			return hex.ToString();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00017F98 File Offset: 0x00016198
		private static IEnumerator ToggleIKControllerEnumerator(bool state)
		{
			yield return new WaitForSeconds(2f);
			OtherUtils.Ani.field_Private_Boolean_0 = !state;
			yield break;
		}

		// Token: 0x04000200 RID: 512
		internal static readonly float InfValue = 2.1474836E+09f;

		// Token: 0x04000201 RID: 513
		internal static bool StopAntiHolds;

		// Token: 0x04000202 RID: 514
		private static VRC_AnimationController Ani;

		// Token: 0x04000203 RID: 515
		private static VRCVrIkController IK;

		// Token: 0x04000204 RID: 516
		private static float capsuleHiderOffsetReset = 0f;

		// Token: 0x04000205 RID: 517
		internal static float ClippingPlaneValue = 0f;

		// Token: 0x04000206 RID: 518
		internal static float defaultVal = 0f;

		// Token: 0x04000207 RID: 519
		private static bool IsHooked = false;
	}
}
