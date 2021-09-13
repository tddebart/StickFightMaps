using System.Collections;
using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
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
        public const string Version = "0.3.0";

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

            #region Create CrateReal
            // Create CrateReal
            var crate = levelObjects.LoadAsset<GameObject>("CrateReal");
            
            // Add physics object
            crate.AddComponent<BossSlothPhysicsObject>().DoThings();
            
            // Add Crate Mono
            crate.AddComponent<Crate>();

            PhotonNetwork.PrefabPool.RegisterPrefab("CrateReal", crate);
            #endregion

            #region Create CrateRealBig

            // Create CrateRealBig
            var crateBig = levelObjects.LoadAsset<GameObject>("CrateRealBig");

            // Add physics object
            crateBig.AddComponent<BossSlothPhysicsObject>().DoThings();
            
            // Add Crate Mono
            crateBig.AddComponent<Crate>();
            
            PhotonNetwork.PrefabPool.RegisterPrefab("CrateRealBig", crateBig);

            #endregion
            
            #region Create CrateLongReal

            // Create CrateLongReal
            var crateLong = levelObjects.LoadAsset<GameObject>("CrateLongReal");
            
            // Add physics object
            crateLong.AddComponent<BossSlothPhysicsObject>().DoThings();
            
            // Add Crate Mono
            crateLong.AddComponent<Crate>();
            
            PhotonNetwork.PrefabPool.RegisterPrefab("CrateLongReal", crateLong);

            #endregion

            // #region Create CubeLong
            // // Create CrateLongReal
            // var cubeLong = levelObjects.LoadAsset<GameObject>("CubeLong");
            //
            // // Add physics object
            // cubeLong.AddComponent<BossSlothPhysicsObject>().DoThings();
            //
            // PhotonNetwork.PrefabPool.RegisterPrefab("CubeLong", cubeLong);
            //
            // #endregion
            //
            // #region Create CubeLongReal
            // // Create CrateLongReal
            // var CubeLongStripe = levelObjects.LoadAsset<GameObject>("CubeLongStripe");
            //
            // // Add physics object
            // CubeLongStripe.AddComponent<BossSlothPhysicsObject>().DoThings();
            //
            // PhotonNetwork.PrefabPool.RegisterPrefab("CubeLongStripe", CubeLongStripe);
            //
            // #endregion
            //
            // #region Create CrateSpin
            // // Create CrateSpin
            // var cubeSpin = levelObjects.LoadAsset<GameObject>("CubeSpinPart");
            //
            // // Add physics object
            // cubeSpin.AddComponent<BossSlothPhysicsObject>().DoThings();
            // cubeSpin.AddComponent<SpinPart>();
            //
            // PhotonNetwork.PrefabPool.RegisterPrefab("CubeSpinPart", cubeSpin);
            //
            // #endregion
            //
            // #region Create Bomb
            // // Create CrateSpin
            // var bomb = levelObjects.LoadAsset<GameObject>("BombReal");
            //
            // // Add physics object
            // bomb.AddComponent<BossSlothPhysicsObject>().DoThings();
            //
            // var objDelay = bomb.AddComponent<SpawnObjectAfterDelay>();
            // //objDelay.objectToSpawn = levelObjects.LoadAsset<GameObject>("A_Explosion_Timed_Detonation");
            // objDelay.objectToSpawn = levelObjects.LoadAsset<GameObject>("ExplosionBig");
            // objDelay.secondsBeforeSpawn = 20;
            // objDelay.removeSelf = true;
            // objDelay.specificSpawnRotation = new Vector3(0, 90, 0);
            //
            // bomb.AddComponent<Bomb>();
            //
            // PhotonNetwork.PrefabPool.RegisterPrefab("Bomb", bomb);
            //
            // #endregion

            #region Create trapDoor left
            
            var trapDoorL = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            trapDoorL.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(trapDoorL);
            trapDoorL.transform.Translate(new Vector3(1000,0,0));
            trapDoorL.name = "TrapDoorL(Trap)";
            //var hinge = trapDoorL.AddComponent<CreateHinge>();
            //hinge.right = false;
            //hinge.runAwake = false;
            // this.ExecuteAfterSeconds(0.5f, () =>
            // {
            //     hinge.runAwake = true;
            // });
            
            PhotonNetwork.PrefabPool.RegisterPrefab("trapDoorL", trapDoorL);
            
            #endregion
            
            #region Create trapDoor right
            
            var trapDoorR = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            trapDoorR.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(trapDoorR);
            trapDoorR.transform.Translate(new Vector3(1000,0,0));
            trapDoorR.name = "TrapDoorR(Trap)";
            // var hinge2 = trapDoorR.AddComponent<CreateHinge>();
            // hinge2.right = true;
            // hinge2.runAwake = false;
            // this.ExecuteAfterSeconds(0.5f, () =>
            // {
            //     hinge2.runAwake = true;
            // });

            PhotonNetwork.PrefabPool.RegisterPrefab("trapDoorR", trapDoorR);
            
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
            
            // #region Create circle
            //
            // var circle = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            // DontDestroyOnLoad(circle);
            // Destroy(circle.GetComponent<BoxCollider2D>());
            // Destroy(circle.GetComponent<SFPolygon>());
            // Destroy(circle.GetComponent<NetworkPhysicsObject>());
            // circle.tag = "NoMask";
            //
            // circle.transform.Translate(new Vector3(0,200,1000));
            //
            // Destroy(circle.transform.GetChild(0).gameObject);
            // Destroy(circle.transform.GetChild(1).gameObject);
            //
            // //circle.GetComponent<SpriteRenderer>().sprite = levelObjects.LoadAsset<Sprite>("Beard_01");
            // circle.gameObject.AddComponent<CircleCollider2D>();
            //
            // var joint = circle.gameObject.AddComponent<SpringJoint2D>();
            // joint.anchor = new Vector2(0.5f, 0);
            // joint.autoConfigureConnectedAnchor = true;
            // joint.enableCollision = true;
            // joint.dampingRatio = 0.25f;
            // joint.frequency = 2;
            //
            // var transformSync = circle.gameObject.AddComponent<PhotonTransformView>();
            // circle.GetComponent<PhotonView>().ObservedComponents.Add(transformSync);
            // transformSync.m_SynchronizePosition = true;
            // transformSync.m_SynchronizeRotation = true;
            //
            // PhotonNetwork.PrefabPool.RegisterPrefab("circle", circle);
            //
            // #endregion
            
            #region Create SpikeBall

            var spikeball = levelObjects.LoadAsset<GameObject>("SpikeBall");

            spikeball.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in spikeball.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }
            
            
            PhotonNetwork.PrefabPool.RegisterPrefab("spikeBall", spikeball);
            
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

            PhotonNetwork.PrefabPool.RegisterPrefab("chain", chain);
            
            #endregion
            
            #region Create PlatformLong

            var platLong = levelObjects.LoadAsset<GameObject>("PlatformLong");

            platLong.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in platLong.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("platformLong", platLong);
            
            #endregion
            
            #region Create ExtraLongPlatform

            var extraLongPlat = levelObjects.LoadAsset<GameObject>("ExtraLongPlatform");

            extraLongPlat.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in extraLongPlat.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("extraLongPlatform", extraLongPlat);
            
            #endregion
            
            #region Castle12

            #region Create Castle12Platform1

            var castle12Platform1 = levelObjects.LoadAsset<GameObject>("Castle12Platform1");

            castle12Platform1.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform1.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle12Platform1", castle12Platform1);
            
            #endregion
            
            #region Create Castle12Platform2

            var castle12Platform2 = levelObjects.LoadAsset<GameObject>("Castle12Platform2");

            castle12Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle12Platform2", castle12Platform2);
            
            #endregion
            
            #region Create Castle12Platform3

            var castle12Platform3 = levelObjects.LoadAsset<GameObject>("Castle12Platform3");

            castle12Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle12Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle12Platform3", castle12Platform3);
            
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

            PhotonNetwork.PrefabPool.RegisterPrefab("castle13Platform1", castle13Platform1);
            
            #endregion
            
            #region Create Castle13Platform2

            var castle13Platform2 = levelObjects.LoadAsset<GameObject>("Castle13Platform2");

            castle13Platform2.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform2.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle13Platform2", castle13Platform2);
            
            #endregion
            
            #region Create Castle13Platform3

            var castle13Platform3 = levelObjects.LoadAsset<GameObject>("Castle13Platform3");

            castle13Platform3.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform3.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle13Platform3", castle13Platform3);
            
            #endregion
            
            #region Create Castle13Platform4

            var castle13Platform4 = levelObjects.LoadAsset<GameObject>("Castle13Platform4");

            castle13Platform4.AddComponent<BossSlothPhysicsObject>().DoThings();
            foreach (var trns in castle13Platform4.GetComponentsInChildren<Transform>())
            {
                trns.gameObject.layer = 17;
            }

            PhotonNetwork.PrefabPool.RegisterPrefab("castle13Platform4", castle13Platform4);
            
            #endregion
            
            #endregion

            LevelManager.RegisterMaps(levelAsset, "StickFight");
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

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.K) && PhotonNetwork.IsMasterClient)
        //     {
        //         var box = PhotonNetwork.Instantiate("4 Map Objects/Box", Vector3.zero, Quaternion.identity);
        //         //box.AddComponent<SpringJoint2D>();
        //     }
        // }
    }
}