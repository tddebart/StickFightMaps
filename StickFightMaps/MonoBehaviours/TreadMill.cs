using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace StickFightMaps.MonoBehaviours
{
    public class TreadMill : MonoBehaviour
    {
        public bool left;
        
        public void Start()
        {
            if (transform.parent.rotation.x == 1)
            {
                left = true;
            }
        }
        
        private void Update()
        {
            foreach (TreadmillPerson treadmillPerson in treadmillPeople.ToArray())
            {
                // foreach (Rigidbody rigidbody in treadmillPerson.velocity)
                // {
                //     rigidbody.AddForce(transform.forward * (Time.deltaTime * 500f * Mathf.Clamp(rigidbody.drag, 3f, 10f)), ForceMode.Acceleration);
                // }
                int num = 1;
                if (transform.InverseTransformPoint(treadmillPerson.player.transform.position).y < 0f)
                {
                    num = -1;
                }
                if (left)
                {
                    typeof(PlayerVelocity).InvokeMember("AddForce",
                        BindingFlags.Instance | BindingFlags.InvokeMethod |
                        BindingFlags.NonPublic, null, treadmillPerson.velocity, new object[] {(Vector2)(transform.right * (Time.deltaTime * 300000f * -num * 2f))});
                }
                else
                {
                    typeof(PlayerVelocity).InvokeMember("AddForce",
                        BindingFlags.Instance | BindingFlags.InvokeMethod |
                        BindingFlags.NonPublic, null, treadmillPerson.velocity, new object[] {(Vector2)(-transform.right * (Time.deltaTime * 300000f * num  * 2f))});
                }
                //treadmillPerson.velocity.AddForce(transform.forward * (Time.deltaTime * 500f * 4000));
                
                if (treadmillPerson.time > 0.3f)
                {
                    treadmillPeople.Remove(treadmillPerson);
                }
                else
                {
                    treadmillPerson.time += Time.deltaTime;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            var player = collision.transform.root.GetComponent<Player>();
            int num = 1;
            if (transform.InverseTransformPoint(collision.contacts[0].point).y < 0f)
            {
                num = -1;
            }
            if (player)
            {
                bool flag = false;
                foreach (TreadmillPerson treadmillPerson in treadmillPeople)
                {
                    if (player == treadmillPerson.player)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    TreadmillPerson treadmillPerson2 = new TreadmillPerson
                    {
                        player = player, 
                        velocity = player.GetComponent<PlayerVelocity>()
                    };
                    treadmillPeople.Add(treadmillPerson2);
                }
            } 
            else if (collision.rigidbody || collision.transform.parent.GetComponent<Rigidbody2D>())
            {
                if (left)
                {
                    collision.rigidbody.AddForce(transform.right * Time.deltaTime * 43000000f * -num);
                }
                else
                {
                    collision.rigidbody.AddForce(-transform.right * Time.deltaTime * 43000000f * num);
                }
            }
        }

        // Token: 0x0400112B RID: 4395
        public readonly List<TreadmillPerson> treadmillPeople = new List<TreadmillPerson>();
    }
    [Serializable]
    public class TreadmillPerson
    {
        // Token: 0x04001128 RID: 4392
        public Player player;

        // Token: 0x04001129 RID: 4393
        public PlayerVelocity velocity;

        // Token: 0x0400112A RID: 4394
        public float time;
    }
}