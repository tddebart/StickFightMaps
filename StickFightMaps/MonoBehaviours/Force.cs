using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class Force : MonoBehaviour
    {
        private void Start()
        {
            this.rig = base.GetComponent<Rigidbody2D>();
            this.rig.AddForce(transform.right * force);
        }

        public void Update()
        {
            rig.velocity = -Vector3.right * 20;
        }

        public float force;
        
        private Rigidbody2D rig;
    }
}