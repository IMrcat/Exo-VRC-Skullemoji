using System;

namespace EXO.Modules
{
	// Token: 0x02000074 RID: 116
	public class RuntimeJson
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00016A71 File Offset: 0x00014C71
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00016A79 File Offset: 0x00014C79
		public string name { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00016A82 File Offset: 0x00014C82
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00016A8A File Offset: 0x00014C8A
		public string udonEvent { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00016A93 File Offset: 0x00014C93
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00016A9B File Offset: 0x00014C9B
		public string eventObject { get; set; } = null;

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00016AA4 File Offset: 0x00014CA4
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00016AAC File Offset: 0x00014CAC
		public string desc { get; set; } = "";
	}
}
