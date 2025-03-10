using System;
using EXO.Core;
using EXO.LogTools;
using UnityEngine.SceneManagement;

namespace EXO.Functions.Managers
{
	// Token: 0x020000A3 RID: 163
	internal class SceneLoadDetector : FunctionModule
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x000229DC File Offset: 0x00020BDC
		public override void OnUpdate()
		{
			Scene currentScene = SceneManager.GetActiveScene();
			bool flag = currentScene.isLoaded && currentScene.name != this.lastSceneName;
			if (flag)
			{
				CLog.D("Scene Loaded: " + currentScene.name, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Managers\\SceneLoadDetector.cs", 24);
				this.lastSceneName = currentScene.name;
				SceneLoadDetector.sceneChange = true;
			}
		}

		// Token: 0x040002E5 RID: 741
		private string lastSceneName = "";

		// Token: 0x040002E6 RID: 742
		internal static bool sceneChange;
	}
}
