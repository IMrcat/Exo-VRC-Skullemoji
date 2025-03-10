using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace EXO
{
	// Token: 0x02000033 RID: 51
	internal class ConfigInit<T> where T : class
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009693 File Offset: 0x00007893
		private string FilePath { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000969B File Offset: 0x0000789B
		private string FileName { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000096A3 File Offset: 0x000078A3
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000096AB File Offset: 0x000078AB
		public T Cfg { get; private set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000247 RID: 583 RVA: 0x000096B4 File Offset: 0x000078B4
		// (remove) Token: 0x06000248 RID: 584 RVA: 0x000096EC File Offset: 0x000078EC
		[field: DebuggerBrowsable(0)]
		public event Action PreOnConfigUpdate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000249 RID: 585 RVA: 0x00009724 File Offset: 0x00007924
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x0000975C File Offset: 0x0000795C
		[field: DebuggerBrowsable(0)]
		public event Action OnConfigUpdate;

		// Token: 0x0600024B RID: 587 RVA: 0x00009791 File Offset: 0x00007991
		public ConfigInit(string Path, string fileName = "")
		{
			this.FilePath = Path;
			this.FileName = fileName;
			this.CheckConfig();
			this.Cfg = JsonConvert.DeserializeObject<T>(File.ReadAllText(this.FilePath));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000097C8 File Offset: 0x000079C8
		private void CheckConfig()
		{
			bool flag = !File.Exists(this.FilePath);
			if (flag)
			{
				Config.Log("Building " + this.FileName + "...");
				Config.Log(this.FilePath);
				File.WriteAllText(this.FilePath, JsonConvert.SerializeObject(Activator.CreateInstance(typeof(T)), Formatting.Indented, new JsonSerializerSettings()));
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009838 File Offset: 0x00007A38
		private void UpdateConfig(object obj, FileSystemEventArgs args)
		{
			try
			{
				T val = JsonConvert.DeserializeObject<T>(File.ReadAllText(this.FilePath));
				bool flag = val == null;
				if (!flag)
				{
					Type type = val.GetType();
					PropertyInfo[] array = ((type != null) ? type.GetProperties() : null);
					foreach (PropertyInfo propertyInfo in array)
					{
						PropertyInfo property = this.Cfg.GetType().GetProperty((propertyInfo != null) ? propertyInfo.Name : null);
						bool flag2 = property != null && propertyInfo.GetValue(val) != property.GetValue(this.Cfg);
						if (flag2)
						{
							Action preOnConfigUpdate = this.PreOnConfigUpdate;
							if (preOnConfigUpdate != null)
							{
								preOnConfigUpdate.Invoke();
							}
							this.Cfg = val;
							Action onConfigUpdate = this.OnConfigUpdate;
							if (onConfigUpdate != null)
							{
								onConfigUpdate.Invoke();
							}
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009948 File Offset: 0x00007B48
		public void Save()
		{
			File.WriteAllText(this.FilePath, JsonConvert.SerializeObject(this.Cfg, Formatting.Indented, new JsonSerializerSettings()));
		}
	}
}
