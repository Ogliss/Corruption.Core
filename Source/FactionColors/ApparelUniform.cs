using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000004 RID: 4
	public class ApparelUniform : Apparel
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002320 File Offset: 0x00000520
		private FactionDefUniform udef
		{
			get
			{
				return base.Wearer.Faction.def as FactionDefUniform;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002348 File Offset: 0x00000548
		private CompFactionColor compF
		{
			get
			{
				return base.GetComp<CompFactionColor>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002360 File Offset: 0x00000560
		public override Graphic Graphic
		{
			get
			{
				bool isRandomMultiGraphic = this.compF.CProps.IsRandomMultiGraphic;
				Graphic result;
				if (isRandomMultiGraphic)
				{
					string path = this.def.apparel.wornGraphicPath + "/" + this.compF.randomGraphicPath;
					result = GraphicDatabase.Get<Graphic_Single>(path, ShaderDatabase.CutoutComplex, this.def.graphicData.drawSize, this.Col1, this.Col2);
				}
				else
				{
					result = GraphicDatabase.Get<Graphic_Single>(this.def.graphicData.texPath, ShaderDatabase.CutoutComplex, this.def.graphicData.drawSize, this.Col1, this.Col2);
				}
				return result;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000024FC File Offset: 0x000006FC
		public override Color DrawColor
		{
			get
			{
				bool firstSpawned = this.FirstSpawned;
				if (firstSpawned)
				{
					CompFactionColor comp = base.GetComp<CompFactionColor>();
					bool flag = comp == null;
					if (flag)
					{
						Log.Error("Uniform Apparel " + this.ToString() + " is missing a CompFactionColors", false);
					}
					bool flag2 = base.Wearer != null;
					if (flag2)
					{
						FactionColorEntry factionColorEntry;
						bool colorEntry = FactionColorUtilities.currentFactionColorTracker.GetColorEntry(base.Wearer.Faction, out factionColorEntry);
						if (colorEntry)
						{
							this.Col1 = factionColorEntry.FactionColor1;
						}
					}
					else
					{
						CompColorable comp2 = base.GetComp<CompColorable>();
						bool flag3 = comp2 != null && comp2.Active;
						if (flag3)
						{
							this.Col1 = comp2.Color;
						}
					}
					bool flag4 = comp != null && comp.CProps.UseCamouflageColor;
					if (flag4)
					{
						this.Col1 = CamouflageColorsUtility.CamouflageColors[0];
					}
				}
				return this.Col1;
			}
			set
			{
				this.SetColor(value, true);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002508 File Offset: 0x00000708
		public override Color DrawColorTwo
		{
			get
			{
				bool firstSpawned = this.FirstSpawned;
				if (firstSpawned)
				{
					CompFactionColor comp = base.GetComp<CompFactionColor>();
					bool flag = base.Wearer != null;
					if (flag)
					{
						FactionColorEntry factionColorEntry;
						bool colorEntry = FactionColorUtilities.currentFactionColorTracker.GetColorEntry(base.Wearer.Faction, out factionColorEntry);
						if (colorEntry)
						{
							this.Col2 = factionColorEntry.FactionColor2;
						}
					}
					else
					{
						CompColorable comp2 = base.GetComp<CompColorable>();
						bool flag2 = comp2 != null && comp2.Active;
						if (flag2)
						{
							this.Col2 = comp2.Color;
						}
					}
					bool flag3 = comp != null && comp.CProps.UseCamouflageColor;
					if (flag3)
					{
						this.Col2 = CamouflageColorsUtility.CamouflageColors[1];
					}
				}
				return this.Col2;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025CC File Offset: 0x000007CC
		private void SetFactionColor(ref Color color, CompFactionColor compF)
		{
			bool flag = base.Wearer != null;
			if (flag)
			{
				bool flag2 = this.udef != null;
				if (flag2)
				{
					bool flag3 = compF != null && compF.CProps.UseCamouflageColor;
					if (flag3)
					{
						color = CamouflageColorsUtility.CamouflageColors[1];
					}
					else
					{
						color = this.udef.FactionColor2;
					}
				}
			}
			else
			{
				CompColorable comp = base.GetComp<CompColorable>();
				bool flag4 = comp != null && comp.Active;
				if (flag4)
				{
					color = comp.Color;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002660 File Offset: 0x00000860
		public override void PostMake()
		{
			base.PostMake();
			bool isRandomMultiGraphic = this.compF.CProps.IsRandomMultiGraphic;
			if (isRandomMultiGraphic)
			{
				this.compF.ResolveRandomGraphics();
			}
			FactionColorsTracker currentFactionColorTracker = FactionColorUtilities.currentFactionColorTracker;
			bool flag = currentFactionColorTracker != null;
			if (flag)
			{
				this.Col1 = currentFactionColorTracker.PlayerColorOne;
				this.Col2 = currentFactionColorTracker.PlayerColorTwo;
				bool flag2 = this.compF == null;
				if (flag2)
				{
					Log.Error("Uniform Apparel " + this.ToString() + " is missing a CompFactionColors", false);
				}
				bool flag3 = this.compF != null && this.compF.CProps.UseCamouflageColor;
				if (flag3)
				{
					this.Col1 = CamouflageColorsUtility.CamouflageColors[0];
					this.Col2 = CamouflageColorsUtility.CamouflageColors[1];
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002732 File Offset: 0x00000932
		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			this.FirstSpawned = false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002748 File Offset: 0x00000948
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<bool>(ref this.FirstSpawned, "FirstSpawned", false, false);
			Scribe_Values.Look<Color>(ref this.Col1, "Col1", Color.white, false);
			Scribe_Values.Look<Color>(ref this.Col2, "Col2", Color.white, false);
		}

		// Token: 0x04000002 RID: 2
		public bool FirstSpawned = true;

		// Token: 0x04000003 RID: 3
		public Color Col1 = Color.grey;

		// Token: 0x04000004 RID: 4
		public Color Col2 = Color.black;

		// Token: 0x04000005 RID: 5
		public Graphic Detail;
	}
}
