// using HarmonyLib;
// using UnboundLib;
// using UnityEngine;
//
// namespace StickFightMaps.Patches
// {
//     [HarmonyPatch(typeof(CharacterCreatorItemEquipper), "Start")]
//     public class CharacterCreatorItemEquipperPatchStart
//     {
//         public static void Postfix(CharacterCreatorItemEquipper __instance)
//         {
//             StickFightMaps.instance.ExecuteAfterSeconds(0.1f, () =>
//             {
//                 foreach (var ren in __instance.GetComponentsInChildren<SpriteRenderer>())
//                 {
//                     ren.sortingLayerName = "Player10";
//                 }
//             });
//         }
//     }
// }