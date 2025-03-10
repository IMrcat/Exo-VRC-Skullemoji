using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRCSDK2;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Controls;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000064 RID: 100
	internal class BringItems
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00012DD8 File Offset: 0x00010FD8
		internal static void MakeMenu()
		{
			BringItems.target = PlayerWrapper.GetSelectedUser;
			BringItems.selectPage = new VRCPage("World Items", false, true, false, null, "", null, false);
			List<global::VRC.SDKBase.VRC_Pickup> pickups = Enumerable.ToList<global::VRC.SDKBase.VRC_Pickup>(Enumerable.Where<global::VRC.SDKBase.VRC_Pickup>(WorldWrapper.allBaseUdonItem, (global::VRC.SDKBase.VRC_Pickup pickup) => pickup.gameObject.activeInHierarchy));
			int totalObjects = pickups.Count;
			CollapsibleButtonGroup itemSelGrp = new CollapsibleButtonGroup(BringItems.selectPage, "Bring Items", false);
			WorldPage worldPage = BringItems.selectPage;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(6, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(totalObjects);
			defaultInterpolatedStringHandler.AppendLiteral(" Items");
			BringItems.bringItemGrp = new ButtonGroup(worldPage, defaultInterpolatedStringHandler.ToStringAndClear(), false, TextAnchor.UpperCenter);
			BringItems.bringItemGrp.RemoveAllChildren();
			BringItems.selectPage.OpenMenu();
			SelectMenu.UpdateDisplayTarget(BringItems.target, BringItems.selectPage);
			new VRCButton(itemSelGrp, "Bring All Items", "Bring All Items To User", delegate
			{
				BringItems.target = PlayerWrapper.GetSelectedUser;
				foreach (global::VRC.SDKBase.VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<global::VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
					vrc_Pickup.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup2 in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup2.gameObject);
					vrc_Pickup2.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (VRCPickup vrc_Pickup3 in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup3.gameObject);
					vrc_Pickup3.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.1f, 0f);
				}
				foreach (VRC_ObjectSync vrc_PickupSDK in Object.FindObjectsOfType<VRC_ObjectSync>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK.gameObject);
					vrc_PickupSDK.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(itemSelGrp, "Pull All Items", "Pull All Items To User", delegate
			{
				BringItems.target = PlayerWrapper.GetSelectedUser;
				float forceMagnitude = 10f;
				foreach (global::VRC.SDKBase.VRC_Pickup vrc_Pickup4 in Object.FindObjectsOfType<global::VRC.SDKBase.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup4.gameObject);
					Vector3 direction = (BringItems.target.transform.position - vrc_Pickup4.transform.position).normalized;
					vrc_Pickup4.GetComponent<Rigidbody>().AddForce(direction * forceMagnitude, ForceMode.Impulse);
				}
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup5 in Object.FindObjectsOfType<global::VRCSDK2.VRC_Pickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup5.gameObject);
					Vector3 direction2 = (BringItems.target.transform.position - vrc_Pickup5.transform.position).normalized;
					vrc_Pickup5.GetComponent<Rigidbody>().AddForce(direction2 * forceMagnitude, ForceMode.Impulse);
				}
				foreach (VRCPickup vrc_Pickup6 in Object.FindObjectsOfType<VRCPickup>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup6.gameObject);
					Vector3 direction3 = (BringItems.target.transform.position - vrc_Pickup6.transform.position).normalized;
					vrc_Pickup6.GetComponent<Rigidbody>().AddForce(direction3 * forceMagnitude, ForceMode.Impulse);
				}
				foreach (VRC_ObjectSync vrc_PickupSDK2 in Object.FindObjectsOfType<VRC_ObjectSync>())
				{
					Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK2.gameObject);
					Vector3 direction4 = (BringItems.target.transform.position - vrc_PickupSDK2.transform.position).normalized;
					vrc_PickupSDK2.GetComponent<Rigidbody>().AddForce(direction4 * forceMagnitude, ForceMode.Impulse);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			for (int i = 0; i < pickups.Count; i += 2)
			{
				global::VRC.SDKBase.VRC_Pickup pickup1 = pickups[i];
				global::VRC.SDKBase.VRC_Pickup pickup2 = ((i + 1 < pickups.Count) ? pickups[i + 1] : null);
				bool flag = pickup2 != null;
				if (flag)
				{
					new DuoButtons(BringItems.bringItemGrp, pickup1.name, "Bring " + pickup1.name + " To " + BringItems.target.DisplayName(), delegate
					{
						Networking.SetOwner(PlayerWrapper.LocalVRCPlayerAPI, pickup1.gameObject);
						pickup1.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.5f, 0f);
					}, pickup2.name, "Bring " + pickup2.name + " To " + BringItems.target.DisplayName(), delegate
					{
						Networking.SetOwner(PlayerWrapper.LocalVRCPlayerAPI, pickup2.gameObject);
						pickup2.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.5f, 0f);
					});
				}
				else
				{
					new VRCButton(BringItems.bringItemGrp, pickup1.name, "Bring " + pickup1.name + " To " + BringItems.target.DisplayName(), delegate
					{
						Networking.SetOwner(PlayerWrapper.LocalVRCPlayerAPI, pickup1.gameObject);
						pickup1.transform.position = BringItems.target.transform.position + new Vector3(0f, 0.5f, 0f);
					}, false, false, null, ExtentedControl.HalfType.Normal, false);
				}
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013062 File Offset: 0x00011262
		internal static IEnumerator PullLoop()
		{
			float forceMagnitude = 1f;
			bool flag;
			do
			{
				foreach (global::VRC.SDKBase.VRC_Pickup vrc_Pickup in WorldWrapper.allBaseUdonItem)
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
					Vector3 direction = (BringItems.target.transform.position - vrc_Pickup.transform.position).normalized;
					vrc_Pickup.GetComponent<Rigidbody>().AddForce(direction * forceMagnitude, ForceMode.Impulse);
					direction = default(Vector3);
					vrc_Pickup = null;
				}
				global::VRC.SDKBase.VRC_Pickup[] array = null;
				foreach (global::VRCSDK2.VRC_Pickup vrc_Pickup2 in WorldWrapper.sdk2Items)
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup2.gameObject);
					Vector3 direction2 = (BringItems.target.transform.position - vrc_Pickup2.transform.position).normalized;
					vrc_Pickup2.GetComponent<Rigidbody>().AddForce(direction2 * forceMagnitude, ForceMode.Impulse);
					direction2 = default(Vector3);
					vrc_Pickup2 = null;
				}
				IEnumerator<global::VRCSDK2.VRC_Pickup> enumerator = null;
				foreach (VRCPickup vrc_Pickup3 in WorldWrapper.sdk3Items)
				{
					Networking.LocalPlayer.TakeOwnership(vrc_Pickup3.gameObject);
					Vector3 direction3 = (BringItems.target.transform.position - vrc_Pickup3.transform.position).normalized;
					vrc_Pickup3.GetComponent<Rigidbody>().AddForce(direction3 * forceMagnitude, ForceMode.Impulse);
					direction3 = default(Vector3);
					vrc_Pickup3 = null;
				}
				IEnumerator<VRCPickup> enumerator2 = null;
				yield return new WaitForSeconds(1f);
				flag = !BringItems.pullState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x040001AB RID: 427
		internal static Player target;

		// Token: 0x040001AC RID: 428
		public static VRCPage selectPage;

		// Token: 0x040001AD RID: 429
		internal static ButtonGroup bringItemGrp;

		// Token: 0x040001AE RID: 430
		internal static bool pullState;
	}
}
