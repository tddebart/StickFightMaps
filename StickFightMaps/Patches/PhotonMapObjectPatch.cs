using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(PhotonMapObject), "Update")]
    class PhotonMapObject_Patch_Update
    {
        private static bool Prefix(PhotonMapObject __instance, ref bool ___waitingToBeRemoved, bool ___photonSpawned, ref float ___counter, Map ___map)
        {
            if (___waitingToBeRemoved)
            {
                return false;
            }
            if (___photonSpawned)
            {
                return false;
            }
            ___counter += Mathf.Clamp(Time.deltaTime, 0f, 0.1f);

            var otherPlayersMostRecentlyLoadedLevel =
                (int) Traverse.Create(MapManager.instance).Field("otherPlayersMostRecentlyLoadedLevel").GetValue();
            var levelID = (int) Traverse.Create(___map).Field("levelID").GetValue();
            var LoadedForAll = otherPlayersMostRecentlyLoadedLevel == levelID;
            if ((PhotonNetwork.OfflineMode && ___counter > 1f && ___map.hasEntered) || (___map && ___map.hasEntered && LoadedForAll))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Transform transform;
                    if (__instance.gameObject.name == "Crate2")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateReal", (transform = __instance.transform).position, transform.rotation, 0, null);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    }
                    else if (__instance.gameObject.name == "Crate2Big")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateRealBig", (transform = __instance.transform).position, transform.rotation, 0, null);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } 
                    else if (__instance.gameObject.name == "CrateLong")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateLongReal", (transform = __instance.transform).position, transform.rotation, 0, null);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("4 Map Objects/" + __instance.gameObject.name.Split(new char[]
                        {
                            char.Parse(" ")
                        })[0], (transform = __instance.transform).position, transform.rotation, 0, null);
                    }
                }

                var missingObjects = (int) Traverse.Create(___map).Field("missingObjects").GetValue();
                Traverse.Create(___map).Field("missingObjects").SetValue(missingObjects+1);
                ___waitingToBeRemoved = true;
            }
            return false;
        }
    }
}