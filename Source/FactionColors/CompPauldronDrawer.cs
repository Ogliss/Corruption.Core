using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000F RID: 15
	public class CompPauldronDrawer : ThingComp
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000033B0 File Offset: 0x000015B0
		public Apparel apparel
		{
			get
			{
				return this.parent as Apparel;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000033D0 File Offset: 0x000015D0
		public Pawn pawn
		{
			get
			{
				return this.apparel.Wearer;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000033F0 File Offset: 0x000015F0
		public Color mainColor
		{
			get
			{
				bool flag = this.useSecondaryColor;
				Color result;
				if (flag)
				{
					result = this.parent.DrawColorTwo;
				}
				else
				{
					result = this.parent.DrawColor;
				}
				return result;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003428 File Offset: 0x00001628
		public Graphic PauldronGraphic
		{
			get
			{
				bool flag = this._pauldronGraphic == null;
				if (flag)
				{
					string path = this.graphicPath + "_" + this.pawn.story.bodyType.ToString();
					this._pauldronGraphic = GraphicDatabase.Get<Graphic_Multi>(path, this.shader, Vector2.one, this.mainColor, this.parent.DrawColorTwo);
				}
				return this._pauldronGraphic;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000034A0 File Offset: 0x000016A0
		public CompProperties_PauldronDrawer pprops
		{
			get
			{
				return this.props as CompProperties_PauldronDrawer;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000034C0 File Offset: 0x000016C0
		public static bool ShouldDrawPauldron(Pawn pawn, Apparel curr, Rot4 bodyFacing, out Material pauldronMaterial)
		{
			pauldronMaterial = null;
			bool result;
			try
			{
				bool flag = pawn.needs != null && pawn.story != null;
				if (flag)
				{
					CompPauldronDrawer compPauldronDrawer;
					bool flag2 = (compPauldronDrawer = curr.TryGetCompFast<CompPauldronDrawer>()) != null;
					if (flag2)
					{
						bool flag3 = !compPauldronDrawer.pauldronInitialized;
						if (flag3)
						{
							compPauldronDrawer.PostSpawnSetup(false);
						}
						bool flag4 = compPauldronDrawer.PauldronGraphic != null;
						if (flag4)
						{
							bool flag5 = compPauldronDrawer.CheckPauldronRotation(pawn, compPauldronDrawer.padType);
							if (flag5)
							{
								pauldronMaterial = compPauldronDrawer.PauldronGraphic.MatAt(bodyFacing, null);
								return true;
							}
						}
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000356C File Offset: 0x0000176C
		public bool CheckPauldronRotation(Pawn pawn, ShoulderPadType shoulderPadType)
		{
			bool flag = shoulderPadType == ShoulderPadType.Left && pawn.Rotation == Rot4.East;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = shoulderPadType == ShoulderPadType.Right && pawn.Rotation == Rot4.West;
				result = !flag2;
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035C0 File Offset: 0x000017C0
		public override void PostSpawnSetup(bool respawningAfterLoad)
		{
			base.PostSpawnSetup(respawningAfterLoad);
			ShoulderPadEntry shoulderPadEntry = this.pprops.PauldronEntries.RandomElementByWeight((ShoulderPadEntry x) => (float)x.commonality);
			this.graphicPath = shoulderPadEntry.padTexPath;
			bool flag = shoulderPadEntry.UseFactionTextures;
			if (flag)
			{
				string str = this.graphicPath;
				string str2 = "_";
				Pawn wearer = this.apparel.Wearer;
				this.graphicPath = str + str2 + ((wearer != null) ? wearer.Faction.Name : null);
			}
			this.shader = ShaderDatabase.LoadShader(shoulderPadEntry.shaderType.shaderPath);
			this.useSecondaryColor = shoulderPadEntry.UseSecondaryColor;
			this.padType = shoulderPadEntry.shoulderPadType;
			this.pauldronInitialized = true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003688 File Offset: 0x00001888
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<string>(ref this.graphicPath, "graphicPath", null, false);
			Scribe_Values.Look<ShoulderPadType>(ref this.padType, "padType", ShoulderPadType.Both, false);
			Scribe_Values.Look<bool>(ref this.useSecondaryColor, "useSecondaryColor", false, false);
		}

		// Token: 0x0400001D RID: 29
		public string graphicPath;

		// Token: 0x0400001E RID: 30
		public Shader shader = ShaderDatabase.Cutout;

		// Token: 0x0400001F RID: 31
		public ShoulderPadType padType;

		// Token: 0x04000020 RID: 32
		private bool useSecondaryColor;

		// Token: 0x04000021 RID: 33
		private bool useFactionTextures;

		// Token: 0x04000022 RID: 34
		private bool pauldronInitialized = false;

		// Token: 0x04000023 RID: 35
		private Graphic _pauldronGraphic;
	}
}
