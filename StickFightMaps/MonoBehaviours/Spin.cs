using UnboundLib;
using UnityEngine;

// Code from StickFight:TheGame
namespace StickFightMaps.MonoBehaviours
{
	public class Spin : MonoBehaviour
	{
		// Token: 0x04000671 RID: 1649
		public Vector3 spinVector;

		// Token: 0x04000673 RID: 1651
		public AnimationCurve startCurve;

		// Token: 0x04000674 RID: 1652
		public float secondsToStart;

		private bool hasWaited = false;

		// Token: 0x060007D1 RID: 2001 RVA: 0x00035954 File Offset: 0x00033D54
		private void Update()
		{
			var num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
			
			if (GameManager.instance.battleOngoing)
			{
				if (!hasWaited)
				{
					this.ExecuteAfterSeconds(secondsToStart, () =>
					{
						var d = 1f;
						d = startCurve.Evaluate(1);
						transform.Rotate(spinVector * Time.deltaTime * d, Space.Self);
						hasWaited = true;
					});
				}
				else
				{
					
					var d = 1f;
					d = startCurve.Evaluate(1 + num / secondsToStart);
					transform.Rotate(spinVector * (num * d), Space.Self);
				}
			}

		}
	}
}
