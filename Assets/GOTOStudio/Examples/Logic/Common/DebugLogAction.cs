using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class DebugLogAction : Action
	{
		public enum ActionType
		{
			Default = 0,
			Variable = 1,
		}

		public ActionType actionType;
		public string message;
		public VariableRef variable = new VariableRef();

		protected override void OnStart()
		{
			string logMessage = string.Empty;

			switch (actionType)
			{
				case ActionType.Default:
					logMessage = message;
					break;
				case ActionType.Variable:
					object value;
					GetVariableValue<object>(variable, out value);
					logMessage = value.ToString();
					break;
			};

			Print(logMessage);
			SetFinished();
		}

		public static void Print(string logMessage) //TODO: unify log output of actions
		{
			Debug.Log(logMessage);
			//TODO Link NativeUtility
			//GOTO.NativeUtility.WriteLineToConsole(logMessage);
		}
	}
}