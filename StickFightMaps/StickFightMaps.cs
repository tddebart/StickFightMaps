using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using Photon.Pun;
using Sonigon;
using Sonigon.Internal;
using StickFightMaps.MonoBehaviours;
using UnboundLib;
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

        private void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            
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

            // Add photonView
            var view = crate.AddComponent<PhotonView>();
            view.Group = 0;
            view.prefixField = -1;
            view.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
            view.Synchronization = ViewSynchronization.UnreliableOnChange;
            view.OwnershipTransfer = OwnershipOption.Takeover;
            view.ObservedComponents = new List<Component>()
            {
                crate.GetComponent<NetworkPhysicsObject>()
            };
            
            // Add Crate Mono
            crate.AddComponent<Crate>();
            
            PhotonNetwork.PrefabPool.RegisterPrefab("CrateReal", crate);
            #endregion

            #region Create CrateRealBig

            // Create CrateRealBig
            var crateBig = levelObjects.LoadAsset<GameObject>("CrateRealBig");

            // Add photonView
            var view1 = crateBig.AddComponent<PhotonView>();
            view1.Group = 0;
            view1.prefixField = -1;
            view1.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
            view1.Synchronization = ViewSynchronization.UnreliableOnChange;
            view1.OwnershipTransfer = OwnershipOption.Takeover;
            view1.ObservedComponents = new List<Component>()
            {
                crateBig.GetComponent<NetworkPhysicsObject>()
            };
            
            // Add Crate Mono
            crateBig.AddComponent<Crate>();
            
            PhotonNetwork.PrefabPool.RegisterPrefab("CrateRealBig", crateBig);

            #endregion
            
            #region Create CrateLongReal

            // Create CrateLongReal
            var crateLong = levelObjects.LoadAsset<GameObject>("CrateLongReal");

            // Add photonView
            var view2 = crateLong.AddComponent<PhotonView>();
            view2.Group = 0;
            view2.prefixField = -1;
            view2.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
            view2.Synchronization = ViewSynchronization.UnreliableOnChange;
            view2.OwnershipTransfer = OwnershipOption.Takeover;
            view2.ObservedComponents = new List<Component>()
            {
                crateLong.GetComponent<NetworkPhysicsObject>()
            };
            
            // Add Crate Mono
            crateLong.AddComponent<Crate>();
            
            PhotonNetwork.PrefabPool.RegisterPrefab("CrateLongReal", crateLong);

            #endregion
            
            Unbound.RegisterMaps(levelAsset);
        }
        
    }
}