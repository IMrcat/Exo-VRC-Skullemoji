using System;
using EXO.Core;
using EXO.Menus.SubMenus.WorldEx;
using EXO.Wrappers;
using EXO_Base;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRCSDK2;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x02000050 RID: 80
	internal class WorldCheats : MenuModule
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public override void LoadMenu()
		{
			WorldCheats.subMenu = new VRCPage("World Cheats", false, true, false, null, "", null, false);
			CollapsibleButtonGroup exploitGrp = new CollapsibleButtonGroup(WorldCheats.subMenu, "World Exploits", false);
			new VRCToggle(exploitGrp, "God Mode", delegate(bool val)
			{
				WorldCheats.godMode = val;
			}, false, "Off", "On", null, null, false);
			new DuoButtons(exploitGrp, "Drop Items", "Force Drops All Items", delegate
			{
				foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
					vrc_Pickup.Drop();
				}
				foreach (VRCPickup vrcPickup in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrcPickup.gameObject);
					vrcPickup.Drop();
				}
				foreach (global::VRCSDK2.VRC_Pickup SDK2vrcPickup in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(SDK2vrcPickup.gameObject);
					SDK2vrcPickup.Drop();
				}
			}, "Pull Items", "Pulls All Items To Your Position", delegate
			{
				float forceMagnitude = 20f;
				foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup2 in Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup2.gameObject);
					Vector3 direction = (PlayerWrapper.GetLocalPlayer().transform.position - vrc_Pickup2.transform.position).normalized;
					vrc_Pickup2.GetComponent<Rigidbody>().AddForce(direction * forceMagnitude, ForceMode.Impulse);
				}
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup3 in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup3.gameObject);
					Vector3 direction2 = (PlayerWrapper.GetLocalPlayer().transform.position - vrc_Pickup3.transform.position).normalized;
					vrc_Pickup3.GetComponent<Rigidbody>().AddForce(direction2 * forceMagnitude, ForceMode.Impulse);
				}
				foreach (VRCPickup vrc_Pickup4 in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup4.gameObject);
					Vector3 direction3 = (PlayerWrapper.GetLocalPlayer().transform.position - vrc_Pickup4.transform.position).normalized;
					vrc_Pickup4.GetComponent<Rigidbody>().AddForce(direction3 * forceMagnitude, ForceMode.Impulse);
				}
				foreach (VRC_ObjectSync vrc_PickupSDK in Object.FindObjectsOfType<VRC_ObjectSync>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK.gameObject);
					Vector3 direction4 = (PlayerWrapper.GetLocalPlayer().transform.position - vrc_PickupSDK.transform.position).normalized;
					vrc_PickupSDK.GetComponent<Rigidbody>().AddForce(direction4 * forceMagnitude, ForceMode.Impulse);
				}
			});
			new DuoButtons(exploitGrp, "Bring Items", "Teleports All Items To You", delegate
			{
				foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup5 in Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup5.gameObject);
					vrc_Pickup5.transform.position = PlayerWrapper.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup6 in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup6.gameObject);
					vrc_Pickup6.transform.position = PlayerWrapper.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (VRCPickup vrc_Pickup7 in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup7.gameObject);
					vrc_Pickup7.transform.position = PlayerWrapper.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (VRC_ObjectSync vrc_PickupSDK2 in Object.FindObjectsOfType<VRC_ObjectSync>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK2.gameObject);
					vrc_PickupSDK2.transform.position = PlayerWrapper.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, "Reset Items", "Respawns All Items", delegate
			{
				foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup8 in Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup8.gameObject);
					vrc_Pickup8.transform.localPosition = new Vector3(0f, -100000f, 0f);
					vrc_Pickup8.Reset();
				}
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup9 in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup9.gameObject);
					vrc_Pickup9.transform.localPosition = new Vector3(0f, -100000f, 0f);
					vrc_Pickup9.Reset();
				}
				foreach (VRCPickup vrc_Pickup10 in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup10.gameObject);
					vrc_Pickup10.transform.localPosition = new Vector3(0f, -100000f, 0f);
					vrc_Pickup10.Reset();
				}
				foreach (VRC_ObjectSync vrc_PickupSDK3 in Object.FindObjectsOfType<VRC_ObjectSync>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK3.gameObject);
					vrc_PickupSDK3.transform.localPosition = new Vector3(0f, -100000f, 0f);
					vrc_PickupSDK3.Respawn();
				}
			});
			new VRCButton(exploitGrp, "Items\nSettings", "Open Items Settings Menu", delegate
			{
				ItemMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.TagIcon), ExtentedControl.HalfType.Normal, false);
			ButtonGroup worldGrp = new ButtonGroup(WorldCheats.subMenu, "World Cheats", false, TextAnchor.UpperCenter);
			new VRCButton(worldGrp, "Murder 4", "Open Murder 4 Menu", delegate
			{
				Murder4.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconKatana), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Among Us", "Open Among Us Menu", delegate
			{
				AmongUs.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconKatana), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Ghost", "Open Ghost Menu", delegate
			{
				Ghost.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.ReticleIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Infested", "Open Infested Menu", delegate
			{
				Infested.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.ReticleIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Prison Escape", "Open Prison Escape Menu", delegate
			{
				PrisonEsc.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.ReticleIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Magic Freeze", "Open Magic Freeze TagIcon Menu", delegate
			{
				MagicFreeze.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.TagIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Zombie Tag", "Open Zombie Tag Menu", delegate
			{
				ZombieTag.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.TagIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "Sunset Bar", "Open Sunset Bar Menu", delegate
			{
				Sunset.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldGrp, "H Party", "Open Just H Party Menu", delegate
			{
				HParty.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			bool flag = !AppStart.devMode;
			if (!flag)
			{
				new VRCButton(worldGrp, "Zombie Survival", "Open Zombie Survival Menu", delegate
				{
					ZombieSurvival.subMenu.OpenMenu();
				}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
				new VRCButton(worldGrp, "STD", "Open Super Tower Defense Menu", delegate
				{
					STD.subMenu.OpenMenu();
				}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
				new VRCButton(worldGrp, "Terrors", "Open Terrors Of Nowhere Menu", delegate
				{
					TerrorsNowhere.subMenu.OpenMenu();
				}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			}
		}

		// Token: 0x04000174 RID: 372
		public static VRCPage subMenu;

		// Token: 0x04000175 RID: 373
		internal static bool godMode;
	}
}
