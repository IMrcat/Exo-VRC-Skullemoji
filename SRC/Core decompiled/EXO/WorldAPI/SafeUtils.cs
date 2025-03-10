using System;
using EXO.LogTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace WorldAPI
{
	// Token: 0x0200000A RID: 10
	public static class SafeUtils
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A18 File Offset: 0x00000C18
		public static bool BringItem(string path)
		{
			return SafeUtils.BringItem(GameObject.Find(path), SafeUtils.GetLocalPlayer().transform.localPosition);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A34 File Offset: 0x00000C34
		public static bool BringItem(GameObject Obj)
		{
			return SafeUtils.BringItem(Obj, SafeUtils.GetLocalPlayer().transform.localPosition);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A4C File Offset: 0x00000C4C
		public static bool BringItem(GameObject Obj, Vector3 Poz)
		{
			bool flag = Obj == null;
			bool flag2;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				flag2 = false;
			}
			else
			{
				Networking.LocalPlayer.TakeOwnership(Obj);
				Obj.transform.position = Poz + new Vector3(0f, 0.15f, 0f);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public static GameObject GetLocalPlayer()
		{
			bool flag = SafeUtils.LocalPlayer != null;
			GameObject gameObject2;
			if (flag)
			{
				gameObject2 = SafeUtils.LocalPlayer;
			}
			else
			{
				foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
				{
					bool flag2 = gameObject.name.StartsWith("VRCPlayer[Local]");
					if (flag2)
					{
						return gameObject;
					}
				}
				gameObject2 = null;
			}
			return gameObject2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static bool SafeSendUdon(GameObject Obj, string Event, NetworkEventTarget targets = NetworkEventTarget.All)
		{
			bool flag = Obj == null;
			bool flag2;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				flag2 = false;
			}
			else
			{
				UdonBehaviour udon;
				Obj.TryGetComponent<UdonBehaviour>(out udon);
				bool flag3 = udon == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					udon.SendCustomNetworkEvent(targets, Event);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static bool SafeSendUdon(string ObjName, string Event, NetworkEventTarget targets = NetworkEventTarget.All)
		{
			GameObject Obj = GameObject.Find(ObjName);
			bool flag = Obj == null;
			bool flag2;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				flag2 = false;
			}
			else
			{
				UdonBehaviour udon;
				Obj.TryGetComponent<UdonBehaviour>(out udon);
				bool flag3 = udon == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					udon.SendCustomNetworkEvent(targets, Event);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public static T SafeGrabComponet<T>(this GameObject Obj) where T : Component
		{
			bool flag = Obj == null;
			T t;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				t = default(T);
			}
			else
			{
				T component;
				Obj.TryGetComponent<T>(out component);
				bool flag2 = component == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = component;
				}
			}
			return t;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C48 File Offset: 0x00000E48
		public static T SafeGrabComponet<T>(this Transform Obj) where T : Component
		{
			bool flag = Obj == null;
			T t;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				t = default(T);
			}
			else
			{
				T component;
				Obj.TryGetComponent<T>(out component);
				bool flag2 = component == null;
				if (flag2)
				{
					CLog.E("Component T Is Null!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\SafeUtils.cs", 135);
					t = default(T);
				}
				else
				{
					t = component;
				}
			}
			return t;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static GameObject SimpleSafeInstantiate(string ObjName, string parent)
		{
			bool flag = string.IsNullOrEmpty(ObjName) || string.IsNullOrEmpty(parent);
			GameObject gameObject;
			if (flag)
			{
				gameObject = null;
			}
			else
			{
				GameObject Obj = GameObject.Find(ObjName);
				bool flag2 = Obj == null;
				if (flag2)
				{
					Logs.Error("Obj Is Null!", null);
					gameObject = null;
				}
				else
				{
					GameObject Parent = GameObject.Find(parent);
					bool flag3 = Parent == null;
					if (flag3)
					{
						gameObject = null;
					}
					else
					{
						GameObject NewObject = Object.Instantiate<GameObject>(Obj, Parent.transform);
						gameObject = NewObject;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static GameObject SimpleSafeInstantiate(GameObject Obj, GameObject Parent)
		{
			bool flag = Obj == null;
			GameObject gameObject;
			if (flag)
			{
				Logs.Error("Obj Is Null!", null);
				gameObject = null;
			}
			else
			{
				bool flag2 = Parent == null;
				if (flag2)
				{
					gameObject = null;
				}
				else
				{
					GameObject NewObject = Object.Instantiate<GameObject>(Obj, Parent.transform);
					gameObject = NewObject;
				}
			}
			return gameObject;
		}

		// Token: 0x04000022 RID: 34
		public static GameObject LocalPlayer;
	}
}
