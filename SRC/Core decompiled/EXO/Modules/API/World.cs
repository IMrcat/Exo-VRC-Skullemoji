using System;
using System.Collections.Generic;
using VRC.Core;

namespace EXO.Modules.API
{
	// Token: 0x02000085 RID: 133
	internal class World
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001B6D2 File Offset: 0x000198D2
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0001B6DA File Offset: 0x000198DA
		public string id { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001B6E3 File Offset: 0x000198E3
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001B6EB File Offset: 0x000198EB
		public bool Populated { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001B6F4 File Offset: 0x000198F4
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0001B6FC File Offset: 0x000198FC
		public string Endpoint { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001B705 File Offset: 0x00019905
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0001B70C File Offset: 0x0001990C
		public static AssetVersion VERSION { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001B714 File Offset: 0x00019914
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0001B71C File Offset: 0x0001991C
		public string name { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001B725 File Offset: 0x00019925
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001B72D File Offset: 0x0001992D
		public string imageUrl { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001B736 File Offset: 0x00019936
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001B73E File Offset: 0x0001993E
		public string thumbnailImageUrl { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001B747 File Offset: 0x00019947
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001B74F File Offset: 0x0001994F
		public string authorName { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001B758 File Offset: 0x00019958
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001B760 File Offset: 0x00019960
		public string releaseStatus { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001B769 File Offset: 0x00019969
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001B771 File Offset: 0x00019971
		public int capacity { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001B77A File Offset: 0x0001997A
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001B782 File Offset: 0x00019982
		public int recommendedCapacity { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001B78B File Offset: 0x0001998B
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001B793 File Offset: 0x00019993
		public int occupants { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001B79C File Offset: 0x0001999C
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001B7A4 File Offset: 0x000199A4
		public int publicOccupants { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001B7AD File Offset: 0x000199AD
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001B7B5 File Offset: 0x000199B5
		public int privateOccupants { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001B7BE File Offset: 0x000199BE
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001B7C6 File Offset: 0x000199C6
		public string authorId { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001B7CF File Offset: 0x000199CF
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001B7D7 File Offset: 0x000199D7
		public DateTime createdAt { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001B7E0 File Offset: 0x000199E0
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001B7E8 File Offset: 0x000199E8
		public string assetUrl { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001B7F1 File Offset: 0x000199F1
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001B7F9 File Offset: 0x000199F9
		public string description { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001B802 File Offset: 0x00019A02
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001B80A File Offset: 0x00019A0A
		public List<string> tags { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001B813 File Offset: 0x00019A13
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001B81B File Offset: 0x00019A1B
		public string unityPackageUrl { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001B824 File Offset: 0x00019A24
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001B82C File Offset: 0x00019A2C
		public int version { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001B835 File Offset: 0x00019A35
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001B83D File Offset: 0x00019A3D
		public string unityVersion { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001B846 File Offset: 0x00019A46
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001B84E File Offset: 0x00019A4E
		public int apiVersion { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001B857 File Offset: 0x00019A57
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0001B85F File Offset: 0x00019A5F
		public int latestAssetVersion { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001B868 File Offset: 0x00019A68
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001B870 File Offset: 0x00019A70
		public DateTime created_at { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001B879 File Offset: 0x00019A79
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001B881 File Offset: 0x00019A81
		public DateTime updated_at { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001B88A File Offset: 0x00019A8A
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001B892 File Offset: 0x00019A92
		public DateTime publicationDate { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001B89B File Offset: 0x00019A9B
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001B8A3 File Offset: 0x00019AA3
		public DateTime labsPublicationDate { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001B8AC File Offset: 0x00019AAC
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		public string platform { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001B8C5 File Offset: 0x00019AC5
		public List<ApiWorldInstance> worldInstances { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001B8CE File Offset: 0x00019ACE
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001B8D6 File Offset: 0x00019AD6
		public Dictionary<string, int> instances { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001B8DF File Offset: 0x00019ADF
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		public bool isAdminApproved { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public bool IsCommunityLabsWorld { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001B901 File Offset: 0x00019B01
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001B909 File Offset: 0x00019B09
		public bool IsPublicPublishedWorld { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001B912 File Offset: 0x00019B12
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001B91A File Offset: 0x00019B1A
		public bool IsInternalWorld { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001B923 File Offset: 0x00019B23
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001B92B File Offset: 0x00019B2B
		public bool unityPackageUpdated { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001B934 File Offset: 0x00019B34
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0001B93C File Offset: 0x00019B3C
		public string organization { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001B945 File Offset: 0x00019B45
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0001B94D File Offset: 0x00019B4D
		public bool shouldAddToAuthor { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001B956 File Offset: 0x00019B56
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0001B95E File Offset: 0x00019B5E
		public string favoriteId { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001B967 File Offset: 0x00019B67
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001B96F File Offset: 0x00019B6F
		public int favorites { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001B978 File Offset: 0x00019B78
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001B980 File Offset: 0x00019B80
		public int visits { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001B989 File Offset: 0x00019B89
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001B991 File Offset: 0x00019B91
		public int popularity { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001B99A File Offset: 0x00019B9A
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		public int heat { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001B9AB File Offset: 0x00019BAB
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0001B9B3 File Offset: 0x00019BB3
		public bool detailed { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001B9BC File Offset: 0x00019BBC
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public AssetVersion assetVersion { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001B9CD File Offset: 0x00019BCD
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001B9D5 File Offset: 0x00019BD5
		public bool isCurated { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001B9DE File Offset: 0x00019BDE
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001B9E6 File Offset: 0x00019BE6
		public List<string> publicTags { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001B9EF File Offset: 0x00019BEF
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0001B9F7 File Offset: 0x00019BF7
		public bool IsLocal { get; set; }
	}
}
