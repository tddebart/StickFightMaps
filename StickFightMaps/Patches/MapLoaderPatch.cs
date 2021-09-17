using System;
using HarmonyLib;
using StickFightMaps.MonoBehaviours;
using UnboundLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace StickFightMaps
{
    [HarmonyPatch(typeof(MapManager), "OnLevelFinishedLoading")]
    class MapManager_Patch_OnLevelFinishedLoading
    {
        private static void Postfix(Scene scene, MapManager __instance)
        {
            foreach (Transform obj in scene.GetRootGameObjects()[0].GetComponentsInChildren<Transform>())
            {
                //obj.gameObject.layer = 0;
                if (obj.name.IndexOf("(Rope)", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    StickFightMaps.instance.ExecuteAfterSeconds(0.05f, () =>
                    {
                        GameObject.Destroy(obj.GetComponent<SpriteMask>());
                        GameObject.Destroy(obj.transform.GetChild(0).GetComponent<SpriteMask>());
                    });
                    obj.GetComponent<MapObjet_Rope>().soundRopeLoop = Resources.Load<GameObject>("4 map objects/Box")
                        .GetComponent<NetworkPhysicsObject>().soundBoxImpact;
                    continue;
                }

                if (obj.name.IndexOf("TreadMill", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    obj.GetChild(0).gameObject.AddComponent<TreadMill>();
                }
                
                if (obj.name.IndexOf("BoxSpawner", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    obj.gameObject.AddComponent<BoxSpawner>();
                }
                
                if (obj.name.IndexOf("PushSpawner", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    var spawner = obj.gameObject.AddComponent<Spawner>();
                    spawner.prefabName = "Pusher";
                    spawner.cooldown = 1.75f;
                    spawner.AdRandomCooldown = 2f;
                }
                
                if (obj.name.IndexOf("(SpinMill)", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    var spin = obj.gameObject.AddComponent<Spin>();
                    spin.spinVector = new Vector3(0, 0, 300);
                    spin.startCurve = AnimationCurve.Constant(0,1,1);
                    spin.secondsToStart = 0.1f;
                }
                
                if (obj.name.IndexOf("EDITOR", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    obj.gameObject.SetActive(false);
                }

                else if (obj.name.IndexOf("NOT COL", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    // check if color was set to white in unity and change it back to white after 0.1 seconds because it changes
                    if ( obj.GetComponent<SpriteRenderer>() && obj.GetComponent<SpriteRenderer>().color == Color.white)
                    {
                        __instance.ExecuteAfterSeconds(0.1f, () =>
                        {
                            obj.GetComponent<SpriteRenderer>().color = Color.white;
                        });
                    }
                    __instance.ExecuteAfterSeconds(0.1f, () =>
                    {
                        // check if it has the shader or if it doesn't have a SpriteRenderer
                        if ((obj.GetComponent<SpriteRenderer>() && obj.GetComponent<SpriteRenderer>().material.shader == Shader.Find("Sprites/SFSoftShadowStencil")) || !obj.GetComponent<SpriteRenderer>()) return;
                        // remove objects that adds art to it
                        Object.Destroy(obj.GetComponent<SpriteMask>());
                    });
                }

                if (obj.name.Contains("(Fall)"))
                {
                    StickFightMaps.instance.StartCoroutine(StickFightMaps.setupThingsPlatform(obj));

                    // obj.gameObject.AddComponent<FallingPlatform>();
                    // var view = obj.gameObject.AddComponent<PhotonView>();
                    // PhotonNetwork.AllocateViewID(view);
                    // PhotonNetwork.RegisterPhotonView(view);
                }
            }
        }
        
    }
}