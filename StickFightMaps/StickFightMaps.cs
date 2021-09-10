using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using Photon.Pun;
using Sonigon;
using Sonigon.Internal;
using StickFightMaps.MonoBehaviours;
using UnboundLib;
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
        public const string Version = "0.2.0";

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
            var hinge = trapDoorL.AddComponent<CreateHinge>();
            hinge.right = false;
            hinge.runAwake = false;
            this.ExecuteAfterSeconds(0.5f, () =>
            {
                hinge.runAwake = true;
            });

            PhotonNetwork.PrefabPool.RegisterPrefab("trapDoorL", trapDoorL);
            
            #endregion
            
            #region Create trapDoor right

            var trapDoorR = Instantiate(Resources.Load<GameObject>("4 Map Objects/Box"));
            trapDoorR.AddComponent<BossSlothPhysicsObject>();
            DontDestroyOnLoad(trapDoorR);
            trapDoorR.transform.Translate(new Vector3(1000,0,0));
            var hinge2 = trapDoorR.AddComponent<CreateHinge>();
            hinge2.right = true;
            hinge2.runAwake = false;
            this.ExecuteAfterSeconds(0.5f, () =>
            {
                hinge2.runAwake = true;
            });

            PhotonNetwork.PrefabPool.RegisterPrefab("trapDoorR", trapDoorR);
            
            #endregion

            LevelManager.RegisterMaps(levelAsset, "StickFight");
        }

    }
}