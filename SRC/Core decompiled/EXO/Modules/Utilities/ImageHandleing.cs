using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CoreRuntime.Manager;
using EXO.LogTools;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EXO.Modules.Utilities
{
	// Token: 0x02000077 RID: 119
	internal class ImageHandleing
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x000173F4 File Offset: 0x000155F4
		internal static void LoadImage(ImageHandleing.ImageTypes Type, GameObject Instance, string Url)
		{
			switch (Type)
			{
			case ImageHandleing.ImageTypes.Image:
				CoroutineManager.RunCoroutine(ImageHandleing.LoadSprite(Instance.GetComponent<Image>(), Url));
				break;
			case ImageHandleing.ImageTypes.ImageThreeSlice:
				CoroutineManager.RunCoroutine(ImageHandleing.LoadThreeSlice(Instance.GetComponent<ImageThreeSlice>(), Url));
				break;
			case ImageHandleing.ImageTypes.Sprite:
				CoroutineManager.RunCoroutine(ImageHandleing.LoadSprite(Instance.GetComponent<Sprite>(), Url));
				break;
			case ImageHandleing.ImageTypes.Texture2D:
				ImageHandleing.LoadTexture2DBase(Convert.FromBase64String(Url), Instance);
				break;
			case ImageHandleing.ImageTypes.RawImage:
				CoroutineManager.RunCoroutine(ImageHandleing.LoadRawSprite(Instance.GetComponent<RawImage>(), Url));
				break;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017481 File Offset: 0x00015681
		internal static Texture2D CreateTexture(string base64)
		{
			return ImageHandleing.LoadTexture2DBase(Convert.FromBase64String(base64), null);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001748F File Offset: 0x0001568F
		internal static Texture2D LoadTexture2DBase(byte[] data, GameObject S = null)
		{
			return ImageHandleing.FromBytes(data).texture;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001749C File Offset: 0x0001569C
		public static Sprite FromFile(string path, bool file = true)
		{
			return ImageHandleing.FromBytes(File.ReadAllBytes(path));
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000174A9 File Offset: 0x000156A9
		public static Sprite FromBase(string data)
		{
			return ImageHandleing.FromBytes(Convert.FromBase64String(data.Replace("data:image/png;base64,", "")));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000174C8 File Offset: 0x000156C8
		public static Sprite FromBytes(byte[] data)
		{
			Sprite png;
			bool flag = ImageHandleing._loadedImages.TryGetValue(data, ref png);
			Sprite sprite2;
			if (flag)
			{
				sprite2 = png;
			}
			else
			{
				Texture2D t = new Texture2D(2, 2);
				t.LoadImage(data);
				Rect rect = new Rect(0f, 0f, (float)t.width, (float)t.height);
				Vector2 pivot = new Vector2(0.5f, 0.5f);
				Vector4 border = Vector4.zero;
				Sprite sprite = Sprite.CreateSprite_Injected(t, ref rect, ref pivot, 100f, 0U, SpriteMeshType.Tight, ref border, false, null);
				sprite.texture.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				ImageHandleing._loadedImages.Add(data, sprite);
				sprite2 = sprite;
			}
			return sprite2;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00017580 File Offset: 0x00015780
		internal static IEnumerator LoadSprite(Image Instance, string url)
		{
			Instance.gameObject.active = false;
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
			UnityWebRequestAsyncOperation asyncOperation = www.SendWebRequest();
			Func<bool> func = () => asyncOperation.isDone;
			yield return new WaitUntil(func);
			bool flag = www.isHttpError || www.isNetworkError;
			if (flag)
			{
				CLog.E("Error2 : " + www.error + " Obj : " + Instance.gameObject.name, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\ImageHandleing.cs", 76);
				yield break;
			}
			Texture2D content = DownloadHandlerTexture.GetContent(www);
			Sprite sprite2 = (Instance.sprite = Sprite.CreateSprite(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0f, 0f), 1f, 1U, SpriteMeshType.FullRect, Vector4.zero, false, null));
			Instance.color = Color.white;
			bool flag2 = sprite2 != null;
			if (flag2)
			{
				Instance.sprite = sprite2;
			}
			Instance.gameObject.active = true;
			yield break;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00017596 File Offset: 0x00015796
		private static IEnumerator LoadRawSprite(RawImage Instance, string url)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
			yield return www.SendWebRequest();
			bool flag = www.isHttpError || www.isNetworkError;
			if (flag)
			{
				CLog.E("Error6 : " + www.error, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\ImageHandleing.cs", 96);
				yield break;
			}
			Instance.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
			yield break;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000175AC File Offset: 0x000157AC
		private static IEnumerator LoadThreeSlice(ImageThreeSlice Instance, string url)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
			UnityWebRequestAsyncOperation asyncOperation = www.SendWebRequest();
			Func<bool> func = () => asyncOperation.isDone;
			yield return new WaitUntil(func);
			bool flag = www.isHttpError || www.isNetworkError;
			if (flag)
			{
				CLog.E("Error6 : " + www.error, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\ImageHandleing.cs", 112);
				yield break;
			}
			Texture2D content = DownloadHandlerTexture.GetContent(www);
			Sprite sprite2 = (Instance._sprite = Sprite.CreateSprite(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, new Vector4(255f, 0f, 255f, 0f), false, null));
			bool flag2 = sprite2 != null;
			if (flag2)
			{
				Instance._sprite = sprite2;
			}
			yield break;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000175C2 File Offset: 0x000157C2
		private static IEnumerator LoadSprite(Sprite Instance, string url)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
			UnityWebRequestAsyncOperation asyncOperation = www.SendWebRequest();
			Func<bool> func = () => asyncOperation.isDone;
			yield return new WaitUntil(func);
			bool flag = www.isHttpError || www.isNetworkError;
			if (flag)
			{
				CLog.E("Error6 : " + www.error, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\ImageHandleing.cs", 133);
				yield break;
			}
			Texture2D content = DownloadHandlerTexture.GetContent(www);
			Sprite sprite3 = Sprite.CreateSprite(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, new Vector4(255f, 0f, 255f, 0f), false, null);
			Sprite sprite2 = sprite3;
			bool flag2 = sprite2 != null;
			if (flag2)
			{
			}
			yield break;
		}

		// Token: 0x040001FE RID: 510
		private static Dictionary<byte[], Sprite> _loadedImages = new Dictionary<byte[], Sprite>();

		// Token: 0x02000155 RID: 341
		internal enum ImageTypes
		{
			// Token: 0x040005FC RID: 1532
			Image,
			// Token: 0x040005FD RID: 1533
			ImageThreeSlice,
			// Token: 0x040005FE RID: 1534
			Sprite,
			// Token: 0x040005FF RID: 1535
			Texture2D,
			// Token: 0x04000600 RID: 1536
			RawImage
		}
	}
}
