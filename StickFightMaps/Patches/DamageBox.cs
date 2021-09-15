using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(DamageBox), "Collide")]
    public class DamageBoxPatch
    {
        public static bool Prefix(Collision2D collision)
        {
            return collision.gameObject.GetComponentInParent<HealthHandler>();
        }
    }
}