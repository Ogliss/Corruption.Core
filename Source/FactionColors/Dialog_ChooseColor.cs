using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000018 RID: 24
	public class Dialog_ChooseColor : Window
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003968 File Offset: 0x00001B68
		public Dialog_ChooseColor(Color color, bool IsSecondaryColor = false)
		{
			this.oldColor = new Color(color.r, color.g, color.b);
			this.isSecondaryColor = IsSecondaryColor;
			if (IsSecondaryColor)
			{
				this.title = "SecondaryColor";
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000039CC File Offset: 0x00001BCC
		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(250f, 150f);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000039F0 File Offset: 0x00001BF0
		public void DrawFactionColorSliders(Rect rect, string title)
		{
			Color color = this.oldColor;
			Rect rect2 = new Rect(rect.x, rect.y, rect.width, 20f);
			Text.Anchor = TextAnchor.MiddleCenter;
			Widgets.Label(rect2, title);
			Text.Anchor = TextAnchor.UpperLeft;
			GUI.color = Color.red;
			Rect rect3 = rect;
			rect3.y += 20f;
			this.RGB[0] = GUI.HorizontalSlider(new Rect(rect3.x + 40f, rect3.y - 1f, 136f, 16f), this.RGB[0], 0f, 1f);
			GUI.color = Color.green;
			this.RGB[1] = GUI.HorizontalSlider(new Rect(rect3.x + 40f, rect3.y + 19f, 136f, 16f), this.RGB[1], 0f, 1f);
			GUI.color = Color.blue;
			this.RGB[2] = GUI.HorizontalSlider(new Rect(rect3.x + 40f, rect3.y + 39f, 136f, 16f), this.RGB[2], 0f, 1f);
			GUI.color = Color.white;
			Color color2 = new Color(this.RGB[0], this.RGB[1], this.RGB[2]);
			Widgets.DrawRectFast(new Rect(rect.x, rect.y, 32f, 32f), color2, null);
			Rect rect4 = new Rect(rect.width / 2f - 50f, rect3.y + 59f, 100f, 25f);
			bool flag = Widgets.ButtonText(rect4, "Confirm".Translate(), true, true, true);
			if (flag)
			{
				bool flag2 = this.isSecondaryColor;
				if (flag2)
				{
					FactionColorUtilities.currentFactionColorTracker.PlayerColorTwo = color2;
				}
				else
				{
					FactionColorUtilities.currentFactionColorTracker.PlayerColorOne = color2;
				}
				this.Close(true);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003C1C File Offset: 0x00001E1C
		public override void PreOpen()
		{
			base.PreOpen();
			this.RGB[0] = this.oldColor.r;
			this.RGB[1] = this.oldColor.g;
			this.RGB[2] = this.oldColor.b;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003C6C File Offset: 0x00001E6C
		public override void DoWindowContents(Rect inRect)
		{
			this.forcePause = true;
			Rect rect = inRect;
			GUI.BeginGroup(inRect);
			Rect rect2 = rect.ContractedBy(10f);
			rect2.width -= 20f;
			this.DrawFactionColorSliders(rect2, this.title.Translate());
			Rect rect3 = new Rect(inRect.x, inRect.y, 15f, 15f);
			bool flag = Widgets.CloseButtonFor(inRect.AtZero());
			if (flag)
			{
				this.Close(true);
			}
			GUI.EndGroup();
		}

		// Token: 0x04000045 RID: 69
		private float[] RGB = new float[3];

		// Token: 0x04000046 RID: 70
		private Color oldColor;

		// Token: 0x04000047 RID: 71
		private string title = "PrimaryColor";

		// Token: 0x04000048 RID: 72
		private bool isSecondaryColor;
	}
}
