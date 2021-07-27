using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000002 RID: 2
	public class ApparelComposite : Apparel
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override Color DrawColorTwo
		{
			get
			{
				CompFactionColor comp;
				bool flag = (comp = base.GetComp<CompFactionColor>()) != null;
				Color result;
				if (flag)
				{
					bool tryUseSecondaryStuffColors = comp.CProps.TryUseSecondaryStuffColors;
					if (tryUseSecondaryStuffColors)
					{
						this.col = this.GetSecondaryStuffColor();
					}
					else
					{
						bool useSecondaryColors = comp.CProps.UseSecondaryColors;
						if (useSecondaryColors)
						{
							this.col = base.GetComp<CompFactionColor>().SecondaryColor;
							return this.col;
						}
					}
					result = this.col;
				}
				else
				{
					result = this.col;
				}
				return result;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		private Color GetSecondaryStuffColor()
		{
			List<ThingDefCountClass> costList = this.def.costList;
			for (int i = 0; i < costList.Count; i++)
			{
				bool isStuff = costList[i].thingDef.IsStuff;
				if (isStuff)
				{
					return costList[i].thingDef.stuffProps.color;
				}
			}
			return Color.gray;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000213C File Offset: 0x0000033C
		public override Graphic Graphic
		{
			get
			{
				return GraphicDatabase.Get<Graphic_Single>(this.def.graphicData.texPath, ShaderDatabase.CutoutComplex, this.def.graphicData.drawSize, this.DrawColor, this.DrawColorTwo);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002184 File Offset: 0x00000384
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<Color>(ref this.col, "col", Color.white, false);
		}

		// Token: 0x04000001 RID: 1
		private Color col = Color.white;
	}
}
