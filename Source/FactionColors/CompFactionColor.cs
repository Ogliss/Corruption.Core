using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000C RID: 12
	public class CompFactionColor : ThingComp
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00003114 File Offset: 0x00001314
		public override void PostSpawnSetup(bool respawningAfterLoad)
		{
			base.PostSpawnSetup(respawningAfterLoad);
			bool flag = this.parent.GetType() == typeof(ApparelUniform);
			if (flag)
			{
				ApparelUniform apparelUniform = this.parent as ApparelUniform;
				apparelUniform.FirstSpawned = false;
			}
			bool useSecondaryColors = this.CProps.UseSecondaryColors;
			if (useSecondaryColors)
			{
				this.SecondaryColor = this.CProps.Coloring.NewRandomizedColor();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003184 File Offset: 0x00001384
		public void ResolveRandomGraphics()
		{
			bool flag = this.randomGraphicPath.NullOrEmpty();
			if (flag)
			{
				this.randomGraphicPath = this.CProps.RandomGraphicPaths.RandomElementByWeight((Pair<string, int> x) => (float)x.Second).First;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000031E0 File Offset: 0x000013E0
		public CompProperties_FactionColor CProps
		{
			get
			{
				return (CompProperties_FactionColor)this.props;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031FD File Offset: 0x000013FD
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<string>(ref this.randomGraphicPath, "randomGraphicPath", "", false);
			Scribe_Values.Look<Color>(ref this.SecondaryColor, "SecondaryColor", Color.white, false);
		}

		// Token: 0x0400001B RID: 27
		public string randomGraphicPath = "";

		// Token: 0x0400001C RID: 28
		public Color SecondaryColor = Color.white;
	}
}
