using System;
using System.Collections.Generic;
using VRC.Core;

namespace EXO.Modules.API
{
	// Token: 0x02000082 RID: 130
	internal class User
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001B496 File Offset: 0x00019696
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0001B49E File Offset: 0x0001969E
		public string id { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001B4A7 File Offset: 0x000196A7
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001B4AF File Offset: 0x000196AF
		public string username { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001B4B8 File Offset: 0x000196B8
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public string displayName { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001B4C9 File Offset: 0x000196C9
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public string bio { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001B4DA File Offset: 0x000196DA
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001B4E2 File Offset: 0x000196E2
		public string currentAvatarImageUrl { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001B4EB File Offset: 0x000196EB
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0001B4F3 File Offset: 0x000196F3
		public string currentAvatarThumbnailImageUrl { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001B4FC File Offset: 0x000196FC
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0001B504 File Offset: 0x00019704
		public string userIcon { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001B50D File Offset: 0x0001970D
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001B515 File Offset: 0x00019715
		public string last_platform { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001B51E File Offset: 0x0001971E
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001B526 File Offset: 0x00019726
		public string status { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001B52F File Offset: 0x0001972F
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001B537 File Offset: 0x00019737
		public string state { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001B540 File Offset: 0x00019740
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0001B548 File Offset: 0x00019748
		public List<string> tags { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001B551 File Offset: 0x00019751
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0001B559 File Offset: 0x00019759
		public string developerType { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001B562 File Offset: 0x00019762
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001B56A File Offset: 0x0001976A
		public bool isFriend { get; set; }

		// Token: 0x060004FB RID: 1275 RVA: 0x0001B574 File Offset: 0x00019774
		internal static UserRank GetRank(APIUser playerAPI)
		{
			bool MOD = playerAPI.hasModerationPowers || playerAPI.tags.Contains("admin_moderator");
			bool ADMIN = playerAPI.hasSuperPowers || playerAPI.tags.Contains("admin_");
			bool flag = MOD || ADMIN;
			UserRank userRank;
			if (flag)
			{
				userRank = UserRank.MOD;
			}
			else
			{
				bool hasVeteranTrustLevel = playerAPI.hasVeteranTrustLevel;
				if (hasVeteranTrustLevel)
				{
					userRank = UserRank.Trusted;
				}
				else
				{
					bool hasTrustedTrustLevel = playerAPI.hasTrustedTrustLevel;
					if (hasTrustedTrustLevel)
					{
						userRank = UserRank.Known;
					}
					else
					{
						bool hasKnownTrustLevel = playerAPI.hasKnownTrustLevel;
						if (hasKnownTrustLevel)
						{
							userRank = UserRank.User;
						}
						else
						{
							bool hasBasicTrustLevel = playerAPI.hasBasicTrustLevel;
							if (hasBasicTrustLevel)
							{
								userRank = UserRank.New;
							}
							else
							{
								userRank = UserRank.Visitor;
							}
						}
					}
				}
			}
			return userRank;
		}
	}
}
