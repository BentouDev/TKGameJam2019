namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(MultiplyAction))]
	public class MultiplyActionEditor : ActionEditor
	{
		new MultiplyAction Target { get { return base.Target as MultiplyAction; } }

		public override string GetInspectorTitle()
		{
			if (Target.CustomTitleUnavailable())
				return base.GetInspectorTitle();

			return "Multiply " + Target.GetNameFor(Target.variable, "?") + " by " + Target.GetNameFor(Target.value, "?");
		}
	}
}
