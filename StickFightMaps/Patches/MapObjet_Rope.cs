using HarmonyLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(MapObjet_Rope), "AddJoint")]
    public class MapObjet_RopePatch
    {
        public static void Postfix(MapObjet_Rope __instance, Rigidbody2D target)
        {
            if (target.name.Contains("(Trap)") && __instance.jointType == MapObjet_Rope.JointType.spring)
            {
                target.GetComponent<SpringJoint2D>().dampingRatio = 0.25f;
            }
        }
    }
}