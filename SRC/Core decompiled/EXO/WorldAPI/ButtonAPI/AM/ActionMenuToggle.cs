using System;
using UnityEngine;

namespace WorldAPI.ButtonAPI.AM
{
	// Token: 0x0200002B RID: 43
	public class ActionMenuToggle
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000876C File Offset: 0x0000696C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00008774 File Offset: 0x00006974
		public bool State { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000877D File Offset: 0x0000697D
		public PedalOption CurrentPedalOption
		{
			get
			{
				return this.actionButton.currentPedalOption;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000878C File Offset: 0x0000698C
		internal Texture2D OnImage
		{
			get
			{
				bool flag = this._onImage == null;
				if (flag)
				{
					this._onImage = ActionMenuToggle.defaultOnImage;
				}
				bool flag2 = this._onImage != null;
				if (flag2)
				{
					bool flag3 = !this._onImage.hideFlags.HasFlag(HideFlags.DontSave);
					if (flag3)
					{
						this._onImage.hideFlags |= HideFlags.DontUnloadUnusedAsset;
					}
				}
				return this._onImage;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008808 File Offset: 0x00006A08
		internal Texture2D OffImage
		{
			get
			{
				bool flag = this._offImage == null;
				if (flag)
				{
					this._offImage = ActionMenuToggle.defaultOffImage;
				}
				bool flag2 = this._offImage != null;
				if (flag2)
				{
					bool flag3 = !this._offImage.hideFlags.HasFlag(HideFlags.DontSave);
					if (flag3)
					{
						this._offImage.hideFlags |= HideFlags.DontUnloadUnusedAsset;
					}
				}
				return this._offImage;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008884 File Offset: 0x00006A84
		public ActionMenuToggle(ActionMenuPage basePage, string text, Action<bool> action, bool state = false, Texture2D onIcon = null, Texture2D offIcon = null)
		{
			ActionMenuToggle <>4__this = this;
			bool flag = !onIcon.hideFlags.HasFlag(HideFlags.DontSave);
			if (flag)
			{
				onIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			}
			bool flag2 = !offIcon.hideFlags.HasFlag(HideFlags.DontSave);
			if (flag2)
			{
				offIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
			}
			this._onImage = onIcon ?? ActionMenuToggle.defaultOnImage;
			this._offImage = offIcon ?? ActionMenuToggle.defaultOffImage;
			this.State = state;
			this.actionButton = new ActionMenuButton(basePage, text, delegate
			{
				<>4__this.State = !<>4__this.State;
				action.Invoke(<>4__this.State);
				ActionMenuButton actionMenuButton = <>4__this.actionButton;
				if (actionMenuButton != null)
				{
					actionMenuButton.SetIcon(<>4__this.State ? <>4__this.OnImage : <>4__this.OffImage);
				}
			}, state ? this.OnImage : this.OffImage);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008963 File Offset: 0x00006B63
		public void SetState(bool newState)
		{
			this.State = newState;
			this.actionButton.SetIcon(newState ? this.OnImage : this.OffImage);
		}

		// Token: 0x040000AC RID: 172
		private ActionMenuButton actionButton;

		// Token: 0x040000AD RID: 173
		private Texture2D _onImage;

		// Token: 0x040000AE RID: 174
		private Texture2D _offImage;

		// Token: 0x040000AF RID: 175
		private static readonly Texture2D defaultOnImage = APIBase.OnSprite.texture;

		// Token: 0x040000B0 RID: 176
		private static readonly Texture2D defaultOffImage = APIBase.OffSprite.texture;
	}
}
