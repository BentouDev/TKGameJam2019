using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[ActionCategory("Math")]
	public class AddAction : GOTO.Logic.Actions.MathBaseActionImpl
	{
		protected override void OnStart()
		{
			if (variable.IsValid)
			{
				var rep = GetVariable(variable);
				rep.Variable += value.GetValue(this);
			}

			SetFinished();
		}
	}
}
