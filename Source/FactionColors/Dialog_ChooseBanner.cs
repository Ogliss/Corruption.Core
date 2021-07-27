using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000019 RID: 25
	internal class Dialog_ChooseBanner : Window
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003D00 File Offset: 0x00001F00
		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(300f, 500f);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D21 File Offset: 0x00001F21
		public Dialog_ChooseBanner(List<string> options)
		{
			this.options = options;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D34 File Offset: 0x00001F34
		public override void DoWindowContents(Rect inRect)
		{
			GUI.BeginGroup(inRect);
			Rect rect = inRect.ContractedBy(10f);
			rect.height = 20f;
			for (int i = 0; i < this.options.Count; i++)
			{
				bool flag = Widgets.RadioButtonLabeled(rect, this.options[i], FactionColorUtilities.currentFactionColorTracker.BannerGraphicPath == "UI/Flags/" + this.options[i]);
				if (flag)
				{
					FactionColorUtilities.currentFactionColorTracker.BannerGraphicPath = "UI/Flags/" + this.options[i];
				}
				rect.y += 28f;
			}
			Rect rect2 = new Rect(rect.x + rect.width / 2f - 50f, rect.y, 100f, 25f);
			bool flag2 = Widgets.ButtonText(rect2, "Confirm".Translate(), true, true, true);
			if (flag2)
			{
				this.Close(true);
			}
			bool flag3 = Widgets.CloseButtonFor(inRect.AtZero());
			if (flag3)
			{
				this.Close(true);
			}
			GUI.EndGroup();
		}

		// Token: 0x04000049 RID: 73
		private List<string> options;
	}
}
