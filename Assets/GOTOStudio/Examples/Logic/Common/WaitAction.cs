using GOTO.Utilities;
using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class WaitAction : Action
	{
		public enum WaitType
		{
			Time = 0,
			RandomTime = 1,
			PreviousActionFinished = 2,
			Variable = 3,
		}

		public WaitType waitType = WaitType.PreviousActionFinished;
		public float seconds;
		public float randomMin;
		public float randomMax;
		[FixedType(typeof(float))]
		public VariableRef variable = new VariableRef();


		private float time;

		public override bool Wait()
		{
			return true;
		}

		protected override void OnStart()
		{
			switch (waitType)
			{
				case WaitType.Time:
					time = seconds;
					break;
				case WaitType.RandomTime:
					time = UnityEngine.Random.Range(randomMin, randomMax);
					break;
				case WaitType.PreviousActionFinished:
					break;
				case WaitType.Variable:
					GetVariableValue<float>(variable, out time);
					break;
			};
		}

		protected override void OnUpdate()
		{
			switch (waitType)
			{
				case WaitType.Time:
				case WaitType.RandomTime:
				case WaitType.Variable:
					if (time <= 0)
					{
						SetFinished();
					}
					else
					{
						time -= Time.deltaTime;
					}
					break;
				case WaitType.PreviousActionFinished:
					if (PlaybackContext.IsPreviousActionFinished)
						SetFinished();
					break;
			};
		}
	}
}
