using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(GameCrownHandler), "Start")]
    public class GameCrownHandlerPatchStart
    {
        public static void Postfix(GameCrownHandler __instance)
        {
            __instance.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Player10";
        }
    }
}