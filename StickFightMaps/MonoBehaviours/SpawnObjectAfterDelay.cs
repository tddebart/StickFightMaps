using UnityEngine;

// Token: 0x02000111 RID: 273
namespace StickFightMaps.MonoBehaviours
{
	public class SpawnObjectAfterDelay : MonoBehaviour
	{
		// Token: 0x04000668 RID: 1640
		public GameObject objectToSpawn;

		// Token: 0x04000669 RID: 1641
		public float secondsBeforeSpawn;

		// Token: 0x0400066A RID: 1642
		public bool removeSelf = true;

		// Token: 0x0400066B RID: 1643
		public Vector3 specificSpawnRotation;

		// Token: 0x0400066C RID: 1644
		public bool identitySpawnRotation;

		// Token: 0x0400066D RID: 1645
		private bool done;

		// Token: 0x0400066F RID: 1647
		public bool dontStartCounting;
		// Token: 0x060007C7 RID: 1991 RVA: 0x00035770 File Offset: 0x00033B70
		private void Start()
		{
			var component = Random.Range(0.8f, 1.2f);
			this.secondsBeforeSpawn *= component;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000357A2 File Offset: 0x00033BA2
		public void StartCounting()
		{
			this.dontStartCounting = false;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000357AC File Offset: 0x00033BAC
		private void Update()
		{
			if (dontStartCounting)
			{
				return;
			}
			secondsBeforeSpawn -= Time.deltaTime;
			if (secondsBeforeSpawn < 0f && !done)
			{
				Spawn();
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000357F8 File Offset: 0x00033BF8
		public void Spawn()
		{
			if (this.done)
			{
				return;
			}
			this.done = true;
			Quaternion rotation = transform.rotation;
			if (specificSpawnRotation != Vector3.zero)
			{
				rotation = Quaternion.Euler(this.specificSpawnRotation);
			}
			if (identitySpawnRotation)
			{
				rotation = Quaternion.identity;
			}
			GameObject _gameObject = Instantiate(objectToSpawn, transform.position, rotation);
			if (removeSelf)
			{
				Destroy(gameObject);
			}
			// Explosion explosion = _gameObject.GetComponentInChildren<Explosion>();
			// if (explosion)
			// {
			// 	explosion.Explode();
			// }
		}

	}
}
