using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000029 RID: 41
	[StaticConstructorOnStartup]
	internal static class HarmonyPatches
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00005024 File Offset: 0x00003224
		static HarmonyPatches()
		{
			Log.Message("Generating FactionColor Patches", false);
			Harmony harmony = new Harmony("rimworld.ohu.factionColors.main");
			harmony.Patch(AccessTools.Method(typeof(PawnGraphicSet), "ResolveApparelGraphics", null, null), new HarmonyMethod(typeof(HarmonyPatches), "ResolveApparelGraphicsOriginal", null), null, null, null);
			harmony.Patch(AccessTools.Method(typeof(PawnRenderer), "DrawEquipmentAiming", null, null), new HarmonyMethod(typeof(HarmonyPatches), "DrawEquipmentAimingModded", null), null, null, null);
			harmony.Patch(AccessTools.Method(typeof(Page_ConfigureStartingPawns), "CanDoNext", null, null), null, new HarmonyMethod(typeof(HarmonyPatches), "WorldGeneratePostfix", null), null, null);
			harmony.Patch(AccessTools.Method(typeof(Faction), "ExposeData", null, null), null, new HarmonyMethod(typeof(HarmonyPatches), "ExposeFactionDataPostfix", null), null, null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000511D File Offset: 0x0000331D
		public static void WorldGeneratePostfix()
		{
			FactionColorUtilities.currentFactionColorTracker.InitalizeFactions();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000512C File Offset: 0x0000332C
		public static bool ResolveApparelGraphicsOriginal(PawnGraphicSet __instance)
		{
			__instance.ClearCache();
			__instance.apparelGraphics.Clear();
			List<Apparel> list = new List<Apparel>();
			foreach (Apparel apparel in __instance.pawn.apparel.WornApparel)
			{
				bool flag = apparel.GetComp<CompFactionColor>() != null;
				if (flag)
				{
					ApparelGraphicRecord item;
					bool flag2 = ApparelGraphicGetterFC.TryGetGraphicApparelModded(apparel, __instance.pawn.story.bodyType, out item);
					if (flag2)
					{
						__instance.apparelGraphics.Add(item);
						ApparelDetailDrawer comp = apparel.GetComp<ApparelDetailDrawer>();
						bool flag3 = comp != null && !apparel.Spawned;
						if (flag3)
						{
							__instance.apparelGraphics.Add(new ApparelGraphicRecord(comp.DetailGraphic, apparel));
						}
					}
				}
				else
				{
					ApparelGraphicRecord item;
					bool flag4 = ApparelGraphicRecordGetter.TryGetGraphicApparel(apparel, __instance.pawn.story.bodyType, out item);
					if (flag4)
					{
						__instance.apparelGraphics.Add(item);
					}
				}
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005254 File Offset: 0x00003454
		public static bool DrawEquipmentAimingModded(Thing eq, Vector3 drawLoc, float aimAngle)
		{
			bool flag = eq.GetType() == typeof(FactionItem);
			bool result;
			if (flag)
			{
				float num = aimAngle - 90f;
				bool flag2 = aimAngle > 20f && aimAngle < 160f;
				Mesh mesh;
				if (flag2)
				{
					mesh = MeshPool.plane10;
					num += eq.def.equippedAngleOffset;
				}
				else
				{
					bool flag3 = aimAngle > 200f && aimAngle < 340f;
					if (flag3)
					{
						mesh = MeshPool.plane10Flip;
						num -= 180f;
						num -= eq.def.equippedAngleOffset;
					}
					else
					{
						mesh = MeshPool.plane10;
						num += eq.def.equippedAngleOffset;
					}
				}
				num %= 360f;
				Graphic_StackCount graphic_StackCount = eq.Graphic as Graphic_StackCount;
				bool flag4 = graphic_StackCount != null;
				Material matSingle;
				if (flag4)
				{
					matSingle = graphic_StackCount.SubGraphicForStackCount(1, eq.def).MatSingle;
				}
				else
				{
					matSingle = eq.Graphic.MatSingle;
				}
				FactionItemDef factionItemDef = eq.def as FactionItemDef;
				float d = 1f;
				Vector3 a = factionItemDef.ItemMeshSize * d;
				Material material = eq.Graphic.MatAt(eq.Rotation, null);
				Matrix4x4 matrix = default(Matrix4x4);
				matrix.SetTRS(drawLoc, Quaternion.AngleAxis(num, Vector3.up), a * 1.2f);
				Graphics.DrawMesh(mesh, matrix, matSingle, 0);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000053C8 File Offset: 0x000035C8
		public static void ExposeFactionDataPostfix(ref Faction __instance)
		{
			bool flag = __instance is FactionUniform;
			if (flag)
			{
				Scribe_Defs.Look<FactionDef>(ref __instance.def, "FactionDef");
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000053F8 File Offset: 0x000035F8
		private static void ResolveApparelGraphicsPostfix(PawnGraphicSet __instance)
		{
			foreach (CompAccessoryDrawer compAccessoryDrawer in from x in __instance.pawn.apparel.WornApparel
			select x.TryGetCompFast<CompAccessoryDrawer>() into x
			where x != null
			select x)
			{
				__instance.apparelGraphics.Add(new ApparelGraphicRecord(compAccessoryDrawer.Graphic, compAccessoryDrawer.FakeApparel));
			}
		}
	}
}
