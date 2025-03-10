using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.Buttons.Groups
{
	// Token: 0x0200001F RID: 31
	public class GrpButtons : Root
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006B71 File Offset: 0x00004D71
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006B79 File Offset: 0x00004D79
		public Transform ObjectHolder { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006B82 File Offset: 0x00004D82
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006B8A File Offset: 0x00004D8A
		public VRCButton ButtonOne { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006B93 File Offset: 0x00004D93
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006B9B File Offset: 0x00004D9B
		public VRCButton ButtonTwo { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006BA4 File Offset: 0x00004DA4
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00006BAC File Offset: 0x00004DAC
		public VRCButton ButtonThree { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public GrpButtons(GameObject menu, string FirstName, string FirstTooltip, Action action, string SecondName = null, string SecondTooltip = null, Action Secondaction = null, string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
		{
			(base.transform = (base.gameObject = new GameObject("Button_GroupOfHalfButtons")).transform).parent = menu.transform;
			QMUtils.ResetTransform(base.transform);
			base.gameObject.AddComponent<LayoutElement>();
			(this.ObjectHolder = new GameObject("Holder").transform).parent = base.gameObject.transform;
			QMUtils.ResetTransform(this.ObjectHolder);
			this.ObjectHolder.localPosition = new Vector3(0f, -3f, 0f);
			this.ButtonOne = new VRCButton(this.ObjectHolder, FirstName, FirstTooltip, action, true, false, null, ExtentedControl.HalfType.Top, true);
			this.ButtonOne.transform.localPosition = new Vector3(0f, 10.7f, 0f);
			bool flag = Secondaction != null;
			if (flag)
			{
				(this.ButtonTwo = new VRCButton(this.ObjectHolder, SecondName, SecondTooltip, Secondaction, true, false, null, ExtentedControl.HalfType.Normal, true)).transform.localPosition = new Vector3(0f, -1.36f, 0f);
			}
			bool flag2 = thirdaction != null;
			if (flag2)
			{
				(this.ButtonThree = new VRCButton(this.ObjectHolder, thirdName, thirdTooltip, thirdaction, true, false, null, ExtentedControl.HalfType.Bottom, true)).transform.localPosition = new Vector3(0f, -13.88f, 0f);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006D3C File Offset: 0x00004F3C
		public GrpButtons(ButtonGroupControl grp, string FirstName, string FirstTooltip, Action action, string SecondName = null, string SecondTooltip = null, Action Secondaction = null, string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
			: this(grp.GroupContents, FirstName, FirstTooltip, action, SecondName, SecondTooltip, Secondaction, thirdName, thirdTooltip, thirdaction)
		{
		}
	}
}
