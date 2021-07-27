using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000009 RID: 9
	[StaticConstructorOnStartup]
	public static class CamouflageColorsUtility
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static Color[] CamouflageColors
		{
			get
			{
				Color[] array = new Color[2];
				CamouflageColorsUtility.GetCamouflageColors(out array[0], out array[1]);
				return array;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static void GetCamouflageColors(out Color col1, out Color col2)
		{
			col1 = new Color(0.3f, 0.46f, 0.3f);
			col2 = new Color(0.4f, 0.34f, 0.24f);
			Map currentMap = Find.CurrentMap;
			bool flag = currentMap != null;
			if (flag)
			{
				BiomeDef biome = currentMap.Biome;
				bool flag2 = Find.CurrentMap.snowGrid.TotalDepth > 0f || biome == BiomeDefOf.IceSheet || biome.defName == "SeaIce";
				if (flag2)
				{
					col1 = Color.white;
					col2 = Color.grey;
				}
				bool flag3 = biome == BiomeDefOf.Desert || biome == BiomeDefOf.AridShrubland || biome.defName == "ExtremeDesert";
				if (flag3)
				{
					col1 = new Color(0.91f, 0.82f, 0.69f);
					col2 = new Color(0.75f, 0.71f, 0.56f);
				}
			}
		}
	}
}
