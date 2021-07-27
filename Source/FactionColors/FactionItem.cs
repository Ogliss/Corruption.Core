using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200001D RID: 29
	public class FactionItem : ThingWithComps
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000044C4 File Offset: 0x000026C4
		private Pawn Wearer
		{
			get
			{
				CompEquippable compEquippable = this.TryGetCompFast<CompEquippable>();
				bool flag = compEquippable != null;
				Pawn result;
				if (flag)
				{
					Pawn casterPawn = compEquippable.PrimaryVerb.CasterPawn;
					result = casterPawn;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000044F8 File Offset: 0x000026F8
		private FactionDefUniform udef
		{
			get
			{
				bool flag = this.Wearer != null;
				FactionDefUniform result;
				if (flag)
				{
					result = (this.Wearer.Faction.def as FactionDefUniform);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004534 File Offset: 0x00002734
		private CompFactionColor compF
		{
			get
			{
				return base.GetComp<CompFactionColor>();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000454C File Offset: 0x0000274C
		public override Graphic Graphic
		{
			get
			{
				return GraphicDatabase.Get<Graphic_Single>(this.def.graphicData.texPath, ShaderDatabase.CutoutComplex, this.def.graphicData.drawSize, this.DrawColor, this.DrawColorTwo);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000024FC File Offset: 0x000006FC
		public override Color DrawColor
		{
			get
			{
				bool flag = !this.firstResolved;
				if (flag)
				{
					bool flag2 = this.Wearer != null;
					if (flag2)
					{
						bool flag3 = this.compF != null;
						if (flag3)
						{
							FactionColorEntry factionColorEntry;
							bool colorEntry = FactionColorUtilities.currentFactionColorTracker.GetColorEntry(this.Wearer.Faction, out factionColorEntry);
							if (colorEntry)
							{
								this.Col1 = factionColorEntry.FactionColor1;
							}
						}
						else
						{
							this.Col1 = this.def.graphicData.color;
						}
					}
					else
					{
						CompColorable comp = base.GetComp<CompColorable>();
						bool flag4 = comp != null && comp.Active;
						if (flag4)
						{
							this.Col1 = comp.Color;
						}
						else
						{
							bool flag5 = base.Stuff != null;
							if (flag5)
							{
								this.Col1 = base.Stuff.stuffProps.color;
							}
							else
							{
								bool flag6 = this.def.graphicData != null;
								if (flag6)
								{
									this.Col1 = this.def.graphicData.color;
								}
							}
						}
					}
					bool flag7 = this.compF != null && this.compF.CProps.UseCamouflageColor;
					if (flag7)
					{
						this.Col1 = CamouflageColorsUtility.CamouflageColors[0];
					}
					this.firstResolved = true;
				}
				return this.Col1;
			}
			set
			{
				this.SetColor(value, true);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000046E9 File Offset: 0x000028E9
		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000046F8 File Offset: 0x000028F8
		public override Color DrawColorTwo
		{
			get
			{
				bool flag = !this.secResolved;
				if (flag)
				{
					bool flag2 = this.Wearer != null;
					if (flag2)
					{
						bool flag3 = this.compF != null;
						if (flag3)
						{
							FactionColorEntry factionColorEntry;
							bool colorEntry = FactionColorUtilities.currentFactionColorTracker.GetColorEntry(this.Wearer.Faction, out factionColorEntry);
							if (colorEntry)
							{
								this.Col2 = factionColorEntry.FactionColor2;
							}
						}
						else
						{
							this.Col2 = this.def.graphicData.colorTwo;
						}
					}
					else
					{
						CompColorable comp = base.GetComp<CompColorable>();
						bool flag4 = comp != null && comp.Active;
						if (flag4)
						{
							this.Col2 = comp.Color;
						}
						else
						{
							this.Col2 = this.def.graphicData.colorTwo;
						}
					}
					this.secResolved = true;
				}
				return this.Col2;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000047D8 File Offset: 0x000029D8
		public override void Draw()
		{
			base.Draw();
			Vector3 s = new Vector3(this.def.graphicData.drawSize.x, 1f, this.def.graphicData.drawSize.y);
			Matrix4x4 matrix = default(Matrix4x4);
			matrix.SetTRS(this.DrawPos, Quaternion.AngleAxis(0f, Vector3.up), s);
			Graphics.DrawMesh(MeshPool.plane10, matrix, this.Graphic.MatSingle, 0);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004864 File Offset: 0x00002A64
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<bool>(ref this.secResolved, "secResolved", false, false);
			Scribe_Values.Look<bool>(ref this.firstResolved, "firstResolved", false, false);
			Scribe_Values.Look<Color>(ref this.Col1, "col1", Color.white, false);
			Scribe_Values.Look<Color>(ref this.Col2, "col2", Color.white, false);
		}

		// Token: 0x04000052 RID: 82
		public bool FirstSpawned = true;

		// Token: 0x04000053 RID: 83
		private bool firstResolved;

		// Token: 0x04000054 RID: 84
		private bool secResolved;

		// Token: 0x04000055 RID: 85
		public Color Col1 = Color.magenta;

		// Token: 0x04000056 RID: 86
		public Color Col2 = Color.grey;

		// Token: 0x04000057 RID: 87
		public Graphic Detail;
	}
}
