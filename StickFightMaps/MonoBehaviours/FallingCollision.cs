using Photon.Pun;
using UnboundLib.Utils;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    
    public class FallingCollision : MonoBehaviour
    {

        public TimeSince timeSinceStart = 0;
        public float timeNeededAlive = 0.5f;

        private FallingPlatform plat;

        private void Start()
        {
            plat = GetComponentInParent<FallingPlatform>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (plat.done)
            {
                return;
            }
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
            }
        }
    }
}