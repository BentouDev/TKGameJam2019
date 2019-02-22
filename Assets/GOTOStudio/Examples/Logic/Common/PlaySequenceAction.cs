using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	public class PlaySequenceAction : Action
	{
		public Value playable = new Value(typeof(IPlayable));
		public enum AlreadyPlayingMode { TryToPlayAnyway, Skip, WaitAndPlay, ForceRestart }
		public AlreadyPlayingMode ifAlreadyPlaying = AlreadyPlayingMode.TryToPlayAnyway;

		bool playPending = false;

		protected override void OnStart()
		{
			playPending = false;
			var thing = playable.GetValue(this) as IPlayable;
			if (thing == null)
			{
				Debug.Log("Skipping unset playable");
				SetFinished();
				return;
			}
			else if (thing.IsPlaying)
			{
				switch (ifAlreadyPlaying)
				{
					case AlreadyPlayingMode.TryToPlayAnyway:
						thing.Play();
						break;
					case AlreadyPlayingMode.Skip:
						SetFinished();
						return;
					case AlreadyPlayingMode.WaitAndPlay:
						playPending = true;
						break;
					case AlreadyPlayingMode.ForceRestart:
						thing.Stop();
						thing.Play();
						break;
				}
			}
			else
			{
				thing.Play();
			}
		}

		protected override void OnUpdate()
		{
			var thing = playable.GetValue(this) as IPlayable;
			if (thing == null)
			{
				SetFinished();
			}
			else if (!thing.IsPlaying)
			{
				if (playPending)
				{
					thing.Play();
					playPending = false;
				}
				else
				{
					SetFinished();
				}
			}
		}
	}
}
