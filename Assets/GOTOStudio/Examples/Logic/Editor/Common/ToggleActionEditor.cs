using UnityEditor;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[CustomActionEditor(typeof(ToggleAction))]
	public class ToggleActionEditor : ActionEditor
	{
		new ToggleAction Target { get { return base.Target as ToggleAction; } }

		public override string GetInspectorTitle()
		{
			return Target.action.ToString() + " " + Target.GetNameFor(Target.thing, "?");
		}

		public override float GetInspectorHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2;
		}

		public override void OnInspectorGUI(Rect rect)
		{
			float w = rect.width;
			rect.width = EditorGUIUtility.labelWidth;
			EditorGUI.PropertyField(rect, this.SerializedObject.FindProperty("action"), new GUIContent(""));
			rect.x += rect.width;
			rect.width = w - rect.width;
			EditorGUI.PropertyField(rect, this.SerializedObject.FindProperty("thing"), new GUIContent(""));
		}
	}
}
