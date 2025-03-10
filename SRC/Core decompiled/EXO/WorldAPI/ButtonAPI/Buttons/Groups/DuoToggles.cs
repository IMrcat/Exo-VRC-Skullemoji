using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.Buttons.Groups
{
	// Token: 0x0200001E RID: 30
	public class DuoToggles : Root
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000069C1 File Offset: 0x00004BC1
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000069C9 File Offset: 0x00004BC9
		public Transform ObjectHolder { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000069D2 File Offset: 0x00004BD2
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000069DA File Offset: 0x00004BDA
		public VRCToggle ToggleOne { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000069E3 File Offset: 0x00004BE3
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000069EB File Offset: 0x00004BEB
		public VRCToggle ToggleTwo { get; private set; }

		// Token: 0x0600011F RID: 287 RVA: 0x000069F4 File Offset: 0x00004BF4
		public DuoToggles(GameObject menu, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange, string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2, Sprite OnImageSprite = null, Sprite OffImageSprite = null, float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			(base.transform = (base.gameObject = new GameObject("Button_DuoToggles")).transform).parent = menu.transform;
			QMUtils.ResetTransform(base.transform);
			base.gameObject.AddComponent<LayoutElement>();
			(this.ObjectHolder = new GameObject("Holder").transform).parent = base.gameObject.transform;
			QMUtils.ResetTransform(this.ObjectHolder);
			this.ObjectHolder.localPosition = new Vector3(0f, -3f, 0f);
			(this.ToggleOne = new VRCToggle(this.ObjectHolder, text, BoolStateChange, FirstState, Ontooltip, OffTooltip, OnImageSprite, OffImageSprite, false)).TurnHalf(new Vector3(0f, 50f, 0f), FirstFontSize);
			(this.ToggleTwo = new VRCToggle(this.ObjectHolder, text2, BoolStateChange2, SecondState, Ontooltip2, OffTooltip2, OnImageSprite, OffImageSprite, false)).TurnHalf(new Vector3(0f, -51f, 0f), SecondFontSize);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006B3C File Offset: 0x00004D3C
		public DuoToggles(ButtonGroupControl btnGrp, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange, string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2, Sprite OnImageSprite = null, Sprite OffImageSprite = null, float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false)
			: this(btnGrp.GroupContents, text, Ontooltip, OffTooltip, BoolStateChange, text2, Ontooltip2, OffTooltip2, BoolStateChange2, OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, FirstState, SecondState)
		{
		}
	}
}
