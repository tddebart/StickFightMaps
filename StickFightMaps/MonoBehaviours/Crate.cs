using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class Crate : MonoBehaviour
    {
        [PunRPC]
        public void doScaling(Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3 (globalScale.x/transform.lossyScale.x, globalScale.y/transform.lossyScale.y, globalScale.z/transform.lossyScale.z);
        }

        [PunRPC]
        public void getSound()
        {
            var box = StickFightMaps.boxOrg;
            var soundImpactOrg = Instantiate(box.GetComponent<NetworkPhysicsObject>().soundBoxImpact);
            GetComponent<NetworkPhysicsObject>().soundBoxImpact = soundImpactOrg;
        }
    }
}