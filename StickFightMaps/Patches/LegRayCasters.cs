using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(LegRaycasters))]
    public class LegRayCastersPatch
    {
        [HarmonyPatch("HitGround")]
        public static void Prefix(RaycastHit2D hit)
        {
            //UnityEngine.Debug.LogWarning(hit.transform.name);
        }
    }
}