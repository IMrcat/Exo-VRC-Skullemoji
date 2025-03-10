using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EXO.LogTools;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC.Core;
using VRC.Management;

namespace EXO.Functions.Avatar
{
	// Token: 0x020000AE RID: 174
	internal static class AviUtils
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00024890 File Offset: 0x00022A90
		public static AnimationCurve ClampCurve(AnimationCurve curve, float minValue, float maxValue)
		{
			bool flag = ((curve != null) ? curve.keys : null) == null;
			AnimationCurve animationCurve;
			if (flag)
			{
				animationCurve = curve;
			}
			else
			{
				Keyframe[] keys = curve.keys;
				for (int i = 0; i < keys.Length; i++)
				{
					keys[i].value = Mathf.Clamp(keys[i].value, minValue, maxValue);
				}
				animationCurve = new AnimationCurve(keys);
			}
			return animationCurve;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000248FF File Offset: 0x00022AFF
		internal static void RemoveObject(this Object @object, string removeContext)
		{
			CLog.D("Destroy Object " + @object.name + " With Context " + removeContext, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Avatar\\AviUtils.cs", 34);
			Object.DestroyImmediate(@object);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002492D File Offset: 0x00022B2D
		internal static void RemoveObject(this Material @object, string removeContext)
		{
			CLog.D("Destroy Object " + @object.name + " With Context " + removeContext, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Avatar\\AviUtils.cs", 40);
			Object.DestroyImmediate(@object);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002495C File Offset: 0x00022B5C
		internal static List<T> FindAllComponentsInGameObject<T>(GameObject gameObject, bool includeInactive = true, bool searchParent = false, bool searchChildren = true) where T : class
		{
			List<T> components = new List<T>();
			foreach (T component in gameObject.GetComponents<T>())
			{
				components.Add(component);
			}
			if (searchParent)
			{
				foreach (T component2 in gameObject.GetComponentsInParent<T>(includeInactive))
				{
					components.Add(component2);
				}
			}
			if (searchChildren)
			{
				foreach (T component3 in gameObject.GetComponentsInChildren<T>(includeInactive))
				{
					components.Add(component3);
				}
			}
			return components;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00024A54 File Offset: 0x00022C54
		internal static bool Clamp(ref float value, float min, float max)
		{
			bool limited = false;
			float newScale = value;
			newScale = Math.Clamp(newScale, min, max);
			bool flag = value != newScale;
			if (flag)
			{
				limited = true;
			}
			value = newScale;
			return limited;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00024A88 File Offset: 0x00022C88
		internal static bool IsAvatarExplicitlyShown(string userId)
		{
			Dictionary<string, List<ApiPlayerModeration>> moderationsDict = ModerationManager.prop_ModerationManager_0.field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0;
			bool flag = !moderationsDict.ContainsKey(userId);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (ApiPlayerModeration playerModeration in moderationsDict[userId])
				{
					bool flag3 = playerModeration.moderationType == ApiPlayerModeration.ModerationType.ShowAvatar;
					if (flag3)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00024AEC File Offset: 0x00022CEC
		[return: TupleElementNames(new string[] { "TotalPolys", "FirstSubmeshOverLimit" })]
		internal static ValueTuple<int, int> CountMeshPolygons(Mesh mesh, int remainingLimit)
		{
			int polyCount = 0;
			int firstSubmeshOverLimit = -1;
			int submeshCount = mesh.subMeshCount;
			for (int i = 0; i < submeshCount; i++)
			{
				uint polysInSubmesh = mesh.GetIndexCount(i);
				switch (mesh.GetTopology(i))
				{
				case MeshTopology.Triangles:
					polysInSubmesh /= 3U;
					break;
				case MeshTopology.Quads:
					polysInSubmesh /= 4U;
					break;
				case MeshTopology.Lines:
					polysInSubmesh /= 2U;
					break;
				}
				bool flag = (long)polyCount + (long)((ulong)polysInSubmesh) >= (long)remainingLimit;
				if (flag)
				{
					firstSubmeshOverLimit = i;
					break;
				}
				polyCount += (int)polysInSubmesh;
			}
			return new ValueTuple<int, int>(polyCount, firstSubmeshOverLimit);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00024B8C File Offset: 0x00022D8C
		internal static bool IsUnsafe(this string str)
		{
			foreach (string a in KeyWords.unsafeKeys)
			{
				bool flag = (a == "c4" && str.Contains("Hidden/Locked/.poiyomi")) || (a == "ouch" && str.Contains("touch", 1)) || (a == "iterations" && str == "_ParallaxInternalIterations");
				if (!flag)
				{
					bool flag2 = str.Replace(" ", string.Empty).Contains(a, 1);
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00024C64 File Offset: 0x00022E64
		internal static bool IsScreenSpace(this string str)
		{
			return Enumerable.Any<string>(AviUtils.screenSpaceKeys, (string a) => str.Replace(" ", string.Empty).ToLower().Contains(a));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00024C94 File Offset: 0x00022E94
		internal static bool IsNsfw(this string str)
		{
			bool isNsfw = Enumerable.Any<string>(KeyWords.nsfwKeys, (string a) => str.Replace(" ", string.Empty).Contains(a, 1));
			bool flag = isNsfw;
			if (flag)
			{
				CLog.D("Nsfw Obj : " + str + ", Matched Word : " + Enumerable.First<string>(KeyWords.nsfwKeys, (string a) => str.Replace(" ", string.Empty).Contains(a, 1)), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Avatar\\AviUtils.cs", 138);
			}
			return isNsfw;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00024D0C File Offset: 0x00022F0C
		// Note: this type is marked as 'beforefieldinit'.
		static AviUtils()
		{
			List<string> list = new List<string>();
			list.Add("shake");
			list.Add("distort");
			list.Add("blur");
			list.Add("zoom");
			list.Add("pixel");
			list.Add("flip");
			list.Add("rotate");
			AviUtils.screenSpaceKeys = list;
		}

		// Token: 0x0400030B RID: 779
		internal static readonly Shader Standard = Shader.Find("Standard");

		// Token: 0x0400030C RID: 780
		internal static readonly Shader ToonLit = Shader.Find("VRChat/Mobile/Toon Lit");

		// Token: 0x0400030D RID: 781
		internal static List<string> screenSpaceKeys;
	}
}
