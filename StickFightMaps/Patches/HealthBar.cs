using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(HealthBar), "Start")]
    public class HealthBarPatchStart
    {
        public static void Postfix(HealthBar __instance)
        {
            __instance.GetComponentInChildren<Canvas>().sortingLayerName = "Player10";
        }
    }
}