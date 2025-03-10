using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using VRC.Localization;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.MM;

namespace WorldAPI.ButtonAPI.Controls
{
	// Token: 0x02000010 RID: 16
	public class Root
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003F4C File Offset: 0x0000214C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003F54 File Offset: 0x00002154
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
				bool flag = this.TMProCompnt != null;
				if (flag)
				{
					this.TMProCompnt.text = this._text;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003F8B File Offset: 0x0000218B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003F93 File Offset: 0x00002193
		public GameObject gameObject { get; internal set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003FA4 File Offset: 0x000021A4
		public Transform transform { get; internal set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003FAD File Offset: 0x000021AD
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003FB5 File Offset: 0x000021B5
		public TextMeshProUGUI TMProCompnt { get; internal set; }

		// Token: 0x06000076 RID: 118 RVA: 0x00003FBE File Offset: 0x000021BE
		public virtual void SetActive(bool Active)
		{
			this.gameObject.SetActive(Active);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003FCD File Offset: 0x000021CD
		public void SetTextColor(Color color)
		{
			this.TMProCompnt.color = color;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003FDC File Offset: 0x000021DC
		public void SetTextColor(string Hex)
		{
			TMP_Text tmproCompnt = this.TMProCompnt;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(16, 2);
			defaultInterpolatedStringHandler.AppendLiteral("<color=");
			defaultInterpolatedStringHandler.AppendFormatted(Hex);
			defaultInterpolatedStringHandler.AppendLiteral(">");
			defaultInterpolatedStringHandler.AppendFormatted(this.Text);
			defaultInterpolatedStringHandler.AppendLiteral("</color>");
			tmproCompnt.text = defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004044 File Offset: 0x00002244
		public void SetRotation(Vector3 Poz)
		{
			this.gameObject.transform.localRotation = Quaternion.Euler(Poz);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000405D File Offset: 0x0000225D
		public void SetPostion(Vector3 Poz)
		{
			this.gameObject.transform.localPosition = Poz;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004071 File Offset: 0x00002271
		public GameObject GetGameObject()
		{
			return this.gameObject;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004079 File Offset: 0x00002279
		public Transform GetTransform()
		{
			return this.gameObject.transform;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004088 File Offset: 0x00002288
		public Transform ChangeParent(GameObject newParent)
		{
			return this.gameObject.transform.parent = newParent.transform;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000040AF File Offset: 0x000022AF
		public Transform AlsoAddToMM(MMPage pg)
		{
			return Object.Instantiate<Transform>(this.gameObject.transform, pg.MenuContents);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000040C8 File Offset: 0x000022C8
		public virtual string SetToolTip(string tip)
		{
			bool Fi = false;
			foreach (ToolTip s in this.gameObject.GetComponentsInChildren<ToolTip>())
			{
				bool flag = !Fi;
				if (flag)
				{
					Fi = true;
					s._localizableString = tip.Localize(null, null, null);
					s.enabled = !string.IsNullOrEmpty(tip);
				}
				else
				{
					s.enabled = false;
				}
			}
			return tip;
		}

		// Token: 0x04000040 RID: 64
		public string _text;
	}
}
