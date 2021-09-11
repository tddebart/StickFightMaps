using System.Linq;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(ProjectileHit))]
        public class ProjectileCollisionPatch
        {
            [HarmonyPatch("Hit")]
            [HarmonyPrefix]
            public static void hitSurface(HitInfo hit)
            {
                if (hit.transform.parent && hit.transform.parent.name.Contains("(Fall)") && PhotonNetwork.IsMasterClient)
                {
                    hit.transform.gameObject.GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
                }
            }
        }
}