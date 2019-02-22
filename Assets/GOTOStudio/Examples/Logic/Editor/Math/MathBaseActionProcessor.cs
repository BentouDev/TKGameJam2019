using GOTO.Utilities;

namespace GOTO.Logic.Actions
{
	[TargetObject(typeof(MathBaseActionImpl))]
	class MathBaseActionProcessor : ObjectProcessor
	{
		new MathBaseActionImpl Target { get { return base.Target as MathBaseActionImpl; } }

		public override void OnValidate()
		{
			var rep = Target.GetVariable(Target.variable);
            if (rep != null)
            {
                Target.value.SetFixedType(rep.Variable.Type, Target);
            }
		}
	}
}
