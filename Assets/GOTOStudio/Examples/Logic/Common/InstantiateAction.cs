using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class InstantiateAction : Action
	{
		public Value prefab = new Value(typeof(GameObject));
		public Value position = new Value(typeof(Transform));
		public Value instantiated = new Value(typeof(GameObject));

		protected override void OnStart()
		{
			var gameObject = (GameObject)prefab.GetValue(this);
			var transform = (Transform)position.GetValue(this);
			if (transform == null)
				transform = PlaybackContext.GetTransform();

			if (gameObject)
				instantiated.SetValue(UnityEngine.Object.Instantiate(gameObject, transform.position, transform.rotation), this);

			SetFinished();
		}
	}
}
