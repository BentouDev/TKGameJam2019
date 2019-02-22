namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(SelfDestructAction))]
	public class SelfDestructActionEditor : ActionEditor
	{
		public override string GetInspectorTitle()
		{
			return "Self-destruct";
		}
	}
}