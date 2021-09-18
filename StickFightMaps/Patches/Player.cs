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
                UnityEngine.Debug.LogWarning("Changed faces");
                foreach (var ren in __instance.transform.Find("Art/Face").GetComponentsInChildren<SpriteRenderer>())
                {
                    ren.sortingLayerName = "Player10";
                }

            });
        }
    }
}