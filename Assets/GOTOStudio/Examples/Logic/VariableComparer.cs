using System;
using UnityEngine;

namespace GOTO.Logic.Comparers
{
	[Serializable]
	public class VariableComparer : Comparer
	{
		public Value lhs = new Value() { StorageMode = Value.Storage.VariableRef }; // Default to variable
		public Value rhs = new Value();

		public enum CompareOperator { Equal, NotEqual, Greater, Less, GreaterOrEqual, LessOrEqual };
		public CompareOperator compareOperator;

		private int CompareVariables()
		{
			Variable lhsSV;
			try
			{
				lhsSV = lhs.GetVariable(this);
			}
			catch (Exception e)
			{
				Debug.LogErrorFormat(e.Message);
				return -20;
			}

			Variable rhsSV;
			try
			{
				rhsSV = rhs.GetVariable(this);
			}
			catch (Exception e)
			{
				Debug.LogErrorFormat(e.Message);
				return -21;
			}

			if (lhsSV == rhsSV)
				return 0;
			else if (lhsSV < rhsSV)
				return -1;
			else if (lhsSV > rhsSV)
				return 1;

			return -10;
		}

		public override bool Compare()
		{
			bool result = false;
			var compareResult = CompareVariables();

			switch (compareOperator)
			{
				case CompareOperator.Equal:
					result = compareResult == 0;
					break;
				case CompareOperator.NotEqual:
					result = compareResult != 0;
					break;
				case CompareOperator.Greater:
					result = compareResult > 0;
					break;
				case CompareOperator.Less:
					result = compareResult < 0;
					break;
				case CompareOperator.GreaterOrEqual:
					result = compareResult >= 0;
					break;
				case CompareOperator.LessOrEqual:
					result = compareResult <= 0;
					break;
			}

			return result;
		}
	}
}
