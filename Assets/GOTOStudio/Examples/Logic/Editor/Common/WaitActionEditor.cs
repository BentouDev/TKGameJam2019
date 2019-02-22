using UnityEditor;
using UnityEngine;
using GOTO.Logic;

namespace GOTO.Logic.Actions
{
	using WaitType = WaitAction.WaitType;

	[CustomActionEditor(typeof(WaitAction))]
	public class WaitActionEditor : ActionEditor
	{
		new WaitAction Target { get { return base.Target as WaitAction; } }

		public override float GetInspectorHeight()
		{
			var rows = Target.waitType == WaitType.PreviousActionFinished ? 2 : 3;
			return EditorGUIUtility.singleLineHeight * rows;
		}

		public override string GetInspectorTitle()
		{
			if (Target.waitType == WaitType.Time)
				return "Wait " + Target.seconds + " seconds";

			if (Target.waitType == WaitType.RandomTime)
				return "Wait " + Target.randomMin + "-" + Target.randomMax + " seconds";

			if (Target.waitType == WaitType.PreviousActionFinished)
				return "Wait for previous action";

			if (Target.waitType == WaitType.Variable)
			{
				var rep = Target.GetVariable(Target.variable);
				if (rep != null)
				{
					if (rep.Variable != null && rep.Variable.Type == typeof(float))
						return string.Format("Wait (variable `{0}` value, default: {1}) seconds.", rep.Name, rep.Variable.Value);
				}
			}

			return base.GetInspectorTitle();
		}

		public override void OnInspectorGUI(Rect rect)
		{
			Target.waitType = (WaitType)EditorGUI.EnumPopup(rect, new GUIContent("Duration"), Target.waitType);
			rect.y += EditorGUIUtility.singleLineHeight;

			if (Target.waitType == WaitType.Time)
				Target.seconds = EditorGUI.FloatField(rect, new GUIContent("Seconds"), Target.seconds);

			if (Target.waitType == WaitType.RandomTime)
			{
				EditorGUI.LabelField(rect, "Seconds Range");
				const int padding = 4;
				var width = (rect.width - EditorGUIUtility.labelWidth - padding) / 2;
				var minRect = new Rect(rect.x + EditorGUIUtility.labelWidth, rect.y, width, rect.height);
				var maxRect = new Rect(minRect.xMax + padding, rect.y, width, rect.height);

				Target.randomMin = EditorGUI.FloatField(minRect, new GUIContent(""), Target.randomMin);
				Target.randomMax = EditorGUI.FloatField(maxRect, new GUIContent(""), Target.randomMax);
			}

			if (Target.waitType == WaitType.Variable)
			{
				var variableProp = this.SerializedObject.FindProperty("variable");
				EditorGUI.PropertyField(rect, variableProp);
			}
		}
	}
}
