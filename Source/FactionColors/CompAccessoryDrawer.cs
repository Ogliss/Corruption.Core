using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000A RID: 10
	public class CompAccessoryDrawer : ThingComp
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002DEA File Offset: 0x00000FEA
		public CompProperties_AccessoryDrawer Props
		{
			get
			{
				return this.props as CompProperties_AccessoryDrawer;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public Apparel FakeApparel
		{
			get
			{
				bool flag = this._fakeApparel == null;
				if (flag)
				{
					this._fakeApparel = (Apparel)ThingMaker.MakeThing(this.Props.templateApparelDef, null);
				}
				return this._fakeApparel;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002E3C File Offset: 0x0000103C
		public Graphic Graphic
		{
			get
			{
				bool flag = this._graphic == null;
				if (flag)
				{
					GraphicData graphicData = this.FakeApparel.def.graphicData;
					string texPath = this.GetTexPath();
					bool flag2 = !this.lockColors;
					if (flag2)
					{
						this.ResolveColors();
					}
					Shader shader = (this.Props.shader != null) ? this.Props.shader.Shader : graphicData.shaderType.Shader;
					Type type = this.Props.graphicClass ?? graphicData.graphicClass;
					this._graphic = GraphicDatabase.Get(type, texPath, shader, graphicData.drawSize, this.ColorOne, this.ColorTwo);
				}
				return this._graphic;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002EFB File Offset: 0x000010FB
		public override void PostSpawnSetup(bool respawningAfterLoad)
		{
			base.PostSpawnSetup(respawningAfterLoad);
			this._graphic = null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F10 File Offset: 0x00001110
		private void ResolveColors()
		{
			bool useParentColors = this.Props.UseParentColors;
			if (useParentColors)
			{
				this.ColorOne = this.parent.DrawColor;
				this.ColorTwo = this.parent.DrawColorTwo;
			}
			bool useColorGenerator = this.Props.UseColorGenerator;
			if (useColorGenerator)
			{
				this.ColorOne = this.parent.def.colorGenerator.NewRandomizedColor();
				this.ColorTwo = this.parent.def.colorGenerator.NewRandomizedColor();
				this.lockColors = true;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002FA0 File Offset: 0x000011A0
		private string GetTexPath()
		{
			string text = this.Props.texPath;
			Apparel apparel = this.parent as Apparel;
			bool flag = apparel == null && apparel.Wearer == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool useBodyTypes = this.Props.useBodyTypes;
				if (useBodyTypes)
				{
					text = string.Join("_", new string[]
					{
						text,
						apparel.Wearer.story.bodyType.defName
					});
				}
				else
				{
					bool useCrownTypes = this.Props.useCrownTypes;
					if (useCrownTypes)
					{
						text = string.Join("_", new string[]
						{
							text,
							apparel.Wearer.story.crownType.ToString()
						});
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003074 File Offset: 0x00001274
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<Color>(ref this.ColorOne, "colorOne", default(Color), false);
			Scribe_Values.Look<Color>(ref this.ColorTwo, "colorTwo", default(Color), false);
			Scribe_Values.Look<bool>(ref this.lockColors, "lockColors", false, false);
		}

		// Token: 0x0400000E RID: 14
		public Color ColorOne = Color.white;

		// Token: 0x0400000F RID: 15
		public Color ColorTwo = Color.white;

		// Token: 0x04000010 RID: 16
		private bool lockColors = false;

		// Token: 0x04000011 RID: 17
		private Apparel _fakeApparel;

		// Token: 0x04000012 RID: 18
		private Graphic _graphic;
	}
}
