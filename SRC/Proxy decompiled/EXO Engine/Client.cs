using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreRuntime.Interfaces;
using CoreRuntime.Manager;

namespace EXO_Engine
{
	// Token: 0x02000003 RID: 3
	[NullableContext(1)]
	[Nullable(0)]
	public class Client : HexedCheat
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002145 File Offset: 0x00000345
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000214C File Offset: 0x0000034C
		internal static DirectoryInfo HexDir { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		public override async void OnLoad(string[] args)
		{
			Client.HexDir = new FileInfo(base.Path).Directory.Parent;
			GetDependencies.OnLoad();
			ConsoleApp.Launch();
			Cleaner.EACacheClean();
			Cleaner.RemoveSteamAPI();
			DiscordClient.Init();
			MonoManager.PatchUpdate(typeof(VRCApplication).GetMethod("Update"));
			MonoManager.PatchOnApplicationQuit(typeof(VRCApplicationSetup).GetMethod("OnApplicationQuit"));
			Client.<OnLoad>g__WaitForId|11_0();
			await Client.Init(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002194 File Offset: 0x00000394
		public override void OnApplicationQuit()
		{
			Client.OnClose();
			ConsoleApp.OnClose();
			try
			{
				Client.applicationQuit.Invoke(Client.instance, null);
			}
			catch
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D8 File Offset: 0x000003D8
		public override void OnUpdate()
		{
			try
			{
				Client.update.Invoke(Client.instance, null);
			}
			catch
			{
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		private static async Task Init(HexedCheat hexInstance)
		{
			Client.client = new TcpClient();
			try
			{
				await Client.client.ConnectAsync("135.181.205.15", 64209);
				CLog.L("Connecting...", ConsoleColor.White, ConsoleColor.DarkRed);
				Client.stream = Client.client.GetStream();
				Client.read = new StreamReader(Client.stream, Encoding.UTF8);
				Client.write = new StreamWriter(Client.stream, Encoding.UTF8)
				{
					AutoFlush = true
				};
				try
				{
					TextWriter textWriter = Client.write;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
					defaultInterpolatedStringHandler.AppendLiteral("DataID:");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(DiscordClient.DiscordAccount.ID);
					defaultInterpolatedStringHandler.AppendLiteral(":");
					defaultInterpolatedStringHandler.AppendFormatted(GetHardware.GetID());
					await textWriter.WriteLineAsync(defaultInterpolatedStringHandler.ToStringAndClear());



                    // DataID:964947247286616074:178BFBFF00A50F00-BR80G9007800908-PCI\VEN_10DE&DEV_2484&SUBSYS_88D9103C&REV_A1\4&1C5D7D4&0&0009-21462786
                }
                catch (Exception ex)
				{
					CLog.E("Data Exception: ", ex);
				}
				CLog.L("Connected", ConsoleColor.White, ConsoleColor.DarkRed);
				string text = await Client.read.ReadLineAsync();
				string incomingData = text;
				text = null;
				if (string.IsNullOrEmpty(incomingData))
				{
					CLog.E("Data is Empty or Null!", ConsoleColor.Red, ConsoleColor.Blue);
				}
				if (incomingData.StartsWith("Authenticated"))
				{
					Msg.Authorized(DiscordClient.DiscordAccount.Username);
					await Client.write.WriteLineAsync("AssemblyLoader");
					string text2 = await Client.read.ReadLineAsync();
					incomingData = text2;
					text2 = null;
					if (incomingData.StartsWith("ASSEMBLY_BYTES:"))
					{
						string[] split = incomingData.Split(':', StringSplitOptions.None);
						byte[] dAssembly = Client.OnDecrypt(Convert.FromBase64String(split[1]), Convert.FromBase64String(split[2]), Convert.FromBase64String(split[3]));
						Assembly assembly = Assembly.Load(dAssembly);
						try
						{
							Type type = assembly.GetType("EXO.AppStart");
							Client.instance = Activator.CreateInstance(type);
							MethodInfo onLoad = type.GetMethod("CallOnLoad");
							onLoad.Invoke(Client.instance, new object[]
							{
								hexInstance,
								ConsoleApp.wasLaunched
							});
							CLog.L("Hooked OnLoad", ConsoleColor.White, ConsoleColor.DarkRed);
							Client.update = type.GetMethod("OnUpdate");
							CLog.L("Hooked OnUpdate", ConsoleColor.White, ConsoleColor.DarkRed);
							Client.applicationQuit = type.GetMethod("OnApplicationQuit");
							CLog.L("Hooked OnApplicationQuit", ConsoleColor.White, ConsoleColor.DarkRed);
							type = null;
							onLoad = null;
						}
						catch (Exception ex2)
						{
							CLog.E("Reflection Exception: ", ex2);
							Client.OnClose();
						}
						split = null;
						dAssembly = null;
						assembly = null;
					}
					else
					{
						CLog.E("Incorrect Data!", ConsoleColor.Red, ConsoleColor.Blue);
					}
					string text3 = await Client.read.ReadLineAsync();
					incomingData = text3;
					text3 = null;
					if (incomingData.StartsWith("Close"))
					{
						ConsoleApp.OnClose();
						Client.OnClose();
						Environment.Exit(0);
					}
					else
					{
						incomingData = null;
					}
				}
				else
				{
					Msg.NotAuthorized(incomingData);
					Client.OnClose();
				}
			}
			catch (Exception ex3)
			{
				CLog.E("Init Exception: ", ex3);
				Client.OnClose();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002254 File Offset: 0x00000454
		internal static void OnClose()
		{
			try
			{
				Client.write.Dispose();
			}
			catch (Exception ex)
			{
				CLog.E("StreamWriter Dispose Exception: ", ex);
			}
			try
			{
				Client.read.Dispose();
			}
			catch (Exception ex2)
			{
				CLog.E("StreamReader Dispose Exception: ", ex2);
			}
			try
			{
				Client.stream.Dispose();
			}
			catch (Exception ex3)
			{
				CLog.E("NetworkStream Dispose Exception: ", ex3);
			}
			try
			{
				Client.client.Close();
			}
			catch (Exception ex4)
			{
				CLog.E("Client Close Exception: ", ex4);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000231C File Offset: 0x0000051C
		public static byte[] OnDecrypt(byte[] bytes, byte[] key, byte[] iv)
		{
			byte[] array;
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = iv;
				using (MemoryStream memory = new MemoryStream())
				{
					using (CryptoStream crypto = new CryptoStream(memory, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						crypto.Write(bytes, 0, bytes.Length);
						crypto.FlushFinalBlock();
					}
					array = memory.ToArray();
				}
			}
			return array;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023CC File Offset: 0x000005CC
		[CompilerGenerated]
		internal static void <OnLoad>g__WaitForId|11_0()
		{
			while (DiscordClient.DiscordAccount == null || string.IsNullOrEmpty(DiscordClient.DiscordAccount.ID.ToString()))
			{
				Thread.Sleep(TimeSpan.FromSeconds(2.0));
			}
		}

		// Token: 0x04000001 RID: 1
		private static MethodInfo update;

		// Token: 0x04000002 RID: 2
		private static MethodInfo applicationQuit;

		// Token: 0x04000003 RID: 3
		private static object instance;

		// Token: 0x04000004 RID: 4
		private static TcpClient client;

		// Token: 0x04000005 RID: 5
		private static NetworkStream stream;

		// Token: 0x04000006 RID: 6
		private static StreamReader read;

		// Token: 0x04000007 RID: 7
		private static StreamWriter write;
	}
}
