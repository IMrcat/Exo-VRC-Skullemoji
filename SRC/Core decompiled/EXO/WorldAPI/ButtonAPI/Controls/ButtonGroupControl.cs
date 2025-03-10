using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.Controls
{
	// Token: 0x0200000E RID: 14
	public class ButtonGroupControl : Root
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000038E9 File Offset: 0x00001AE9
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000038F1 File Offset: 0x00001AF1
		public GameObject headerGameObject { get; internal set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000038FA File Offset: 0x00001AFA
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003902 File Offset: 0x00001B02
		public GameObject GroupContents { get; internal set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000390B File Offset: 0x00001B0B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003913 File Offset: 0x00001B13
		public RectMask2D parentMenuMask { get; internal set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000391C File Offset: 0x00001B1C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003924 File Offset: 0x00001B24
		public bool WasNoText { get; internal set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000392D File Offset: 0x00001B2D
		public List<VRCButton> Buttons
		{
			get
			{
				return Enumerable.ToList<VRCButton>(Enumerable.Where<VRCButton>(this._buttons, (VRCButton x) => x.gameObject != null));
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000395E File Offset: 0x00001B5E
		public List<VRCToggle> Toggles
		{
			get
			{
				return Enumerable.ToList<VRCToggle>(Enumerable.Where<VRCToggle>(this._toggles, (VRCToggle x) => x.gameObject != null));
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000398F File Offset: 0x00001B8F
		public void RemoveAllChildren()
		{
			this.GroupContents.transform.DestroyChildren(null);
			this._buttons.Clear();
			this._toggles.Clear();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039BC File Offset: 0x00001BBC
		public VRCButton AddButton(string text, string tooltip, Action listener, bool Half = false, bool SubMenuIcon = false, Sprite Icon = null)
		{
			return new VRCButton(base.gameObject.transform, text, tooltip, listener, Half, SubMenuIcon, Icon, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000039E4 File Offset: 0x00001BE4
		public VRCButton AddButton(string text, string tooltip, Action<VRCButton> listener, bool Half = false, bool SubMenuIcon = false, Sprite Icon = null)
		{
			return new VRCButton(base.gameObject.transform, text, tooltip, listener, Half, SubMenuIcon, Icon, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A0C File Offset: 0x00001C0C
		public VRCToggle AddToggle(string Ontext, Action<bool> listener, bool DefaultState = false, string OffTooltip = null, string OnToolTip = null, Sprite onSprite = null, Sprite offSprite = null, bool Half = false)
		{
			return new VRCToggle(base.gameObject.transform, Ontext, listener, DefaultState, OffTooltip, OnToolTip, onSprite, offSprite, Half);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003A36 File Offset: 0x00001C36
		public VRCLable AddLable(string text, string LowerText, Action onClick = null, bool Bg = true)
		{
			return new VRCLable(base.gameObject.transform, text, LowerText, onClick, Bg);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003A50 File Offset: 0x00001C50
		public GrpButtons AddGrpOfButtons(string FirstName, string FirstTooltip, Action action, string SecondName = null, string SecondTooltip = null, Action Secondaction = null, string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
		{
			return new GrpButtons(base.gameObject, FirstName, FirstTooltip, action, SecondName, SecondTooltip, Secondaction, thirdName, thirdTooltip, thirdaction);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003A78 File Offset: 0x00001C78
		public DuoToggles AddGrpToggles(string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange, string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2, Sprite OnImageSprite = null, Sprite OffImageSprite = null, float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false)
		{
			return new DuoToggles(base.gameObject, text, Ontooltip, OffTooltip, BoolStateChange, text2, Ontooltip2, OffTooltip2, BoolStateChange2, OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, FirstState, SecondState);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003AAC File Offset: 0x00001CAC
		public DuoButtons AddDuoButtons(string buttonOne, string buttonOneTooltip, Action btnAction, string buttonTwo, string buttonTwoTooltip, Action buttonTwoAction)
		{
			return new DuoButtons(base.gameObject, buttonOne, buttonOneTooltip, delegate(DuoButtons _)
			{
				btnAction.Invoke();
			}, buttonTwo, buttonTwoTooltip, delegate(DuoButtons _)
			{
				buttonTwoAction.Invoke();
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public DuoButtons AddDuoButtons(string buttonOne, string buttonOneTooltip, Action<DuoButtons> btnAction, string buttonTwo, string buttonTwoTooltip, Action<DuoButtons> buttonTwoAction)
		{
			return new DuoButtons(base.gameObject, buttonOne, buttonOneTooltip, btnAction, buttonTwo, buttonTwoTooltip, buttonTwoAction);
		}

		// Token: 0x04000035 RID: 53
		internal List<VRCButton> _buttons = new List<VRCButton>();

		// Token: 0x04000036 RID: 54
		internal List<VRCToggle> _toggles = new List<VRCToggle>();
	}
}
