using System;
using EXO.Core;
using EXO.Functions.Item;
using EXO.Wrappers;
using VRC;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000066 RID: 102
	internal class OrbitSelect : MenuModule
	{
		// Token: 0x0600037E RID: 894 RVA: 0x000131E8 File Offset: 0x000113E8
		internal static void AddAction()
		{
			VRCPage vrcpage = OrbitSelect.selectPage;
			vrcpage.OnMenuOpen = (Action)Delegate.Combine(vrcpage.OnMenuOpen, delegate
			{
				SelectMenu.UpdateDisplayTarget(OrbitSelect.target, OrbitSelect.selectPage);
			});
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013224 File Offset: 0x00011424
		public override void LoadMenu()
		{
			OrbitSelect.selectPage = new VRCPage("Item Orbit", false, true, false, null, "", null, false);
			CollapsibleButtonGroup orbitSelGrp = new CollapsibleButtonGroup(OrbitSelect.selectPage, "Item Orbits", true);
			OrbitSelect.AddAction();
			OrbitSelect.orbitToggle = new VRCToggle(orbitSelGrp, "Orbit Items", delegate(bool val)
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				if (val)
				{
					ItemOrbit.itemType = 0;
					ItemOrbit.target = OrbitSelect.target;
				}
				else
				{
					ItemOrbit.itemType = -1;
					ItemOrbit.target = null;
				}
			}, false, "Orbit Items Around To User", "Stop Orbiting Items Around To User", null, null, false);
			new DuoButtons(orbitSelGrp, "Head Spam", "Set Orbit Type To Head Spam", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "head";
			}, "Circle Orbit", "Set Orbit Type To Circle", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "circle";
			});
			new DuoButtons(orbitSelGrp, "Bubble Orbit", "Set Orbit Type To Bubble", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "bubble";
			}, "Ellipse Orbit", "Set Orbit Type To Ellipse", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "ellipse";
			});
			new DuoButtons(orbitSelGrp, "Spiral Orbit", "Set Orbit Type To Spiral", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "spiral";
			}, "Figure 8 Orbit", "Set Orbit Type To Figure Eight", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				WorldWrapper.FindItems();
				ItemOrbit.orbitShape = "figure8";
			});
			new VRCSlider(OrbitSelect.selectPage, "Orbit Speed : 1", "Changes Orbit Speed", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "Orbit Speed : " + val.ToString("F2");
				ItemOrbit.orbitFrequency = val;
			}, 1f, -10f, 10f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 1f;
				ItemOrbit.orbitFrequency = 1f;
			}, "Reset to 1", null);
			new VRCSlider(OrbitSelect.selectPage, "Orbit Radius : 1", "Changes Orbit Radius", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "Orbit Radius : " + val.ToString("F2");
				ItemOrbit.orbitRadius = val;
			}, 1f, 0f, 10f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 1f;
				ItemOrbit.orbitRadius = 1f;
			}, "Reset to 1", null);
			new VRCSlider(OrbitSelect.selectPage, "Orbit Height : 0", "Changes Orbit Height", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "Orbit Height : " + val.ToString("F2");
				ItemOrbit.orbitHeight = val;
			}, 0f, -5f, 5f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 0f;
				ItemOrbit.orbitHeight = 0f;
			}, "Reset to 0", null);
		}

		// Token: 0x040001B9 RID: 441
		internal static Player target;

		// Token: 0x040001BA RID: 442
		public static VRCPage selectPage;

		// Token: 0x040001BB RID: 443
		internal static VRCToggle orbitToggle;
	}
}
