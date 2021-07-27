using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	public class ITab_FactionColor : ITab
	{
		public ITab_FactionColor()
		{
			this.size = new Vector2(400f, 250f);
			this.labelKey = "TabCoA";
		}

		public override void FillTab()
		{
			Rect rect = new Rect(0f, 0f, this.size.x, this.size.y).ContractedBy(5f);
			FactionColorUtilities.DrawFactionColorTab(rect);
		}
	}
}
