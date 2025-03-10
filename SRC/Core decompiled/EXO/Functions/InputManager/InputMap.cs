using System;
using EXO.LogTools;
using UnityEngine;

namespace EXO.Functions.InputManager
{
	// Token: 0x020000A7 RID: 167
	internal class InputMap
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x000231C0 File Offset: 0x000213C0
		internal static void WasPressedVoid(Action action, KeyCode button1 = KeyCode.None, KeyCode button2 = KeyCode.None, bool condition = true)
		{
			InputMap.isProcessedThisFrame = false;
			bool flag = button2 == KeyCode.None && button1 != KeyCode.None && Input.GetKey(button1) && condition;
			if (flag)
			{
				action.Invoke();
				InputMap.isProcessedThisFrame = true;
			}
			else
			{
				bool flag2 = button1 == KeyCode.None && button2 != KeyCode.None && Input.GetKey(button2) && condition;
				if (flag2)
				{
					action.Invoke();
					InputMap.isProcessedThisFrame = true;
				}
				else
				{
					bool flag3 = button1 != KeyCode.None && button2 != KeyCode.None && Input.GetKey(button1) && Input.GetKey(button2) && condition;
					if (flag3)
					{
						action.Invoke();
						InputMap.isProcessedThisFrame = true;
					}
					else
					{
						bool flag4 = button1 != KeyCode.None && button2 != KeyCode.None && Input.GetKey(button2) && Input.GetKey(button1) && condition;
						if (flag4)
						{
							action.Invoke();
							InputMap.isProcessedThisFrame = true;
						}
					}
				}
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00023280 File Offset: 0x00021480
		internal static bool WasPressed(KeyCode button1 = KeyCode.None, KeyCode button2 = KeyCode.None)
		{
			InputMap.isProcessedThisFrame = false;
			bool flag = button2 == KeyCode.None && button1 != KeyCode.None && Input.GetKeyDown(button1);
			if (flag)
			{
				InputMap.isProcessedThisFrame = true;
			}
			else
			{
				bool flag2 = button1 == KeyCode.None && button2 != KeyCode.None && Input.GetKeyDown(button2);
				if (flag2)
				{
					InputMap.isProcessedThisFrame = true;
				}
				else
				{
					bool flag3 = button1 != KeyCode.None && button2 != KeyCode.None && Input.GetKey(button1) && Input.GetKeyDown(button2);
					if (flag3)
					{
						InputMap.isProcessedThisFrame = true;
					}
					else
					{
						bool flag4 = button1 != KeyCode.None && button2 != KeyCode.None && Input.GetKey(button2) && Input.GetKeyDown(button1);
						if (flag4)
						{
							InputMap.isProcessedThisFrame = true;
						}
					}
				}
			}
			return InputMap.isProcessedThisFrame;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002331C File Offset: 0x0002151C
		internal static bool IsPressed(KeyCode button1 = KeyCode.None, KeyCode button2 = KeyCode.None)
		{
			bool flag = button1 != KeyCode.None && Input.GetKey(button1);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = button2 != KeyCode.None && Input.GetKey(button2);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = button1 != KeyCode.None && button2 != KeyCode.None && Input.GetKey(button1) && Input.GetKey(button2);
					flag2 = flag4;
				}
			}
			return flag2;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00023378 File Offset: 0x00021578
		public static KeyCode GetKeyCode(string keyName)
		{
			bool flag = string.IsNullOrEmpty(keyName);
			KeyCode keyCode;
			if (flag)
			{
				keyCode = KeyCode.None;
			}
			else
			{
				keyName = keyName.ToUpper();
				foreach (object obj in Enum.GetValues(typeof(KeyCode)))
				{
					KeyCode code = (KeyCode)obj;
					bool flag2 = code.ToString().ToUpper() == keyName;
					if (flag2)
					{
						return code;
					}
				}
				keyCode = KeyCode.None;
			}
			return keyCode;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00023418 File Offset: 0x00021618
		internal static void LogAnyKeyPressed()
		{
			foreach (object obj in Enum.GetValues(typeof(KeyCode)))
			{
				KeyCode keyCode = (KeyCode)obj;
				bool keyDown = Input.GetKeyDown(keyCode);
				if (keyDown)
				{
					CLog.L("Key Pressed: " + keyCode.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\InputMap.cs", 105);
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000234A8 File Offset: 0x000216A8
		internal static bool DoublePress(float interval = 0.5f)
		{
			bool flag = VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Boolean_0;
			bool flag3;
			if (flag)
			{
				bool flag2 = Time.time - InputMap.lastClickTime <= interval;
				if (flag2)
				{
					flag3 = true;
				}
				else
				{
					InputMap.lastClickTime = Time.time;
					flag3 = false;
				}
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000234F8 File Offset: 0x000216F8
		internal static bool DoublePress(KeyCode button, float interval = 0.5f)
		{
			bool keyDown = Input.GetKeyDown(button);
			bool flag2;
			if (keyDown)
			{
				bool flag = Time.time - InputMap._lastClickTime <= interval;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					InputMap._lastClickTime = Time.time;
					flag2 = false;
				}
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x040002F7 RID: 759
		private static bool isProcessedThisFrame;

		// Token: 0x040002F8 RID: 760
		private static float lastClickTime;

		// Token: 0x040002F9 RID: 761
		private static float _lastClickTime;
	}
}
