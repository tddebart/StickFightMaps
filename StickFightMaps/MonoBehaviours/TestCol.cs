using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class TestCol : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D other)
        {
            UnityEngine.Debug.LogWarning("We got entered2d");
        }
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            UnityEngine.Debug.LogWarning("Staying");
        }

        private void OnCollisionEnter(Collision other)
        {
            UnityEngine.Debug.LogWarning("We got entered");
        }
    }
}