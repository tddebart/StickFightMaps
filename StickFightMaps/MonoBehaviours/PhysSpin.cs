using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class PhysSpin : MonoBehaviour
    {
        public int spinAmount = 100000;
        public Rigidbody2D rig;

        private void Start()
        {
            rig = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            rig.AddTorque(spinAmount);
        }
    }
}