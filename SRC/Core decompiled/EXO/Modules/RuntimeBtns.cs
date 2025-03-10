using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using EXO.LogTools;
using EXO.Modules.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Controls;

namespace EXO.Modules
{
	// Token: 0x02000073 RID: 115
	internal class RuntimeBtns
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x00016830 File Offset: 0x00014A30
		public static void CreateButtons()
		{
			string filePath = AppStart.HexedDirectory.FullName + "\\EXO\\Runtime.json";
			bool flag = !File.Exists(filePath);
			if (flag)
			{
				List<RuntimeJson> list = new List<RuntimeJson>();
				list.Add(new RuntimeJson
				{
					name = "DefaultButton",
					udonEvent = "DefaultEvent",
					desc = "Default description"
				});
				RuntimeBtns.buttons = list;
				string json = JsonConvert.SerializeObject(RuntimeBtns.buttons, Formatting.Indented);
				File.WriteAllText(filePath, json);
			}
			else
			{
				try
				{
					string json2 = File.ReadAllText(filePath);
					RuntimeBtns.buttons = JsonConvert.DeserializeObject<List<RuntimeJson>>(json2);
				}
				catch (Exception e)
				{
					CLog.E("Error reading or deserializing the JSON file", e);
					return;
				}
			}
			RuntimeBtns.runtimePage = new VRCPage("World Items", false, true, false, null, "", null, false);
			WorldPage worldPage = RuntimeBtns.runtimePage;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(8, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(RuntimeBtns.buttons.Count);
			defaultInterpolatedStringHandler.AppendLiteral(" Buttons");
			RuntimeBtns.runtimeBtnGrp = new ButtonGroup(worldPage, defaultInterpolatedStringHandler.ToStringAndClear(), false, TextAnchor.UpperCenter);
			RuntimeBtns.runtimeBtnGrp.RemoveAllChildren();
			RuntimeBtns.runtimePage.OpenMenu();
			for (int i = 0; i < RuntimeBtns.buttons.Count; i += 2)
			{
				RuntimeJson button1 = RuntimeBtns.buttons[i];
				RuntimeJson button2 = ((i + 1 < RuntimeBtns.buttons.Count) ? RuntimeBtns.buttons[i + 1] : null);
				bool flag2 = button2 != null;
				if (flag2)
				{
					new DuoButtons(RuntimeBtns.runtimeBtnGrp, button1.name, button1.desc, delegate
					{
						UdonUtils.SendUdonEvent(button1.eventObject, button1.udonEvent, NetworkEventTarget.All);
					}, button2.name, button2.desc, delegate
					{
						UdonUtils.SendUdonEvent(button2.eventObject, button2.udonEvent, NetworkEventTarget.All);
					});
				}
				else
				{
					new VRCButton(RuntimeBtns.runtimeBtnGrp, button1.name, button1.desc, delegate
					{
						UdonUtils.SendUdonEvent(button1.eventObject, button1.udonEvent, NetworkEventTarget.All);
					}, false, false, null, ExtentedControl.HalfType.Normal, false);
				}
			}
		}

		// Token: 0x040001F3 RID: 499
		public static List<RuntimeJson> buttons;

		// Token: 0x040001F4 RID: 500
		public static VRCPage runtimePage;

		// Token: 0x040001F5 RID: 501
		internal static ButtonGroup runtimeBtnGrp;
	}
}
