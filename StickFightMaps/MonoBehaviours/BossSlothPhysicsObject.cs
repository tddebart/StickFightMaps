using System.Collections.Generic;
using Photon.Pun;
using Sonigon;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class BossSlothPhysicsObject : MonoBehaviour
    {
        public void DoThings()
        {
            this.gameObject.layer = 17;
            // Add photonView
            var view = gameObject.AddComponent<PhotonView>();
            view.Group = 0;
            view.prefixField = -1;
            view.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
            view.Synchronization = ViewSynchronization.UnreliableOnChange;
            view.OwnershipTransfer = OwnershipOption.Takeover;
            view.ObservedComponents = new List<Component>()
            {
                GetComponent<NetworkPhysicsObject>()
            };
            var networkObject = gameObject.GetComponent<NetworkPhysicsObject>();
            networkObject.soundBoxImpact = ScriptableObject.CreateInstance<SoundEvent>();
        }
        
        [PunRPC]
        public void doScaling(Vector3 globalScale)
        {
            var transform1 = transform;
            transform1.localScale = Vector3.one;
            var lossyScale = transform1.lossyScale;
            transform1.localScale = new Vector3 (globalScale.x/lossyScale.x, globalScale.y/lossyScale.y, globalScale.z/lossyScale.z);
        }

        [PunRPC]
        public void doRotation()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0, 15));
        }
    }
}