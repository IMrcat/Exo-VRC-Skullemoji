using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.Buttons.Groups
{
	// Token: 0x0200001D RID: 29
	public class DuoButtons : Root
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000067A8 File Offset: 0x000049A8
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000067B0 File Offset: 0x000049B0
		public Transform ObjectHolder { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000067B9 File Offset: 0x000049B9
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000067C1 File Offset: 0x000049C1
		public VRCButton ButtonOne { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000067CA File Offset: 0x000049CA
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000067D2 File Offset: 0x000049D2
		public VRCButton ButtonTwo { get; private set; }

		// Token: 0x06000116 RID: 278 RVA: 0x000067DC File Offset: 0x000049DC
		public DuoButtons(GameObject menu, string buttonOne, string buttonOneTooltip, Action<DuoButtons> btnAction, string buttonTwo, string buttonTwoTooltip, Action<DuoButtons> buttonTwoAction)
		{
			DuoButtons <>4__this = this;
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
			(this.ButtonOne = new VRCButton(this.ObjectHolder, buttonOne, buttonOneTooltip, delegate
			{
				btnAction.Invoke(<>4__this);
			}, true, false, null, ExtentedControl.HalfType.Normal, false)).transform.localPosition = new Vector3(0f, 50f, 0f);
			(this.ButtonTwo = new VRCButton(this.ObjectHolder, buttonTwo, buttonTwoTooltip, delegate
			{
				buttonTwoAction.Invoke(<>4__this);
			}, true, false, null, ExtentedControl.HalfType.Normal, false)).transform.localPosition = new Vector3(0f, -51f, 0f);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006958 File Offset: 0x00004B58
		public DuoButtons(ButtonGroupControl grp, string buttonOne, string buttonOneTooltip, Action btnAction, string buttonTwo, string buttonTwoTooltip, Action buttonTwoAction)
			: this(grp.GroupContents, buttonOne, buttonOneTooltip, delegate(DuoButtons _)
			{
				btnAction.Invoke();
			}, buttonTwo, buttonTwoTooltip, delegate(DuoButtons _)
			{
				buttonTwoAction.Invoke();
			})
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000069A7 File Offset: 0x00004BA7
		public DuoButtons(ButtonGroupControl grp, string buttonOne, string buttonOneTooltip, Action<DuoButtons> btnAction, string buttonTwo, string buttonTwoTooltip, Action<DuoButtons> buttonTwoAction)
			: this(grp.GroupContents, buttonOne, buttonOneTooltip, btnAction, buttonTwo, buttonTwoTooltip, buttonTwoAction)
		{
		}
	}
}
