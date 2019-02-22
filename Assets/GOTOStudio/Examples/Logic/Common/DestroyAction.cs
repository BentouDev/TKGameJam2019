using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class DestroyAction : Action
	{
		public Value thing = new Value(typeof(UnityEngine.Object));

		protected override void OnStart()
		{
			var obj = (UnityEngine.Object)thing.GetValue(this);

			if (obj)
                UnityEngine.Object.Destroy(obj);

			SetFinished();
		}
	}
}
