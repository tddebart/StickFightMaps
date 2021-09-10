using System.Net.NetworkInformation;
using HarmonyLib;
using Photon.Pun;
using UnboundLib;
using UnityEngine;

namespace StickFightMaps.Patches
{
    [HarmonyPatch(typeof(Map))]
    public class MapPatch
    {
        [HarmonyPatch("Start")]
        public static bool Prefix(Map __instance, int ___levelID)
        {
            if (!PhotonNetwork.OfflineMode)
            {
                MapManager.instance.InvokeMethod("ReportMapLoaded", ___levelID);
            }
            if (MapManager.instance.isTestingMap)
            {
                __instance.wasSpawned = true;
            }
            if (!__instance.wasSpawned)
            {
                MapManager.instance.UnloadScene(__instance.gameObject.scene);
            }
            SpriteRenderer[] componentsInChildren = __instance.GetComponentsInChildren<SpriteRenderer>(true);
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if ((double)componentsInChildren[i].color.a >= 0.5)
                {
                    if (!componentsInChildren[i].gameObject.CompareTag("NoMask"))
                    {
                        componentsInChildren[i].transform.position = new Vector3(componentsInChildren[i].transform.position.x, componentsInChildren[i].transform.position.y, -3f);
                        componentsInChildren[i].color = new Color(0.21568628f, 0.21568628f, 0.21568628f);
                        if (!componentsInChildren[i].GetComponent<SpriteMask>())
                        {
                            componentsInChildren[i].gameObject.AddComponent<SpriteMask>().sprite = componentsInChildren[i].sprite;
                        }
                    }
                }
            }
            SpriteMask[] componentsInChildren2 = __instance.GetComponentsInChildren<SpriteMask>();
            for (int j = 0; j < componentsInChildren2.Length; j++)
            {
                if (!componentsInChildren2[j].gameObject.CompareTag("NoMask"))
                {
                    componentsInChildren2[j].isCustomRangeActive = true;
                    componentsInChildren2[j].frontSortingLayerID = SortingLayer.NameToID("MapParticle");
                    componentsInChildren2[j].frontSortingOrder = 1;
                    componentsInChildren2[j].backSortingLayerID = SortingLayer.NameToID("MapParticle");
                    componentsInChildren2[j].backSortingOrder = 0;
                }
            }

            return false;
        }
    }
}