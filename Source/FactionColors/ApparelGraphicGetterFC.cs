using System;
using RimWorld;
using Verse;

namespace FactionColors
{
	// Token: 0x02000003 RID: 3
	public static class ApparelGraphicGetterFC
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021BC File Offset: 0x000003BC
		public static bool TryGetGraphicApparelModded(Apparel apparel, BodyTypeDef bodyType, out ApparelGraphicRecord rec)
		{
			bool flag = bodyType == null;
			if (flag)
			{
				Log.Error("Getting apparel graphic with undefined body type.", false);
				bodyType = BodyTypeDefOf.Male;
			}
			bool flag2 = apparel.def.apparel.wornGraphicPath.NullOrEmpty();
			bool result;
			if (flag2)
			{
				rec = new ApparelGraphicRecord(null, null);
				result = false;
			}
			else
			{
				string text = apparel.def.apparel.wornGraphicPath;
				CompFactionColor compFactionColor = apparel.TryGetCompFast<CompFactionColor>();
				bool flag3 = compFactionColor != null;
				if (flag3)
				{
					bool isRandomMultiGraphic = compFactionColor.CProps.IsRandomMultiGraphic;
					if (isRandomMultiGraphic)
					{
						text = string.Concat(new string[]
						{
							text,
							"/",
							compFactionColor.randomGraphicPath,
							"/",
							compFactionColor.randomGraphicPath
						});
					}
				}
				bool flag4 = apparel.def.apparel.LastLayer != ApparelLayerDefOf.Overhead;
				if (flag4)
				{
					text = text + "_" + bodyType.ToString();
				}
				Graphic graphic = new Graphic();
				graphic = GraphicDatabase.Get<Graphic_Multi>(text, ShaderDatabase.CutoutComplex, apparel.def.graphicData.drawSize, apparel.DrawColor, apparel.DrawColorTwo);
				rec = new ApparelGraphicRecord(graphic, apparel);
				result = true;
			}
			return result;
		}
	}
}
