using TMPro;
using UnityEngine;

// Token: 0x02000018 RID: 24
namespace StickFightMaps.MonoBehaviours
{
    public class Bomb : MonoBehaviour
    {
        // Token: 0x04000077 RID: 119
        private TextMeshPro text;

        // Token: 0x04000078 RID: 120
        private SpawnObjectAfterDelay bombScipt;

        // Token: 0x04000079 RID: 121
        private Rigidbody2D rig;
        // Token: 0x06000057 RID: 87 RVA: 0x0000448B File Offset: 0x0000288B
        private void Start()
        {
            text = base.GetComponentInChildren<TextMeshPro>();
            bombScipt = base.GetComponentInChildren<SpawnObjectAfterDelay>();
            rig = base.GetComponent<Rigidbody2D>();
        }

        // Token: 0x06000058 RID: 88 RVA: 0x000044B1 File Offset: 0x000028B1
        private void Update()
        {
            this.text.text = this.bombScipt.secondsBeforeSpawn.ToString("F0");
        }

        // Token: 0x06000059 RID: 89 RVA: 0x000044D3 File Offset: 0x000028D3
        private void OnDestroy()
        {
            Explode();
        }

        // Token: 0x0600005A RID: 90 RVA: 0x000044DC File Offset: 0x000028DC
        public void Explode()
        {
            //GetComponent<Explosion>().Explode();
        }

    }
}