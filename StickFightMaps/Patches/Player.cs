using HarmonyLib;
using UnboundLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(Player), "Start")]
    public class PlayerPatch
    {
        public static void Postfix(Player __instance)
        {
            StickFightMaps.instance.ExecuteAfterSeconds(10, () =>
            {
                if (__instance != null)
                {
                    foreach (var ren in __instance.transform.Find("Art/Face").GetComponentsInChildren<SpriteRenderer>())
                    {
                        ren.sortingLayerName = "Player10";
                    }
                }
            });
        }
    }
}