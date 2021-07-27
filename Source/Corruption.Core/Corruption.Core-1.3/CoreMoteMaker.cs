using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Corruption.Core
{
    public static class CoreMoteMaker
    {
        public static Mote ThrowMetaIcon(IntVec3 cell, Map map, ThingDef moteDef)
        {
            if (!cell.ShouldSpawnMotesAt(map) || map.moteCounter.Saturated)
            {
                return null;
            }
            MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(moteDef, null);
            moteThrown.Scale = 0.7f;
            moteThrown.rotationRate = Rand.Range(-3f, 3f);
            moteThrown.exactPosition = cell.ToVector3Shifted();
            moteThrown.exactPosition += new Vector3(0.35f, 0f, 0.35f);
            moteThrown.exactPosition += new Vector3(Rand.Value, 0f, Rand.Value) * 0.1f;
            moteThrown.SetVelocity((float)Rand.Range(30, 60), 0.42f);
            GenSpawn.Spawn(moteThrown, cell, map, WipeMode.Vanish);
            return moteThrown;
        }
    }
}
