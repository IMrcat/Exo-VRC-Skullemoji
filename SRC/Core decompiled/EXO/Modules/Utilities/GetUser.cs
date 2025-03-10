using System;
using System.Net;
using System.Net.Cache;

namespace EXO.Modules.Utilities
{
	// Token: 0x02000076 RID: 118
	internal class GetUser
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00017237 File Offset: 0x00015437
		internal static void Init()
		{
			GetUser.web = new WebClient();
			GetUser.web.CachePolicy = new RequestCachePolicy(1);
			GetUser.idList = GetUser.web.DownloadString("https://gist.githubusercontent.com/Cyconi/be98f0ddc0c3ede11fd2083e332070df/raw/EXO_VRC.txt");
			GetUser.isReady = true;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00017270 File Offset: 0x00015470
		internal static bool Auth(string ID)
		{
			bool flag = GetUser.idList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string[] lines = GetUser.idList.Split('\n', 0);
				foreach (string line in lines)
				{
					bool flag3 = string.IsNullOrWhiteSpace(line) || line.StartsWith("//");
					if (!flag3)
					{
						string[] parts = line.Split(':', 0);
						for (int i = 0; i < parts.Length; i++)
						{
							parts[i] = parts[i].Trim();
						}
						bool flag4 = parts[0] == ID;
						if (flag4)
						{
							return true;
						}
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001732C File Offset: 0x0001552C
		internal static string GetDetails(string ID)
		{
			string[] lines = GetUser.idList.Split('\n', 0);
			foreach (string line in lines)
			{
				bool flag = string.IsNullOrWhiteSpace(line) || line.StartsWith("//");
				if (!flag)
				{
					string[] parts = line.Split(':', 0);
					for (int i = 0; i < parts.Length; i++)
					{
						parts[i] = parts[i].Trim();
					}
					bool flag2 = parts[0] == ID;
					if (flag2)
					{
						string dateStr = null;
						bool flag3 = parts.Length > 2;
						if (flag3)
						{
							dateStr = parts[2];
						}
						return dateStr;
					}
				}
			}
			return null;
		}

		// Token: 0x040001FB RID: 507
		private static string idList;

		// Token: 0x040001FC RID: 508
		private static WebClient web;

		// Token: 0x040001FD RID: 509
		internal static bool isReady;
	}
}
