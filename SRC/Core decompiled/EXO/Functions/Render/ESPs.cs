using System;
using System.Collections;
using System.Collections.Generic;
using EXO.Core;
using EXO.Patches;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRCSDK2;

namespace EXO.Functions.Render
{
	// Token: 0x0200008E RID: 142
	internal class ESPs : FunctionModule
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		public override void OnInject()
		{
			JoinLeavePatch.OnPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerJoin, delegate(Player plr)
			{
				ESPs.CapsuleHighlight(plr, ESPs.playerCapsuleESP);
			});
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		public override void OnPlayerWasInit()
		{
			bool flag = ESPs.playerCapsuleESP;
			if (flag)
			{
				foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
				{
					ESPs.CapsuleHighlight(player, ESPs.playerCapsuleESP);
				}
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001DD30 File Offset: 0x0001BF30
		public static void AdjustESPAlpha(float alpha)
		{
			foreach (HighlightsFXStandalone highlightFX in ESPs.list.Values)
			{
				bool flag = highlightFX != null && highlightFX.field_Protected_Material_0 != null;
				if (flag)
				{
					Color currentColor = highlightFX.field_Protected_Material_0.GetColor("_HighlightColor");
					highlightFX.field_Protected_Material_0.SetColor("_HighlightColor", new Color(currentColor.r, currentColor.g, currentColor.b, alpha));
				}
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		public static void HandleItems(Component component, bool state)
		{
			bool flag = component == null || component.gameObject == null || !component.gameObject.active || component.name.Contains("ViewFinder") || component.name.Contains("MirrorPick") || HighlightsFX.prop_HighlightsFX_0 == null;
			if (!flag)
			{
				Il2CppArrayBase<MeshRenderer> renderers = component.GetComponentsInChildren<MeshRenderer>();
				HighlightsFXStandalone highlightFX = ESPs.GetLight(Color.red);
				foreach (MeshRenderer render in renderers)
				{
					ESPs.HighlightRender(highlightFX, render, state);
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		public static void HandleObjects(Component component, bool state, Color highlightColor)
		{
			bool flag = component == null || component.name.Contains("ViewFinder") || component.name.Contains("MirrorPick") || HighlightsFX.prop_HighlightsFX_0 == null;
			if (!flag)
			{
				Il2CppArrayBase<MeshRenderer> renderers = component.GetComponentsInChildren<MeshRenderer>();
				HighlightsFXStandalone highlightFX = ESPs.GetLight(highlightColor);
				foreach (MeshRenderer render in renderers)
				{
					ESPs.HighlightRender(highlightFX, render, state);
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001DF40 File Offset: 0x0001C140
		internal static void HighlightRender(HighlightsFXStandalone instance, Renderer renderer, bool state)
		{
			MeshFilter filter = renderer.GetComponent<MeshFilter>();
			bool flag = state && !instance.field_Protected_HashSet_1_MeshFilter_0.Contains(filter);
			if (flag)
			{
				instance.Method_Public_Void_MeshFilter_PDM_0(filter);
			}
			else
			{
				bool flag2 = !state && instance.field_Protected_HashSet_1_MeshFilter_0.Contains(filter);
				if (flag2)
				{
					instance.field_Protected_HashSet_1_MeshFilter_0.Remove(filter);
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public static HighlightsFXStandalone GetLight(Color color)
		{
			string colorName = color.ToString();
			HighlightsFXStandalone highlight;
			ESPs.list.TryGetValue(colorName, ref highlight);
			bool flag = highlight == null;
			if (flag)
			{
				highlight = HighlightsFX.prop_HighlightsFX_0.gameObject.AddComponent<HighlightsFXStandalone>();
				highlight.highlightColor = color;
				ESPs.list.Add(colorName, highlight);
			}
			return highlight;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001E000 File Offset: 0x0001C200
		public static void CapsuleHighlight(Player player, bool state)
		{
			bool flag = !player;
			if (!flag)
			{
				Transform transform = player.transform.Find("SelectRegion");
				Renderer renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
				Renderer renderer2 = renderer;
				bool flag2 = renderer2;
				if (flag2)
				{
					bool trustColors = Config.cfg.TrustColors;
					HighlightsFXStandalone highlightFX;
					if (trustColors)
					{
						highlightFX = ESPs.GetLight(player.GetTrustColor());
					}
					else
					{
						bool flag3 = player.IsFriend();
						if (flag3)
						{
							highlightFX = ESPs.GetLight(Color.yellow);
						}
						else
						{
							highlightFX = ESPs.GetLight(Color.red);
						}
					}
					ESPs.HighlightRender(highlightFX, renderer2, state);
				}
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001E09A File Offset: 0x0001C29A
		public static IEnumerator ItemHighlight()
		{
			WorldWrapper.allBaseUdonItem = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Pickup>();
			WorldWrapper.sdk2Items = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Pickup>();
			WorldWrapper.sdk3Items = Resources.FindObjectsOfTypeAll<VRCPickup>();
			WorldWrapper.allSyncItems = Resources.FindObjectsOfTypeAll<VRC_ObjectSync>();
			WorldWrapper.allSDK3SyncItems = Resources.FindObjectsOfTypeAll<VRCObjectSync>();
			WorldWrapper.allPoolItems = Resources.FindObjectsOfTypeAll<VRCObjectPool>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (global::VRC.SDKBase.VRC_Pickup VRC_Item in WorldWrapper.allBaseUdonItem)
				{
					bool flag = VRC_Item.gameObject && !processedObjects.Contains(VRC_Item.gameObject);
					if (flag)
					{
						ESPs.HandleItems(VRC_Item, ESPs.itemESP);
						processedObjects.Add(VRC_Item.gameObject);
					}
					VRC_Item = null;
				}
				global::VRC.SDKBase.VRC_Pickup[] array = null;
				foreach (global::VRCSDK2.VRC_Pickup VRC_Item2 in WorldWrapper.sdk2Items)
				{
					bool flag2 = VRC_Item2.gameObject && !processedObjects.Contains(VRC_Item2.gameObject);
					if (flag2)
					{
						ESPs.HandleItems(VRC_Item2, ESPs.itemESP);
						processedObjects.Add(VRC_Item2.gameObject);
					}
					VRC_Item2 = null;
				}
				IEnumerator<global::VRCSDK2.VRC_Pickup> enumerator = null;
				foreach (VRCPickup VRC_Item3 in WorldWrapper.sdk3Items)
				{
					bool flag3 = VRC_Item3.gameObject && !processedObjects.Contains(VRC_Item3.gameObject);
					if (flag3)
					{
						ESPs.HandleItems(VRC_Item3, ESPs.itemESP);
						processedObjects.Add(VRC_Item3.gameObject);
					}
					VRC_Item3 = null;
				}
				IEnumerator<VRCPickup> enumerator2 = null;
				foreach (VRC_ObjectSync syncItem in WorldWrapper.allSyncItems)
				{
					try
					{
						bool flag4 = syncItem.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(syncItem.gameObject);
						if (flag4)
						{
							MeshRenderer renderer = syncItem.GetComponent<MeshRenderer>();
							bool flag5 = renderer != null;
							if (flag5)
							{
								ESPs.HandleItems(renderer, ESPs.itemESP);
								processedObjects.Add(syncItem.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					syncItem = null;
				}
				VRC_ObjectSync[] array2 = null;
				foreach (VRCObjectSync sdk3SyncItem in WorldWrapper.allSDK3SyncItems)
				{
					try
					{
						bool flag6 = sdk3SyncItem.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(sdk3SyncItem.gameObject);
						if (flag6)
						{
							MeshRenderer renderer2 = sdk3SyncItem.GetComponent<MeshRenderer>();
							bool flag7 = renderer2 != null;
							if (flag7)
							{
								ESPs.HandleItems(renderer2, ESPs.itemESP);
								processedObjects.Add(sdk3SyncItem.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					sdk3SyncItem = null;
				}
				IEnumerator<VRCObjectSync> enumerator3 = null;
				foreach (VRCObjectPool poolItem in WorldWrapper.allPoolItems)
				{
					try
					{
						bool flag8 = poolItem.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(poolItem.gameObject);
						if (flag8)
						{
							MeshRenderer renderer3 = poolItem.GetComponent<MeshRenderer>();
							bool flag9 = renderer3 != null;
							if (flag9)
							{
								ESPs.HandleItems(renderer3, ESPs.itemESP);
								processedObjects.Add(poolItem.gameObject);
							}
							renderer3 = null;
						}
					}
					catch
					{
					}
					poolItem = null;
				}
				IEnumerator<VRCObjectPool> enumerator4 = null;
				bool flag10 = !ESPs.itemESP;
				if (flag10)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001E0A2 File Offset: 0x0001C2A2
		public static IEnumerator TriggerHighlight()
		{
			WorldWrapper.allTriggers = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Trigger>();
			WorldWrapper.allTriggerCol = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger>();
			WorldWrapper.allSDK2Triggers = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Trigger>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (global::VRC.SDKBase.VRC_Trigger trigger in WorldWrapper.allTriggers)
				{
					try
					{
						bool flag = trigger.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(trigger.gameObject);
						if (flag)
						{
							MeshRenderer renderer = trigger.GetComponent<MeshRenderer>();
							bool flag2 = renderer != null;
							if (flag2)
							{
								ESPs.HandleObjects(renderer, ESPs.triggerESP, Color.red);
								processedObjects.Add(trigger.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					trigger = null;
				}
				IEnumerator<global::VRC.SDKBase.VRC_Trigger> enumerator = null;
				foreach (global::VRC.SDKBase.VRC_TriggerColliderEventTrigger triggerCol in WorldWrapper.allTriggerCol)
				{
					try
					{
						bool flag3 = triggerCol.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(triggerCol.gameObject);
						if (flag3)
						{
							MeshRenderer renderer2 = triggerCol.GetComponent<MeshRenderer>();
							bool flag4 = renderer2 != null;
							if (flag4)
							{
								ESPs.HandleObjects(renderer2, ESPs.triggerESP, Color.red);
								processedObjects.Add(triggerCol.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					triggerCol = null;
				}
				IEnumerator<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger> enumerator2 = null;
				foreach (global::VRCSDK2.VRC_Trigger sdk2Trigger in WorldWrapper.allSDK2Triggers)
				{
					try
					{
						bool flag5 = sdk2Trigger.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(sdk2Trigger.gameObject);
						if (flag5)
						{
							MeshRenderer renderer3 = sdk2Trigger.GetComponent<MeshRenderer>();
							bool flag6 = renderer3 != null;
							if (flag6)
							{
								ESPs.HandleObjects(renderer3, ESPs.triggerESP, Color.red);
								processedObjects.Add(sdk2Trigger.gameObject);
							}
							renderer3 = null;
						}
					}
					catch
					{
					}
					sdk2Trigger = null;
				}
				IEnumerator<global::VRCSDK2.VRC_Trigger> enumerator3 = null;
				bool flag7 = !ESPs.triggerESP;
				if (flag7)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001E0AA File Offset: 0x0001C2AA
		public static IEnumerator IntractableHighlight()
		{
			WorldWrapper.allInteractable = Resources.FindObjectsOfTypeAll<VRCInteractable>();
			WorldWrapper.allBaseInteractable = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Interactable>();
			WorldWrapper.allSDK2Interactable = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Interactable>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (VRCInteractable interactable in WorldWrapper.allInteractable)
				{
					try
					{
						bool flag = interactable.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(interactable.gameObject);
						if (flag)
						{
							MeshRenderer renderer = interactable.GetComponent<MeshRenderer>();
							bool flag2 = renderer != null;
							if (flag2)
							{
								ESPs.HandleObjects(renderer, ESPs.interactESP, Color.red);
								processedObjects.Add(interactable.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					interactable = null;
				}
				IEnumerator<VRCInteractable> enumerator = null;
				foreach (global::VRC.SDKBase.VRC_Interactable baseInteractable in WorldWrapper.allBaseInteractable)
				{
					try
					{
						bool flag3 = baseInteractable.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(baseInteractable.gameObject);
						if (flag3)
						{
							MeshRenderer renderer2 = baseInteractable.GetComponent<MeshRenderer>();
							bool flag4 = renderer2 != null;
							if (flag4)
							{
								ESPs.HandleObjects(renderer2, ESPs.interactESP, Color.red);
								processedObjects.Add(baseInteractable.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					baseInteractable = null;
				}
				IEnumerator<global::VRC.SDKBase.VRC_Interactable> enumerator2 = null;
				foreach (global::VRCSDK2.VRC_Interactable sdk2Interactable in WorldWrapper.allSDK2Interactable)
				{
					try
					{
						bool flag5 = sdk2Interactable.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(sdk2Interactable.gameObject);
						if (flag5)
						{
							MeshRenderer renderer3 = sdk2Interactable.GetComponent<MeshRenderer>();
							bool flag6 = renderer3 != null;
							if (flag6)
							{
								ESPs.HandleObjects(renderer3, ESPs.interactESP, Color.red);
								processedObjects.Add(sdk2Interactable.gameObject);
							}
							renderer3 = null;
						}
					}
					catch
					{
					}
					sdk2Interactable = null;
				}
				IEnumerator<global::VRCSDK2.VRC_Interactable> enumerator3 = null;
				bool flag7 = !ESPs.interactESP;
				if (flag7)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001E0B2 File Offset: 0x0001C2B2
		public static IEnumerator BoxColliderHighlight()
		{
			Il2CppArrayBase<BoxCollider> BoxCol = Resources.FindObjectsOfTypeAll<BoxCollider>();
			Il2CppArrayBase<BoxCollider2D> BoxCol2D = Resources.FindObjectsOfTypeAll<BoxCollider2D>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (BoxCollider boxCol in BoxCol)
				{
					try
					{
						bool flag = boxCol.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(boxCol.gameObject);
						if (flag)
						{
							MeshRenderer renderer = boxCol.GetComponent<MeshRenderer>();
							bool flag2 = renderer != null;
							if (flag2)
							{
								ESPs.HandleObjects(renderer, ESPs.boxColESP, Color.red);
								processedObjects.Add(boxCol.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					boxCol = null;
				}
				IEnumerator<BoxCollider> enumerator = null;
				foreach (BoxCollider2D boxCol2D in BoxCol2D)
				{
					try
					{
						bool flag3 = boxCol2D.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(boxCol2D.gameObject);
						if (flag3)
						{
							MeshRenderer renderer2 = boxCol2D.GetComponent<MeshRenderer>();
							bool flag4 = renderer2 != null;
							if (flag4)
							{
								ESPs.HandleObjects(renderer2, ESPs.boxColESP, Color.red);
								processedObjects.Add(boxCol2D.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					boxCol2D = null;
				}
				IEnumerator<BoxCollider2D> enumerator2 = null;
				bool flag5 = !ESPs.boxColESP;
				if (flag5)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001E0BA File Offset: 0x0001C2BA
		public static IEnumerator RigidbodyHighlight()
		{
			Il2CppArrayBase<Rigidbody> Rigidbody = Resources.FindObjectsOfTypeAll<Rigidbody>();
			Il2CppArrayBase<Rigidbody2D> Rigidbody2D = Resources.FindObjectsOfTypeAll<Rigidbody2D>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (Rigidbody rigidbody in Rigidbody)
				{
					try
					{
						bool flag = rigidbody.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(rigidbody.gameObject);
						if (flag)
						{
							MeshRenderer renderer = rigidbody.GetComponent<MeshRenderer>();
							bool flag2 = renderer != null;
							if (flag2)
							{
								ESPs.HandleObjects(renderer, ESPs.rigidbodyESP, Color.red);
								processedObjects.Add(rigidbody.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					rigidbody = null;
				}
				IEnumerator<Rigidbody> enumerator = null;
				foreach (Rigidbody2D rigidbody2D in Rigidbody2D)
				{
					try
					{
						bool flag3 = rigidbody2D.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(rigidbody2D.gameObject);
						if (flag3)
						{
							MeshRenderer renderer2 = rigidbody2D.GetComponent<MeshRenderer>();
							bool flag4 = renderer2 != null;
							if (flag4)
							{
								ESPs.HandleObjects(renderer2, ESPs.rigidbodyESP, Color.red);
								processedObjects.Add(rigidbody2D.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					rigidbody2D = null;
				}
				IEnumerator<Rigidbody2D> enumerator2 = null;
				bool flag5 = !ESPs.rigidbodyESP;
				if (flag5)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001E0C2 File Offset: 0x0001C2C2
		public static IEnumerator UdonHighlight()
		{
			WorldWrapper.udonBehaviours = Resources.FindObjectsOfTypeAll<UdonBehaviour>();
			WorldWrapper.udonSync = Resources.FindObjectsOfTypeAll<UdonSync>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			HashSet<GameObject> processedObjects = new HashSet<GameObject>();
			for (;;)
			{
				foreach (UdonBehaviour udonBehaviour in WorldWrapper.udonBehaviours)
				{
					try
					{
						bool flag = udonBehaviour.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(udonBehaviour.gameObject);
						if (flag)
						{
							MeshRenderer renderer = udonBehaviour.GetComponent<MeshRenderer>();
							bool flag2 = renderer != null;
							if (flag2)
							{
								ESPs.HandleObjects(renderer, ESPs.udonESP, Color.red);
								processedObjects.Add(udonBehaviour.gameObject);
							}
							renderer = null;
						}
					}
					catch
					{
					}
					udonBehaviour = null;
				}
				UdonBehaviour[] array = null;
				foreach (UdonSync udonSync in WorldWrapper.udonSync)
				{
					try
					{
						bool flag3 = udonSync.gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null) && !processedObjects.Contains(udonSync.gameObject);
						if (flag3)
						{
							MeshRenderer renderer2 = udonSync.GetComponent<MeshRenderer>();
							bool flag4 = renderer2 != null;
							if (flag4)
							{
								ESPs.HandleObjects(renderer2, ESPs.udonESP, Color.green);
								processedObjects.Add(udonSync.gameObject);
							}
							renderer2 = null;
						}
					}
					catch
					{
					}
					udonSync = null;
				}
				IEnumerator<UdonSync> enumerator = null;
				bool flag5 = !ESPs.udonESP;
				if (flag5)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001E0CA File Offset: 0x0001C2CA
		public static IEnumerator FlatBufferHighlight()
		{
			Il2CppArrayBase<FlatBufferNetworkSerializer> buf = Resources.FindObjectsOfTypeAll<FlatBufferNetworkSerializer>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			for (;;)
			{
				int num;
				for (int i = 0; i < buf.Length; i = num + 1)
				{
					try
					{
						bool flag = buf[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null);
						if (flag)
						{
							MeshRenderer renderer = buf[i].GetComponent<MeshRenderer>() ?? buf[i].gameObject.AddComponent<MeshRenderer>();
							ESPs.HandleObjects(renderer, ESPs.bufferESP, Color.red);
							renderer = null;
						}
					}
					catch
					{
					}
					num = i;
				}
				bool flag2 = !ESPs.bufferESP;
				if (flag2)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001E0D2 File Offset: 0x0001C2D2
		public static IEnumerator UdonProxyHighlight()
		{
			WorldWrapper.udonOnTrigger = Resources.FindObjectsOfTypeAll<OnTriggerStayProxy>();
			WorldWrapper.udonOnCol = Resources.FindObjectsOfTypeAll<OnCollisionStayProxy>();
			WorldWrapper.udonOnRender = Resources.FindObjectsOfTypeAll<OnRenderObjectProxy>();
			while (RoomManager.field_Internal_Static_ApiWorld_0 == null)
			{
				yield return null;
			}
			for (;;)
			{
				int num;
				for (int i = 0; i < WorldWrapper.udonOnTrigger.Length; i = num + 1)
				{
					try
					{
						bool flag = WorldWrapper.udonOnTrigger[i].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null);
						if (flag)
						{
							bool flag2 = WorldWrapper.udonOnTrigger[i].GetComponent<MeshRenderer>() != null;
							if (flag2)
							{
								ESPs.HandleObjects(WorldWrapper.udonOnTrigger[i].GetComponent<MeshRenderer>(), ESPs.udonProxyESP, Color.red);
							}
						}
					}
					catch
					{
					}
					num = i;
				}
				for (int j = 0; j < WorldWrapper.udonOnCol.Length; j = num + 1)
				{
					try
					{
						bool flag3 = WorldWrapper.udonOnCol[j].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null);
						if (flag3)
						{
							bool flag4 = WorldWrapper.udonOnCol[j].GetComponent<MeshRenderer>() != null;
							if (flag4)
							{
								ESPs.HandleObjects(WorldWrapper.udonOnCol[j].GetComponent<MeshRenderer>(), ESPs.udonProxyESP, Color.green);
							}
						}
					}
					catch
					{
					}
					num = j;
				}
				for (int k = 0; k < WorldWrapper.udonOnRender.Length; k = num + 1)
				{
					try
					{
						bool flag5 = WorldWrapper.udonOnRender[k].gameObject && !(HighlightsFX.prop_HighlightsFX_0 == null);
						if (flag5)
						{
							bool flag6 = WorldWrapper.udonOnRender[k].GetComponent<MeshRenderer>() != null;
							if (flag6)
							{
								ESPs.HandleObjects(WorldWrapper.udonOnRender[k].GetComponent<MeshRenderer>(), ESPs.udonProxyESP, Color.blue);
							}
						}
					}
					catch
					{
					}
					num = k;
				}
				bool flag7 = !ESPs.udonProxyESP;
				if (flag7)
				{
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
			yield break;
		}

		// Token: 0x0400028F RID: 655
		private static Dictionary<string, HighlightsFXStandalone> list = new Dictionary<string, HighlightsFXStandalone>();

		// Token: 0x04000290 RID: 656
		internal static bool itemESP;

		// Token: 0x04000291 RID: 657
		internal static bool triggerESP;

		// Token: 0x04000292 RID: 658
		internal static bool boxColESP;

		// Token: 0x04000293 RID: 659
		internal static bool playerCapsuleESP;

		// Token: 0x04000294 RID: 660
		internal static bool rigidbodyESP;

		// Token: 0x04000295 RID: 661
		internal static bool interactESP;

		// Token: 0x04000296 RID: 662
		internal static bool udonESP;

		// Token: 0x04000297 RID: 663
		internal static bool udonProxyESP;

		// Token: 0x04000298 RID: 664
		internal static bool bufferESP;
	}
}
