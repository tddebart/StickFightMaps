using System;
using System.Collections;
using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using MapsExt;
using Photon.Pun;
using StickFightMaps.MonoBehaviours;
using UnboundLib.Utils;
using UnityEngine;

namespace StickFightMaps
{
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class StickFightMaps : BaseUnityPlugin
    {
        
        private const string ModId = "com.bosssloth.rounds.StickFightMaps";
        private const string ModName = "StickFightMaps";
        public const string Version = "0.3.2";

        internal static AssetBundle levelAsset;
        internal static AssetBundle levelObjects;

        internal static GameObject boxOrg = null;

        internal static StickFightMaps instance;

        internal static bool didWarning;

        private void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            

            instance = this;
            
            // Load assetBundles
            levelAsset = AssetUtils.LoadAssetBundleFromResources("sticklevels", typeof(StickFightMaps).Assembly);
            if (levelAsset == null)
            {
                UnityEngine.Debug.LogError("Couldn't find levelAsset?");
            }
            levelObjects = AssetUtils.LoadAssetBundleFromResources("stickobjects", typeof(StickFightMaps).Assembly);
            if (levelObjects == null)
            {
                UnityEngine.Debug.LogError("Couldn't find levelObjects");
            }

            #region Create sounds
            

            #endregion

            var customBox = Instantiate(Resources.Load<GameObject>("4 map objects/Box"));
            DontDestroyOnLoad(customBox);

            customBox.AddComponent<BossSlothPhysicsObject>();
            customBox.transform.Translate(1000,1000,0);

            PhotonNetwork.PrefabPool.RegisterPrefab("customBox", customBox);

            #region DESERT

            #region Create CrateReal
            // Create CrateReal
            var crate = levelObjects.LoadAsset<GameObject>("CrateReal");
            
            // Add physics object
            crate.AddComponent<BossSlothPhysicsObject>().DoThings();

            // mapObjects.Add(crate);            
            //
            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Crate2", crate);
            #endregion

            #region Create CrateRealBig

            // Create CrateRealBig
            var crateBig = levelObjects.LoadAsset<GameObject>("CrateRealBig");

            // Add physics object
            crateBig.AddComponent<BossSlothPhysicsObject>().DoThings();

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Crate2Big", crateBig);

            #endregion
            
            #region Create CrateLongReal

            // Create CrateLongReal
            var crateLong = levelObjects.LoadAsset<GameObject>("CrateLongReal");
            
            // Add physics object
            crateLong.AddComponent<BossSlothPhysicsObject>().DoThings();

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/CrateLong", crateLong);

            #endregion

            #endregion

            #region CASTLE
            
            #region Create trapDoor left
            
            var trapDoorL = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            trapDoorL.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(trapDoorL);
            trapDoorL.transform.Translate(new Vector3(1000,0,0));
            trapDoorL.name = "TrapDoorL(Trap)";
            trapDoorL.GetComponent<SpriteRenderer>().sortingLayerName = "Front";
            //var hinge = trapDoorL.AddComponent<CreateHinge>();
            //hinge.right = false;
            //hinge.runAwake = false;
            // this.ExecuteAfterSeconds(0.5f, () =>
            // {
            //     hinge.runAwake = true;
            // });
            
            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Box(hingeL)", trapDoorL);
            
            #endregion
            
            #region Create trapDoor right
            
            var trapDoorR = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            trapDoorR.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(trapDoorR);
            trapDoorR.transform.Translate(new Vector3(1000,0,0));
            trapDoorR.name = "TrapDoorR(Trap)";
            trapDoorR.GetComponent<SpriteRenderer>().sortingLayerName = "Front";
            // var hinge2 = trapDoorR.AddComponent<CreateHinge>();
            // hinge2.right = true;
            // hinge2.runAwake = false;
            // this.ExecuteAfterSeconds(0.5f, () =>
            // {
            //     hinge2.runAwake = true;
            // });

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Box(hingeR)", trapDoorR);
            
            #endregion
            
            #region Create castle platform Fall

            var platform = Instantiate(levelObjects.LoadAsset<GameObject>("Castle_Platform3(Fall)"));
            DontDestroyOnLoad(platform);
            platform.AddComponent<FallingPlatform>();
            platform.AddComponent<PhotonView>();
            platform.transform.GetChild(0).gameObject.AddComponent<FallingCollision>();
            platform.transform.Translate(new Vector3(0,200,1000));

            PhotonNetwork.PrefabPool.RegisterPrefab("castle_Platform", platform);
            
            #endregion
            
            
            #region Create SpikeBall

            var spikeball = levelObjects.LoadAsset<GameObject>("SpikeBall");

            spikeball.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in spikeball.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }
            
            
            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/SpikeBall", spikeball);
            
            #endregion
            
            #region Create castle chain
            
            var chain = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box_Destructible"));
            chain.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(chain);
            chain.transform.Translate(new Vector3(1500,0,0));
            chain.transform.localScale = new Vector3(3.3f, 2.3f, 2);
            Destroy(chain.GetComponent<SFPolygon>());
            //Destroy(chain.GetComponent<DestructibleBoxDestruction>());
            chain.name = "Chain";

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/(Chain2)", chain);
            
            #endregion
            
            #region Create PlatformLong

            var platLong = levelObjects.LoadAsset<GameObject>("PlatformLong");

            platLong.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in platLong.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/PlatformLong", platLong);
            
            #endregion
            
            #region Create ExtraLongPlatform

            var extraLongPlat = levelObjects.LoadAsset<GameObject>("ExtraLongPlatform");

            extraLongPlat.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in extraLongPlat.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/ExtraLongPlatform", extraLongPlat);
            
            #endregion

            #region Castle12

            #region Create Castle12Platform1

            var castle12Platform1 = levelObjects.LoadAsset<GameObject>("Castle12Platform1");

            castle12Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle12Platform1", castle12Platform1);
            
            #endregion
            
            #region Create Castle12Platform2

            var castle12Platform2 = levelObjects.LoadAsset<GameObject>("Castle12Platform2");

            castle12Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle12Platform2", castle12Platform2);
            
            #endregion
            
            #region Create Castle12Platform3

            var castle12Platform3 = levelObjects.LoadAsset<GameObject>("Castle12Platform3");

            castle12Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle12Platform3", castle12Platform3);
            
            #endregion
            
            #endregion
            
            #region Castle13
            
            #region Create Castle13Platform1

            var castle13Platform1 = levelObjects.LoadAsset<GameObject>("Castle13Platform1");

            castle13Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle13Platform1", castle13Platform1);
            
            #endregion
            
            #region Create Castle13Platform2

            var castle13Platform2 = levelObjects.LoadAsset<GameObject>("Castle13Platform2");

            castle13Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle13Platform2", castle13Platform2);
            
            #endregion
            
            #region Create Castle13Platform3

            var castle13Platform3 = levelObjects.LoadAsset<GameObject>("Castle13Platform3");

            castle13Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle13Platform3", castle13Platform3);
            
            #endregion
            
            #region Create Castle13Platform4

            var castle13Platform4 = levelObjects.LoadAsset<GameObject>("Castle13Platform4");

            castle13Platform4.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform4.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle13Platform4", castle13Platform4);

            #endregion
            
            
            #endregion
            
            #region Create ScaryBall

            var scaryBall = levelObjects.LoadAsset<GameObject>("ScaryBall");

            scaryBall.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in scaryBall.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/ScaryBall", scaryBall);

            #endregion

            #region Castle15

            #region Create Castle15Platform1

            var castle15Platform1 = levelObjects.LoadAsset<GameObject>("Castle15Platform1");

            castle15Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle15Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle15Platform1", castle15Platform1);
            
            #endregion
            
            #region Create Castle15Platform2

            var castle15Platform2 = levelObjects.LoadAsset<GameObject>("Castle15Platform2");

            castle15Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle15Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle15Platform2", castle15Platform2);
            
            #endregion

            #region Create Castle15Platform3

            var castle15Platform3 = levelObjects.LoadAsset<GameObject>("Castle15Platform3");

            castle15Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle15Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle15Platform3", castle15Platform3);
            
            #endregion

            #endregion

            #region Castle16

            #region Create Castle16Platform1

            var castle16Platform1 = levelObjects.LoadAsset<GameObject>("Castle16Platform1");

            castle16Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle16Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle16Platform1", castle16Platform1);
            
            #endregion
            
            #region Create Castle16Platform2

            var castle16Platform2 = levelObjects.LoadAsset<GameObject>("Castle16Platform2");

            castle16Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle16Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle16Platform2", castle16Platform2);
            
            #endregion

            #endregion
            
            #region Castle17

            #region Create Castle17Platform1

            var castle17Platform1 = levelObjects.LoadAsset<GameObject>("Castle17Platform1");

            castle17Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle17Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle17Platform1", castle17Platform1);
            
            #endregion
            
            #region Create Castle17Platform2

            var castle17Platform2 = levelObjects.LoadAsset<GameObject>("Castle17Platform2");

            castle17Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle17Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle17Platform2", castle17Platform2);
            
            #endregion

            #region Create Castle17Platform3

            var castle17Platform3 = levelObjects.LoadAsset<GameObject>("Castle17Platform3");

            castle17Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle17Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle17Platform3", castle17Platform3);
            
            #endregion
            
            #region Create Castle17Platform4

            var castle17Platform4 = levelObjects.LoadAsset<GameObject>("Castle17Platform4");

            castle17Platform4.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle17Platform4.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Castle17Platform4", castle17Platform4);
            
            #endregion

            #endregion
            
            //TODO: spikes on physics objects not properly working
            #endregion

            #region Factory

            #region Factory1

            #region Create factory1Platform1

            var factory1Platform1 = levelObjects.LoadAsset<GameObject>("Factory1Platform1");

            factory1Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in factory1Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            var phys = factory1Platform1.AddComponent<PhysSpin>();
            phys.spinAmount = 300000;

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Factory1Platform1", factory1Platform1);
            
            #endregion

            #endregion
            
            #region Factory2

            #region Create factory2Platform1

            var factory2Platform1 = levelObjects.LoadAsset<GameObject>("Factory2Platform1");

            factory2Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in factory2Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }
            var phys1 = factory2Platform1.AddComponent<PhysSpin>();
            phys1.spinAmount = 1000000;

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Factory2Platform1", factory2Platform1);
            
            #endregion
            
            #region Create factory2Platform2

            var factory2Platform2 = levelObjects.LoadAsset<GameObject>("Factory2Platform2");

            factory2Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in factory2Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }
            var phys2 = factory2Platform2.AddComponent<PhysSpin>();
            phys2.spinAmount = 1000000;

            PhotonNetwork.PrefabPool.RegisterPrefab("4 Map Objects/Factory2Platform2", factory2Platform2);
            
            #endregion

            #endregion

            #region Factory13

            var pusher = levelObjects.LoadAsset<GameObject>("(Pusher)");

            pusher.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in pusher.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }
            
            pusher.AddComponent<Force>().force = -17000000;
            pusher.AddComponent<RemoveAfterSeconds>().seconds = 4f;

            PhotonNetwork.PrefabPool.RegisterPrefab("Pusher", pusher);

            #endregion

            #endregion
            

            LevelManager.RegisterMaps(levelAsset, "StickFight");

            // var assembly = Assembly.GetCallingAssembly();
            
            // this.ExecuteAfterSeconds(0.1f, () => {MapsExtended.instance.RegisterMapObjectsAction?.Invoke(assembly);});
        }
        
        public static IEnumerator setupThingsPlatform(Transform obj)
        {
            var map = MapManager.instance.currentMap.Map;
            var otherPlayersMostRecentlyLoadedLevel = (int) Traverse.Create(MapManager.instance).Field("otherPlayersMostRecentlyLoadedLevel").GetValue();
            var levelID = (int) Traverse.Create(map).Field("levelID").GetValue();
            var loadedForAll = otherPlayersMostRecentlyLoadedLevel == levelID;
            while (MapTransition.isTransitioning || !GameManager.instance.battleOngoing || !GameManager.instance.isPlaying || !map.hasEntered )//&& !PhotonNetwork.OfflineMode && !loadedForAll)
            {
                yield return null;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var platformNeedsEverything = MapManager.instance.currentMap.Scene.name == "Castle9";
                
                var transform = obj.transform;
                var phot = PhotonNetwork.Instantiate("castle_Platform", transform.position, transform.rotation);
                phot.GetComponent<PhotonView>().RPC("RPCA_Setup", RpcTarget.All, obj.transform.lossyScale, obj.transform.position, platformNeedsEverything);
            }
            Destroy(obj.gameObject);
        }

        public static IEnumerator doSomethingOnlyWhenInbattle(Action action)
        {
            while (!GameManager.instance.battleOngoing)
            {
                yield return null;
            }

            yield return new WaitUntil(() => GameManager.instance.battleOngoing);
            action?.Invoke();
        }
    }
}

