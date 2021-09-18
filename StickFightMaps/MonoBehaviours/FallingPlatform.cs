using Photon.Pun;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class FallingPlatform : MonoBehaviour
    {
        public bool done;
        private static readonly int FallAni = Animator.StringToHash("Fall");
        private Animator animation;
        private Rigidbody2D rig;
        private ParticleSystem particle;

        private readonly TimeSince timeSinceStart = 0;

        public void Start()
        {
            if (!this) return;
            animation = GetComponentInChildren<Animator>();
            rig = GetComponentInChildren<Rigidbody2D>();
            particle = GetComponentInChildren<ParticleSystem>();
        }

        [PunRPC]
        public void RPCA_Setup(Vector3 scale, Vector3 position, bool collideAnything = false)
        {
            transform.parent = MapManager.instance.currentMap.Map.transform;
            transform.localScale = scale;
            transform.position = position;
            //GetComponentInChildren<FallingCollision>().enabled = !collideAnything;
        }


        [PunRPC]
        public void RPCA_Fall()
        {
            if (done || animation == null || rig == null || particle == null || timeSinceStart < 1f || !GameManager.instance.battleOngoing || MapManager.instance.currentMap == null)
            {
                return;
            }
            done = true;
            name = "FallingPlatform";
            animation.SetTrigger(FallAni);
            particle.Play();
            StickFightMaps.instance.ExecuteAfterSeconds(0.5f, () =>
            {
                foreach (var col in GetComponentsInChildren<Collider2D>())
                {
                    col.enabled = false;
                }

                animation.enabled = false;

                rig.constraints = RigidbodyConstraints2D.None;
                rig.AddTorque(Random.Range(-2.5f, 2.5f), ForceMode2D.Impulse);
                rig.AddForce(Vector2.up * Random.Range(1f, 2f), ForceMode2D.Impulse);

            });
            var rem = gameObject.AddComponent<RemoveAfterSeconds>();
            rem.seconds = 20f;
        }
    }
}