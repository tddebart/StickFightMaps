using System.Reflection;
using UnityEngine;

namespace StickFightMaps
{
    public static class PlayerVelocityExtension
    {
        public static void AddForce(this PlayerVelocity vel, Vector2 direction)
        {
            typeof(PlayerVelocity).InvokeMember("AddForce",
                BindingFlags.Instance | BindingFlags.InvokeMethod |
                BindingFlags.NonPublic, null, vel, new object[]{direction});
        }
    }
}