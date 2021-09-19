﻿using HarmonyLib;
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

            var otherPlayersMostRecentlyLoadedLevel = (int) Traverse.Create(MapManager.instance).Field("otherPlayersMostRecentlyLoadedLevel").GetValue();
            var levelID = (int) Traverse.Create(___map).Field("levelID").GetValue();
            var LoadedForAll = otherPlayersMostRecentlyLoadedLevel == levelID;
            if ((PhotonNetwork.OfflineMode && ___counter > 1f && ___map.hasEntered) || (___map && ___map.hasEntered && LoadedForAll))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Transform transform;
                    if (__instance.gameObject.name == "Crate2")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateReal", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    }
                    else if (__instance.gameObject.name == "Crate2Big")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateRealBig", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } 
                    else if (__instance.gameObject.name == "CrateLong")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CrateLongReal", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } else if (__instance.gameObject.name == "CubeLong")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CubeLong", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } else if (__instance.gameObject.name == "CubeSpinPart")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CubeSpinPart", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        crateReal.GetComponent<PhotonView>().RPC("GetParentAndApply", RpcTarget.All);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } else if (__instance.gameObject.name == "CubeLongStripe")
                    {
                        var crateReal = PhotonNetwork.Instantiate("CubeLongStripe", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } else if (__instance.gameObject.name == "Bomb")
                    {
                        var crateReal = PhotonNetwork.Instantiate("Bomb", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, __instance.transform.lossyScale);
                        //crateReal.GetComponent<PhotonView>().RPC("getSound", RpcTarget.All);
                    } 
                    
                    #region CASTLE
                    
                    else if(__instance.gameObject.name.Contains("(hingeL)"))
                    {
                        var crateReal = PhotonNetwork.Instantiate("trapDoorL", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupHinge", RpcTarget.All, false);
                    }
                    else if(__instance.gameObject.name.Contains("(hingeR)"))
                    {
                        var crateReal = PhotonNetwork.Instantiate("trapDoorR", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupHinge", RpcTarget.All, true);
                    } else if(__instance.gameObject.name == "SpikeBall")
                    {
                        var crateReal = PhotonNetwork.Instantiate("spikeBall", (transform = __instance.transform).position, transform.rotation);
                        //crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupHinge", RpcTarget.All, true);
                    } else if (__instance.gameObject.name.Contains("(Chain2)"))
                    {
                        var crateReal = PhotonNetwork.Instantiate("chain", (transform = __instance.transform).position, transform.rotation);
                        crateReal.GetComponent<PhotonView>().RPC("doScaling", RpcTarget.All, transform.lossyScale);
                        crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupChain", RpcTarget.All, 2);
                    } else if(__instance.gameObject.name == "PlatformLong")
                    {
                        var crateReal = PhotonNetwork.Instantiate("platformLong", (transform = __instance.transform).position, transform.rotation);
                        //crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupHinge", RpcTarget.All, true);
                    } else if(__instance.gameObject.name == "ExtraLongPlatform")
                    {
                        var crateReal = PhotonNetwork.Instantiate("extraLongPlatform", (transform = __instance.transform).position, transform.rotation);
                        //crateReal.GetComponent<PhotonView>().RPC("RPCA_SetupHinge", RpcTarget.All, true);
                    }

                    #region Castle12
                    else if(__instance.gameObject.name == "Castle12Platform1")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle12Platform1", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle12Platform2")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle12Platform2", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle12Platform3")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle12Platform3", (transform = __instance.transform).position, transform.rotation);
                    }
                    #endregion
                    
                    #region Castle13
                    else if(__instance.gameObject.name == "Castle13Platform1")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle13Platform1", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle13Platform2")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle13Platform2", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle13Platform3")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle13Platform3", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle13Platform4")
                    {
                        var crateReal = PhotonNetwork.Instantiate("castle13Platform4", (transform = __instance.transform).position, transform.rotation);
                    }
                    #endregion

                    #region Castle14

                    else if(__instance.gameObject.name == "ScaryBall")
                    {
                        var crateReal = PhotonNetwork.Instantiate("scaryBall", (transform = __instance.transform).position, transform.rotation);
                    }

                    #endregion

                    #region Castle15

                    else if(__instance.gameObject.name == "Castle15Platform1")
                    {
                        PhotonNetwork.Instantiate("castle15Platform1", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle15Platform2")
                    {
                        PhotonNetwork.Instantiate("castle15Platform2", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle15Platform3")
                    {
                        PhotonNetwork.Instantiate("castle15Platform3", (transform = __instance.transform).position, transform.rotation);
                    }

                    #endregion
                    
                    #region Castle16

                    else if(__instance.gameObject.name == "Castle16Platform1")
                    {
                        PhotonNetwork.Instantiate("castle16Platform1", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle16Platform2")
                    {
                        PhotonNetwork.Instantiate("castle16Platform2", (transform = __instance.transform).position, transform.rotation);
                    }

                    #endregion
                    
                    #region Castle17

                    else if(__instance.gameObject.name == "Castle17Platform1")
                    {
                        PhotonNetwork.Instantiate("castle17Platform1", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle17Platform2")
                    {
                        PhotonNetwork.Instantiate("castle17Platform2", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle17Platform3")
                    {
                        PhotonNetwork.Instantiate("castle17Platform3", (transform = __instance.transform).position, transform.rotation);
                    } else if(__instance.gameObject.name == "Castle17Platform4")
                    {
                        PhotonNetwork.Instantiate("castle17Platform4", (transform = __instance.transform).position, transform.rotation);
                    }

                    #endregion
                    
                    #endregion
                    
                    #region Factory
                    
                    #region Factory1
                    
                    else if(__instance.gameObject.name == "Factory1Platform1")
                    {
                        PhotonNetwork.Instantiate("factory1Platform1", (transform = __instance.transform).position, transform.rotation);
                    }
                    
                    #endregion
                    
                    #region Factory2
                    
                    else if(__instance.gameObject.name == "Factory2Platform1")
                    {
                        PhotonNetwork.Instantiate("factory2Platform1", (transform = __instance.transform).position, transform.rotation);
                    }
                    else if(__instance.gameObject.name == "Factory2Platform2")
                    {
                        PhotonNetwork.Instantiate("factory2Platform2", (transform = __instance.transform).position, transform.rotation);
                    }
                    
                    #endregion
                    
                    else if(__instance.gameObject.name == "(Pusher)")
                    {
                        PhotonNetwork.Instantiate("Pusher", (transform = __instance.transform).position, transform.rotation);
                    }
                    
                    #endregion
                    
                    else
                    {
                        var box = PhotonNetwork.Instantiate("4 Map Objects/" + __instance.gameObject.name.Split(new char[]
                        {
                            char.Parse(" ")
                        })[0], (transform = __instance.transform).position, transform.rotation);
                        box.name += " " + __instance.name;
                    }
                }

                var missingObjects = (int) Traverse.Create(___map).Field("missingObjects").GetValue();
                Traverse.Create(___map).Field("missingObjects").SetValue(missingObjects+1);
                ___waitingToBeRemoved = true;
            }
            return false;
        }
    }

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