using System;
using System.Collections;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;

namespace EXO.Functions.Item
{
	// Token: 0x020000A5 RID: 165
	internal class AntiTheft : FunctionModule
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00022AE4 File Offset: 0x00020CE4
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00022AEB File Offset: 0x00020CEB
		internal static bool Enabled { get; set; }

		// Token: 0x06000637 RID: 1591 RVA: 0x00022AF4 File Offset: 0x00020CF4
		public override void OnUpdate()
		{
			bool flag = !AntiTheft.Enabled && !AntiTheft._desktopHolding;
			if (!flag)
			{
				bool flag2;
				if (WorldWrapper.In_World)
				{
					Player localPlayer = PlayerWrapper.LocalPlayer;
					flag2 = ((localPlayer != null) ? localPlayer.GetHandGraspers() : null) == null;
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (!flag3)
				{
					bool mouseButtonDown = Input.GetMouseButtonDown(1);
					if (mouseButtonDown)
					{
						AntiTheft._desktopHolding = false;
					}
					VRCHandGrasper handLeft = PlayerWrapper.GetHandGrasper(VRC_Pickup.PickupHand.Left);
					VRCHandGrasper handRight = PlayerWrapper.LocalPlayer.GetHandGraspers()[1];
					VRCInput handLeftInput = handLeft.field_Internal_VRCInput_2;
					VRCInput handRightInput = handRight.field_Internal_VRCInput_2;
					bool flag4 = AntiTheft.AnT1 != null && (AntiTheft._desktopHolding || handRightInput.field_Public_Single_0 != 0f);
					if (flag4)
					{
						AntiTheft.HoldItem(AntiTheft.AnT1, VRC_Pickup.PickupHand.Right, true);
					}
					else
					{
						bool flag5 = (!AntiTheft._desktopHolding || handRightInput.field_Public_Single_0 == 0f) && AntiTheft.AnT1 != null;
						if (flag5)
						{
							AntiTheft.StrongDrop(VRC_Pickup.PickupHand.Right);
							AntiTheft.AnT1 = null;
						}
						else
						{
							bool flag6 = handRight.field_Internal_VRC_Pickup_0 != null && AntiTheft.AnT1 == null;
							if (flag6)
							{
								AntiTheft.AnT1 = handRight.field_Internal_VRC_Pickup_0.gameObject;
							}
						}
					}
					bool flag7 = AntiTheft.AnT2 != null && (AntiTheft._desktopHolding || handLeftInput.field_Public_Single_0 != 0f);
					if (flag7)
					{
						AntiTheft.HoldItem(AntiTheft.AnT2, VRC_Pickup.PickupHand.Left, true);
					}
					else
					{
						bool flag8 = handLeft.field_Internal_VRC_Pickup_0 != null && AntiTheft.AnT2 == null;
						if (flag8)
						{
							AntiTheft.AnT2 = handLeft.field_Internal_VRC_Pickup_0.gameObject;
						}
						else
						{
							bool flag9 = (!AntiTheft._desktopHolding || handLeftInput.field_Public_Single_0 == 0f) && AntiTheft.AnT2 != null;
							if (flag9)
							{
								AntiTheft.StrongDrop(VRC_Pickup.PickupHand.Left);
								AntiTheft.AnT2 = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00022CD8 File Offset: 0x00020ED8
		internal static void HoldPickup(VRC_Pickup pickup, VRC_Pickup.PickupHand hand = VRC_Pickup.PickupHand.Right)
		{
			CoroutineManager.RunCoroutine(AntiTheft._hold(pickup.gameObject, hand));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00022CEC File Offset: 0x00020EEC
		internal static void HoldPickup(Transform pickup, VRC_Pickup.PickupHand hand = VRC_Pickup.PickupHand.Right)
		{
			CoroutineManager.RunCoroutine(AntiTheft._hold(pickup.gameObject, hand));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00022D00 File Offset: 0x00020F00
		internal static void HoldPickup(GameObject pickup, VRC_Pickup.PickupHand hand = VRC_Pickup.PickupHand.Right)
		{
			CoroutineManager.RunCoroutine(AntiTheft._hold(pickup, hand));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00022D0F File Offset: 0x00020F0F
		private static IEnumerator _hold(GameObject obj, VRC_Pickup.PickupHand hand = VRC_Pickup.PickupHand.Right)
		{
			bool flag = (AntiTheft._usingL && hand == VRC_Pickup.PickupHand.Left) || (AntiTheft._usingR && hand == VRC_Pickup.PickupHand.Right);
			if (flag)
			{
				yield break;
			}
			obj.TakeOwnershipIfNecessary();
			obj.transform.localPosition = PlayerWrapper.LocalPlayer.transform.position + new Vector3(0f, 0.5f, 0f);
			bool flag2 = hand == VRC_Pickup.PickupHand.Left;
			if (flag2)
			{
				AntiTheft._usingL = true;
			}
			else
			{
				bool flag3 = hand == VRC_Pickup.PickupHand.Right;
				if (flag3)
				{
					AntiTheft._usingR = true;
				}
			}
			int num;
			for (int i = 0; i < 30; i = num + 1)
			{
				obj.TakeOwnershipIfNecessary();
				AntiTheft.HoldItem(obj, hand, false);
				yield return new WaitForSeconds(0.01f);
				num = i;
			}
			bool flag4 = hand == VRC_Pickup.PickupHand.Left;
			if (flag4)
			{
				AntiTheft._usingL = false;
			}
			else
			{
				bool flag5 = hand == VRC_Pickup.PickupHand.Right;
				if (flag5)
				{
					AntiTheft._usingR = false;
				}
			}
			yield break;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00022D28 File Offset: 0x00020F28
		private static void StrongDrop(VRC_Pickup.PickupHand hand)
		{
			VRCHandGrasper VrcHand = PlayerWrapper.GetHandGrasper(hand);
			bool flag = VrcHand.field_Internal_VRC_Pickup_0 == null;
			if (!flag)
			{
				VRC_Pickup field_Internal_VRC_Pickup_ = VrcHand.field_Internal_VRC_Pickup_0;
				if (field_Internal_VRC_Pickup_ != null)
				{
					field_Internal_VRC_Pickup_.Drop();
				}
				VrcHand.field_Private_Boolean_1 = false;
				VrcHand.field_Private_Boolean_2 = false;
				VrcHand.field_Private_List_1_VRC_Interactable_0 = new List<VRC_Interactable>();
				VrcHand.field_Internal_VRC_Pickup_0.currentLocalPlayer = null;
				VrcHand.field_Internal_VRC_Pickup_0.currentlyHeldBy = null;
				VrcHand.field_Internal_VRC_Pickup_0 = null;
				VrcHand.field_Private_Rigidbody_0 = null;
				VrcHand.field_Private_VRC_Pickup_0 = null;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00022DB0 File Offset: 0x00020FB0
		private static void HoldItem(GameObject obj, VRC_Pickup.PickupHand hand, bool sync = false)
		{
			VRCHandGrasper VrcHand = PlayerWrapper.GetHandGrasper(hand);
			bool flag2 = !PlayerWrapper.IsInVR();
			if (flag2)
			{
				AntiTheft._desktopHolding = true;
			}
			obj.TakeOwnershipIfNecessary();
			VrcHand.field_Internal_VRC_Pickup_0 = obj.GetComponent<VRC_Pickup>();
			VrcHand.field_Private_Rigidbody_0 = obj.GetComponent<Rigidbody>();
			VrcHand.field_Private_VRC_Pickup_0 = obj.GetComponent<VRC_Pickup>();
			List<VRC_Interactable> interactions = new List<VRC_Interactable>();
			foreach (UdonBehaviour inteac in obj.GetComponents<UdonBehaviour>())
			{
				interactions.Add(inteac);
			}
			VrcHand.field_Private_Boolean_3 = true;
			VrcHand.field_Private_List_1_VRC_Interactable_0 = interactions;
			VrcHand.field_Internal_VRC_Pickup_0.currentLocalPlayer = Networking.LocalPlayer;
			VrcHand.field_Internal_VRC_Pickup_0.currentlyHeldBy = VrcHand;
			VrcHand.field_Internal_VRC_Pickup_0.AutoHold = VRC_Pickup.AutoHoldMode.Yes;
			obj.TakeOwnershipIfNecessary();
			bool flag3 = !sync;
			if (flag3)
			{
				bool flag;
				VrcHand.Method_Private_Void_byref_Boolean_0(out flag);
				bool flag4 = flag;
				if (flag4)
				{
					VrcHand.field_Private_Single_1 = Time.fixedTime;
				}
			}
		}

		// Token: 0x040002EA RID: 746
		internal static GameObject AnT1;

		// Token: 0x040002EB RID: 747
		internal static GameObject AnT2;

		// Token: 0x040002EC RID: 748
		private static bool _usingL;

		// Token: 0x040002ED RID: 749
		private static bool _usingR;

		// Token: 0x040002EE RID: 750
		private static bool _desktopHolding;
	}
}
