using System.Collections.Generic;
using Photon.Pun;
using Sonigon;
using UnboundLib;
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
        public void RPCA_SetupHinge(bool right)
        {
            var obj = gameObject;
            Destroy(obj.GetComponent<SpriteRenderer>());
            if (obj.transform.childCount > 1)
            {
                Destroy(obj.transform.GetChild(0).gameObject);
                Destroy(obj.transform.GetChild(1).gameObject);
            }
            var transformLocalScale = obj.transform.localScale;
            transformLocalScale.x = right ? -8 : 8;
            transformLocalScale.y = 1;
            obj.transform.localScale = transformLocalScale;

            obj.transform.SetZPosition(0);
            
            //obj.transform.Translate(2.5f,0,0);
            var hinge = obj.GetOrAddComponent<HingeJoint2D>();
            hinge.anchor = new Vector2(-0.5f, 0);
                
            hinge.useLimits = true;
            hinge.limits = new JointAngleLimits2D
            {
                max = -90,
                min = 90
            };
            hinge.enableCollision = true;
            
            // var spring = obj.GetOrAddComponent<SpringJoint2D>();
            // spring.anchor = new Vector2(0.5f, 0);
            // //spring.autoConfigureConnectedAnchor = true;
            // spring.enableCollision = true;
            // spring.dampingRatio = 0.25f;


            var spriteObj = new GameObject("sprite");
            spriteObj.transform.parent = obj.transform;
            var ren = spriteObj.AddComponent<SpriteRenderer>();
            ren.sprite = StickFightMaps.levelObjects.LoadAsset<Sprite>("Castle_Platform1");
            spriteObj.transform.localScale = new Vector3(1 / 4.3f, 2f, 1);
            spriteObj.transform.localPosition = Vector3.zero;
            spriteObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

            Destroy(obj.GetComponent<SpriteMask>());

            GetComponent<PhotonView>().Synchronization = ViewSynchronization.Unreliable;

            if (obj.GetComponent<SpringJoint2D>())
                obj.GetComponent<SpringJoint2D>().dampingRatio = 0.25f;
        }
    }
}