# EXO VRC And Petty EDrama
Below i will be sharing very unwanted/cringy edrama caused and started because of cyconi and his massive ego.

> [!NOTE]
> 1. Cyconi Need's to lower his ego
> 2. Cyconi You are not him
> 3. EXO and everything else he makes/puts out is pasted
> 4. EXO's entire Security and Networking isn't even made by him even though it's simple and easy
> 5. Your unoriginal and have no Rizz


Alright let's get started with this review of the code blocks that are blatantly pasted/ Chatgpt/ Spoonfed
1. This code is reversed/Deob so will not be 1:1
2. We will not be sharing any actually assembly's or full code blocks due to parts being 1:1 pasted from WCV2

Startup code block, No authentication on the core that's funny
```text
	public override void OnLoad(string[] args)
	{
		AppStart.CallOnLoad(this, false);
	}

	public static void CallOnLoad(HexedCheat instance, bool consoleLaunched = false)
	{
		AppStart.HexedDirectory = new FileInfo(instance.Path).Directory.Parent;
		ConfigManager.LoadConfig();
		ConsoleApp.wasLaunched = consoleLaunched;
		ConsoleApp.Launch();
		CLog.R("[====== EXO Loaded ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\AppStart.cs", 45);
		PlayerInit.playerTimer.Start();
		DiscordClient.Init();
		GetUser.Init();
		MonoManager.PatchUpdate(typeof(VRCApplication).GetMethod("Update"));
		MonoManager.PatchOnApplicationQuit(typeof(VRCApplicationSetup).GetMethod("OnApplicationQuit"));
		UpdateManager.StartFixedUpdate();
		StartMsgs.Watermark();
		CoroutineManager.RunCoroutine(AppStart.smethod_0());
		PatchCore.LoadPatches();
		FunctionCore.OnLoad();
		ModuleCore.OnLoad();
		WorldWrapper.AddAction();
	}
```

Im not even going to to say anything on this one
```text
internal class CLog
{
	internal static void Tag()
	{
		Console.ResetColor();
		Logs.WriteOut("[", 15);
		Console.ResetColor();
		Logs.WriteOut("EXO", 4);
		Console.ResetColor();
		Logs.WriteOut("] ", 15);
		Console.ResetColor();
	}

	internal static void L(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
	{
		string fileName = Path.GetFileName(filePath);
		CLog.Tag();
		if (AppStart.debugMode)
		{
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(fileName);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
			Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
		}
		Console.ResetColor();
		Logs.WriteOut("[", 15);
		Console.ResetColor();
		Logs.WriteOut("~>", 4);
		Console.ResetColor();
		Logs.WriteOut("] ", 15);
		Console.ResetColor();
		Logs.WriteOutLine(MessageToLog, 15);
		Console.ResetColor();
		if (logToGUI && MenuCore.quickMenu != null)
		{
			GUILog.DisplayOnScreen(MessageToLog);
		}
	}

	internal static void D(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
	{
		if (AppStart.debugMode)
		{
			string fileName = Path.GetFileName(filePath);
			CLog.Tag();
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(fileName);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
			Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			Logs.WriteOut("~>", 4);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, 13);
			Console.ResetColor();
			if (logToGUI)
			{
				GUILog.DisplayOnScreen(MessageToLog);
			}
		}
	}

	internal static void R(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
	{
		string fileName = Path.GetFileName(filePath);
		CLog.Tag();
		if (AppStart.debugMode)
		{
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(fileName);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
			Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
		}
		Console.ResetColor();
		Logs.WriteOut("[", 15);
		Console.ResetColor();
		Logs.WriteOut("~>", 4);
		Console.ResetColor();
		Logs.WriteOut("] ", 15);
		Console.ResetColor();
		Logs.WriteOutLine(MessageToLog, 4);
		Console.ResetColor();
		if (logToGUI)
		{
			GUILog.DisplayOnScreen(MessageToLog);
		}
	}

	internal static void E(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
	{
		string fileName = Path.GetFileName(filePath);
		CLog.Tag();
		if (AppStart.debugMode)
		{
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(fileName);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
			Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
		}
		Logs.WriteOut("[", 15);
		Console.ResetColor();
		Logs.WriteOut("~>", 4);
		Console.ResetColor();
		Logs.WriteOut("] ", 15);
		Console.ResetColor();
		Logs.WriteOutLine(MessageToLog, 12);
		Console.ResetColor();
		if (logToGUI)
		{
			GUILog.DisplayOnScreen("[ERROR] " + MessageToLog);
		}
	}

	public static void E(Exception ex)
	{
		string stackTrace = ex.StackTrace;
		string message = ex.Message;
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
		defaultInterpolatedStringHandler..ctor(122, 4);
		defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \nTIME: ");
		defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
		defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
		defaultInterpolatedStringHandler.AppendFormatted(message);
		defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
		defaultInterpolatedStringHandler.AppendFormatted(stackTrace);
		defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
		defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
		defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
		CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\CLog.cs", 133);
	}

	public static void E(string msg, Exception ex)
	{
		string stackTrace = ex.StackTrace;
		string message = ex.Message;
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
		defaultInterpolatedStringHandler..ctor(123, 5);
		defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \n");
		defaultInterpolatedStringHandler.AppendFormatted(msg);
		defaultInterpolatedStringHandler.AppendLiteral("\nTIME: ");
		defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
		defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
		defaultInterpolatedStringHandler.AppendFormatted(message);
		defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
		defaultInterpolatedStringHandler.AppendFormatted(stackTrace);
		defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
		defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
		defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
		CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\CLog.cs", 140);
	}
}
```

Don't think hacker will care since this old and does not work, and Cyc is alittle to slow to fix this but yet he leaves/uses it though it doesn't work
```text
internal class ColorMenus
{
	public static void Paint()
	{
		Transform transform = UtilFunc.UserInterface.FindObject("Canvas_MainMenu(Clone)/Container/MMParent");
		transform.FindObject("Page_MM_Backgrounds").SetActive(true);
		Button[] array = Resources.FindObjectsOfTypeAll<Button>();
		Button button = null;
		foreach (Button button2 in array)
		{
			if (button2.gameObject.name == "Filigree" && button2.gameObject.transform.parent.name == "Page_MM_Backgrounds")
			{
				button = button2;
				IL_008A:
				if (button != null)
				{
					button.onClick.Invoke();
				}
				else
				{
					CLog.E("Button not found", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\ColorMenus.cs", 54);
				}
				return;
			}
		}
		goto IL_008A;
	}

	internal static Color HexToColor(string hexColor)
	{
		if (hexColor.IndexOf('#') != -1)
		{
			hexColor = hexColor.Replace("#", "");
		}
		float num = (float)int.Parse(hexColor.Substring(0, 2), 512) / 255f;
		float num2 = (float)int.Parse(hexColor.Substring(2, 2), 512) / 255f;
		float num3 = (float)int.Parse(hexColor.Substring(4, 2), 512) / 255f;
		return new Color(num, num2, num3);
	}

	internal static string ColorToHex(Color baseColor, bool hash = false)
	{
		int num = Convert.ToInt32(baseColor.r * 255f);
		int num2 = Convert.ToInt32(baseColor.g * 255f);
		int num3 = Convert.ToInt32(baseColor.b * 255f);
		string text = num.ToString("X2") + num2.ToString("X2") + num3.ToString("X2");
		if (hash)
		{
			text = "#" + text;
		}
		return text;
	}

	internal static void RecolorText(string hex)
	{
		foreach (Text text in MenuCore.quickMenu.GetComponentsInChildren<Text>(true))
		{
			text.color = ColorMenus.HexToColor(hex);
		}
		foreach (TextMeshProUGUI textMeshProUGUI in MenuCore.quickMenu.GetComponentsInChildren<TextMeshProUGUI>(true))
		{
			textMeshProUGUI.color = ColorMenus.HexToColor(hex);
		}
	}

	internal static void RecolorButton(string hex)
	{
		Color color = ColorMenus.HexToColor(hex);
		foreach (Toggle toggle in MenuCore.quickMenu.GetComponentsInChildren<Toggle>(true))
		{
			try
			{
				if (!ColorMenus.HasParentWithName(toggle.gameObject, "HorizontalLayoutGroup"))
				{
					ColorMenus.RecolorBackGrn(toggle.transform.Find("Background").gameObject, color);
				}
			}
			catch (Exception)
			{
			}
		}
		foreach (Button button in MenuCore.quickMenu.GetComponentsInChildren<Button>(true))
		{
			try
			{
				if (ColorMenus.HasParentWithName(button.gameObject, "HorizontalLayoutGroup"))
				{
					continue;
				}
				ColorMenus.RecolorBackGrn(button.transform.Find("Background").gameObject, color);
			}
			catch (Exception)
			{
			}
			try
			{
				if (ColorMenus.HasParentWithName(button.gameObject, "HorizontalLayoutGroup"))
				{
					continue;
				}
				if (button.transform.Find("Background") == null)
				{
					ColorMenus.RecolorBackGrn(button.transform.Find("Background (1)").gameObject, color);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (ColorMenus.HasParentWithName(button.gameObject, "HorizontalLayoutGroup"))
				{
					continue;
				}
				if (button.transform.Find("Background") == null)
				{
					ColorMenus.RecolorBackGrn(button.transform.Find("Background (2)").gameObject, color);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (!ColorMenus.HasParentWithName(button.gameObject, "HorizontalLayoutGroup"))
				{
					ColorMenus.RecolorBackGrn(button.transform.Find("Container/Background").gameObject, color);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	private static bool HasParentWithName(GameObject obj, string parentName)
	{
		Transform transform = obj.transform.parent;
		while (transform != null)
		{
			if (transform.gameObject.name.Equals(parentName))
			{
				return true;
			}
			transform = transform.parent;
		}
		return false;
	}

	internal static void RecolorBackGrn(Transform transform, Color color)
	{
		ColorMenus.RecolorBackGrn(transform.gameObject, color);
	}

	internal static void RecolorBackGrn(GameObject bg, Color color)
	{
		if (bg.transform.parent.Find("Bg") == null)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(bg.gameObject, bg.transform.parent);
			gameObject.name = "Bg";
			gameObject.GetComponent<RectTransform>().SetSiblingIndex(1);
			bg.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			bg.active = false;
		}
		Image component = bg.transform.parent.Find("Bg").GetComponent<Image>();
		component.color = color;
	}

	internal static void RecolorPointer(int side, string hex)
	{
		if (PlayerWrapper.IsInVR())
		{
			Color color = UtilFunc.HexToColor(hex);
			Color color2 = new Color(color.r, color.g, color.b, 0.8944f);
			if (ColorMenus.VRUiCursorL == null)
			{
				ColorMenus.VRUiCursorL = GameObject.Find("CursorManager").FindObject("DotLeftHand").GetComponent<VRCUiCursor>();
				ColorMenus.VRUiCursorR = GameObject.Find("CursorManager").FindObject("DotRightHand").GetComponent<VRCUiCursor>();
			}
			if (side == -1)
			{
				ColorMenus.VRUiCursorL.field_Public_Color_0 = color2;
				ColorMenus.VRUiCursorL.field_Public_Color_1 = color2;
			}
			else
			{
				ColorMenus.VRUiCursorR.field_Public_Color_0 = color2;
				ColorMenus.VRUiCursorR.field_Public_Color_1 = color2;
			}
			if (ColorMenus.VRCUICursorIcon == null)
			{
				ColorMenus.VRCUICursorIcon = VRCApplication.prop_VRCApplication_0.FindObject("CursorManager/MouseArrow/VRCUICursorIcon").GetComponent<SpriteRenderer>();
			}
			ColorMenus.VRCUICursorIcon.color = color;
		}
	}

	internal static void ChangeQMBG()
	{
		Transform transform = APIBase.QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/BackgroundLayer01");
		if (ColorMenus.QMImage == null)
		{
			ColorMenus.QMImage = transform.GetComponent<Image>();
		}
		ColorMenus.UpdateQMColor();
	}

	internal static void UpdateQMColor()
	{
		ColorMenus.QMImage.color = new Color(0f, 0f, 0f, 0f);
	}

	internal static void ColorWing(int side, string hex)
	{
		Color color = UtilFunc.HexToColor(hex);
		string text = ((side == 1) ? "Right" : "Left");
		Transform transform = APIBase.QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_" + text + "/Container/InnerContainer/Background");
		transform.GetComponent<Image>().color = color;
	}

	private static VRCUiCursor VRUiCursorL;

	private static VRCUiCursor VRUiCursorR;

	private static SpriteRenderer VRCUICursorIcon;

	private static Image QMImage;
}
```

Code was removed on this build but this was lit just from umbra's github lol
```text
internal class AmplitudeWrapperPatch : PatchModule
{
	private delegate void _ReturnAllDelegate();
}

```

Join and leave patch, From WCV2. removed methods code
```text
internal class JoinLeavePatch : PatchModule
{

	internal static Action<Player> OnPlayerJoin { get; set; }
	internal static Action<Player> OnPlayerLeave { get; set; }
	internal static Action<Player> OnLocalPlayerJoin { get; set; }
	internal static Action<Player> OnLocalPlayerLeave { get; set; }
	internal static VRCPickup[] curItems { get; set; }

	public override void LoadPatch()
	{

	}

	private static void OnPlayerJoin_Internal(VRCPlayer __instance)
	{

	}


	internal static void JoinLogs(string user, string id)
	{
		CLog.Tag();
		"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 85).WriteToConsole("Join", 10, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 86).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 87)
			.WriteToConsole(user + " ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 88)
			.WriteToConsole("| ", 10, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 89)
			.WriteLineToConsole(id + " ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 90);
	}

	internal static void LeaveLogs(string user, string id)
	{
		CLog.Tag();
		"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 95).WriteToConsole("Left", 12, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 96).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 97)
			.WriteToConsole(user + " ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 98)
			.WriteToConsole("| ", 12, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 99)
			.WriteLineToConsole(id + " ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 100);
	}

	[CompilerGenerated]
	private static class <>O
	{
		public static Action<VRCPlayer> <0>__OnPlayerJoin_Internal;
		public static Action<Player> <1>__OnPlayerLeave_Internal;
	}
```

OPevent Patch and Functions, From WCV2, Removed Some code
```text
internal class OpEvents : PatchModule {
internal static bool EventLog { get; set; }

	internal static bool Serlz { get; set; }

	public override void LoadPatch()
	{
		bool hasSerlzNoft = false;
		MethodInfo method = typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0");
		<>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool> <>F{00000048};
		if ((<>F{00000048} = OpEvents.<>O.<0>__OpRaseEvents) == null)
		{
			<>F{00000048} = (OpEvents.<>O.<0>__OpRaseEvents = new <>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool>(OpEvents.OpRaseEvents));
		}
		PatchHandler.Detour(method, <>F{00000048});
		PatchHandler.Detour(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), delegate(LoadBalancingClient instance, byte __0, Object __1, RaiseEventOptions __2, IntPtr __3)
		{
			if (__0 == 12)
			{
				if (!hasSerlzNoft && OpEvents.Serlz)
				{
					hasSerlzNoft = true;
					CLog.L("Failed to Block OPEvents, Fake Crash has failed!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\OpEvents.cs", 47);
				}
				else if (!OpEvents.Serlz)
				{
					hasSerlzNoft = false;
				}
			}
		});
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000F674 File Offset: 0x0000D874
	private static bool OpRaseEvents(LoadBalancingClient instance, ref byte __0, ref Object __1, RaiseEventOptions __2, IntPtr __3)
	{
		bool flag;
		if (OpEvents.blockLocalUSpeak && __0 == 1)
		{
			if (!OpEvents.HasNoft)
			{
				CLog.L("For your safety you can't Speak!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\OpEvents.cs", 63);
				OpEvents.HasNoft = true;
				UtilFunc.Delay(10f, delegate
				{
					OpEvents.HasNoft = false;
				});
			}
			flag = false;
		}
		else if (OpEvents.blockLocalMovment && __0 == 12)
		{
			flag = false;
		}
		else
		{
			byte b = __0;
			byte b2 = b;
			if (b2 != 172)
			{
				if (b2 == 181)
				{
					__0 = 1;
				}
				if (__0 == 1 || __0 == 12)
				{
					try
					{
						OpEvents.EventData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__1.Pointer);
					}
					catch
					{
						return true;
					}
				}
				OpEvents.IsReady = true;
				if (OpEvents.PozFreze && __0 == 12 && OpEvents.FrezePoz == null)
				{
					OpEvents.FrezePoz =
				}
				if (UtilsMenu.FuckUspeak && __0 == 1)
				{
					if (OpEvents.lastServTime == Networking.GetServerTimeInMilliseconds())
					{

					}
					else
					{

					}
				}
				if (!OpEvents.EventLog)	{
				}
				flag = !OpEvents.Serlz || ;
			}
			else
			{
				flag = false;
			}
		}
		return flag;
	}

	internal static int lastServTime;
	internal static bool blockLocalUSpeak;
	internal static bool blockLocalMovment;
	internal static bool PozFreze;
	internal static bool IsReady;
	internal static bool HasNoft;
	internal static byte[] FrezePoz;
	internal static byte[] EventData;

	[CompilerGenerated]
	private static class <>O
	{
		public static <>F{00000048}<LoadBalancingClient, byte, Object, RaiseEventOptions, IntPtr, bool> <0>__OpRaseEvents;
	}
}
```

VRC+ Stuff Just edited "Hardly" from WCV2, Remove parts of code
```text
internal class ColorPaletteOverride
{
	private static void MakePalette()
	{
		try
		{
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
			{
				MenuCore.MenuLogs("Made Palette exo");
			}
		}
		catch (Exception ex)
		{
			MenuCore.MenuLogs("Error while creating palette: " + ex.Message);
		}
	}

	internal static void Palettes()
	{
		using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
		{
			foreach (string text in registryKey.GetValueNames())
			{
				if (text.Contains("COLOR_PALETTES_" + PlayerWrapper.LocalAPIUser.id) && text.Contains(PlayerWrapper.LocalAPIUser.id))
				{
					break;
				}
			}
		}
	}

	internal static void InitPalettes()
	{
		Transform mmpar = UtilFunc.UserInterface.FindObject("Canvas_MainMenu(Clone)/Container/MMParent");
		if (!PlayerWrapper.LocalAPIUser.isSupporter)
		{
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
			{
				string[] valueNames = registryKey.GetValueNames();
				for (int i = 0; i < valueNames.Length; i++)
				{
					string text = valueNames[i];
					if (text.Contains("COLOR_PALETTES_CURRENT_" + PlayerWrapper.LocalAPIUser.id) && text.Contains(PlayerWrapper.LocalAPIUser.id))
					{
							MenuCore.MenuLogs("EXO Color Palette\n\n#9E0000,#FFFFFF,#0A0A0A,#000000,#FFFFFF,#FF0000\n");
							MenuCore.MenuLogs("Applying " + name + "...");

							objComp.SetActive(true);
							UtilFunc.UpdateClamp(0.3f, delegate
							{
							});
						});
						IL_0125:
						return;
					}
				}
				goto IL_0125;
			}
		}
	}

	internal static void EnableVRCPlus()
	{
		if (!PlayerWrapper.LocalAPIUser.isSupporter)
		{
			UtilFunc.Delay(0.1f, delegate
			{

			});
		}
	}

	private static Action applyAction; 
```

Player and World Based Wrappers from WCV2, Obv not going to post all of that
```text
internal class WorldWrapper
{
	internal static void AddAction()
	{
		JoinLeavePatch.OnLocalPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerJoin, delegate(Player plr)
		{

		});
	}
```

PhotonExtensions 1:1 Wcv2
```text
public static void OpRaiseEvent<[IsUnmanaged] T>(byte code, T[] customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions) where T : struct, ValueType
	{

	}

	public static void OpRaiseEvent<S, T>(byte code, Dictionary<S, T> customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
	{

	}

	public static void OpRaiseEvent(byte code, global::Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
	{
		if (customObject == null)
		{
			throw new NullReferenceException("YOUR DUMBASS ALMOST GOT BANNED, AGAIN!");
		}
		}internal static void EditMovmentData(ref byte[] EventData, Vector3 Poz, Quaternion rotation, byte Fps = 0, short Ping = -1, bool check = true)
	{
		if (check && !PhotonExtensions.CheckIsValid(EventData, null))
		{
			throw new Exception("WARNING : BAD PAYLOAD SKIPPING!!");
		}
	}internal static void Scan(byte[] array, int ac)
	{
		{
			while (Math.Round((double)BitConverter.ToSingle(array, num), 1) != num2)
			{
				num++;
			}
			if (Math.Round((double)BitConverter.ToSingle(array, num + 4), 1) != num3)
			{
				CLog.D("Failed?", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 116);
			}
			else
			{
				PhotonExtensions.FpsOffset = num;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(52, 4);
				defaultInterpolatedStringHandler.AppendLiteral("Got Momvnet offsets (Poz : ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.MovmentOffset);
				defaultInterpolatedStringHandler.AppendLiteral(" | Rot : ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.RotOffset);
				defaultInterpolatedStringHandler.AppendLiteral(" Ping : ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.PingOffset);
				defaultInterpolatedStringHandler.AppendLiteral(" Fps : ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(PhotonExtensions.FpsOffset);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\PhotonExtensions.cs", 126);
			}
		}
	}
```

Third Person Module From WCV2, Again Edit BUT FROM OUR GREAT FRIEND CHATGPT !!!
```text
internal class ThirdPerson : FunctionModule
{
	public override void OnUpdate()
	{
		if (InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.ThirdPersonBind1), InputMap.GetKeyCode(Config.binds.ThirdPersonBind2)) && Config.cfg.ThirdPersonBind && !ThirdPerson.isUpdating && PlayerInit.playerInit)
		{
			ThirdPerson.On3rdPersonStart();
		}
		if (ThirdPerson.isUpdating && PlayerInit.playerInit)
		{
			ThirdPerson.Camera3rdUpdate();
		}
	}

	public static void On3rdPersonStart()
	{
		ThirdPerson.mainCamera = Camera.main.gameObject;
		if (ThirdPerson.mainCamera != null)
		{
		
			UtilFunc.Delay(0.01f, delegate
			{
				ThirdPerson.ResetCameras(CamMenu.offsetValue);
			});
			ThirdPerson.isUpdating = true;
		}
		else
		{
			CLog.L("[Third Person] Camera was Null!!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\ThirdPerson.cs", 84);
		}
	}

	public static void Switch()
	{

		switch (ThirdPerson.lastCamera)
		{
		case 0:

			break;
		case 1:

			break;
		case 2:

			break;
		}
	}

	public static void Camera3rdUpdate()
	{
		if (Config.cfg.ThirdPersonBind && !(PlayerWrapper.LocalVRCPlayer == null) && PlayerWrapper.LocalVRCPlayerAPI != null)
		{
			try
			{
				CamTweaks.currentHeight = PlayerWrapper.LocalVRCPlayerAPI.GetAvatarEyeHeightAsMeters();
				ThirdPerson.frontCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
				ThirdPerson.backCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
				if (ThirdPerson.backCamera != null && ThirdPerson.frontCamera != null && ThirdPerson.inThird && ThirdPerson.lastCamera != 0)
				{
					if (Input.GetKeyDown(InputMap.GetKeyCode(Config.binds.CameraResetBind)))
					{
						ThirdPerson.ResetCameras(CamMenu.offsetValue);
					}
					float axis = Input.GetAxis("Mouse ScrollWheel");
					float num = Vector3.Distance(ThirdPerson.frontCamera.transform.position, ThirdPerson.backCamera.transform.position);
					if (axis > 0f && num > ThirdPerson.proxThresh * CamTweaks.currentHeight)
					{
						ThirdPerson.backCamera.transform.position += ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
						ThirdPerson.frontCamera.transform.position -= ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
					}
					else if (axis < 0f)
					{
						ThirdPerson.backCamera.transform.position -= ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
						ThirdPerson.frontCamera.transform.position += ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
					}
				}
				if (InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.ThirdPersonBind1), InputMap.GetKeyCode(Config.binds.ThirdPersonBind2)) && Config.cfg.ThirdPersonBind)
				{
					ThirdPerson.Switch();
				}
			}
			catch (Exception ex)
			{
				string text = "Error\n";
				Exception ex2 = ex;
				CLog.L(text + ((ex2 != null) ? ex2.ToString() : null), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\ThirdPerson.cs", 158);
			}
		}
	}

	internal static void ResetCameras(float offset = 0f)
	{
		if (ThirdPerson.inThird)
		{
			Vector3 position = ThirdPerson.mainCamera.transform.position;
			Vector3 vector;
			try
			{
				vector = PlayerWrapper.LocalVRCPlayer.GetAnimator().GetBoneTransform(HumanBodyBones.Head).position;
			}
			catch
			{
				vector = ThirdPerson.mainCamera.transform.position;
			}
		}
	}

	public static bool inThird = false;
	private static GameObject mainCamera;
	private static GameObject backCamera;
	private static GameObject frontCamera;
	private static int lastCamera;
	internal static bool isUpdating;
	internal static float fov = 80f;
	public static float proxThresh = 0.8f;
}
```

Do i even have to say where this from
```text
internal class AttachToPlayer : FunctionModule
{
	internal static string attachLocation { get; set; } = "head";
	internal static bool isAttached { get; set; } = false;
	internal static Player target { get; set; }

	public override void OnPlayerWasInit()
	{
		JoinLeavePatch.OnPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerJoin, delegate(Player plr)
		{
		});
	}

	public override void OnUpdate()
	{
		if (AttachToPlayer.isAttached && !(AttachToPlayer.target == null) && !(AttachToPlayer.target == AttachToPlayer.localPlayer))
		{
			if (AttachToPlayer.localPlayer == null)
			{
			}
			string attachLocation = AttachToPlayer.attachLocation;
			string text = attachLocation;
			Transform transform;
			Vector3 vector;
			if (!(text == "hip"))
			{
				if (!(text == "rightHand"))
				{
					if (!(text == "leftHand"))
					{
						if (!(text == "rightFoot"))
						{
							if (!(text == "leftFoot"))
							{
							
							}
							else
							{

							}
						}
						else
						{
						}
					}
					else
					{

					}
				}
				else
				{

				}
			}
			else
			{
			}
		}
	}

	internal static Player localPlayer;
	internal static float xOffset = 0f;
	internal static float yOffset = 0f;
	internal static float zOffset = 0f;
}
```

We love chatgpt
```text
		private static Vector3 CalcOrbitPos(int index, Vector3 targetPosition, float height)
	{
		float num = ItemOrbit.orbitFrequency * (float)index;
		float num2 = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + num) * ItemOrbit.orbitRadius;
		float num3 = Mathf.Cos(Time.time * ItemOrbit.orbitFrequency + num) * ItemOrbit.orbitRadius;
		return targetPosition + new Vector3(num2, height + ItemOrbit.orbitHeight, num3);
	}

	private static Vector3 CalcBubbleOrbitPos(int index, Vector3 targetPosition, float height)
	{
		float num = ItemOrbit.orbitFrequency * (float)index;
		float num2 = Time.time * ItemOrbit.orbitFrequency + num;
		float num3 = 6.2831855f * (float)index / 50f;
		float num4 = ItemOrbit.orbitRadius * Mathf.Sin(num2) * Mathf.Cos(num3);
		float num5 = ItemOrbit.orbitRadius * Mathf.Sin(num2) * Mathf.Sin(num3) + height;
		float num6 = ItemOrbit.orbitRadius * Mathf.Cos(num2);
		return targetPosition + new Vector3(num4, num5 + height + ItemOrbit.orbitHeight, num6);
	}

	private static Vector3 CalcEllipseOrbitPos(int index, Vector3 targetPosition, float height)
	{
		float num = ItemOrbit.orbitFrequency * (float)index;
		float num2 = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + num) * ItemOrbit.orbitRadius;
		float num3 = Mathf.Cos(Time.time * (ItemOrbit.orbitFrequency / 2f) + num) * (ItemOrbit.orbitRadius * 2f);
		return targetPosition + new Vector3(num2, height + ItemOrbit.orbitHeight, num3);
	}

	private static Vector3 CalcSpiralOrbitPos(int index, Vector3 targetPosition, float height)
	{
		float num = ItemOrbit.orbitFrequency * (float)index;
		float num2 = 0.05f;
		float num3 = 1f;
		float num4 = num3 + num * num2;
		if (num4 > num3 + ItemOrbit.orbitRadius * 3f)
		{
			num4 = num3 + ItemOrbit.orbitRadius * 3f;
		}
		float num5 = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + num) * num4;
		float num6 = Mathf.Cos(Time.time * ItemOrbit.orbitFrequency + num) * num4;
		return targetPosition + new Vector3(num5, height + ItemOrbit.orbitHeight, num6);
	}

	private static Vector3 CalcFigure8OrbitPos(int index, Vector3 targetPosition, float height)
	{
		float num = ItemOrbit.orbitFrequency * (float)index;
		float num2 = Time.time * ItemOrbit.orbitFrequency + num;
		float num3 = Mathf.Sqrt(1f + Mathf.Pow(Mathf.Sin(num2), 2f));
		float num4 = 2f * ItemOrbit.orbitRadius * Mathf.Sin(num2) / num3;
		float num5 = 2f * ItemOrbit.orbitRadius * Mathf.Sin(num2) * Mathf.Cos(num2) / num3;
		return targetPosition + new Vector3(num4, height + ItemOrbit.orbitHeight, num5);
	}
```

Bone Esp 1:1 From WCV2
```text
internal static void RenderPlayer(Player player)
	{
		bool flag;
		if (player == null)
		{
			flag = false;
		}
		else
		{
		}
		if (!flag)
		{
			if (lineRenderer == null)
			{
			}
			for (int i = 0; i < BoneESP.ToReneder.Length; i++)
			{
				try
				{
					if (!flag3 && BoneESP.ToReneder[i] == HumanBodyBones.UpperChest)
					{
					
					}
					else
					{
						
					}
				}
				catch
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(4, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Num ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(i);
					CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Render\\BoneESP.cs", 114);
				}
			}

		}
	}
```

Chatgptin up in the club !!!!
```text
internal class LaserSight : FunctionModule
{
	private static Mesh CreateCircleMesh(float radius, int segments)
	{
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[segments + 1];
		int[] array2 = new int[segments * 3];
		array[0] = Vector3.zero;
		float num = 360f / (float)segments;
		for (int i = 1; i <= segments; i++)
		{
			float num2 = num * (float)i * 0.017453292f;
			array[i] = new Vector3(Mathf.Cos(num2) * radius, Mathf.Sin(num2) * radius, 0f);
		}
		for (int j = 0; j < segments; j++)
		{
			int num3 = j + 1;
			array2[j * 3] = 0;
			array2[j * 3 + 1] = num3;
			array2[j * 3 + 2] = ((num3 == segments) ? 1 : (num3 + 1));
		}
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.RecalculateNormals();
		return mesh;
	}
	internal static void PrepLineRenderer(string objectName)
	{
		GameObject gameObject = GameObject.Find(objectName);
		if (gameObject != null)
		{
			GameObject gameObject2 = new GameObject(objectName + " Laser");
			gameObject2.transform.SetParent(gameObject.transform);
			LineRenderer lineRenderer = gameObject2.AddComponent<LineRenderer>();
			lineRenderer.startWidth = 0.005f;
			lineRenderer.endWidth = 0.005f;
			lineRenderer.alignment = LineAlignment.View;
			lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
			Color color = new Color(1f, 0f, 0f, 1f);
			lineRenderer.material.color = color;
			lineRenderer.enabled = true;
			GameObject gameObject3 = new GameObject("Circle");
			gameObject3.transform.SetParent(gameObject2.transform);
			MeshFilter meshFilter = gameObject3.AddComponent<MeshFilter>();
			meshFilter.mesh = LaserSight.CreateCircleMesh(0.015f, 20);
			MeshRenderer meshRenderer = gameObject3.AddComponent<MeshRenderer>();
			meshRenderer.material = new Material(Shader.Find("GUI/Text Shader"))
			{
				color = color
			};
			LaserSight.lineRenderers[objectName] = lineRenderer;
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001F834 File Offset: 0x0001DA34
	private static void UpdateLineRenderer(string objectName, Vector3 directionOverride = default(Vector3), float minDistanceThreshold = 0.15f)
	{
		LineRenderer lineRenderer;
		if (LaserSight.lineRenderers.TryGetValue(objectName, ref lineRenderer) && lineRenderer != null)
		{
			GameObject gameObject = GameObject.Find(objectName);
			if (gameObject != null)
			{
				Vector3 position = gameObject.transform.position;
				lineRenderer.SetPosition(1, position);
				Vector3 vector = ((directionOverride == default(Vector3)) ? gameObject.transform.forward : directionOverride);
				Ray ray = new Ray(position, vector);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 3.4028235E+38f, LaserSight.raycastLayerMask) && raycastHit.transform != null && raycastHit.distance > minDistanceThreshold && !raycastHit.transform.name.ToLower().Contains("mirror"))
				{
					Vector3 vector2 = raycastHit.point;
					lineRenderer.SetPosition(0, vector2);
					if (lineRenderer.transform.childCount > 0)
					{
						Transform child = lineRenderer.transform.GetChild(0);
						child.gameObject.SetActive(true);
						child.position = vector2 + raycastHit.normal * 0.001f;
						child.rotation = Quaternion.LookRotation(raycastHit.normal);
					}
				}
				else
				{
					Vector3 vector2 = ray.GetPoint(200f);
					lineRenderer.SetPosition(0, vector2);
					if (lineRenderer.transform.childCount > 0)
					{
						Transform child2 = lineRenderer.transform.GetChild(0);
						child2.gameObject.SetActive(false);
					}
				}
			}
		}
	}
	public override void OnUpdate()
	{
		if (Murder4.toggleLaser)
		{
			if (!LaserSight.lineRenderers.ContainsKey(this.revolverPath))
			{
				LaserSight.PrepLineRenderer(this.revolverPath);
			}
			LaserSight.UpdateLineRenderer(this.revolverPath, default(Vector3), 0.15f);
			if (!LaserSight.lineRenderers.ContainsKey(this.shotgunPath))
			{
				LaserSight.PrepLineRenderer(this.shotgunPath);
			}
			LaserSight.UpdateLineRenderer(this.shotgunPath, default(Vector3), 0.15f);
			if (!LaserSight.lineRenderers.ContainsKey(this.lugerPath))
			{
				LaserSight.PrepLineRenderer(this.lugerPath);
			}
			LaserSight.UpdateLineRenderer(this.lugerPath, default(Vector3), 0.15f);
		}
		else
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, LineRenderer> keyValuePair in LaserSight.lineRenderers)
			{
				if (keyValuePair.Value != null)
				{
					Object.Destroy(keyValuePair.Value.gameObject);
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string text in list)
			{
				LaserSight.lineRenderers.Remove(text);
			}
		}
	}
	public override void OnPlayerWasDestroyed()
	{
		foreach (KeyValuePair<string, LineRenderer> keyValuePair in LaserSight.lineRenderers)
		{
			if (keyValuePair.Value != null)
			{
				Object.Destroy(keyValuePair.Value.gameObject);
			}
		}
		LaserSight.lineRenderers.Clear();
	}
	internal static Dictionary<string, LineRenderer> lineRenderers = new Dictionary<string, LineRenderer>();
	internal string revolverPath = "Game Logic/Weapons/Revolver/Recoil Anim/Recoil";
	internal string shotgunPath = "Game Logic/Weapons/Unlockables/Shotgun (0)/Recoil Anim/Recoil";
	internal string lugerPath = "Game Logic/Weapons/Unlockables/Luger (0)/Recoil Anim/Recoil";
	private static LayerMask raycastLayerMask = ~LayerMask.GetMask(new string[] { "IgnoreRaycastEXO" });
}}
```

AntiBlock From Wcv2
```text
internal static bool IsBlocking(EventData __0)
	{
		bool flag;
		if (__0.Code != 33 || !Config.cfg.AntiBlock)
		{
			flag = false;
		}
		else
		{		
			byte b2 = b;
			if (b2 != 8)
			{
				if (b2 != 13)
				{
					if (b2 == 21)
					{
						if (dictionary.Count == 4)
						{
							if (byActorID != null)
							{
								if (dictionary[10].Unbox<bool>())
								{
								}
								else if (!dictionary[10].Unbox<bool>() && AntiBlock.hasBlockedYou.Contains(byActorID.ActorID()))
								{
									CLog.L(byActorID.DisplayName() + " Unblocked You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 55);
								}
								if (dictionary[11].Unbox<bool>())
								{
								}
								else if (!dictionary[11].Unbox<bool>() && AntiBlock.hasMutedYou.Contains(byActorID.ActorID()))
								{
								}
								return dictionary[10].Unbox<bool>();
							}
							return false;
						}
						else if (dictionary.Count == 3)
						{					
							foreach (int num in il2CppArrayBase)
							{
								if (byActorID2 == null)
								{
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
									defaultInterpolatedStringHandler.AppendLiteral("Unknown Actor (");
									defaultInterpolatedStringHandler.AppendFormatted<int>(num);
									defaultInterpolatedStringHandler.AppendLiteral(") Blocked you!");
									CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 76);
								}
								else if (!AntiBlock.hasBlockedYou.Contains(num))
								{
								}
								else
								{
									CLog.L(byActorID2.DisplayName() + " Unblocked You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 84);
									AntiBlock.hasBlockedYou.Remove(num);
								}
							}
							foreach (int num2 in il2CppArrayBase2)
							{
							}
							if (il2CppArrayBase.Length > 0)
							{
								return true;
							}
						}
					}
				}
				else
				{					
					string text = dictionary[2].ToString().Replace("A vote kick has been initiated against ", "").Replace(", do you agree?", "");
					CLog.L("Votekick started on " + text, true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 102);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(24, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Votekick started on ");
					defaultInterpolatedStringHandler.AppendFormatted<Player>(byActorID3);
					defaultInterpolatedStringHandler.AppendLiteral(" by ");
					defaultInterpolatedStringHandler.AppendFormatted<Player>(byActorID4);
					CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 103);
				}
				flag = false;
			}
			else
			{
				CLog.L("The Room Owner Just Tried to Mute You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 96);
				flag = true;
			}
		}
		return flag;
	}
```

My old QM Edit code, Cyc uses it as his qm edit and it looks so bad :SkullEmoji:
```text
internal static void ApplyQMOverride()
	{
		MenuCore.quickMenu = GameObject.Find("Canvas_QuickMenu(Clone)");
		if (!(MenuCore.quickMenu == null))
		{
			Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
			string text = "Button_Worlds";
			Transform transform = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Worlds");
			dictionary.Add(text, (transform != null) ? transform.gameObject : null);
			string text2 = "Button_Avatars";
			Transform transform2 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Avatars");
			dictionary.Add(text2, (transform2 != null) ? transform2.gameObject : null);
			string text3 = "Button_Social";
			Transform transform3 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Social");
			dictionary.Add(text3, (transform3 != null) ? transform3.gameObject : null);
			string text4 = "Button_ViewGroups";
			Transform transform4 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_ViewGroups");
			dictionary.Add(text4, (transform4 != null) ? transform4.gameObject : null);
			string text5 = "Button_GoHome";
			Transform transform5 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_GoHome");
			dictionary.Add(text5, (transform5 != null) ? transform5.gameObject : null);
			string text6 = "Button_Respawn";
			Transform transform6 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_Respawn");
			dictionary.Add(text6, (transform6 != null) ? transform6.gameObject : null);
			string text7 = "Button_SelectUser";
			Transform transform7 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_SelectUser");
			dictionary.Add(text7, (transform7 != null) ? transform7.gameObject : null);
			string text8 = "Button_Safety";
			Transform transform8 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_Safety");
			dictionary.Add(text8, (transform8 != null) ? transform8.gameObject : null);
			DashOverride.qmButtons = dictionary;
			DashOverride.headerText = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
			DashOverride.headerText.richText = true;
			DashOverride.headerText.text = "<color=#c00000>EXO</color>";
			DashOverride.headerText.transform.localPosition = new Vector3(55f, 0f, 0f);
			Transform transform9 = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks");
			if (transform9 != null)
			{
				transform9.gameObject.SetActive(false);
			}
			Transform transform10 = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions");
			if (transform10 != null)
			{
				transform10.gameObject.SetActive(false);
			}
			Delegate onQuickMenuOpen = QuickMenuPatch.OnQuickMenuOpen;
			Action action;
			if ((action = DashOverride.<>O.<0>__FormateDash) == null)
			{
				action = (DashOverride.<>O.<0>__FormateDash = new Action(DashOverride.FormateDash));
			}
			QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(onQuickMenuOpen, action);
			Delegate onQuickMenuClose = QuickMenuPatch.OnQuickMenuClose;
			Action action2;
			if ((action2 = DashOverride.<>O.<0>__FormateDash) == null)
			{
				action2 = (DashOverride.<>O.<0>__FormateDash = new Action(DashOverride.FormateDash));
			}
			QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(onQuickMenuClose, action2);
			DashOverride.FormateDash();
			MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup").GetComponent<VerticalLayoutGroup>().enabled = false;
			if (QMConsole.logObject != null)
			{
				QMConsole.logObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
			}
			Transform transform11 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons);
			if (transform11 != null)
			{
				transform11.localPosition = new Vector3(0f, -150f, 0f);
			}
			Transform transform12 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons);
			if (transform12 != null)
			{
				transform12.localPosition = new Vector3(0f, -50f, 0f);
			}
			foreach (GameObject gameObject in DashOverride.qmButtons.Values)
			{
				if (!(gameObject == null))
				{
					DashOverride.SetButtonProperties(gameObject);
				}
			}
		}
	}
```

More of my old QM Stuff
```text
private static void SetButtonProperties(GameObject button)
	{
		Transform transform = button.transform.Find("Background");
		if (transform != null)
		{
			transform.transform.localScale = new Vector3(1f, 0.5f, 1f);
		}
		foreach (TextMeshProUGUI textMeshProUGUI in button.GetComponentsInChildren<TextMeshProUGUI>())
		{
			textMeshProUGUI.enableAutoSizing = false;
			textMeshProUGUI.transform.localScale = Vector3.one;
		}
		Transform transform2 = button.transform.Find("TextLayoutParent/Text_H4");
		if (transform2 != null)
		{
			TextMeshProUGUI component = transform2.GetComponent<TextMeshProUGUI>();
			if (component != null)
			{
				component.enableAutoSizing = false;
				component.rectTransform.localScale = Vector3.one;
				component.rectTransform.sizeDelta = new Vector2(200f, 50f);
				TextMeshProUGUI textMeshProUGUI2 = component;
				string name = button.name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				string text;
				if (num <= 2034525333U)
				{
					if (num <= 548756159U)
					{
						if (num != 112140175U)
						{
							if (num == 548756159U)
							{
								if (name == "Button_Worlds")
								{
									text = "World";
									goto IL_0248;
								}
							}
						}
						else if (name == "Button_GoHome")
						{
							text = "Home";
							goto IL_0248;
						}
					}
					else if (num != 860866672U)
					{
						if (num == 2034525333U)
						{
							if (name == "Button_SelectUser")
							{
								text = "Select";
								goto IL_0248;
							}
						}
					}
					else if (name == "Button_Safety")
					{
						text = "Safety";
						goto IL_0248;
					}
				}
				else if (num <= 2424950392U)
				{
					if (num != 2066866337U)
					{
						if (num == 2424950392U)
						{
							if (name == "Button_Avatars")
							{
								text = "Avatar";
								goto IL_0248;
							}
						}
					}
					else if (name == "Button_Social")
					{
						text = "Social";
						goto IL_0248;
					}
				}
				else if (num != 2566887506U)
				{
					if (num == 4200933131U)
					{
						if (name == "Button_ViewGroups")
						{
							text = "Group";
							goto IL_0248;
						}
					}
				}
				else if (name == "Button_Respawn")
				{
					text = "Respawn";
					goto IL_0248;
				}
				text = "die";
				IL_0248:
				textMeshProUGUI2.text = text;
			}
		}
		Transform transform3 = button.transform.Find("Badge_Close");
		if (transform3 != null)
		{
			transform3.gameObject.SetActive(true);
			transform3.localPosition = new Vector3(85f, 35f, 0f);
			Image component2 = transform3.GetComponent<Image>();
			if (component2 != null)
			{
				component2.color = Color.white;
				component2.overrideSprite = button.transform.Find("Icons/Icon").GetComponent<Image>().sprite;
				transform3.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
			}
		}
		Transform transform4 = button.transform.Find("Badge_MMJump");
		if (transform4 != null)
		{
			transform4.gameObject.SetActive(false);
		}
		Transform transform5 = button.transform.Find("Icons");
		if (transform5 != null)
		{
			transform5.gameObject.SetActive(false);
		}
	}

	internal static void FormateDash()
	{
		if (QMConsole.logObject != null)
		{
			QMConsole.logObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
		}
		DashOverride.headerText.transform.localPosition = new Vector3(55f, 0f, 0f);
		foreach (GameObject gameObject in DashOverride.qmButtons.Values)
		{
			if (!(gameObject == null))
			{
				Transform transform = gameObject.transform;
				string name = gameObject.name;
				Vector3 vector;
				if (!(name == "Button_Avatars"))
				{
					if (!(name == "Button_Respawn"))
					{
						if (!(name == "Button_Social"))
						{
							if (!(name == "Button_SelectUser"))
							{
								if (!(name == "Button_ViewGroups"))
								{
									if (!(name == "Button_Safety"))
									{
										vector = gameObject.transform.localPosition;
									}
									else
									{
										vector = new Vector3(-348f, -600f, 0f);
									}
								}
								else
								{
									vector = new Vector3(-348f, -600f, 0f);
								}
							}
							else
							{
								vector = new Vector3(-348f, -400f, 0f);
							}
						}
						else
						{
							vector = new Vector3(-348f, -400f, 0f);
						}
					}
					else
					{
						vector = new Vector3(-348f, -200f, 0f);
					}
				}
				else
				{
					vector = new Vector3(-348f, -200f, 0f);
				}
				transform.localPosition = vector;
			}
		}
	}
```

Wcv2 Antitheft
```text
	public override void OnUpdate()
	{
		if (AntiTheft.Enabled || AntiTheft._desktopHolding)
		{
			bool flag;
			if (WorldWrapper.In_World)
			{
		
			}
			else
			{
		
			}
			if (!flag)
			{		
				
			
				}
			}
		}
	}
```

There is alot more Stolen/Skided Code within this paste, But the main funny part is the amount of code made from chatgpt and it's obv it is most of the this kind of code is within
- All the Utility's, And note there is alot of repeated methods in just differently named utility classes
- More WCV2 tooken things, Serialization functions, Avatar functions, All patch's, All Wrappers, WC's Utility methods, Json Paser, WC Nameplate base, WC Obj data classes, WC Popup/Notif, Most Esp Functions

Basically what i got at in there is that for the most part the only original code is just the udon functions and i don't mean how he sends them either lmao, Everything else is mainly stolen from the following WCV2, NOC, GITHUB, and the most funniest one full code blocks and functions from chatgpt, So if you ever wanted to wonder why exo is so unoptimz and has multp mem leaks and issues well now you know !!!

# Now onto the point why i posted this

Let's get the story straight, Because Cyconi has decided to grow a massive ego felt like spreading a shit ton of misinfo to multip people which spreaded to more people

1. No i did leak Cyconi's Paste, HWID, IP or anything else but he has decided to tell people i helped do and leaked to people even when he was told i didn't and was shown, I did infact say after he started this petty fight and after star reversed it himself, I dumped it after his server update and didn't hide i did that ? but he took my words and changed them to fit his own agenda But you can see what i said in the WCV2 server as it's were i said it !!

2. This petty drama start because cyconi had a E boy ego because a girl i was hanging out with didn't like/Was close to him and he decides to Go behind my back spread goofyness


[Meow](https://media.discordapp.net/attachments/1303622326386032640/1303622374750687312/68747470733a2f2f6d656469612e646973636f72646170702e6e65742f6174746163686d656e74732f313238353034393238323335363834323537302f313238353131323036363736333635333137342f494d475f373537342e706e673f65783d3637326230303630266.webp?ex=672c6c72&is=672b1af2&hm=6f25f5bc63406724b9d967c3b7f9e1ce53b30a9cf667da47a5aeb92b432adcfe&=&format=webp&width=1267&height=285)
[Meow](https://media.discordapp.net/attachments/1303622326386032640/1303622375211794483/68747470733a2f2f6d656469612e646973636f72646170702e6e65742f6174746163686d656e74732f313238353034393238323335363834323537302f313238353131323036353539333138303139302f494d475f373537392e706e673f65783d3637326230303630266.webp?ex=672c6c72&is=672b1af2&hm=6b4784c76c8ec7be9e1c6f7cf0b9470386ade13db6834cbeba7f36e3f9100899&=&format=webp&width=1267&height=366)

There's more goofy dm ss's like that but they have the other persons name in them sho i'll just leave it at that

3. So when we called him out in a about 1 hour ish long vc with me and 2 other's, and just sate this he back tracked alot or pulled the idk card on us, This is when he leave's and kicks us out his server's ect start this goofy ahhhh hate train to fuel his ego, if you want to learn more about this just dm me lmao i'll explain and show more than a 1 sided story like he's been doing

Oh and i wonder how much his ego hurts when the girl's he's been targeting call him a creep and a werido for trying to start crap and for hitting on them after a day of knowing them XD, Also keep in mind some of his fav female friends to hang out with are below the age of 16 !!!!! Not saying anything but those dms we saw for a few sec's while in that vc where kinda telling. One more note 9 girl's under 4 months is crazy Ight Peace vrchatians


# Extra Notes

1. The User's database to exo was hosted on a public github repo https://gist.github.com/Cyconi/be98f0ddc0c3ede11fd2083e332070df , Which was stored in this format DISCORDID : HWID : IP : SUBTIME

2. Cyconi was told by me and other's to update his server/security/User privacy stuff but he decided not to because he was in his own words "To lazy", So if this is the person you want to store your userdata/prevent you from getting enjoy that :SkullEmoji:

3. Cyconi SS the girl's Address and saved them to his pc and to google maps

4. Cyconi Logged me, one of those girls, and three other friends IP's while we were all in VR chilling and started Saying oh this is were you live ect

5. Cyconi when he broke up with his one ex during WCV1 Peak, He dm mutipl to bully his now ex I.E to say hate fullshit like kys ect/ Double note he enjoy the Age Regression kink and this can be proven? How many times did yall erp again because you wanted to ? that 3y age diff goes hard sir almost 20 !!

6. Cyconi Trying to hit me off with my own silly cats

[Meow](https://media.discordapp.net/attachments/1303622326386032640/1303622374184321105/68747470733a2f2f6d656469612e646973636f72646170702e6e65742f6174746163686d656e74732f313136383238333330313437363331353138372f313330333239303633373931303534303331392f696d6167652e706e673f65783d36373262333737652669733d3.webp?ex=672c6c72&is=672b1af2&hm=700429daa18782e9ebac063992d36c3dbcbde1316e2feff13746ed0cdb5bd54d&=&format=webp)

7. Sounds clips of cyconi spreading miss info to one of the girl's, I wonder if he can back a name to the "Girls" i hit on
- https://cdn.discordapp.com/attachments/1284965312797741160/1284965574648004709/Record_2024-09-15_at_02h45m48s.wav?ex=672b20b2&is=6729cf32&hm=9e84fd76c1b1237fade47b900cdb6b02b959f991c1d18ccd0e429397c8bfd5ce&

8. Cyconi Spreading more mis info, And a very funny sound byte with his fav little female minor !!!, And keep in mind Cyconi being the "Nice person" ect is him asking way to personal questions, Being creepy af, and Spreading miss info about me which is so easy to disprove because i do the same thing on the daily with guy's i chill with !!
- https://cdn.discordapp.com/attachments/1168283301476315187/1303291899498463253/Record_2024-09-15_at_02h29m53s.wav?ex=672b38aa&is=6729e72a&hm=1b5d299e516677a0d5ed878f27cd2ee5c448e1492809098caf42e86c76b6691a&
