using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200002B RID: 43
	public class FactionColorsTracker : WorldComponent
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00005528 File Offset: 0x00003728
		public FactionColorsTracker(World world) : base(world)
		{
			this.PlayerColorOne = Color.red;
			this.PlayerColorTwo = Color.black;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005578 File Offset: 0x00003778
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<Color>(ref this.PlayerColorOne, "PlayerColorOne", Color.red, false);
			Scribe_Values.Look<Color>(ref this.PlayerColorTwo, "PlayerColorTwo", Color.red, false);
			Scribe_Values.Look<string>(ref this.BannerGraphicPath, "BannerGraphicPath", "UI/Flags/Plain", false);
			Scribe_Collections.Look<FactionColorEntry>(ref this.FactionColorList, "FactionColorList", LookMode.Deep, Array.Empty<object>());
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000055E9 File Offset: 0x000037E9
		public override void FinalizeInit()
		{
			base.FinalizeInit();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000055F4 File Offset: 0x000037F4
		public void InitalizeFactions()
		{
			foreach (Faction faction in Find.World.factionManager.AllFactions)
			{
				this.AddColorEntry(faction);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005650 File Offset: 0x00003850
		public void AddColorEntry(Faction faction)
		{
			FactionDefUniform factionDefUniform = faction.def as FactionDefUniform;
			bool flag = factionDefUniform != null && !this.FactionColorList.Any((FactionColorEntry x) => x.Faction == faction);
			if (flag)
			{
				this.FactionColorList.Add(new FactionColorEntry(faction, factionDefUniform.FactionColor1, factionDefUniform.FactionColor2));
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000056C8 File Offset: 0x000038C8
		public bool GetColorEntry(Faction faction, out FactionColorEntry entry)
		{
			bool flag = this.FactionColorList.Any((FactionColorEntry x) => x.Faction == faction);
			bool result;
			if (flag)
			{
				entry = this.FactionColorList.FirstOrDefault((FactionColorEntry x) => x.Faction == faction);
				result = true;
			}
			else
			{
				entry = new FactionColorEntry();
				result = false;
			}
			return result;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000572C File Offset: 0x0000392C
		public List<string> BannerOptions
		{
			get
			{
				IEnumerable<Texture2D> allInFolder = ContentFinder<Texture2D>.GetAllInFolder("UI/Flags");
				List<string> list = new List<string>();
				foreach (Texture2D texture2D in allInFolder)
				{
					string name = texture2D.name;
					bool flag = !name.Contains("_m");
					if (flag)
					{
						list.Add(name);
					}
				}
				return list;
			}
		}

		// Token: 0x0400006B RID: 107
		public Color PlayerColorOne;

		// Token: 0x0400006C RID: 108
		public Color PlayerColorTwo;

		// Token: 0x0400006D RID: 109
		public string BannerGraphicPath = "UI/Flags/Plain";

		// Token: 0x0400006E RID: 110
		public Dictionary<Faction, string> FactionGraphicPaths = new Dictionary<Faction, string>();

		// Token: 0x0400006F RID: 111
		public List<FactionColorEntry> FactionColorList = new List<FactionColorEntry>();
	}
}
