using System;
using System.Collections;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Wrappers;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x0200005A RID: 90
	internal class STD : MenuModule
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public override void LoadMenu()
		{
			STD.subMenu = new VRCPage("Super Tower Defence", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(STD.subMenu, "STD", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "Auto Regen", delegate(bool val)
			{
				STD.RegenLoopState = val;
				if (val)
				{
					CoroutineManager.RunCoroutine(STD.RegenLoop());
				}
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Reset Money", "Resets your money to 4500", delegate
			{
				GameObject.Find("Bank").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Restart");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Regen Health", "Restores your health", delegate
			{
				GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Revive");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Create Last tower", "Creates the last tower you placed down", delegate
			{
				GameObject tower = GameObject.Find("TowerCannon1Grabbable (1)");
				Networking.LocalPlayer.TakeOwnership(tower);
				tower.GetComponent<VRCPickup>().currentlyHeldBy = PlayerWrapper.LocalPlayer;
				tower.transform.position = new Vector3(5.4498f, -0.1651f, 0.8042f);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Upgrade Last Tower", "Upgrades the last tower you placed down", delegate
			{
				GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TryUpgradeTower");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Reset All Towers", "Clears all you towers", delegate
			{
				GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ClearTowers");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011074 File Offset: 0x0000F274
		internal static IEnumerator RegenLoop()
		{
			bool flag;
			do
			{
				GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Revive");
				yield return new WaitForSeconds(2f);
				flag = !STD.RegenLoopState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x04000193 RID: 403
		public static VRCPage subMenu;

		// Token: 0x04000194 RID: 404
		internal static bool RegenLoopState;
	}
}
