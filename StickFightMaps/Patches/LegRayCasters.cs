using HarmonyLib;
using Photon.Pun;
using StickFightMaps.MonoBehaviours;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(LegRaycasters))]
    public class LegRayCastersPatch
    {
        [HarmonyPatch("HitGround")]
        public static void Prefix(RaycastHit2D hit)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //UnityEngine.Debug.LogWarning(hit.transform.name + " | " + hit.transform.parent.name);
                if (hit.transform.parent && hit.transform.parent.name.Contains("(Fall)") && hit.transform.parent.GetComponent<FallingPlatform>())
                {
                    hit.transform.gameObject.GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
                }
            }
        }
    }
}