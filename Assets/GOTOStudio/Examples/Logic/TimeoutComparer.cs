using System;
using UnityEngine;

namespace GOTO.Logic.Comparers
{
	[Serializable]
	public class TimeoutComparer : Comparer
	{
		public Value seconds = new Value(typeof(float));

		private float time;

		private bool timedOut;

		protected override void OnReset()
		{
			timedOut = false;
			time = (float)seconds.GetValue(this);
		}

		protected override void OnUpdate()
		{
			if (timedOut)
				return;

			time -= Time.deltaTime;
			if (time <= 0)
				timedOut = true;
		}

		public override bool Compare()
		{
			return timedOut;
		}
	}
}
