using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(MapTransition), "Move")]
    public class MapTransitionPatchMove
    {
        public static bool Prefix(GameObject target, Vector3 distance, Map targetMap)
        {
            if (target == null)
            {
                return false;
            }
    
            return true;
        }
    }
}