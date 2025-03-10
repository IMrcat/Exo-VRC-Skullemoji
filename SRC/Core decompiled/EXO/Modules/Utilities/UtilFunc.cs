using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.LogTools;
using EXO.Patches;
using EXO.Wrappers;
using UnityEngine;
using VRC.Localization;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.UI.Core.Styles;
using VRCSDK2;
using WorldAPI;
using WorldAPI.ButtonAPI.Controls;

namespace EXO.Modules.Utilities
{
	// Token: 0x0200007A RID: 122
	internal static class UtilFunc
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00018141 File Offset: 0x00016341
		internal static GameObject UserInterface
		{
			get
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				return (field_Private_Static_VRCUiManager_ != null) ? field_Private_Static_VRCUiManager_.gameObject : null;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001815D File Offset: 0x0001635D
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00018154 File Offset: 0x00016354
		internal static string Clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00018164 File Offset: 0x00016364
		internal static void Delay(float del, Action action)
		{
			CoroutineManager.RunCoroutine(UtilFunc.DelayFunc(del, action));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00018173 File Offset: 0x00016373
		private static IEnumerator DelayFunc(float del, Action action)
		{
			yield return new WaitForSeconds(del);
			action.Invoke();
			yield break;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00018189 File Offset: 0x00016389
		internal static void WaitForObj(string obj, Action action)
		{
			CoroutineManager.RunCoroutine(UtilFunc.WaitForObjFunc(obj, action));
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00018198 File Offset: 0x00016398
		private static IEnumerator WaitForObjFunc(string obj, Action action)
		{
			while (GameObject.Find(obj) == null)
			{
				yield return new WaitForFixedUpdate();
			}
			yield return new WaitForFixedUpdate();
			action.Invoke();
			yield break;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000181AE File Offset: 0x000163AE
		internal static void WaitFor(string obj, Action action)
		{
			CoroutineManager.RunCoroutine(UtilFunc.WaitForFunc(obj, action));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000181BD File Offset: 0x000163BD
		private static IEnumerator WaitForFunc(string obj, Action action)
		{
			for (;;)
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				Object @object;
				if (field_Private_Static_VRCUiManager_ == null)
				{
					@object = null;
				}
				else
				{
					GameObject gameObject = field_Private_Static_VRCUiManager_.gameObject;
					@object = ((gameObject != null) ? gameObject.FindObject(obj) : null);
				}
				if (!(@object == null))
				{
					break;
				}
				yield return new WaitForFixedUpdate();
			}
			yield return new WaitForFixedUpdate();
			action.Invoke();
			yield break;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000181D3 File Offset: 0x000163D3
		internal static void WaitFor(Transform par, string obj, Action action)
		{
			CoroutineManager.RunCoroutine(UtilFunc.WaitForFunc(par, obj, action));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000181E3 File Offset: 0x000163E3
		private static IEnumerator WaitForFunc(Transform par, string obj, Action action)
		{
			while (((par != null) ? par.Find(obj) : null) == null)
			{
				yield return new WaitForFixedUpdate();
			}
			yield return new WaitForFixedUpdate();
			action.Invoke();
			yield break;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00018200 File Offset: 0x00016400
		internal static string EzByteConvert<T>(this T[] array) where T : struct
		{
			return array.Join(", ");
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00018210 File Offset: 0x00016410
		internal static void TakeOwnershipIfNecessary(this GameObject gameObject)
		{
			bool flag = gameObject.getOwnerOfGameObject().UserID() != PlayerWrapper.LocalPlayer.UserID();
			if (flag)
			{
				Networking.SetOwner(Networking.LocalPlayer, gameObject);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018248 File Offset: 0x00016448
		public static string Join<T>(this T[] array, string separator = "")
		{
			return string.Join<T>(separator, array);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018251 File Offset: 0x00016451
		public static string Join<T>(this T[] array, Func<T, object> selector, string separator = "")
		{
			return string.Join<object>(separator, Enumerable.Select<T, object>(Enumerable.Where<T>(array, delegate(T item)
			{
				ref T ptr = ref item;
				T t = default(T);
				string text;
				if (t == null)
				{
					t = item;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_0031;
					}
				}
				text = ptr.ToString();
				IL_0031:
				return !string.IsNullOrEmpty(text);
			}), selector));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00018284 File Offset: 0x00016484
		public static bool CheckWorldID(string ID, int? Playercount = null)
		{
			bool flag = !ID.Contains(":");
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string[] splt = ID.Split(':', 0);
				string WorldID = splt[0];
				string InstanceID = splt[1];
				bool flag3 = WorldID.Length != 41 || InstanceID.Length < 1;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = !WorldID.StartsWith("wrld_");
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = !Enumerable.All<char>(WorldID, (char c) => char.IsLetterOrDigit(c) || c == '_' || c == '-');
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool flag6 = !Enumerable.All<char>(InstanceID, (char c) => char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '(' || c == ')' || c == '~');
							if (flag6)
							{
								flag2 = false;
							}
							else
							{
								bool flag7 = InstanceID.Contains("~private") || InstanceID.Contains("~hidden") || InstanceID.Contains("~friends") || InstanceID.Contains("~canRequestInvite");
								if (flag7)
								{
									bool flag8 = !InstanceID.Contains("(usr_");
									if (flag8)
									{
										return false;
									}
								}
								bool flag9 = InstanceID.Contains("~region") && !InstanceID.Contains("~region(use)") && !InstanceID.Contains("~region(eu)") && !InstanceID.Contains("~region(jp)") && !InstanceID.Contains("~region(us)");
								if (flag9)
								{
									flag2 = false;
								}
								else
								{
									bool flag10;
									if (Playercount != null)
									{
										int? num = Playercount;
										int num2 = 0;
										if (!((num.GetValueOrDefault() < num2) & (num != null)))
										{
											num = Playercount;
											num2 = 80;
											flag10 = (num.GetValueOrDefault() > num2) & (num != null);
										}
										else
										{
											flag10 = true;
										}
									}
									else
									{
										flag10 = false;
									}
									bool flag11 = flag10;
									flag2 = !flag11;
								}
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00018468 File Offset: 0x00016668
		internal static void UpdateClamp(float time, Action callback)
		{
			UtilFunc.<>c__DisplayClass18_0 CS$<>8__locals1 = new UtilFunc.<>c__DisplayClass18_0();
			CS$<>8__locals1.callback = callback;
			CS$<>8__locals1.time = time;
			CoroutineManager.RunCoroutine(CS$<>8__locals1.<UpdateClamp>g__UpdateClamp|0());
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00018498 File Offset: 0x00016698
		internal static Sprite GeVRCSprite(string name)
		{
			Sprite _sprite = null;
			StyleResource styleResource = UtilFunc.UserInterface.FindObject("Canvas_QuickMenu(Clone)").field_Public_StyleResource_0;
			for (int i = 0; i < styleResource.resources.Count; i++)
			{
				StyleResource.Resource resource = styleResource.resources[i];
				string text;
				if (resource == null)
				{
					text = null;
				}
				else
				{
					Object obj = resource.obj;
					text = ((obj != null) ? obj.name : null);
				}
				bool flag = text == name;
				if (flag)
				{
					_sprite = styleResource.resources[i].obj.Cast<Sprite>();
					break;
				}
			}
			bool flag2 = _sprite != null;
			if (flag2)
			{
				CLog.D("Found sprite: " + _sprite.name, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\UtilFunc.cs", 123);
				return _sprite;
			}
			throw new NullReferenceException("Unable to find the Page_Tab_Backdrop sprite.");
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00018560 File Offset: 0x00016760
		public static Type FindFirstMatchingMonoBehaviour()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(MonoBehaviour));
			Type[] types = assembly.GetTypes();
			return Enumerable.FirstOrDefault<Type>(types, (Type t) => t.Name.StartsWith("MonoBehaviour1PublicTrVo"));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000185B0 File Offset: 0x000167B0
		internal static bool FindObject(string path, out GameObject obj)
		{
			GameObject gameObject;
			obj = (gameObject = GameObject.Find(path));
			return gameObject != null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000185D0 File Offset: 0x000167D0
		internal static bool FindObject<T>(string path, out T obj) where T : Object
		{
			GameObject gameObject = GameObject.Find(path);
			return (obj = ((gameObject != null) ? gameObject.GetComponent<T>() : default(T))) != null;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001860C File Offset: 0x0001680C
		internal static bool FindObject<T>(this GameObject obje, out T monoBehv) where T : Object
		{
			return (monoBehv = ((obje != null) ? obje.GetComponent<T>() : default(T))) != null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00018641 File Offset: 0x00016841
		internal static bool FindObject<T>(this Transform obje, out T monoBehv) where T : Object
		{
			return obje.gameObject.FindObject(out monoBehv);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00018650 File Offset: 0x00016850
		internal static T FindObject<T>(this Transform obje, string path) where T : Object
		{
			Transform transform = obje.Find(path);
			return (transform != null) ? transform.GetComponent<T>() : default(T);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00018678 File Offset: 0x00016878
		internal static T FindObject<T>(this GameObject obje, string path) where T : Object
		{
			return obje.transform.FindObject(path);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00018686 File Offset: 0x00016886
		internal static T FindObject<T>(this MonoBehaviour obje, string path) where T : Object
		{
			return obje.transform.FindObject(path);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00018694 File Offset: 0x00016894
		internal static Transform FindObject(this Transform trm, string path)
		{
			return (UtilFunc._lastFind.Item1 == path && UtilFunc._lastFind.Item2) ? UtilFunc._lastFind.Item2 : (UtilFunc._lastFind.Item2 = ((trm != null) ? trm.Find(path) : null));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000186EA File Offset: 0x000168EA
		internal static Transform FindObject(this GameObject trm, string path)
		{
			return trm.transform.FindObject(path);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000186F8 File Offset: 0x000168F8
		internal static Transform FindObject(this MonoBehaviour trm, string path)
		{
			return trm.transform.FindObject(path);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00018706 File Offset: 0x00016906
		internal static Transform FindObject(this Root trm, string path)
		{
			return trm.transform.FindObject(path);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00018714 File Offset: 0x00016914
		internal static Transform FindObject(this Transform obje, string path, out Transform monoBehv)
		{
			Transform transform;
			monoBehv = (transform = obje.gameObject.FindObject(path));
			return transform;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00018732 File Offset: 0x00016932
		internal static Transform FindObject(this MonoBehaviour obje, string path, out Transform monoBehv)
		{
			return obje.transform.FindObject(path, out monoBehv);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00018741 File Offset: 0x00016941
		internal static Transform FindObject(this GameObject obje, string path, out Transform monoBehv)
		{
			return obje.transform.FindObject(path, out monoBehv);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018750 File Offset: 0x00016950
		internal static void SetActive(this Transform trs, bool val)
		{
			trs.gameObject.active = val;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001875F File Offset: 0x0001695F
		internal static void SetActive(this MonoBehaviour trs, bool val)
		{
			trs.gameObject.active = val;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00018770 File Offset: 0x00016970
		internal static void RemoveWhere<T>(this List<T> list, Func<T, bool> predi)
		{
			T itemToRemove = Enumerable.FirstOrDefault<T>(list, predi);
			bool flag = itemToRemove != null;
			if (flag)
			{
				list.Remove(itemToRemove);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001879C File Offset: 0x0001699C
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			bool flag = enumerable == null;
			if (flag)
			{
				throw new ArgumentNullException("enumerable");
			}
			bool flag2 = action == null;
			if (flag2)
			{
				throw new ArgumentNullException("action");
			}
			foreach (T item in enumerable)
			{
				action.Invoke(item);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00018810 File Offset: 0x00016A10
		internal static LocalizableString ConvertToLocalized(this string str)
		{
			return str.Localize(null, null, null);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001881B File Offset: 0x00016A1B
		internal static VRCInput GetInput(string name)
		{
			return VRCInputManager.Method_Public_Static_VRCInput_String_0(name);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00018823 File Offset: 0x00016A23
		internal static float GetInputSingle(string name)
		{
			return VRCInputManager.Method_Public_Static_VRCInput_String_0(name).prop_Single_0;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00018830 File Offset: 0x00016A30
		internal static bool KeyboardOpen
		{
			get
			{
				Canvas canvas;
				if ((canvas = UtilFunc._keyboard) == null)
				{
					Transform transform = APIBase.MMM.FindObject("Container/MMParent/Modal_MM_Keyboard");
					canvas = (UtilFunc._keyboard = ((transform != null) ? transform.GetComponent<Canvas>() : null));
				}
				return canvas.enabled;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00018861 File Offset: 0x00016A61
		internal static bool IsAnyUIOpen
		{
			get
			{
				return UtilFunc.KeyboardOpen || QuickMenuPatch.IsOpen;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00018874 File Offset: 0x00016A74
		internal static string HexColor(this string str, string hex)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(hex);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000188CC File Offset: 0x00016ACC
		internal static string HexColor(this int str, string hex)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(hex);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted<int>(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00018924 File Offset: 0x00016B24
		internal static string HexColor(this float str, string hex)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(hex);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted<float>(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001897C File Offset: 0x00016B7C
		internal static string HexColor(this string str, Color color)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(color.ToHex());
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000189D8 File Offset: 0x00016BD8
		internal static string HexColor(this int str, Color color)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(color.ToHex());
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted<int>(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00018A34 File Offset: 0x00016C34
		internal static string HexColor(this float str, Color color)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(color.ToHex());
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted<float>(str);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00018A90 File Offset: 0x00016C90
		internal static string ToHex(this Color color)
		{
			int r = Mathf.RoundToInt(color.r * 255f);
			int g = Mathf.RoundToInt(color.g * 255f);
			int b = Mathf.RoundToInt(color.b * 255f);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 3);
			defaultInterpolatedStringHandler.AppendLiteral("#");
			defaultInterpolatedStringHandler.AppendFormatted<int>(r, "X2");
			defaultInterpolatedStringHandler.AppendFormatted<int>(g, "X2");
			defaultInterpolatedStringHandler.AppendFormatted<int>(b, "X2");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00018B24 File Offset: 0x00016D24
		internal static Color HexToColor(string hexColor)
		{
			bool flag = hexColor.IndexOf('#') != -1;
			if (flag)
			{
				hexColor = hexColor.Replace("#", "");
			}
			float r = (float)int.Parse(hexColor.Substring(0, 2), 512) / 255f;
			float g = (float)int.Parse(hexColor.Substring(2, 2), 512) / 255f;
			float b = (float)int.Parse(hexColor.Substring(4, 2), 512) / 255f;
			return new Color(r, g, b);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00018BB4 File Offset: 0x00016DB4
		internal static string ColorToHex(Color baseColor, bool hash = false)
		{
			int R = Convert.ToInt32(baseColor.r * 255f);
			int G = Convert.ToInt32(baseColor.g * 255f);
			int B = Convert.ToInt32(baseColor.b * 255f);
			string hexColor = R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
			if (hash)
			{
				hexColor = "#" + hexColor;
			}
			return hexColor;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00018C3C File Offset: 0x00016E3C
		public static string GetGameObjectPath(Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00018C84 File Offset: 0x00016E84
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

		// Token: 0x0600046E RID: 1134 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public static List<T> GetAllComponentsInChildren<T>(this GameObject gameObject) where T : Component
		{
			List<T> componentsList = new List<T>();
			T[] components = gameObject.GetComponentsInChildren<T>();
			componentsList.AddRange(components);
			return componentsList;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00018D20 File Offset: 0x00016F20
		internal static bool ContiansIgnoreCase(this string string1, string string2)
		{
			return string1.Contains(string2, 1);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00018D3C File Offset: 0x00016F3C
		public static void PlayVideo(string url)
		{
			Enumerable.ToList<SyncVideoPlayer>(Enumerable.Where<SyncVideoPlayer>(Object.FindObjectsOfType<SyncVideoPlayer>(), (SyncVideoPlayer dat) => dat != null)).ForEach(delegate(SyncVideoPlayer dat)
			{
				Networking.LocalPlayer.TakeOwnership(dat.gameObject);
				VRC_SyncVideoPlayer vidp = dat.field_Private_VRC_SyncVideoPlayer_0;
				vidp.Stop();
				vidp.Clear();
				vidp.AddURL(url);
				vidp.Next();
				vidp.Play();
			});
			Enumerable.ToList<SyncVideoStream>(Enumerable.Where<SyncVideoStream>(Object.FindObjectsOfType<SyncVideoStream>(), (SyncVideoStream dat) => dat != null)).ForEach(delegate(SyncVideoStream dat)
			{
				Networking.LocalPlayer.TakeOwnership(dat.gameObject);
				VRC_SyncVideoStream str = dat.field_Private_VRC_SyncVideoStream_0;
				str.Stop();
				str.Clear();
				str.AddURL(url);
				str.Next();
				str.Play();
			});
			Enumerable.ToList<VRCUrlInputField>(Enumerable.Where<VRCUrlInputField>(Object.FindObjectsOfType<VRCUrlInputField>(), (VRCUrlInputField dat) => dat != null)).ForEach(delegate(VRCUrlInputField dat)
			{
				Networking.LocalPlayer.TakeOwnership(dat.gameObject);
				dat.text = url;
				dat.onEndEdit.Invoke(url);
			});
		}

		// Token: 0x04000208 RID: 520
		private static ValueTuple<string, Transform> _lastFind = new ValueTuple<string, Transform>("", null);

		// Token: 0x04000209 RID: 521
		private static Canvas _keyboard;
	}
}
