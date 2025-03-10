using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.Groups
{
	// Token: 0x02000021 RID: 33
	public class CollapsibleButtonGroup : ButtonGroupControl
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006EBF File Offset: 0x000050BF
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006EC7 File Offset: 0x000050C7
		public bool IsOpen { get; internal set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006ED0 File Offset: 0x000050D0
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006ED8 File Offset: 0x000050D8
		public GameObject headerObj { get; internal set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006EE1 File Offset: 0x000050E1
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006EE9 File Offset: 0x000050E9
		public ButtonGroup buttonGroup { get; internal set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006EF2 File Offset: 0x000050F2
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006EFA File Offset: 0x000050FA
		public Transform QMCParent { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006F03 File Offset: 0x00005103
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006F0B File Offset: 0x0000510B
		public Action<bool> OnClose { get; set; }

		// Token: 0x06000138 RID: 312 RVA: 0x00006F14 File Offset: 0x00005114
		public CollapsibleButtonGroup(Transform parent, string text, bool openByDefault = true)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			this.headerObj = Object.Instantiate<GameObject>(APIBase.ColpButtonGrp, parent);
			this.headerObj.name = text + "_CollapsibleButtonGroup";
			this.headerObj.transform.Find("QM_Settings_Panel/VerticalLayoutGroup").DestroyChildren(null);
			this.QMCParent = this.headerObj.transform.Find("QM_Settings_Panel/VerticalLayoutGroup");
			base.TMProCompnt = this.headerObj.transform.Find("QM_Foldout/Label").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.richText = true;
			base.TMProCompnt.text = text;
			base.gameObject = (this.buttonGroup = new ButtonGroup(parent, string.Empty, true, TextAnchor.UpperCenter)).gameObject;
			base.GroupContents = this.buttonGroup.GroupContents;
			this.OnClose = delegate(bool val)
			{
				this.buttonGroup.gameObject.SetActive(val);
				this.IsOpen = val;
			};
			Toggle foldout = this.headerObj.transform.Find("QM_Foldout/Background_Button").GetComponent<Toggle>();
			foldout.isOn = openByDefault;
			foldout.onValueChanged.AddListener(delegate(bool val)
			{
				Action<bool> onClose = this.OnClose;
				if (onClose != null)
				{
					onClose.Invoke(val);
				}
			});
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007064 File Offset: 0x00005264
		public CollapsibleButtonGroup(WorldPage page, string text, bool openByDefault = false)
			: this(page.MenuContents, text, openByDefault)
		{
		}
	}
}
