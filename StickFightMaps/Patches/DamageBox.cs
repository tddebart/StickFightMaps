using HarmonyLib;
using Sonigon;
using UnityEngine;

namespace StickFightMaps.Patches
{
    // [HarmonyPatch(typeof(DamageBox), "Collide")]
    // public class DamageBoxPatch
    // {
    //     public static bool Prefix(Collision2D collision, DamageBox __instance, ref float ___time, SpawnedAttack ___spawned)
    //     {
    //         // if (Time.time < ___time + __instance.cd)
    //         // {
    //         //     return false;
    //         // }
    //         //
    //         // Vector3 vector = __instance.transform.root.forward;
    //         // if (__instance.towardsCenterOfMap)
    //         // {
    //         //     vector = -collision.contacts[0].point.normalized;
    //         // }
    //         // if (__instance.awayFromMe)
    //         // {
    //         //     vector = (collision.transform.position - __instance.transform.position).normalized;
    //         // }
    //         // Damagable componentInParent = collision.transform.GetComponentInParent<Damagable>();
    //         // if (componentInParent)
    //         // {
    //         //     UnityEngine.Debug.LogWarning("comp");
    //         //     ___time = Time.time;
    //         //     UnityEngine.Debug.LogWarning("Time");
    //         //     HealthHandler component = componentInParent.GetComponent<HealthHandler>();
    //         //     UnityEngine.Debug.LogWarning("health");
    //         //     CharacterData component2 = component.GetComponent<CharacterData>();
    //         //     UnityEngine.Debug.LogWarning("Health and data");
    //         //     if (component2 && !component2.view.IsMine)
    //         //     {
    //         //         return false;
    //         //     }
    //         //     if (component)
    //         //     {
    //         //         component.CallTakeForce(vector * __instance.force, ForceMode2D.Impulse, false, __instance.ignoreBlock, __instance.setFlyingFor);
    //         //     }
    //         //
    //         //     UnityEngine.Debug.LogWarning("Force");
    //         //     componentInParent.CallTakeDamage(__instance.damage * vector, __instance.transform.position, null, (___spawned != null) ? ___spawned.spawner : null, true);
    //         //     if (__instance.soundPlaySawDamage)
    //         //     {
    //         //         SoundManager.Instance.PlayAtPosition(__instance.soundSawDamage, SoundManager.Instance.GetTransform(), __instance.transform);
    //         //     }
    //         //     if (__instance.dmgPart)
    //         //     {
    //         //         Vector3 forward = vector;
    //         //         vector.z = 0f;
    //         //         __instance.dmgPart.transform.parent.rotation = Quaternion.LookRotation(forward);
    //         //         __instance.dmgPart.Play();
    //         //     }
    //         //     // if (__instance.shake != 0f)
    //         //     // {
    //         //     //     component2.player.Call_AllGameFeel(__instance.shake * vector);
    //         //     // }
    //         // }
    //
    //         //return false;
    //         //return collision != null && collision.transform.parent && collision.transform.GetComponentInParent<Damagable>() && collision.transform.GetComponentInParent<Damagable>().GetComponent<HealthHandler>();
    //     }
    // }
}