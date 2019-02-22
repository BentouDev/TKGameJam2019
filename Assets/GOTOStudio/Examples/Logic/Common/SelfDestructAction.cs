using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class SelfDestructAction : Action
	{
		protected override void OnStart()
		{
			UnityEngine.Object.Destroy(this.PlaybackContext.GetTransform().gameObject);
			SetFinished();
		}
	}
}
