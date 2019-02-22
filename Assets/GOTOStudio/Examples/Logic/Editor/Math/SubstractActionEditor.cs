namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(SubstractAction))]
	public class SubstractActionEditor : ActionEditor
	{
		new SubstractAction Target { get { return base.Target as SubstractAction; } }

		public override string GetInspectorTitle()
		{
			if (Target.CustomTitleUnavailable())
				return base.GetInspectorTitle();

			return "Substract " + Target.GetNameFor(Target.value, "?") + " from " + Target.GetNameFor(Target.variable, "?");
		}
	}
}
