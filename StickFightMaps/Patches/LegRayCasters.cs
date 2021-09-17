using System.Linq;
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
        public static void Prefix(LegRaycasters __instance, RaycastHit2D hit)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //UnityEngine.Debug.LogWarning(hit.transform.name + " | " + hit.transform.parent.name);
                if (hit.transform.parent && hit.transform.parent.name.Contains("(Fall)") && hit.transform.parent.GetComponent<FallingPlatform>())
                {
                    hit.transform.gameObject.GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
                }
                
            }
            if (hit.transform.parent && hit.transform.parent.name.Contains("TreadMill") &&
                hit.transform.GetComponent<TreadMill>())
            {
                var player = __instance.transform.parent.gameObject.GetComponentInParent<Player>();
                if (hit.transform.GetComponent<TreadMill>().treadmillPeople.Any(l => l.player == player))
                {
                    return;
                }
                TreadmillPerson treadmillPerson2 = new TreadmillPerson();
                treadmillPerson2.player = player;
                treadmillPerson2.velocity = player.GetComponent<PlayerVelocity>();
                hit.transform.GetComponent<TreadMill>().treadmillPeople.Add(treadmillPerson2);
            }
        }
    }
}