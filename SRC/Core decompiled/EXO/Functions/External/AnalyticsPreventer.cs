using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace EXO.Functions.External
{
	// Token: 0x020000A9 RID: 169
	internal class AnalyticsPreventer
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00023CC0 File Offset: 0x00021EC0
		internal static void BlockAnalytics()
		{
			string HostsFile = Path.Combine(Environment.GetFolderPath(37), "drivers/etc/hosts");
			List<string> AllHostLines = Enumerable.ToList<string>(File.ReadAllLines(HostsFile));
			foreach (string url in AnalyticsPreventer.Blocklist)
			{
				bool IsExisting = false;
				foreach (string line in AllHostLines)
				{
					bool flag = line.Contains(url);
					if (flag)
					{
						IsExisting = true;
						break;
					}
				}
				bool flag2 = !IsExisting;
				if (flag2)
				{
					AllHostLines.Add("0.0.0.0 " + url);
				}
			}
			File.WriteAllLines(HostsFile, AllHostLines);
			foreach (string url2 in AnalyticsPreventer.Blocklist)
			{
				try
				{
					Uri uri = new Uri("http://" + url2 + "/");
					IPAddress ip = Dns.GetHostAddresses(uri.Host)[0];
				}
				catch
				{
				}
			}
		}

		// Token: 0x040002FB RID: 763
		private static readonly string[] Blocklist = new string[]
		{
			"amplitude.com", "api.amplitude.com", "api2.amplitude.com", "cdn.amplitude.com", "api.lab.amplitude.com", "api3.amplitude.com", "api.eu.amplitude.com", "analytics.amplitude.com", "analytics.eu.amplitude.com", "info.amplitude.com",
			"static.amplitude.com", "regionconfig.amplitude.com", "regionconfig.eu.amplitude.com", "o1125869.ingest.sentry.io", "api.uca.cloud.unity3d.com", "config.uca.cloud.unity3d.com", "cdp.cloud.unity3d.com", "data-optout-service.uca.cloud.unity3d.com", "perf-events.cloud.unity3d.com", "public.cloud.unity3d.com",
			"ecommerce.iap.unity3d.com", "remote-config-proxy-prd.uca.cloud.unity3d.com", "thind-gke-euw.prd.data.corp.unity3d.com", "thind-gke-usc.prd.data.corp.unity3d.com", "thind-gke-ape.prd.data.corp.unity3d.com", "stats.unity3d.com", "ads-game-configuration-master.ads.prd.ie.internal.unity3d.com", "ads-game-configuration.unityads.unity3d.com", "ads-privacy-api.prd.mz.internal.unity3d.com", "adserver.unityads.unity3d.com",
			"analytics.cloud.unity3d.com", "analytics.social.unity.com", "analytics.uca.cloud.unity3d.com", "analytics.unity3d.com", "api.uca.cloud.unity3d.com", "auction.unityads.unity3d.com", "config.unityads.unity3d.com", "events.iap.unity3d.com", "events.mz.unity3d.com", "geocdn.unityads.unity3d.com",
			"httpkafka.unityads.unity3d.com", "mediation-tracking.prd.mz.internal.unity3d.com", "publisher-event.ads.prd.ie.internal.unity3d.com", "tracking.prd.mz.internal.unity3d.com", "unityads.unity3d.com", "userreporting.cloud.unity3d.com", "webview.unityads.unity3d.com", "gamelogs.live.bhvrdbd.com", "rtm.live.dbd.bhvronline.com", "engage14352dbdmb.deltadna.net",
			"collect14352dbdmb.deltadna.net", "log-upload-os.mihoyo.com", "api.gameanalytics.com", "in.treasuredata.com", "api.redshell.io", "rubick.gameanalytics.com", "nelo2-col.nhncorp.jp", "galaxy-client-reports.gog.com", "insights-collector.gog.com", "gwent-bi-collector.gog.com",
			"stats.g.doubleclick.net", "adservice.google.com", "crash.steampowered.com", "logs-01.loggly.com", "5fs-crashify.s3-accelerate.amazonaws.com", "crashlogs.woniu.com", "crashlytics.com", "down.anticheatexpert.com", "log-upload-os.hoyoverse.com", "webstatic.hoyoverse.com",
			"minor-api-os.hoyoverse.com", "abtest-api-data-sg.hoyoverse.com", "sg-public-data-api.hoyoverse.com", "vortex.data.microsoft.com", "v10.events.data.microsoft.com", "settings-win.data.microsoft.com", "v10.vortex-win.data.microsoft.com", "v20.events.data.microsoft.com", "watson.telemetry.microsoft.com", "watson.events.data.microsoft.com",
			"web.vortex.data.microsoft.com", "mobile.events.data.microsoft.com", "cortana.ai", "api.cortana.ai", "issue.labymod.net", "gamelogs.rec.net", "datacollection.rec.net", "bugreporting.rec.net", "commerce.rec.net", "notify.bugsnag.com",
			"oculuscdn.com", "fbcdn.com", "fbsbx.com", "facebook-hardware.com", "cdn.optimizely.com", "analytics.xboxlive.com", "cdf-anon.xboxlive.com", "settings-ssl.xboxlive.com", "athenaprod.maelstrom.gameservices.xboxlive.com", "et.epicgames.com",
			"et2.epicgames.com", "udn.epicgames.com", "etsource.epicgames.com", "metrics.ol.epicgames.com", "datarouter.ol.epicgames.com"
		};
	}
}
