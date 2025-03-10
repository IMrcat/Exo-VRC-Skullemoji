using System;
using System.Collections;
using System.IO;
using System.Net;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using UnityEngine;
using UnityEngine.Networking;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009E RID: 158
	internal class MenuMusic : FunctionModule
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00022064 File Offset: 0x00020264
		public override void OnInject()
		{
			bool flag = !File.Exists(MenuMusic.path) || Config.cfg.ForceUpdate16;
			if (flag)
			{
				CLog.L("Noct Music Downloading...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuMusic.cs", 29);
				Directory.CreateDirectory(Path.GetDirectoryName(MenuMusic.path));
				File.WriteAllBytes(MenuMusic.path, MenuMusic.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/qmMusic.mp3"));
				CLog.L("Noct Music Downloaded", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuMusic.cs", 33);
			}
			try
			{
				QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, delegate
				{
					bool flag2 = MenuMusic.audioSource != null && MenuMusic.loadedClip != null;
					if (flag2)
					{
						MenuMusic.audioSource.clip = MenuMusic.loadedClip;
					}
					bool flag3 = QuickMenuPatch.IsOpen && !MenuMusic.isPlaying;
					if (flag3)
					{
						AudioSource audioSource = MenuMusic.audioSource;
						if (audioSource != null)
						{
							audioSource.Play();
						}
						MenuMusic.isPlaying = true;
					}
				});
				QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuClose, delegate
				{
					UtilFunc.Delay(0.1f, delegate
					{
						bool flag4 = !QuickMenuPatch.IsOpen && !MainMenuPatch.IsOpen;
						if (flag4)
						{
							AudioSource audioSource2 = MenuMusic.audioSource;
							if (audioSource2 != null)
							{
								audioSource2.Stop();
							}
							MenuMusic.isPlaying = false;
						}
					});
				});
				MainMenuPatch.OnMainMenuOpen = (Action)Delegate.Combine(MainMenuPatch.OnMainMenuOpen, delegate
				{
					bool flag5 = MenuMusic.audioSource != null && MenuMusic.loadedClip != null;
					if (flag5)
					{
						MenuMusic.audioSource.clip = MenuMusic.loadedClip;
					}
					bool flag6 = MainMenuPatch.IsOpen && !MenuMusic.isPlaying;
					if (flag6)
					{
						AudioSource audioSource3 = MenuMusic.audioSource;
						if (audioSource3 != null)
						{
							audioSource3.Play();
						}
						MenuMusic.isPlaying = true;
					}
				});
				MainMenuPatch.OnMainMenuClose = (Action)Delegate.Combine(MainMenuPatch.OnMainMenuClose, delegate
				{
					UtilFunc.Delay(0.1f, delegate
					{
						bool flag7 = !QuickMenuPatch.IsOpen && !MainMenuPatch.IsOpen;
						if (flag7)
						{
							AudioSource audioSource4 = MenuMusic.audioSource;
							if (audioSource4 != null)
							{
								audioSource4.Stop();
							}
							MenuMusic.isPlaying = false;
						}
					});
				});
			}
			catch (Exception e)
			{
				CLog.E("Failed to add QM Listener", e);
			}
			UtilFunc.WaitForObj("Canvas_QuickMenu(Clone)", delegate
			{
				CoroutineManager.RunCoroutine(MenuMusic.InitSong());
			});
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00022204 File Offset: 0x00020404
		internal static IEnumerator InitSong()
		{
			UnityWebRequest www = UnityWebRequest.Get("file://" + MenuMusic.path);
			www.SendWebRequest();
			while (!www.isDone)
			{
				yield return new WaitForEndOfFrame();
			}
			bool flag = !www.isHttpError;
			if (flag)
			{
				AudioClip clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false, false, AudioType.UNKNOWN);
				bool flag2 = MenuMusic.audioSource == null;
				if (flag2)
				{
					MenuMusic.audioSource = UtilFunc.UserInterface.FindObject("Canvas_QuickMenu(Clone)").gameObject.AddComponent<AudioSource>();
				}
				MenuMusic.audioSource.clip = clip;
				MenuMusic.audioSource.volume = Config.cfg.MenuMusic;
				MenuMusic.audioSource.loop = true;
				clip = null;
			}
			else
			{
				CLog.E("Error loading music: " + www.error, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuMusic.cs", 109);
			}
			www.Dispose();
			yield break;
		}

		// Token: 0x040002D1 RID: 721
		internal static AudioSource audioSource;

		// Token: 0x040002D2 RID: 722
		private static AudioClip loadedClip;

		// Token: 0x040002D3 RID: 723
		private static WebClient web = new WebClient();

		// Token: 0x040002D4 RID: 724
		internal static string path = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Music").FullName, "qmMusic.mp3");

		// Token: 0x040002D5 RID: 725
		internal static bool isPlaying;
	}
}
