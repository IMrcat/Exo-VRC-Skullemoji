using System;
using EXO.Functions;
using EXO.Modules.Utilities;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using VRC.Core;

namespace EXO.Modules.API
{
	// Token: 0x02000080 RID: 128
	public class AvatarObject
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0001AD45 File Offset: 0x00018F45
		public AvatarObject()
		{
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001AD4F File Offset: 0x00018F4F
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0001AD57 File Offset: 0x00018F57
		public string name { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001AD60 File Offset: 0x00018F60
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0001AD68 File Offset: 0x00018F68
		public string id { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001AD71 File Offset: 0x00018F71
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001AD79 File Offset: 0x00018F79
		public string authorId { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001AD82 File Offset: 0x00018F82
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0001AD8A File Offset: 0x00018F8A
		public string authorName { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001AD93 File Offset: 0x00018F93
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0001AD9B File Offset: 0x00018F9B
		public string assetUrl { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001ADAC File Offset: 0x00018FAC
		public string thumbnailImageUrl { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001ADB5 File Offset: 0x00018FB5
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001ADBD File Offset: 0x00018FBD
		public ApiModel.SupportedPlatforms supportedPlatforms { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001ADC6 File Offset: 0x00018FC6
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0001ADCE File Offset: 0x00018FCE
		public string releaseStatus { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001ADD7 File Offset: 0x00018FD7
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0001ADDF File Offset: 0x00018FDF
		public string description { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public string unityVersion { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001ADF9 File Offset: 0x00018FF9
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0001AE01 File Offset: 0x00019001
		public int version { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001AE0A File Offset: 0x0001900A
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0001AE12 File Offset: 0x00019012
		public int apiVersion { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001AE1B File Offset: 0x0001901B
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001AE23 File Offset: 0x00019023
		public bool NsfwDetected { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001AE2C File Offset: 0x0001902C
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0001AE34 File Offset: 0x00019034
		public bool FurryDetected { get; set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001AE40 File Offset: 0x00019040
		internal static Object GrabAvatarTable(ApiAvatar avatar)
		{
			Dictionary<string, Object> Table = new Dictionary<string, Object>();
			Table.System_Collections_IDictionary_Add("id", avatar.id);
			Table.System_Collections_IDictionary_Add("assetUrl", "");
			Table.System_Collections_IDictionary_Add("authorId", avatar.authorId);
			Table.System_Collections_IDictionary_Add("authorName", avatar.authorName);
			Table.System_Collections_IDictionary_Add("updated_at", avatar.updated_at.ToString());
			Table.System_Collections_IDictionary_Add("description", avatar.description);
			bool featured = avatar.featured;
			if (featured)
			{
				Table.System_Collections_IDictionary_Add("featured", avatar.featured.ToString());
			}
			Table.System_Collections_IDictionary_Add("imageUrl", avatar.imageUrl);
			Table.System_Collections_IDictionary_Add("thumbnailImageUrl", avatar.thumbnailImageUrl);
			Table.System_Collections_IDictionary_Add("name", avatar.name);
			Table.System_Collections_IDictionary_Add("releaseStatus", avatar.releaseStatus);
			Table.System_Collections_IDictionary_Add("version", Serialization.MakeObject((double)avatar.version));
			bool flag = avatar.tags == null;
			if (flag)
			{
				Table.System_Collections_IDictionary_Add("tags", new List<string>());
			}
			else
			{
				Table.System_Collections_IDictionary_Add("tags", avatar.tags);
			}
			List<Object> unityPackages = new List<Object>();
			Dictionary<string, Object> UnityPackageDicPC = new Dictionary<string, Object>();
			bool flag2 = UnityPackageDicPC != null;
			if (flag2)
			{
				UnityPackageDicPC.System_Collections_IDictionary_Add("assetUrl", avatar.assetUrl);
				UnityPackageDicPC.System_Collections_IDictionary_Add("unityVersion", avatar.unityVersion);
				UnityPackageDicPC.System_Collections_IDictionary_Add("unitySortNumber", Serialization.MakeObject(UtilFunc.SortNumber(avatar.unityVersion)));
				UnityPackageDicPC.System_Collections_IDictionary_Add("assetVersion", Serialization.MakeObject((double)avatar.assetVersion.ApiVersion));
				UnityPackageDicPC.System_Collections_IDictionary_Add("platform", avatar.platform);
				unityPackages.System_Collections_IList_Add(UnityPackageDicPC);
			}
			Dictionary<string, Object> UnityPackageDicQuest = new Dictionary<string, Object>();
			bool flag3 = UnityPackageDicQuest != null;
			if (flag3)
			{
				UnityPackageDicQuest.System_Collections_IDictionary_Add("platform", "android");
				UnityPackageDicQuest.System_Collections_IDictionary_Add("unityVersion", "2019.4.31f1");
				unityPackages.System_Collections_IList_Add(UnityPackageDicQuest);
			}
			Table.System_Collections_IDictionary_Add("unityPackages", unityPackages);
			return Table;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001B129 File Offset: 0x00019329
		public ApiAvatar ToApiAvatar()
		{
			return AvatarObject.ApiAvatar(this);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001B134 File Offset: 0x00019334
		public static ApiAvatar ApiAvatar(AvatarObject avatar)
		{
			return new ApiAvatar
			{
				name = avatar.name,
				id = avatar.id,
				authorId = avatar.authorId,
				authorName = avatar.authorName,
				assetUrl = avatar.assetUrl,
				thumbnailImageUrl = avatar.thumbnailImageUrl,
				supportedPlatforms = avatar.supportedPlatforms,
				description = avatar.description,
				releaseStatus = avatar.releaseStatus,
				version = avatar.version,
				unityVersion = avatar.unityVersion,
				assetVersion = new AssetVersion(avatar.unityVersion, avatar.version),
				apiVersion = avatar.apiVersion
			};
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001B1FA File Offset: 0x000193FA
		public AvatarObject(IntPtr pointer)
			: this(new ApiAvatar(pointer), false, false)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001B20C File Offset: 0x0001940C
		public AvatarObject(ApiAvatar avtr, bool nsfwDetected = false, bool furryDetected = false)
		{
			this.name = avtr.name;
			this.id = avtr.id;
			this.authorId = avtr.authorId;
			this.authorName = avtr.authorName;
			this.assetUrl = avtr.assetUrl;
			this.thumbnailImageUrl = avtr.thumbnailImageUrl;
			this.supportedPlatforms = avtr.supportedPlatforms;
			this.description = avtr.description;
			this.releaseStatus = avtr.releaseStatus;
			this.version = avtr.version;
			this.unityVersion = avtr.unityVersion;
			this.apiVersion = avtr.apiVersion;
			this.NsfwDetected = nsfwDetected;
			this.FurryDetected = furryDetected;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public AvatarObject(Dictionary<string, Object> avtr)
		{
			this.name = avtr["name"].ToString();
			this.id = avtr["id"].ToString();
			this.authorId = avtr["authorId"].ToString();
			this.authorName = avtr["authorName"].ToString();
			this.assetUrl = avtr["assetUrl"].ToString();
			this.thumbnailImageUrl = avtr["thumbnailImageUrl"].ToString();
			this.supportedPlatforms = ApiModel.SupportedPlatforms.All;
			this.description = avtr["description"].ToString();
			this.releaseStatus = avtr["releaseStatus"].ToString();
			Object @object = avtr["unityVersion"];
			this.unityVersion = ((@object != null) ? @object.ToString() : null);
			Object object2 = avtr["apiVersion"];
			int gay;
			int.TryParse((object2 != null) ? object2.ToString() : null, ref gay);
			this.apiVersion = gay;
		}

		// Token: 0x04000228 RID: 552
		internal static readonly AvatarObject NullrobotAvatar = new AvatarObject
		{
			id = "avtr_c38a1615-5bf5-42b4-84eb-a8b6c37cbd11",
			name = "Robot",
			releaseStatus = "public",
			assetUrl = "https://api.vrchat.cloud/api/1/file/file_3c521ce5-e662-4a5d-a2f1-d9088cfde086/1/file",
			version = 1,
			authorName = "vrchat",
			authorId = "8JoV9XEdpo",
			description = "Beep Boop",
			thumbnailImageUrl = "https://api.vrchat.cloud/api/1/file/file_0e8c4e32-7444-44ea-ade4-313c010d4bae/1/file"
		};
	}
}
