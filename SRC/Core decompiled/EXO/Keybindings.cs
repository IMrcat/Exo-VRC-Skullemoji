using System;
using System.Reflection;

namespace EXO
{
	// Token: 0x02000030 RID: 48
	public class Keybindings
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000916A File Offset: 0x0000736A
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00009172 File Offset: 0x00007372
		public string FlyBind1 { get; set; } = "F";

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000917B File Offset: 0x0000737B
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00009183 File Offset: 0x00007383
		public string FlyBind2 { get; set; } = "LeftControl";

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000918C File Offset: 0x0000738C
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00009194 File Offset: 0x00007394
		public string VRFlyBind1 { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000919D File Offset: 0x0000739D
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000091A5 File Offset: 0x000073A5
		public string VRFlyBind2 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000091AE File Offset: 0x000073AE
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000091B6 File Offset: 0x000073B6
		public string NoClipBind1 { get; set; } = "X";

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000091BF File Offset: 0x000073BF
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000091C7 File Offset: 0x000073C7
		public string NoClipBind2 { get; set; } = "LeftControl";

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000091D0 File Offset: 0x000073D0
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000091D8 File Offset: 0x000073D8
		public string VRNoClipBind1 { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000091E1 File Offset: 0x000073E1
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000091E9 File Offset: 0x000073E9
		public string VRNoClipBind2 { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000091F2 File Offset: 0x000073F2
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000091FA File Offset: 0x000073FA
		public string SpeedBind1 { get; set; } = "G";

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009203 File Offset: 0x00007403
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000920B File Offset: 0x0000740B
		public string SpeedBind2 { get; set; } = "LeftControl";

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009214 File Offset: 0x00007414
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000921C File Offset: 0x0000741C
		public string VRSpeedBind1 { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009225 File Offset: 0x00007425
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000922D File Offset: 0x0000742D
		public string VRSpeedBind2 { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009236 File Offset: 0x00007436
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000923E File Offset: 0x0000743E
		public string FlyUpBtn { get; set; } = "E";

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009247 File Offset: 0x00007447
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000924F File Offset: 0x0000744F
		public string FlyDownBtn { get; set; } = "Q";

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009258 File Offset: 0x00007458
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009260 File Offset: 0x00007460
		public string ZoomBind1 { get; set; } = "Mouse4";

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009269 File Offset: 0x00007469
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009271 File Offset: 0x00007471
		public string ZoomBind2 { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000927A File Offset: 0x0000747A
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009282 File Offset: 0x00007482
		public string CameraResetBind { get; set; } = "Mouse2";

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000928B File Offset: 0x0000748B
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00009293 File Offset: 0x00007493
		public string ThirdPersonBind1 { get; set; } = "T";

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000929C File Offset: 0x0000749C
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000092A4 File Offset: 0x000074A4
		public string ThirdPersonBind2 { get; set; } = "LeftControl";

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000092AD File Offset: 0x000074AD
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000092B5 File Offset: 0x000074B5
		public string MouseTpBind1 { get; set; } = "Mouse1";

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000092BE File Offset: 0x000074BE
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000092C6 File Offset: 0x000074C6
		public string MouseTpBind2 { get; set; } = "LeftControl";

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000092CF File Offset: 0x000074CF
		// (set) Token: 0x0600022A RID: 554 RVA: 0x000092D7 File Offset: 0x000074D7
		public string AntiUdonBind1 { get; set; } = "U";

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000092E0 File Offset: 0x000074E0
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000092E8 File Offset: 0x000074E8
		public string AntiUdonBind2 { get; set; } = "LeftControl";

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000092F1 File Offset: 0x000074F1
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000092F9 File Offset: 0x000074F9
		public string SerializationBind1 { get; set; } = "Mouse3";

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009302 File Offset: 0x00007502
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000930A File Offset: 0x0000750A
		public string SerializationBind2 { get; set; } = "LeftControl";

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009313 File Offset: 0x00007513
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000931B File Offset: 0x0000751B
		public string RespawnBind1 { get; set; } = "R";

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009324 File Offset: 0x00007524
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000932C File Offset: 0x0000752C
		public string RespawnBind2 { get; set; } = "LeftControl";

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00009335 File Offset: 0x00007535
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000933D File Offset: 0x0000753D
		public string Debug1 { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009346 File Offset: 0x00007546
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000934E File Offset: 0x0000754E
		public string Debug2 { get; set; }

		// Token: 0x06000239 RID: 569 RVA: 0x00009358 File Offset: 0x00007558
		internal static void WriteConfig()
		{
			Type type = Config.binds.GetType();
			PropertyInfo[] properties = type.GetProperties(20);
			Config.Log("");
			Config.Log("Keybinds:");
			foreach (PropertyInfo property in properties)
			{
				string name = property.Name;
				string text = ": ";
				object value = property.GetValue(Config.binds);
				Config.Log(name + text + ((value != null) ? value.ToString() : null));
			}
		}

		// Token: 0x040000EA RID: 234
		public readonly string KeyCodes = "https://docs.unity3d.com/ScriptReference/KeyCode.html";
	}
}
