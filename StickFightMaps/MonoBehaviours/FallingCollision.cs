using Photon.Pun;
using UnboundLib.Utils;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    
    public class FallingCollision : MonoBehaviour
    {
        public bool done;

        public TimeSince timeSinceStart = 0;
        public float timeNeededAlive;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (done || timeSinceStart > timeNeededAlive)
            {
                return;
            }

            done = true;
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponentInParent<PhotonView>().RPC("RPCA_Fall", RpcTarget.All);
            }
        }
    }
}