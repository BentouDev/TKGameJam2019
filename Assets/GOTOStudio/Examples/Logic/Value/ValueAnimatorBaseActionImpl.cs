using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[HideAction]
	public abstract class VariableAnimatorBaseActionImpl : Action
	{
		public Value seconds = new Value(typeof(float));
		public Value from = new Value();
		public Value to = new Value();
		public AnimationCurve curve = new AnimationCurve(new[] { new Keyframe(0, 0), new Keyframe(1, 1) });

		public AnimationMode mode;
		public enum AnimationMode { Absolute, Additive, Multiply };

		public bool mustComplete;



		private object startValue;
		private float currentTime;
		private bool completedNormally;

		[HideInInspector]
		public virtual object value { get; set; }
		[HideInInspector]
		public virtual System.Type valueType { get; set; }



		protected override void OnStart()
		{
			startValue = value;
			currentTime = 0;
			completedNormally = false;
		}

		protected override void OnUpdate()
		{
			var endTime = (float)seconds.GetValue(this);
			var time = currentTime / endTime;
			if (float.IsNaN(time))
				return;

			time = Mathf.Clamp01(time);
			var interpolant = curve.Evaluate(time);
			ApplyValue(interpolant);

			if (currentTime >= endTime)
			{
				completedNormally = true;
				SetFinished();
			}
			else
			{
				currentTime += Time.deltaTime;
			}
		}

		protected override void OnFinish()
		{
			if (mustComplete && !completedNormally)
			{
				var interpolant = curve.Evaluate(1.0f);
				ApplyValue(interpolant);
			}
		}

		internal static object Lerp(object a, object b, float t, System.Type valueType)
		{
			if (valueType == typeof(int))
				return (int)Mathf.Lerp((int)a, (int)b, t);//!
			else if (valueType == typeof(float))
				return (float)Mathf.Lerp((float)a, (float)b, t);
			else if (valueType == typeof(Vector2))
				return Vector2.Lerp((Vector2)a, (Vector2)b, t);
			else if (valueType == typeof(Vector3))
				return Vector3.Lerp((Vector3)a, (Vector3)b, t);
			else if (valueType == typeof(Vector4))
				return Vector4.Lerp((Vector4)a, (Vector4)b, t);
			else if (valueType == typeof(Color))
				return Color.Lerp((Color)a, (Color)b, t);
			else if (valueType == typeof(Quaternion))
				return Quaternion.Lerp((Quaternion)a, (Quaternion)b, t);

			return null;
		}

		private void ApplyValue(float interpolant)
		{
			var interpolatedValue = Lerp(from.GetValue(this), to.GetValue(this), interpolant, valueType);
			if (interpolatedValue == null)
				return;

			switch (mode)
			{
				case AnimationMode.Absolute:
					value = interpolatedValue;
					break;
				case AnimationMode.Additive:
					var add = AddValue(startValue, interpolatedValue);
					value = add ?? value;
					break;
				case AnimationMode.Multiply:
					var mul = MultiplyValue(startValue, interpolatedValue);
					value = mul ?? value;
					break;
			}
		}

		private object AddValue(object a, object b)
		{
			if (valueType == typeof(int))
				return (int)a + (int)b;
			if (valueType == typeof(float))
				return (float)a + (float)b;
			if (valueType == typeof(Vector2))
				return (Vector2)a + (Vector2)b;
			if (valueType == typeof(Vector3))
				return (Vector3)a + (Vector3)b;
			if (valueType == typeof(Color))
				return (Color)a + (Color)b;
			if (valueType == typeof(Vector4))
				return (Vector4)a + (Vector4)b;

			return null;
		}

		private object MultiplyValue(object a, object b)
		{
			if (valueType == typeof(int))
				return (int)a * (int)b;
			if (valueType == typeof(float))
				return (float)a * (float)b;
			if (valueType == typeof(Color))
				return (Color)a * (Color)b;

			return null;
		}

	}
}
