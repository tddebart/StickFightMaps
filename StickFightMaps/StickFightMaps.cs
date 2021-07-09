using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
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
        public const string Version = "0.1.0";

        private static AssetBundle levelAsset;

        private void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            
            levelAsset = AssetUtils.LoadAssetBundleFromResources("sticklevels", typeof(StickFightMaps).Assembly);
            if (levelAsset == null)
            {
                UnityEngine.Debug.LogError("Couldn't find levelAsset?");
            }
            
            Unbound.RegisterMaps(levelAsset);
        }
        
    }
}