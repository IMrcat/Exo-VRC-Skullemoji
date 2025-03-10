using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EXO.Menus.BigMenu;
using EXO.Modules.API;
using Newtonsoft.Json;
using VRC.Core;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Functions.Avatar
{
	// Token: 0x020000AD RID: 173
	internal class AviFavs
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x000246A0 File Offset: 0x000228A0
		internal static void Init()
		{
			AviFavs.FavPath = AppStart.HexedDirectory.FullName + "\\EXO\\FavoritesPlus.json";
			AviFavs.SeenPath = AppStart.HexedDirectory.FullName + "\\EXO\\SeenAvatars.json";
			bool flag = !File.Exists(AviFavs.FavPath);
			if (flag)
			{
				using (StreamWriter sw = File.CreateText(AviFavs.FavPath))
				{
					sw.WriteLine("[]");
				}
			}
			bool flag2 = !File.Exists(AviFavs.SeenPath);
			if (flag2)
			{
				using (StreamWriter sw2 = File.CreateText(AviFavs.SeenPath))
				{
					sw2.WriteLine("[]");
				}
			}
			AviFavs.FavAvis = JsonConvert.DeserializeObject<List<AvatarObject>>(File.ReadAllText(AviFavs.FavPath));
			if (AviFavs.FavAvis == null)
			{
				AviFavs.FavAvis = new List<AvatarObject>();
			}
			AviFavs.SeenAvi = JsonConvert.DeserializeObject<List<AvatarObject>>(File.ReadAllText(AviFavs.SeenPath));
			if (AviFavs.SeenAvi == null)
			{
				AviFavs.SeenAvi = new List<AvatarObject>();
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000247B4 File Offset: 0x000229B4
		internal static void FavAvi(ApiAvatar avi)
		{
			File.WriteAllText(AviFavs.FavPath, JsonConvert.SerializeObject(AviFavs.FavAvis, Formatting.Indented));
			AvatarFavMenu.AddAvatar(avi);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000247D4 File Offset: 0x000229D4
		internal static void UnFavAvi(ApiAvatar avi)
		{
			bool flag = AviFavs.FavAvis.Exists((AvatarObject avi) => avi.id == avi.id);
			if (flag)
			{
				AviFavs.FavAvis.Remove(Enumerable.FirstOrDefault<AvatarObject>(Enumerable.Where<AvatarObject>(AviFavs.FavAvis, (AvatarObject avi) => avi.id == avi.id)));
			}
			File.WriteAllText(AviFavs.FavPath, JsonConvert.SerializeObject(AviFavs.FavAvis, Formatting.Indented));
			AvatarFavMenu.RemoveAvatar(avi);
		}

		// Token: 0x04000300 RID: 768
		internal static VRCPage MainPg;

		// Token: 0x04000301 RID: 769
		internal static VRCPage AviFav;

		// Token: 0x04000302 RID: 770
		internal static VRCPage SeenAvis;

		// Token: 0x04000303 RID: 771
		internal static ButtonGroup FavAviGRP;

		// Token: 0x04000304 RID: 772
		internal static ButtonGroup SeenAviGRP;

		// Token: 0x04000305 RID: 773
		internal static List<AvatarObject> FavAvis;

		// Token: 0x04000306 RID: 774
		internal static List<AvatarObject> SeenAvi = new List<AvatarObject>();

		// Token: 0x04000307 RID: 775
		internal static Dictionary<string, VRCButton> FavBtns = new Dictionary<string, VRCButton>();

		// Token: 0x04000308 RID: 776
		internal static List<VRCButton> SeenBtns = new List<VRCButton>();

		// Token: 0x04000309 RID: 777
		private static string FavPath;

		// Token: 0x0400030A RID: 778
		private static string SeenPath;
	}
}
