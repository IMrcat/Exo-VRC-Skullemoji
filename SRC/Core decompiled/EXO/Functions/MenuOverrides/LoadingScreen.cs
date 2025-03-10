using System;
using System.Collections;
using System.IO;
using System.Net;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO_Base;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009C RID: 156
	internal class LoadingScreen : FunctionModule
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00021C37 File Offset: 0x0001FE37
		private static Transform UserInterface
		{
			get
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				return (field_Private_Static_VRCUiManager_ != null) ? field_Private_Static_VRCUiManager_.transform : null;
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00021C4A File Offset: 0x0001FE4A
		public override void OnInject()
		{
			CoroutineManager.RunCoroutine(LoadingScreen.StartSong());
			CoroutineManager.RunCoroutine(LoadingScreen.StartLoadingScreenUI());
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00021C63 File Offset: 0x0001FE63
		private static IEnumerator StartSong()
		{
			string soundPath = "LoadingBackground_TealGradient_Music/LoadingSound";
			string soundPath2 = "MenuContent/Popups/LoadingPopup/LoadingSound";
			bool flag = !File.Exists(LoadingScreen.path) || Config.cfg.ForceUpdate16;
			if (flag)
			{
				CLog.L("Loading Music Downloading...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\LoadingScreen.cs", 47);
				LoadingScreen.web.DownloadFile("https://github.com/Cyconi/EXO-Resources/raw/main/LoadingMusic.mp3", LoadingScreen.path);
				CLog.L("Loading Music Downloaded", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\LoadingScreen.cs", 49);
			}
			UnityWebRequest www = UnityWebRequest.Get("file://" + LoadingScreen.path);
			www.SendWebRequest();
			while (!www.isDone)
			{
				yield return new WaitForEndOfFrame();
			}
			bool flag2 = !www.isHttpError;
			if (flag2)
			{
				while (LoadingScreen.UserInterface == null)
				{
					yield return new WaitForEndOfFrame();
				}
				LoadingScreen.soundClip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false, false, AudioType.UNKNOWN);
				while (LoadingScreen.UserInterface.Find(soundPath) == null)
				{
					yield return new WaitForEndOfFrame();
				}
				LoadingScreen.audioSource = LoadingScreen.UserInterface.Find(soundPath).GetComponent<AudioSource>();
				LoadingScreen.ReplaceClip(LoadingScreen.audioSource, LoadingScreen.soundClip);
				for (;;)
				{
					Transform transform = LoadingScreen.UserInterface.Find(soundPath2);
					bool flag3;
					if (transform == null)
					{
						flag3 = false;
					}
					else
					{
						GameObject gameObject = transform.gameObject;
						bool? flag4 = ((gameObject != null) ? new bool?(gameObject.active) : default(bool?));
						bool flag5 = false;
						flag3 = (flag4.GetValueOrDefault() == flag5) & (flag4 != null);
					}
					if (!flag3)
					{
						break;
					}
					yield return new WaitForEndOfFrame();
				}
				yield return new WaitForSeconds(0.1f);
				LoadingScreen.audioSource1 = LoadingScreen.UserInterface.Find(soundPath2).GetComponent<AudioSource>();
				LoadingScreen.ReplaceClip(LoadingScreen.audioSource1, LoadingScreen.soundClip);
			}
			yield break;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00021C6B File Offset: 0x0001FE6B
		internal static void ReplaceClip(AudioSource src, AudioClip clip)
		{
			Object.Destroy(src.GetComponent<VRCUiPageLoadingMusicController>());
			src.clip = clip;
			src.volume = Config.cfg.LoadingMusic;
			src.loop = true;
			src.playOnAwake = true;
			src.Play();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00021CAA File Offset: 0x0001FEAA
		private static IEnumerator StartLoadingScreenUI()
		{
			while (LoadingScreen.UserInterface == null)
			{
				yield return new WaitForEndOfFrame();
			}
			while (LoadingScreen.UserInterface.Find("MenuContent/Popups/LoadingPopup") == null)
			{
				yield return new WaitForEndOfFrame();
			}
			Transform menuCont = LoadingScreen.UserInterface.Find("MenuContent/Popups");
			Transform FX_CloseParticles = menuCont.Find("LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_CloseParticles");
			Transform FX_snow = menuCont.Find("LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow");
			FX_snow.GetComponent<ParticleSystem>().startColor = new Color(1f, 0f, 0f, 1f);
			FX_snow.GetComponent<ParticleSystem>().emissionRate = 80f;
			FX_CloseParticles.GetComponent<ParticleSystem>().startColor = new Color(1f, 1f, 1f, 1f);
			FX_CloseParticles.GetComponent<ParticleSystem>().startLifetime = 0.1f;
			FX_CloseParticles.GetComponent<ParticleSystem>().startSize = 1f;
			FX_CloseParticles.GetComponent<ParticleSystem>().emissionRate = 10f;
			FX_CloseParticles.GetComponent<ParticleSystem>().shape.shapeType = ParticleSystemShapeType.Donut;
			FX_CloseParticles.transform.localPosition = new Vector3(0.0455f, 0.1f, 5.5f);
			FX_CloseParticles.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			FX_CloseParticles.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
			menuCont.Find("LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM").SetActive(false);
			menuCont.Find("LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").SetActive(false);
			menuCont.Find("LoadingPopup/3DElements/LoadingInfoPanel").SetActive(false);
			menuCont.Find("LoadingPopup/MirroredElements").SetActive(false);
			Transform decorationLeft = menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left");
			Transform decorationRight = menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right");
			decorationLeft.GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.IconKatana);
			decorationLeft.transform.localPosition = new Vector3(-400f, 0f, 0f);
			decorationLeft.GetComponent<Image>().color = Color.white;
			decorationRight.GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.IconKatana);
			decorationRight.transform.localPosition = new Vector3(400f, 0f, 0f);
			decorationRight.GetComponent<Image>().color = Color.white;
			menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(0.8f, 0f, 0f, 1f);
			menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
			menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = Color.red;
			menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = Color.red;
			menuCont.Find("LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Image>().color = Color.red;
			menuCont.Find("LoadingPopup/ButtonMiddle").GetComponent<Image>().color = Color.red;
			menuCont.Find("StandardPopup/Darkness").GetComponent<Image>().color = Color.black;
			menuCont.Find("StandardPopup/Rectangle").GetComponent<Image>().color = Color.black;
			menuCont.Find("StandardPopup/MidRing").GetComponent<Image>().color = Color.red;
			menuCont.Find("StandardPopup/InnerDashRing").GetComponent<Image>().color = new Color(1f, 0f, 0f);
			menuCont.Find("StandardPopup/ButtonMiddle").GetComponent<Button>().image.color = Color.red;
			menuCont.Find("StandardPopup/RingGlow").GetComponent<Image>().color = new Color(1f, 0f, 0f);
			GameObject gameObject = GameObject.Find("TrackingVolume");
			bool flag = ((gameObject != null) ? gameObject.FindObject("VRLoadingOverlay/FlatLoadingOverlay(Clone)/Container/Canvas/Background") : null) != null;
			if (flag)
			{
				LoadingScreen.ChangeSwitchScreen();
			}
			yield break;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		private static void ChangeSwitchScreen()
		{
			bool flag = !File.Exists(LoadingScreen.imgPath);
			if (flag)
			{
				CLog.L("Loading Image Downloading...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\LoadingScreen.cs", 152);
				Directory.CreateDirectory(Path.GetDirectoryName(LoadingScreen.imgPath));
				File.WriteAllBytes(LoadingScreen.imgPath, LoadingScreen.web.DownloadData("https://github.com/Cyconi/EXO-Resources/blob/main/LoadingImage.png?raw=true"));
				CLog.L("Loading Image Downloaded", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\LoadingScreen.cs", 156);
			}
			LoadingScreen.loadingImg = GameObject.Find("TrackingVolume").FindObject("VRLoadingOverlay/FlatLoadingOverlay(Clone)/Container/Canvas/Background").GetComponent<Image>();
			LoadingScreen.loadingImg.color = Color.white;
			LoadingScreen.loadingImg.sprite = ImageHandleing.FromBytes(File.ReadAllBytes(LoadingScreen.imgPath));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00021D74 File Offset: 0x0001FF74
		public static void DeactivateGameObject(Transform par, string path)
		{
			UtilFunc.WaitFor(par, path, delegate
			{
				CoroutineManager.RunCoroutine(base.<DeactivateGameObject>g__KeepInactive|1());
			});
		}

		// Token: 0x040002C5 RID: 709
		internal static AudioSource audioSource;

		// Token: 0x040002C6 RID: 710
		internal static AudioSource audioSource1;

		// Token: 0x040002C7 RID: 711
		private static readonly WebClient web = new WebClient();

		// Token: 0x040002C8 RID: 712
		private static Image loadingImg;

		// Token: 0x040002C9 RID: 713
		internal static AudioClip soundClip;

		// Token: 0x040002CA RID: 714
		internal static string path = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Music").FullName, "LoadingMusic.mp3");

		// Token: 0x040002CB RID: 715
		internal static string imgPath = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Images").FullName, "LoadingImage.png");
	}
}
