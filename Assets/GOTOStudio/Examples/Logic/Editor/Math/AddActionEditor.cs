namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(AddAction))]
	public class AddActionEditor : ActionEditor
	{
		new AddAction Target { get { return base.Target as AddAction; } }

		public override string GetInspectorTitle()
		{
			if (Target.CustomTitleUnavailable())
				return base.GetInspectorTitle();

			return "Add " + Target.GetNameFor(Target.value, "?") + " to " + Target.GetNameFor(Target.variable, "?");
		}
	}
}
