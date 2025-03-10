using System;
using System.Reflection;

namespace EXO
{
	// Token: 0x0200002F RID: 47
	public class ConfigSettings
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008C79 File Offset: 0x00006E79
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00008C81 File Offset: 0x00006E81
		public bool QMConsole { get; set; } = true;

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008C8A File Offset: 0x00006E8A
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00008C92 File Offset: 0x00006E92
		public bool BoldQMConsole { get; set; } = true;

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008C9B File Offset: 0x00006E9B
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00008CA3 File Offset: 0x00006EA3
		public bool UdonLogger { get; set; } = true;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008CAC File Offset: 0x00006EAC
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public bool GodModeLogger { get; set; } = true;

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008CBD File Offset: 0x00006EBD
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00008CC5 File Offset: 0x00006EC5
		public bool ScreenLogger { get; set; } = true;

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008CCE File Offset: 0x00006ECE
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00008CD6 File Offset: 0x00006ED6
		public bool BoldScreenLogs { get; set; } = false;

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008CDF File Offset: 0x00006EDF
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00008CE7 File Offset: 0x00006EE7
		public bool LogFriendActivity { get; set; } = false;

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008CF0 File Offset: 0x00006EF0
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008CF8 File Offset: 0x00006EF8
		public bool LogFriendLocations { get; set; } = false;

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008D01 File Offset: 0x00006F01
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008D09 File Offset: 0x00006F09
		public bool LogFriendInstanceInfo { get; set; } = false;

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008D12 File Offset: 0x00006F12
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008D1A File Offset: 0x00006F1A
		public bool LogFriendsToHUD { get; set; } = false;

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008D23 File Offset: 0x00006F23
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00008D2B File Offset: 0x00006F2B
		public bool FlyToggle { get; set; } = false;

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008D34 File Offset: 0x00006F34
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00008D3C File Offset: 0x00006F3C
		public bool SpeedToggle { get; set; } = false;

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008D45 File Offset: 0x00006F45
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00008D4D File Offset: 0x00006F4D
		public bool JetPackToggle { get; set; } = true;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008D56 File Offset: 0x00006F56
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00008D5E File Offset: 0x00006F5E
		public bool AutoJumpState { get; set; } = true;

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008D67 File Offset: 0x00006F67
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00008D6F File Offset: 0x00006F6F
		public bool ForceGrab { get; set; } = true;

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008D78 File Offset: 0x00006F78
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008D80 File Offset: 0x00006F80
		public bool AntiDrop { get; set; } = true;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008D89 File Offset: 0x00006F89
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00008D91 File Offset: 0x00006F91
		public bool ItemReach { get; set; } = true;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008D9A File Offset: 0x00006F9A
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00008DA2 File Offset: 0x00006FA2
		public bool InteractReach { get; set; } = true;

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008DAB File Offset: 0x00006FAB
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00008DB3 File Offset: 0x00006FB3
		public bool AntiUdonBind { get; set; } = true;

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008DBC File Offset: 0x00006FBC
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public bool MinecraftFly { get; set; } = true;

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008DCD File Offset: 0x00006FCD
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00008DD5 File Offset: 0x00006FD5
		public bool FlyBind { get; set; } = true;

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008DDE File Offset: 0x00006FDE
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00008DE6 File Offset: 0x00006FE6
		public bool FlyVRBind { get; set; } = true;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008DEF File Offset: 0x00006FEF
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008DF7 File Offset: 0x00006FF7
		public bool NoClipBind { get; set; } = true;

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008E00 File Offset: 0x00007000
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00008E08 File Offset: 0x00007008
		public bool NoClipVRBind { get; set; } = true;

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008E11 File Offset: 0x00007011
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00008E19 File Offset: 0x00007019
		public bool SpeedBind { get; set; } = true;

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008E22 File Offset: 0x00007022
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008E2A File Offset: 0x0000702A
		public bool SpeedVRBind { get; set; } = true;

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008E33 File Offset: 0x00007033
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00008E3B File Offset: 0x0000703B
		public bool ZoomBind { get; set; } = true;

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008E44 File Offset: 0x00007044
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00008E4C File Offset: 0x0000704C
		public bool ThirdPersonBind { get; set; } = true;

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008E55 File Offset: 0x00007055
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00008E5D File Offset: 0x0000705D
		public bool MouseTpBind { get; set; } = true;

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008E66 File Offset: 0x00007066
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00008E6E File Offset: 0x0000706E
		public bool SerializationBind { get; set; } = true;

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008E77 File Offset: 0x00007077
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00008E7F File Offset: 0x0000707F
		public bool HeadFlipper { get; set; } = false;

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008E88 File Offset: 0x00007088
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008E90 File Offset: 0x00007090
		public bool MenuRecolor { get; set; } = false;

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008E99 File Offset: 0x00007099
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008EA1 File Offset: 0x000070A1
		public bool IdSpoof { get; set; } = true;

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008EAA File Offset: 0x000070AA
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008EB2 File Offset: 0x000070B2
		public bool AntiBlock { get; set; } = true;

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00008EBB File Offset: 0x000070BB
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00008EC3 File Offset: 0x000070C3
		public float MenuMusic { get; set; } = 0.5f;

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008ECC File Offset: 0x000070CC
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00008ED4 File Offset: 0x000070D4
		public float LoadingMusic { get; set; } = 0.2f;

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00008EDD File Offset: 0x000070DD
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00008EE5 File Offset: 0x000070E5
		public bool ScreenLogFade { get; set; } = true;

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008EEE File Offset: 0x000070EE
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00008EF6 File Offset: 0x000070F6
		public bool TrustColors { get; set; } = true;

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008EFF File Offset: 0x000070FF
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00008F07 File Offset: 0x00007107
		public bool NamePlates { get; set; } = true;

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008F10 File Offset: 0x00007110
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008F18 File Offset: 0x00007118
		public bool TabExtension { get; set; } = false;

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008F21 File Offset: 0x00007121
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008F29 File Offset: 0x00007129
		public int TabsPerRow { get; set; } = 7;

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008F32 File Offset: 0x00007132
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00008F3A File Offset: 0x0000713A
		public bool DisplayHUD { get; set; } = true;

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008F43 File Offset: 0x00007143
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00008F4B File Offset: 0x0000714B
		public bool QMBackground { get; set; } = true;

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008F54 File Offset: 0x00007154
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00008F5C File Offset: 0x0000715C
		public bool MMBackground { get; set; } = true;

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008F65 File Offset: 0x00007165
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00008F6D File Offset: 0x0000716D
		public float BackgroundTransparency { get; set; } = 0.95f;

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008F76 File Offset: 0x00007176
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00008F7E File Offset: 0x0000717E
		public bool ForceUpdate16 { get; set; } = true;

		// Token: 0x060001FD RID: 509 RVA: 0x00008F88 File Offset: 0x00007188
		internal static void WriteConfig()
		{
			Type type = Config.cfg.GetType();
			Config.Log("");
			Config.Log("Config:");
			PropertyInfo[] properties = type.GetProperties(20);
			foreach (PropertyInfo property in properties)
			{
				string name = property.Name;
				string text = ": ";
				object value = property.GetValue(Config.cfg);
				Config.Log(name + text + ((value != null) ? value.ToString() : null));
			}
		}
	}
}
