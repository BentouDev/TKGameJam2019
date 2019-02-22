namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(InstantiateAction))]
	public class InstantiateActionActionEditor : ActionEditor
	{
		new InstantiateAction Target { get { return base.Target as InstantiateAction; } }

		public override string GetInspectorTitle()
		{
			return "Local instantiate \"" + Target.GetNameFor(Target.prefab, "(null)") + "\"";
		}
	}
}
