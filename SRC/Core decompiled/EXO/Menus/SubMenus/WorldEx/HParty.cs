using System;
using System.Collections;
using System.Collections.Generic;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Patches;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x02000057 RID: 87
	internal class HParty : MenuModule
	{
		// Token: 0x0600034F RID: 847 RVA: 0x000106A4 File Offset: 0x0000E8A4
		public override void LoadMenu()
		{
			HParty.subMenu = new VRCPage("Just H Party", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(HParty.subMenu, "H Party", false, TextAnchor.UpperCenter);
			JoinLeavePatch.OnLocalPlayerLeave = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerLeave, delegate(Player player)
			{
				HParty.dropToggle.State = false;
			});
			new VRCButton(mainGrp, "Floor 1", "Teleports you to the first floor", delegate
			{
				HParty.EnableFloors(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(0f, 0f, 0f);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Floor 2", "Teleports you to the first floor", delegate
			{
				HParty.EnableFloors(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(3f, 54f, 11.5f);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Floor 3", "Teleports you to the first floor", delegate
			{
				HParty.EnableFloors(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(1f, 112f, 11.5f);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "VIP Room", "Teleports you to the VIP Room", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(20f, 34f, 301f);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "SF Room 1", "Teleports you to SF Room 1", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(20f, -22f, 338f);
			}, "SF Room 2", "Teleports you to SF Room 2", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(20f, -22f, 172f);
			});
			new DuoButtons(mainGrp, "Normal Room 1", "Teleports you to Normal Room 1", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(21f, 34f, 334f);
			}, "Normal Room 2", "Teleports you to Normal Room 2", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(21f, 34f, 169f);
			});
			new DuoButtons(mainGrp, "Japan Room 1", "Teleports you to Japan Room 1", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(24f, 10f, 334f);
			}, "Japan Room 2", "Teleports you to Japan Room 2", delegate
			{
				HParty.EnableRooms(true);
				HParty.RemoveBlocks(true);
				PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(24f, 10f, 169f);
			});
			HParty.dropToggle = new VRCToggle(mainGrp, "Floor Drop", delegate(bool val)
			{
				HParty.floorDrop = val;
				CoroutineManager.RunCoroutine(HParty.Drop());
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Hear Mute Area", "Allows you to hear people in the mute area on the 2nd floor", delegate
			{
				foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
				{
					bool flag = obj.name.Contains("soundproof", 1);
					if (flag)
					{
						bool flag2 = obj.gameObject != null;
						if (flag2)
						{
							Object.Destroy(obj);
						}
					}
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010964 File Offset: 0x0000EB64
		internal static void EnableRooms(bool val)
		{
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				bool flag = obj.name.Contains("room", 1);
				if (flag)
				{
					obj.SetActive(val);
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000109CC File Offset: 0x0000EBCC
		internal static void RemoveBlocks(bool val)
		{
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				bool flag = obj.name.Contains("blockarea", 1);
				if (flag)
				{
					obj.SetActive(!val);
				}
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010A34 File Offset: 0x0000EC34
		internal static void EnableFloors(bool val)
		{
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				bool flag = obj.name.Contains("main", 1);
				if (flag)
				{
					obj.SetActive(val);
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010A9C File Offset: 0x0000EC9C
		internal static IEnumerator Drop()
		{
			List<UdonBehaviour> cubes = new List<UdonBehaviour>();
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				bool flag = obj.name.Contains("cube.165", 1);
				if (flag)
				{
					cubes.Add(obj.GetComponent<UdonBehaviour>());
				}
				obj = null;
			}
			IEnumerator<GameObject> enumerator = null;
			bool flag2;
			do
			{
				try
				{
					foreach (UdonBehaviour cube in cubes)
					{
						cube.SendCustomNetworkEvent(NetworkEventTarget.All, "GrantControl");
						cube = null;
					}
					List<UdonBehaviour>.Enumerator enumerator2 = default(List<UdonBehaviour>.Enumerator);
				}
				catch
				{
				}
				yield return new WaitForSeconds(0.1f);
				flag2 = !HParty.floorDrop;
			}
			while (!flag2);
			yield break;
			yield break;
		}

		// Token: 0x0400018E RID: 398
		public static VRCPage subMenu;

		// Token: 0x0400018F RID: 399
		private static bool floorDrop;

		// Token: 0x04000190 RID: 400
		internal static VRCToggle dropToggle;
	}
}
