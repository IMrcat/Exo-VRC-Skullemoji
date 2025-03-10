using System;
using EXO.Core;
using EXO.Wrappers;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRCSDK2;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x02000058 RID: 88
	internal class ItemMenu : MenuModule
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public override void LoadMenu()
		{
			ItemMenu.subMenu = new VRCPage("Items", false, true, false, null, "", null, false);
			ButtonGroup itemsGrp = new ButtonGroup(ItemMenu.subMenu, "Items", false, TextAnchor.UpperCenter);
			new VRCToggle(itemsGrp, "Force Grab", delegate(bool val)
			{
				WorldWrapper.FindItems();
				Config.cfg.ForceGrab = val;
			}, Config.cfg.ForceGrab, "Off", "On", null, null, false);
			new VRCToggle(itemsGrp, "Anti Drop", delegate(bool val)
			{
				WorldWrapper.FindItems();
				Config.cfg.AntiDrop = val;
			}, Config.cfg.AntiDrop, "Off", "On", null, null, false);
			new VRCToggle(itemsGrp, "Item Reach", delegate(bool val)
			{
				WorldWrapper.FindItems();
				Config.cfg.ItemReach = val;
			}, Config.cfg.ItemReach, "Off", "On", null, null, false);
			new VRCToggle(itemsGrp, "Interactable Reach", delegate(bool val)
			{
				WorldWrapper.FindInteractables();
				Config.cfg.InteractReach = val;
			}, Config.cfg.InteractReach, "Off", "On", null, null, false);
			new VRCToggle(itemsGrp, "Hide Items", delegate(bool val)
			{
				ItemMenu.ItemHide<VRC.SDKBase.VRC_Pickup>(val);
				ItemMenu.ItemHide<global::VRCSDK2.VRC_Pickup>(val);
				ItemMenu.ItemHide<VRCPickup>(val);
			}, false, "Off", "On", null, null, false);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010C2C File Offset: 0x0000EE2C
		internal static void ItemHide<T>(bool state) where T : Component
		{
			T[] items = Resources.FindObjectsOfTypeAll<T>();
			foreach (T item in items)
			{
				bool flag = item.gameObject.layer == 13;
				if (flag)
				{
					item.gameObject.SetActive(!state);
				}
			}
		}

		// Token: 0x04000191 RID: 401
		public static VRCPage subMenu;
	}
}
