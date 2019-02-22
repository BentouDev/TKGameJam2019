namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(DivideAction))]
	public class DivideActionEditor : ActionEditor
	{
		new DivideAction Target { get { return base.Target as DivideAction; } }

		public override string GetInspectorTitle()
		{
			if (Target.CustomTitleUnavailable())
				return base.GetInspectorTitle();

			return "Divide " + Target.GetNameFor(Target.variable, "?") + " by " + Target.GetNameFor(Target.value, "?");
		}
	}
}
