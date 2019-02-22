using System;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[ActionCategory("Value")]
	public class SetAction : Action
	{
		public VariableRef variable = new VariableRef();
		public Value value = new Value();

		protected override void OnStart()
		{
			SetVariableValue(variable, value.GetValue(this));

			SetFinished();
		}

		public bool CustomTitleUnavailable()
		{
			bool ret = !variable.IsValid || !value.IsValid;
			return ret;
		}

		public SetAction()
		{
			variable.OnChangedGUID = OnValidate;
		}

		public override void OnValidate()
		{
			var rep = GetVariable(variable);
			if (rep != null)
				value.SetFixedType(rep.Variable.Type, this);
		}
	}
}
