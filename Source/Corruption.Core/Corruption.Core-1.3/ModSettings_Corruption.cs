using Corruption.Core.Gods;
using Corruption.Core.Soul;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using static Corruption.Core.ModSettings_Corruption;

namespace Corruption.Core
{
    public class ModSettings_Corruption : ModSettings
    {
        public sealed class SoulRaceEntry : IExposable
        {
            public SoulRaceEntry()
            {

            }

            public string Race;
            public bool Enabled;
            public string DefaultPantheon;
            public float StartingCorruption;
            public float BaseCorruptionGainFactor = 1f;
            public string CorruptionGainBuffer = "1";


            public void ExposeData()
            {
                Scribe_Values.Look<string>(ref this.Race, "Race");
                Scribe_Values.Look<bool>(ref this.Enabled, "Enabled");
                Scribe_Values.Look<string>(ref this.DefaultPantheon, "DefaultPantheon");
                Scribe_Values.Look<float>(ref this.StartingCorruption, "StartingCorruption");
                Scribe_Values.Look<float>(ref this.BaseCorruptionGainFactor, "BaseCorruptionResistanceFactor");
                if (Scribe.mode == LoadSaveMode.PostLoadInit)
                {
                    this.CorruptionGainBuffer = BaseCorruptionGainFactor.ToString();
                }
            }
        }

        public List<SoulRaceEntry> SoulRaceCombinations = new List<SoulRaceEntry>();

        public static float CorruptionGainFactor = 1f;
        public static float PossessionGainFactor = 1f;
        public static float WorshipGainSpeedFactor = 1f;

        internal float CorruptionGainFactorInternal = 1f;
        internal float PossessionGainFactorInternal = 1f;
        internal float WorshipGainFactorInternal = 1f;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look<SoulRaceEntry>(ref this.SoulRaceCombinations, "soulRaceCombinations", LookMode.Deep);
            Scribe_Values.Look<float>(ref this.CorruptionGainFactorInternal, "CorruptionGainFactor", 1f);
            Scribe_Values.Look<float>(ref this.PossessionGainFactorInternal, "PossessionGainFactor", 1f);
            Scribe_Values.Look<float>(ref this.WorshipGainFactorInternal, "WorshipGainSpeedFactor", 1f);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                ModSettings_Corruption.CorruptionGainFactor = CorruptionGainFactorInternal;
                ModSettings_Corruption.PossessionGainFactor = PossessionGainFactorInternal;
                ModSettings_Corruption.WorshipGainSpeedFactor = WorshipGainFactorInternal;
            }
        }
    }

    public class CorruptionMod : Mod
    {
        public static ModSettings_Corruption settings;

        public override string SettingsCategory()
        {
            return "CorruptionModName".Translate();
        }

        internal static List<ThingDef> AvailableRaces;
        internal static List<PantheonDef> AvailablePantheons;
        private static Vector2 ScrollPos;

        public CorruptionMod(ModContentPack content) : base(content)
        {
            settings = ((Mod)this).GetSettings<ModSettings_Corruption>();

            /*
            foreach (var def in DefDatabase<ThingDef>.AllDefs.Where(x => x.race != null && x.race.Humanlike))
            {
                var entry = settings.SoulRaceCombinations.FirstOrDefault(x => x.Race == def.defName);
                if (entry != null)
                {
                    Log.Message("Corruption Soulcheck: " + def.LabelCap);
                    bool resolve = false;
                    if (!def.HasComp(typeof(CompProperties_Soul)))
                    {
                        Log.Message("Try add CompProperties_Soul");
                        var soulComp = new CompProperties_Soul();
                        soulComp.defaultPantheon = DefDatabase<PantheonDef>.AllDefs.Where(x => x.requiresMod == null || (ModLister.GetModWithIdentifier(x.requiresMod)?.Active ?? false)).FirstOrDefault(x => x.defName == entry.DefaultPantheon);
                        soulComp.baseCorruptionResistanceFactor = entry.BaseCorruptionGainFactor;
                        def.comps.Add(soulComp);
                        resolve = true;
                    }
                    if (!def.inspectorTabs.Contains(typeof(ITab_Pawn_Soul)))
                    {
                        Log.Message("Try add ITab_Pawn_Soul");
                        def.inspectorTabs.Add(typeof(ITab_Pawn_Soul));
                        resolve = true;
                    }
                    if (!def.recipes.Contains(RecipeDefOf.Exorcism))
                    {
                        Log.Message("Try add Exorcism to");
                        def.recipes.Add(RecipeDefOf.Exorcism);
                        resolve = true;
                    }
                    if (resolve) def.ResolveReferences();
                }
            }
            */
        }

        public override void WriteSettings()
        {
            settings.SoulRaceCombinations.RemoveAll(x => x.Race == null);
            base.WriteSettings();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {

            Rect factorSliderRect = new Rect(inRect.x, inRect.y + 32f, inRect.width * 0.5f, 24f);
            settings.CorruptionGainFactorInternal = Widgets.HorizontalSlider(factorSliderRect, settings.CorruptionGainFactorInternal, 0f, 2f, false, "CorruptionGainFactor".Translate(), "0x", "2x");
            factorSliderRect.y += 32f;
            settings.PossessionGainFactorInternal = Widgets.HorizontalSlider(factorSliderRect, settings.PossessionGainFactorInternal, 0f, 2f, false, "PossessionGainFactor".Translate(), "0x", "2x");
            factorSliderRect.y += 32f;
            settings.WorshipGainFactorInternal = Widgets.HorizontalSlider(factorSliderRect, settings.WorshipGainFactorInternal, 0f, 2f, false, "WorshipGainFactor".Translate(), "0x", "2x");


            Rect raceRect = new Rect(inRect);
            raceRect.y = factorSliderRect.yMax + 8f;
            raceRect.height = inRect.height - 34f * 3;
            GUI.BeginGroup(raceRect);
            Text.Font = GameFont.Medium;
            Rect titleRect = new Rect(0f, 0f, inRect.width, Text.LineHeight);

            Widgets.Label(titleRect, "SoulSettings".Translate());
            Widgets.DrawLineHorizontal(4f, titleRect.yMax + 2f, inRect.width - 8f);
            Text.Font = GameFont.Small;

            Rect descrRect = new Rect(0f, titleRect.yMax + 6f, inRect.width, Text.LineHeight * 3f);
            Widgets.Label(descrRect, "SoulSettingsDesc".Translate());



            Listing_Standard listingStandard = new Listing_Standard();
            Rect allRect = new Rect(0f, descrRect.yMax + 8f, inRect.width - 24f, 256f);

        //    Rect viewRect = new Rect(allRect.x, allRect.y, allRect.ContractedBy(4).width, AvailableRaces.Count * Text.LineHeight * 4.5f);
            Rect viewRect = new Rect(0f, 0f, allRect.width - 36f, AvailableRaces.Count * Text.LineHeight * 4.5f);
            Widgets.DrawBox(allRect);
            Widgets.DrawWindowBackground(allRect);

            allRect = allRect.ContractedBy(4f);

            listingStandard.BeginScrollView(allRect, ref ScrollPos, ref viewRect);
            foreach (var race in AvailableRaces)
            {
                Rect rect = listingStandard.GetRect(Text.LineHeight * 4f);
                Widgets.Label(rect, race.LabelCap);

                Rect btnRect = new Rect(220f, rect.y, 200f, Text.LineHeight + 8f);

                var entry = settings.SoulRaceCombinations.FirstOrDefault(x => x.Race == race.defName);

                if (entry != null)
                {
                    if (Widgets.ButtonText(btnRect, entry.DefaultPantheon ?? "NoneLower".Translate()))
                    {
                        this.OpenPantheonSelectMenu(entry);
                    }
                }
                else
                {
                    if (Widgets.ButtonText(btnRect, "NoneLower".Translate()))
                    {
                        entry = new SoulRaceEntry() { Race = race.defName, DefaultPantheon = null, Enabled = race.HasComp(typeof(CompSoul))};
                        settings.SoulRaceCombinations?.Add(entry);
                        this.OpenPantheonSelectMenu(entry);
                    }
                }

                if (!string.IsNullOrEmpty(entry?.DefaultPantheon))
                {
                    Rect sliderRect = new Rect(btnRect);
                    sliderRect.x += btnRect.width + 8f;
                    sliderRect.width = allRect.width - btnRect.xMax - 32f;
                    if (entry.DefaultPantheon.Equals(PantheonDefOf.Chaos.defName))
                    {
                        entry.StartingCorruption = Widgets.HorizontalSlider(sliderRect, entry.StartingCorruption, SoulAfflictionDefOf.Corrupted.Threshold, SoulAfflictionDefOf.Lost.Threshold, true, "StartingCorruption".Translate(), SoulAfflictionDefOf.Corrupted.LabelCap, SoulAfflictionDefOf.Lost.LabelCap);
                    }
                    else
                    {
                        entry.StartingCorruption = Widgets.HorizontalSlider(sliderRect, entry.StartingCorruption, SoulAfflictionDefOf.Pure.Threshold, SoulAfflictionDefOf.Corrupted.Threshold - 0.05f, true, "StartingCorruption".Translate(), SoulAfflictionDefOf.Pure.LabelCap, SoulAfflictionDefOf.Tainted.LabelCap);
                    }

                    Rect resistanceRect = new Rect(0f, sliderRect.yMax + 4f, 400f, Text.LineHeight);
                    Widgets.TextFieldNumericLabeled<float>(resistanceRect, "CorruptionGainFactor".Translate(), ref entry.BaseCorruptionGainFactor, ref entry.CorruptionGainBuffer, 0f, 10f);
                    
                    
                }

                Widgets.DrawLineHorizontal(0f, rect.yMax - 1f, viewRect.width);


            }
            listingStandard.EndScrollView(ref viewRect);
            GUI.EndGroup();
            base.DoSettingsWindowContents(inRect);
        }

        private void OpenPantheonSelectMenu(SoulRaceEntry raceEntry)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("Disabled".Translate(), delegate
            {
                raceEntry.DefaultPantheon = null;
                raceEntry.Enabled = false;
            }, MenuOptionPriority.Default, null, null, 0f, null));

            list.Add(new FloatMenuOption("NoneLower".Translate(), delegate
            {
                raceEntry.DefaultPantheon = null;
                raceEntry.Enabled = true;
            }, MenuOptionPriority.Default, null, null, 0f, null));

            foreach (var pantheon in AvailablePantheons)
            {
                list.Add(new FloatMenuOption(pantheon.LabelCap, delegate
                {
                    raceEntry.DefaultPantheon = pantheon.defName;
                    raceEntry.Enabled = true;
                }, MenuOptionPriority.Default, null, null, 0f, null));
            }

            Find.WindowStack.Add(new FloatMenu(list));
        }
    }
}
