using System.Linq;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(ProjectileCollision))]
        public class ProjectileCollisionPatch
        {
            [HarmonyPatch("HitSurface")]
            [HarmonyPrefix]
            public static void hitSurface(ProjectileCollision __instance, ref ProjectileHitSurface.HasToStop __result, GameObject projectile, HitInfo hit)
            {
                if (hit.transform.name.Contains("(Fall)") && PhotonNetwork.IsMasterClient)
                {
                    hit.transform.gameObject.GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
                }
            }
        }
}