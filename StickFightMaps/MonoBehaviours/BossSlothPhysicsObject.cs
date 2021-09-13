using System.Collections.Generic;
using Photon.Pun;
using Sonigon;
using UnboundLib;
using UnityEngine;
using UnityEngine.Events;

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
            networkObject.soundBoxImpact = Resources.Load<GameObject>("4 map objects/Box").GetComponent<NetworkPhysicsObject>().soundBoxImpact;
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

        [PunRPC]
        public void RPCA_SetupChain(int chainID)
        {
            var obj = gameObject;
            //obj.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            //Destroy(obj.GetComponent<BoxCollider2D>());
            Destroy(obj.GetComponent<SpriteRenderer>());
            if (obj.transform.childCount > 1)
            {
                Destroy(obj.transform.GetChild(0).gameObject);
                //Destroy(obj.transform.GetChild(1).gameObject);
            }
            
            var spriteObj = new GameObject("sprite");
            spriteObj.layer = 17;
            spriteObj.transform.parent = obj.transform;
            var ren = spriteObj.AddComponent<SpriteRenderer>();
            ren.sprite = StickFightMaps.levelObjects.LoadAsset<Sprite>("Castle_Chain" + chainID);
            ren.color = new Color(0.4191176f,0.4191176f,0.4191176f);
            spriteObj.transform.localScale = new Vector3(1.5f, 1.05f, 1);
            spriteObj.transform.localPosition = Vector3.zero;
            spriteObj.transform.localRotation = Quaternion.Euler(new Vector3(0,0,90));

            // var shadowObj = new GameObject("shadow");
            // shadowObj.transform.parent = obj.transform;
            // shadowObj.transform.localPosition = Vector3.zero;
            // shadowObj.transform.localRotation = Quaternion.Euler(new Vector3(0,0,90));
            // var sf = shadowObj.AddComponent<SFPolygon>();
            // sf.verts = new[]
            // {
            //     new Vector2(-0.269364f, 0.43f),
            //     new Vector2(-0.269364f, -0.44f),
            //     new Vector2(-0.249364f, -0.45f),
            //     new Vector2(0.270636f, -0.45f),
            //     new Vector2(0.270636f, 0.42f),
            //     new Vector2(0.260636f, 0.43f)
            // };
            // sf.looped = true;
            // sf.shadowLayers = -1;

            var damageEvent = obj.GetComponent<DamagableEvent>();
            damageEvent.maxHP = 250;
            damageEvent.currentHP = 250;
            damageEvent.damageEvent = new UnityEvent();
            damageEvent.damageEvent.AddListener(() =>
            {
                var prevCol = ren.color;
                ren.color = new Color(0.7f,0.7f,0.7f);
                StickFightMaps.instance.ExecuteAfterSeconds(0.1f, () =>
                {
                    ren.color = prevCol;
                });
            });

            spriteObj.AddComponent<BoxCollider2D>();

            Destroy(obj.GetComponent<SpriteMask>());
        }
    }
}