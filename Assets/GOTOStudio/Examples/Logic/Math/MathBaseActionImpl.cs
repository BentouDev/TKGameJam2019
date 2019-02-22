using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[HideAction]
	public abstract class MathBaseActionImpl : Action
	{
		public VariableRef variable = new VariableRef();
		public Value value = new Value();

		public bool CustomTitleUnavailable()
		{
			bool ret = !variable.IsValid || !value.IsValid;
			return ret;
		}
	}
}
