using UnityEditor;

namespace GOTO.Logic.Actions
{
	using UnityEngine;
	using ActionType = DebugLogAction.ActionType;

	[CustomActionEditor(typeof(DebugLogAction))]
	public class DebugLogActionEditor : ActionEditor
	{
		new DebugLogAction Target { get { return base.Target as DebugLogAction; } }

		public override float GetInspectorHeight()
		{
			return EditorGUIUtility.singleLineHeight * 3;
		}

		public override string GetInspectorTitle()
		{
			switch (Target.actionType)
			{
				case ActionType.Default:
					{
						return "DebugLog: " + Target.message;
					}
				case ActionType.Variable:
					{
						var rep = Target.GetVariable(Target.variable);
						if (rep != null)
							return string.Format("DebugLog: variable `{0}` value.", rep.Name);
						break;
					}
			};

			return base.GetInspectorTitle();
		}

		public override void OnInspectorGUI(Rect rect)
		{
			Target.actionType = (ActionType)EditorGUI.EnumPopup(rect, new GUIContent("Type"), Target.actionType);
			rect.y += EditorGUIUtility.singleLineHeight;

			if (Target.actionType == ActionType.Default)
			{
				Target.message = EditorGUI.TextField(rect, new GUIContent("Message"), Target.message);
			}
			else if (Target.actionType == ActionType.Variable)
			{
				var variableProp = this.SerializedObject.FindProperty("variable");
				EditorGUI.PropertyField(rect, variableProp);
			}
		}
	}
}
