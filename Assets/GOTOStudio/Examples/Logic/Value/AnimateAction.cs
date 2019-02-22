using System;
using GOTO.Logic;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[ActionCategory("Value")]
	public class AnimateAction : VariableAnimatorBaseActionImpl
	{
		public VariableRef variable = new VariableRef();

		public override object value

		{
			get { return GetVariableValue<object>(variable); }
			set { SetVariableValue(variable, value); }
		}

		public override System.Type valueType
		{
			get
			{
				var sv = GetVariable(variable);
				return sv.Variable.Type;
			}
		}

		public AnimateAction()
		{
			variable.OnChangedGUID = OnValidate;
		}

		public override void OnValidate()
		{
			var rep = GetVariable(variable);
			if (rep != null)
			{
				from.SetFixedType(rep.Variable.Type, this);
				to.SetFixedType(rep.Variable.Type, this);
			}
		}
	}
}
