using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class BoxSpawner : MonoBehaviour
    {
        public float cooldown = 1;
        public float AdRandomCooldown = 3;
        public float removeAfterSeconds = 25;

        public void Start()
        {
            //gameObject.AddComponent<RemoveAfterSeconds>().seconds = removeAfterSeconds;
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(Spawning());
            }
        }

        private IEnumerator Spawning()
        {
            while (!GameManager.instance.battleOngoing)
            {
                yield return null;
            }
            yield return new WaitForSeconds(cooldown + Random.Range(0f, AdRandomCooldown));
            var transform1 = transform;
            var box = PhotonNetwork.Instantiate("customBox", transform1.position, transform1.rotation);
            yield return new WaitForSeconds(0.1f);
            box.GetComponent<PhotonView>().RPC("RPCA_SetupCustomBox", RpcTarget.All);
            StartCoroutine(Spawning());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}