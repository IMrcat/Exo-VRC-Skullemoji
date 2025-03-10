using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using TMPro;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SubMenus.Visuals
{
	// Token: 0x0200005F RID: 95
	internal class CamMenu : MenuModule
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public override void LoadMenu()
		{
			CamMenu.subMenu = new VRCPage("Camera", false, true, false, null, "", null, false);
			this.mainCam = Camera.main;
			new VRCSlider(CamMenu.subMenu, "View Distance : 1000m", "Adjust how far you can see", delegate(float val, VRCSlider s)
			{
				this.mainCam = this.mainCam ?? Camera.main;
				this.mainCam.farClipPlane = val;
				CamMenu.viewDistance = val;
				TMP_Text textMeshPro = s.TextMeshPro;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(17, 1);
				defaultInterpolatedStringHandler.AppendLiteral("View Distance : ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(val, "F0");
				defaultInterpolatedStringHandler.AppendLiteral("m");
				textMeshPro.text = defaultInterpolatedStringHandler.ToStringAndClear();
			}, 1000f, 1f, 1000f).Button(delegate(VRCSlider s)
			{
				this.mainCam = this.mainCam ?? Camera.main;
				this.mainCam.farClipPlane = 1000f;
				CamMenu.viewDistance = 1000f;
				s.snapSlider.value = 1000f;
			}, "Reset", null);
			new VRCSlider(CamMenu.subMenu, "Third Person Fov : 80", "", delegate(float val, VRCSlider s)
			{
				ThirdPerson.fov = (float)((int)val);
				TMP_Text textMeshPro2 = s.TextMeshPro;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				defaultInterpolatedStringHandler2..ctor(19, 1);
				defaultInterpolatedStringHandler2.AppendLiteral("Third Person Fov : ");
				defaultInterpolatedStringHandler2.AppendFormatted<float>(val, "F0");
				textMeshPro2.text = defaultInterpolatedStringHandler2.ToStringAndClear();
			}, 80f, 30f, 160f).Button(delegate(VRCSlider s)
			{
				ThirdPerson.fov = 80f;
				s.snapSlider.value = 80f;
			}, "Reset", null);
			new VRCSlider(CamMenu.subMenu, "Third Person Offset : 0", "", delegate(float val, VRCSlider s)
			{
				CamMenu.offsetValue = val;
				ThirdPerson.ResetCameras(CamMenu.offsetValue);
				TMP_Text textMeshPro3 = s.TextMeshPro;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3;
				defaultInterpolatedStringHandler3..ctor(22, 1);
				defaultInterpolatedStringHandler3.AppendLiteral("Third Person Offset : ");
				defaultInterpolatedStringHandler3.AppendFormatted<float>(CamMenu.offsetValue, "F2");
				textMeshPro3.text = defaultInterpolatedStringHandler3.ToStringAndClear();
			}, 0f, -1f, 1f).Button(delegate(VRCSlider s)
			{
				CamMenu.offsetValue = 0f;
				ThirdPerson.ResetCameras(0f);
				s.snapSlider.value = 0f;
			}, "Reset", null);
			ButtonGroup mainGrp = new ButtonGroup(CamMenu.subMenu, "Camera", false, TextAnchor.UpperCenter);
			new VRCButton(mainGrp, "Max View Distance", "Sets your view distance to the highest value", delegate
			{
				this.mainCam = this.mainCam ?? Camera.main;
				this.mainCam.farClipPlane = float.MaxValue;
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			ButtonGroupControl buttonGroupControl = mainGrp;
			string text = "Head Light";
			Action<bool> action;
			if ((action = CamMenu.<>O.<0>__HeadFlashlights) == null)
			{
				action = (CamMenu.<>O.<0>__HeadFlashlights = new Action<bool>(HeadLight.HeadFlashlights));
			}
			new VRCToggle(buttonGroupControl, text, action, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Third Person", "", delegate
			{
				bool flag = !ThirdPerson.isUpdating;
				if (flag)
				{
					ThirdPerson.On3rdPersonStart();
				}
				ThirdPerson.Switch();
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x0400019B RID: 411
		public static VRCPage subMenu;

		// Token: 0x0400019C RID: 412
		internal static float offsetValue = 0f;

		// Token: 0x0400019D RID: 413
		internal static float viewDistance = 1000f;

		// Token: 0x0400019E RID: 414
		private Camera mainCam;

		// Token: 0x02000121 RID: 289
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004EE RID: 1262
			public static Action<bool> <0>__HeadFlashlights;
		}
	}
}
