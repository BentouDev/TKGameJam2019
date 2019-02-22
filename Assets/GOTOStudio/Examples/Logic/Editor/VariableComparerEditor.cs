using GOTO.Graph;
using UnityEditor;
using UnityEngine;

namespace GOTO.Logic.Comparers
{
	using CompareOperator = VariableComparer.CompareOperator;

	[CustomComparerEditor(typeof(VariableComparer))]
	public class VariableComparerEditor : ComparerEditor
	{
		new VariableComparer Target { get { return base.Target as VariableComparer; } }

		static string[] compareOptions = new[] { "==", " !=", " >", " <", ">=", "<=" };

		public override string GetInspectorTitle()
		{
			var leftHandOperandName = Target.GetNameFor(Target.lhs, null);
			var rightHandOperandName = Target.GetNameFor(Target.rhs, null);
			if (string.IsNullOrEmpty(leftHandOperandName) || string.IsNullOrEmpty(rightHandOperandName))
				return ComparerEditorUtility.NicifyName(GetType());

			var compare = compareOptions[(int)Target.compareOperator];
			return leftHandOperandName + " " + compare + " " + rightHandOperandName;
		}

		public override float GetInspectorHeight()
		{
			return 2 * EditorGUIUtility.singleLineHeight;
		}

		public override void OnInspectorGUI(Rect rect)
		{
			var leftHandProperty = this.SerializedObject.FindProperty("lhs");
			var rightHandProperty = this.SerializedObject.FindProperty("rhs");

			if (leftHandProperty == null || rightHandProperty == null)
				return;

			var centerWidth = 20;
			var spacing = 6;
			var fieldWidth = (rect.width - centerWidth - spacing * 2) / 2;

			var leftHandRect = new Rect(rect.x, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight);
			var compareRect = new Rect(leftHandRect.xMax + spacing, rect.y, centerWidth, EditorGUIUtility.singleLineHeight);
			var rightHandRect = new Rect(compareRect.xMax + spacing, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight);

			//var conditionalRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, 40, EditorGUIUtility.singleLineHeight);

			EditorGUI.PropertyField(leftHandRect, leftHandProperty, GUIContent.none);
			Target.compareOperator = (CompareOperator)EditorGUI.Popup(compareRect, (int)Target.compareOperator, compareOptions, GraphEditorStyles.comparerDropdownStyle);
			EditorGUI.PropertyField(rightHandRect, rightHandProperty, GUIContent.none);
		}
	}
}
