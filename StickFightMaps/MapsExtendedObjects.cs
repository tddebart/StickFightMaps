using System;
using MapsExt;
using MapsExt.MapObjects;
using StickFightMaps.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace StickFightMaps
{
    /*
    #region Woods

    #region GroundWoods

    [EditorMapObjectSpec(typeof(GroundWoods), "Ground", "StickFight | Woods")]
    public static class EditorGroundWoods
    {
        [EditorMapObjectPrefab] public static GameObject Prefab => GroundWoodsSpec.Prefab;

        [EditorMapObjectSerializer]
        public static SerializerAction<GroundWoods> Serialize => EditorSpatialSerializer.BuildSerializer<GroundWoods>(GroundWoodsSpec.Serialize);

        [EditorMapObjectDeserializer]
        public static DeserializerAction<GroundWoods> Deserialize =>
            EditorSpatialSerializer.BuildDeserializer<GroundWoods>(GroundWoodsSpec.Deserialize);
    }

    public class GroundWoods : SpatialMapObject
    {
    }

    [MapObjectSpec(typeof(GroundWoods))]
    public static class GroundWoodsSpec
    {
        [MapObjectPrefab]
        public static GameObject Prefab => StickFightMaps.levelObjects.LoadAsset<GameObject>("GroundWoods");

        [MapObjectSerializer]
        public static void Serialize(GameObject instance, GroundWoods target)
        {
            SpatialSerializer.Serialize(instance, target);
        }

        [MapObjectDeserializer]
        public static void Deserialize(GroundWoods data, GameObject target)
        {
            SpatialSerializer.Deserialize(data, target);
            // BoilerPlate code
            StickFightMaps.instance.ExecuteAfterFrames(1, () =>
            {
                GameObject.Destroy(target.GetComponent<SpriteMask>());
                // GameObject.Destroy(target.GetComponent<SFPolygon>());
            });

            // target.GetComponent<SpriteRenderer>().color = new Color(0.7132353f, 0.5784729f, 0.2884408f);
        }
    }

    #endregion

    #region GroundGrassWoods

    [EditorMapObjectSpec(typeof(GroundGrassWoods), "Ground grass", "StickFight | Woods")]
    public static class EditorGroundGrassWoods
    {
        [EditorMapObjectPrefab] public static GameObject Prefab => GroundGrassWoodsSpec.Prefab;

        [EditorMapObjectSerializer]
        public static SerializerAction<GroundGrassWoods> Serialize => EditorSpatialSerializer.BuildSerializer<GroundGrassWoods>(GroundGrassWoodsSpec.Serialize);

        [EditorMapObjectDeserializer]
        public static DeserializerAction<GroundGrassWoods> Deserialize =>
            EditorSpatialSerializer.BuildDeserializer<GroundGrassWoods>(GroundGrassWoodsSpec.Deserialize);
    }

    public class GroundGrassWoods : SpatialMapObject
    {
    }

    [MapObjectSpec(typeof(GroundGrassWoods))]
    public static class GroundGrassWoodsSpec
    {
        [MapObjectPrefab] public static GameObject Prefab => StickFightMaps.levelObjects.LoadAsset<GameObject>("GroundGrassWoods");

        [MapObjectSerializer]
        public static void Serialize(GameObject instance, GroundGrassWoods target)
        {
            SpatialSerializer.Serialize(instance, target);
        }

        [MapObjectDeserializer]
        public static void Deserialize(GroundGrassWoods data, GameObject target)
        {
            SpatialSerializer.Deserialize(data, target);
            // BoilerPlate code
            StickFightMaps.instance.ExecuteAfterFrames(1, () =>
            {
                GameObject.Destroy(target.GetComponent<SpriteMask>());
                // GameObject.Destroy(target.GetComponent<SFPolygon>());
            });

            // target.GetComponent<SpriteRenderer>().color = new Color(0.7132353f, 0.5784729f, 0.2884408f);
        }
    }

    #endregion

    #region BushWoods

    [EditorMapObjectSpec(typeof(BushWoods), "Bush", "StickFight | Woods")]
    public static class EditorBushWoods
    {
        [EditorMapObjectPrefab] public static GameObject Prefab => BushWoodsSpec.Prefab;

        [EditorMapObjectSerializer]
        public static SerializerAction<BushWoods> Serialize => EditorSpatialSerializer.BuildSerializer<BushWoods>(BushWoodsSpec.Serialize);

        [EditorMapObjectDeserializer]
        public static DeserializerAction<BushWoods> Deserialize =>
            EditorSpatialSerializer.BuildDeserializer<BushWoods>(BushWoodsSpec.Deserialize);
    }

    public class BushWoods : SpatialMapObject
    {
    }

    [MapObjectSpec(typeof(BushWoods))]
    public static class BushWoodsSpec
    {
        [MapObjectPrefab] public static GameObject Prefab => StickFightMaps.levelObjects.LoadAsset<GameObject>("BushWoods");

        [MapObjectSerializer]
        public static void Serialize(GameObject instance, BushWoods target)
        {
            SpatialSerializer.Serialize(instance, target);
        }

        [MapObjectDeserializer]
        public static void Deserialize(BushWoods data, GameObject target)
        {
            SpatialSerializer.Deserialize(data, target);
            // BoilerPlate code
            StickFightMaps.instance.ExecuteAfterFrames(1, () =>
            {
                GameObject.Destroy(target.GetComponent<SpriteMask>());
                GameObject.Destroy(target.GetComponent<SFPolygon>());
            });

            // target.GetComponent<SpriteRenderer>().color = new Color(0.7132353f, 0.5784729f, 0.2884408f);
        }
    }

    #endregion
    
    #endregion
    
    
    #region Crate
    [EditorMapObjectSpec(typeof(Crate), "Crate", "StickFight")]
    public static class EditorBoxSpecial
    {
        [EditorMapObjectPrefab]
        public static GameObject Prefab => CrateSpec.Prefab;
        [EditorMapObjectSerializer]
        public static SerializerAction<Crate> Serialize => EditorSpatialSerializer.BuildSerializer<Crate>(CrateSpec.Serialize);
        [EditorMapObjectDeserializer]
        public static DeserializerAction<Crate> Deserialize => EditorSpatialSerializer.BuildDeserializer<Crate>(CrateSpec.Deserialize);
    }

    public class Crate : SpatialMapObject { }

    [MapObjectSpec(typeof(Crate))]
    public static class CrateSpec
    {
        [MapObjectPrefab] public static GameObject Prefab => StickFightMaps.levelObjects.LoadAsset<GameObject>("CrateReal");

        [MapsExt.MapObjectSerializer]
        public static void Serialize(GameObject instance, Crate target)
        {
            SpatialSerializer.Serialize(instance, target);
        }

        [MapObjectDeserializer]
        public static void Deserialize(Crate data, GameObject target)
        {
            SpatialSerializer.Deserialize(data, target);
            // BoilerPlate code
            StickFightMaps.instance.ExecuteAfterFrames(1, () =>
            {
                GameObject.Destroy(target.GetComponent<SpriteMask>());
                GameObject.Destroy(target.GetComponent<SFPolygon>());
            });

            target.GetComponent<SpriteRenderer>().color = new Color(0.7132353f, 0.5784729f, 0.2884408f);
        }
    }
    #endregion
    */
}