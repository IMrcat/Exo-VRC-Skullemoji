using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldAPI
{
	// Token: 0x02000007 RID: 7
	public class Base64
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002100 File Offset: 0x00000300
		public static Sprite FromBase(string data)
		{
			Sprite sprite;
			Base64.AlreadyLoaded.TryGetValue(data, ref sprite);
			bool flag = sprite != null;
			Sprite sprite2;
			if (flag)
			{
				sprite2 = Base64.AlreadyLoaded[data];
			}
			else
			{
				Texture2D t = new Texture2D(2, 2);
				t.LoadImage(Convert.FromBase64String(data));
				Rect rect = new Rect(0f, 0f, (float)t.width, (float)t.height);
				Vector2 pivot = new Vector2(0.5f, 0.5f);
				Vector4 border = Vector4.zero;
				Sprite s = Sprite.CreateSprite_Injected(t, ref rect, ref pivot, 100f, 0U, SpriteMeshType.Tight, ref border, false, null);
				Base64.AlreadyLoaded.Add(data, s);
				sprite2 = s;
			}
			return sprite2;
		}

		// Token: 0x04000004 RID: 4
		public static Dictionary<string, Sprite> AlreadyLoaded = new Dictionary<string, Sprite>();
	}
}
