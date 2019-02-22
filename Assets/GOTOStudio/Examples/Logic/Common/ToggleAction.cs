using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class ToggleAction : Action
	{
		public enum ToggleType
		{
			Toggle,
			Enable,
			Disable,
		}
		public ToggleType action;

		public Value thing = new Value(typeof(UnityEngine.Object));

		protected override void OnStart()
		{
			var obj = (UnityEngine.Object)thing.GetValue(this);
			if (obj == null)
			{
				SetFinished();
				return;
			}

			switch (action)
			{
				case ToggleType.Enable:
					ObjectUtility.Enable(obj, true);
					break;
				case ToggleType.Disable:
					ObjectUtility.Enable(obj, false);
					break;
				case ToggleType.Toggle:
					ObjectUtility.Toggle(obj);
					break;
			}

			SetFinished();
		}
	}
}
