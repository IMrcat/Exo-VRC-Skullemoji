using System;
using UnityEngine;
using VRC.UI.Elements;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.QM.Controls
{
	// Token: 0x02000013 RID: 19
	public class WorldPage : Root
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004BFB File Offset: 0x00002DFB
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004C03 File Offset: 0x00002E03
		public string MenuName { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004C0C File Offset: 0x00002E0C
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004C14 File Offset: 0x00002E14
		public Action OnMenuOpen { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C1D File Offset: 0x00002E1D
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004C25 File Offset: 0x00002E25
		public UIPage Page { get; internal set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004C2E File Offset: 0x00002E2E
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004C36 File Offset: 0x00002E36
		public Transform MenuContents { get; internal set; }
	}
}
