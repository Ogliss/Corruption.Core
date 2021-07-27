using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200001A RID: 26
	public class FactionColorEntry : IExposable
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003E74 File Offset: 0x00002074
		public void ExposeData()
		{
			Scribe_References.Look<Faction>(ref this.Faction, "Faction", false);
			Scribe_Values.Look<Color>(ref this.FactionColor1, "MainColor", default(Color), false);
			Scribe_Values.Look<Color>(ref this.FactionColor2, "SecColor", default(Color), false);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003ECA File Offset: 0x000020CA
		public FactionColorEntry()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003ED4 File Offset: 0x000020D4
		public FactionColorEntry(Faction faction, Color main, Color second)
		{
			this.Faction = faction;
			this.FactionColor1 = main;
			this.FactionColor2 = second;
		}

		// Token: 0x0400004A RID: 74
		public Faction Faction;

		// Token: 0x0400004B RID: 75
		public Color FactionColor1;

		// Token: 0x0400004C RID: 76
		public Color FactionColor2;
	}
}
