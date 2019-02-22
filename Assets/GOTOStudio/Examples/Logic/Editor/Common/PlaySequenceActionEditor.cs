namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(PlaySequenceAction))]
	public class PlaySequenceActionEditor : ActionEditor
	{
		new PlaySequenceAction Target { get { return base.Target as PlaySequenceAction; } }

		public override string GetInspectorTitle()
		{
			return "Play \"" + Target.GetNameFor(Target.playable, "?") + "\"";
		}
	}
}
