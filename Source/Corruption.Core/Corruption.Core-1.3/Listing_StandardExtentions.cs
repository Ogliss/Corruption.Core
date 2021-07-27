using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Corruption.Core
{
    public static class Listing_StandardExtentions
	{
		public static void BeginScrollView(this Listing_Standard listing, Rect rect, ref Vector2 scrollPosition, ref Rect viewRect)
		{
			Widgets.BeginScrollView(rect, ref scrollPosition, viewRect, true);
			rect.height = 100000f;
			rect.width -= 20f;
			listing.Begin(rect.AtZero());
		}
		public static void EndScrollView(this Listing_Standard listing, ref Rect viewRect)
		{
			viewRect = new Rect(0f, 0f, listing.listingRect.width, listing.curY);
			Widgets.EndScrollView();
			listing.End();
		}
	}
}
