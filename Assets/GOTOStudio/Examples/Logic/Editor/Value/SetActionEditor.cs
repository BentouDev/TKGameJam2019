namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(SetAction))]
	public class SetActionEditor : ActionEditor
	{
		new SetAction Target { get { return base.Target as SetAction; } }

		public override string GetInspectorTitle()
		{
			if (Target.CustomTitleUnavailable())
				return base.GetInspectorTitle();

			return "Set " + Target.GetNameFor(Target.variable, "?") + " to " + Target.GetNameFor(Target.value, "?");
		}
	}
}
