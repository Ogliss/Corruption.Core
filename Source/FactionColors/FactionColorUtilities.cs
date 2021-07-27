using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200001B RID: 27
	[StaticConstructorOnStartup]
	public static class FactionColorUtilities
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003EF4 File Offset: 0x000020F4
		public static Texture2D getColoredBannerTex()
		{
			Texture2D texture2D = (Texture2D)FactionColorUtilities.currentPlayerBanner.MatSingle.mainTexture;
			Texture2D maskTexture = FactionColorUtilities.currentPlayerBanner.MatSingle.GetMaskTexture();
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					bool flag = maskTexture.GetPixel(i, j) == Color.red;
					if (flag)
					{
						texture2D.SetPixel(i, j, texture2D.GetPixel(i, j) * FactionColorUtilities.currentPlayerBanner.color);
					}
					bool flag2 = maskTexture.GetPixel(i, j) == Color.green;
					if (flag2)
					{
						texture2D.SetPixel(i, j, texture2D.GetPixel(i, j) * FactionColorUtilities.currentPlayerBanner.ColorTwo);
					}
				}
			}
			return texture2D;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003FE0 File Offset: 0x000021E0
		public static Graphic currentPlayerBanner
		{
			get
			{
				return GraphicDatabase.Get<Graphic_Single>(FactionColorUtilities.currentFactionColorTracker.BannerGraphicPath, ShaderDatabase.CutoutComplex, Vector2.one, FactionColorUtilities.currentFactionColorTracker.PlayerColorOne, FactionColorUtilities.currentFactionColorTracker.PlayerColorTwo);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004020 File Offset: 0x00002220
		public static FactionColorsTracker currentFactionColorTracker
		{
			get
			{
				return Find.World.GetComponent<FactionColorsTracker>();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000403C File Offset: 0x0000223C
		public static void DrawFactionColorTab(Rect rect)
		{
			GUI.BeginGroup(rect);
			Rect rect2 = new Rect(rect.x, rect.y + 20f, 250f, 55f);
			Text.Font = GameFont.Medium;
			Widgets.Label(rect2, "PlayerHeraldry".Translate(new object[]
			{
				Faction.OfPlayer.Name
			}));
			Text.Font = GameFont.Small;
			Rect rect3 = new Rect(rect.width - 205f, rect2.yMax + 5f, 200f, 200f);
			Rect rect4 = new Rect(rect2.x, rect2.height + 5f, 150f, 30f);
			Rect rect5 = new Rect(rect4.x + 150f, rect4.y, 15f, 15f);
			Widgets.Label(rect4, "PlayerColorOne".Translate());
			Widgets.DrawRectFast(rect5, FactionColorUtilities.currentFactionColorTracker.PlayerColorOne, null);
			bool flag = Widgets.ButtonInvisible(rect5, true);
			if (flag)
			{
				FactionColorUtilities.ShowColorDialog(FactionColorUtilities.currentFactionColorTracker.PlayerColorOne, false);
			}
			Rect rect6 = rect4;
			rect6.y += rect4.height + 5f;
			Rect rect7 = new Rect(rect6.x + 150f, rect6.y, 15f, 15f);
			Widgets.Label(rect6, "PlayerColorTwo".Translate());
			Widgets.DrawRectFast(rect7, FactionColorUtilities.currentFactionColorTracker.PlayerColorTwo, null);
			bool flag2 = Widgets.ButtonInvisible(rect7, true);
			if (flag2)
			{
				FactionColorUtilities.ShowColorDialog(FactionColorUtilities.currentFactionColorTracker.PlayerColorTwo, true);
			}
			Texture2D maskTexture = FactionColorUtilities.currentPlayerBanner.MatSingle.GetMaskTexture();
			Rect rect8 = rect6;
			rect8.y += rect6.height + 5f;
			Rect butRect = new Rect(rect8.x + 150f, rect8.y, 15f, 15f);
			Widgets.Label(rect8, "BannerType".Translate());
			bool flag3 = Widgets.ButtonImage(butRect, maskTexture, true);
			if (flag3)
			{
				FactionColorUtilities.ShowBannerDialog();
			}
			Text.Font = GameFont.Small;
			RenderTexture renderTexture = new RenderTexture(64, 64, 24);
			Rect position = new Rect(rect.width - 100f, rect.y + 50f, 64f, 64f);
			GUI.DrawTexture(position, maskTexture);
			GUI.color = Color.white;
			Vector3 pos = new Vector3((float)UI.screenWidth / 2f, 50f, (float)UI.screenHeight / 2f);
			pos.y += 50f;
			Mesh mesh = FactionColorUtilities.currentPlayerBanner.MeshAt(Rot4.North);
			Matrix4x4 matrix = default(Matrix4x4);
			matrix.SetTRS(pos, Quaternion.AngleAxis(0f, Vector3.up), new Vector3(1f, 1f, 1f));
			Graphics.DrawMesh(mesh, matrix, FactionColorUtilities.currentPlayerBanner.MatSingle, 0);
			GUI.EndGroup();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004354 File Offset: 0x00002554
		private static void ShowBannerDialog()
		{
			List<string> bannerOptions = FactionColorUtilities.currentFactionColorTracker.BannerOptions;
			Dialog_ChooseBanner window = new Dialog_ChooseBanner(bannerOptions);
			Find.WindowStack.Add(window);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004380 File Offset: 0x00002580
		private static void ShowColorDialog(Color color, bool IsSecondaryColor)
		{
			Dialog_ChooseColor window = new Dialog_ChooseColor(color, IsSecondaryColor);
			Find.WindowStack.Add(window);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000043A4 File Offset: 0x000025A4
		private static Texture2D GetBannerTint(string path)
		{
			return FactionColorUtilities.currentPlayerBanner.MatSingle.GetMaskTexture();
		}

		// Token: 0x0400004D RID: 77
		public static Texture2D buttonBanner = ContentFinder<Texture2D>.Get("UI/Buttons/Banner", true);
	}
}
