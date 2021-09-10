using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class Crate : MonoBehaviour
    {
        [PunRPC]
        public void getSound()
        {
            var box = StickFightMaps.boxOrg;
            var soundImpactOrg = Instantiate(box.GetComponent<NetworkPhysicsObject>().soundBoxImpact);
            GetComponent<NetworkPhysicsObject>().soundBoxImpact = soundImpactOrg;
        }
    }
}