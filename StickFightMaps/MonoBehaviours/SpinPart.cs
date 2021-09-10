using Photon.Pun;
using UnboundLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StickFightMaps.MonoBehaviours
{
    public class SpinPart : MonoBehaviour
    {
        [PunRPC]
        public void GetParentAndApply()
        {
            this.ExecuteAfterSeconds(2f, () =>
            {
                var parent = transform.parent;
                if (parent.transform.Find("SpinRoot") == null)
                {
                    var spinRoot = new GameObject("SpinRoot");
                    spinRoot.transform.parent = parent;

                    // Add spin mono
                    var spin = spinRoot.AddComponent<Spin>();
                    spin.spinVector = new Vector3(0, 0, -15);
                    spin.secondsToStart = 0.01f;
                    spin.startCurve = AnimationCurve.EaseInOut(0,1,1,2);
                    //     new AnimationCurve(new []
                    // {
                    //     new Keyframe(0,0, 0,0,0.33333334f, 0.33333334f),
                    //     new Keyframe(0.4872682f, 0.4435258f, 3.580405f, 3.580405f, 0.33333334f, 0.33333334f),
                    //     new Keyframe(1,1,0,0,0.33333334f,0.33333334f)
                    // });
                    transform.parent = spinRoot.transform;
                }
                else
                {
                    var spinRoot = SceneManager.GetSceneAt(1).GetRootGameObjects()[0].transform.Find("SpinRoot");
                    transform.parent = spinRoot.transform;
                }
            });
            
        }
    }
}