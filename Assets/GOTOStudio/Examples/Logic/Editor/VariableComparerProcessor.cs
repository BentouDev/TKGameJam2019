using GOTO.Utilities;

namespace GOTO.Logic.Comparers
{
	[TargetObject(typeof(VariableComparer))]
	class VariableComparerProcessor : ObjectProcessor
	{
		new VariableComparer Target { get { return base.Target as VariableComparer; } }

		public override void OnValidate()
		{
			Target.rhs.SetFixedType(Target.lhs, Target);
		}
	}
}
