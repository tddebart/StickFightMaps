using System.Linq;
using HarmonyLib;
using Photon.Pun;
using StickFightMaps.MonoBehaviours;
using UnboundLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StickFightMaps.Patches
{
    
    [HarmonyPatch(typeof(PhotonMapObject), "Start")]
    class mapObjectStart
    {
        public static void Prefix()
        {
            if (!StickFightMaps.didWarning)
            {
                UnityEngine.Debug.LogWarning("[StickFightMaps] Just ignore these errors below i can't get rid of them");
                StickFightMaps.didWarning = true;
            }
        }
    }
}