using System;
using UnityEngine;

namespace GOTO.Logic.Actions
{
	[Serializable]
	[ActionCategory("Audio")]
	public class PlaySoundAction : Action
	{
		public Value audioClip = new Value(typeof(AudioClip));

		protected override void OnStart()
		{
			if(audioSource != null)
				audioSource.PlayOneShot(audioClip.GetValue(this) as AudioClip);
			SetFinished();
		}

		public AudioSource audioSource 
		{
			get 
			{
				AudioSource result = null;
				GameObject go = Camera.main.gameObject;
				if(go == null)
				{
					AudioListener audioListener = FindObjectOfType<AudioListener>();
					if(audioListener != null)
					{
						go = audioListener.gameObject;
					} 
					else 
					{
						Debug.Log("There are no AudioListener(s) in the scene! PlaySound action aborted.");
					}
				} 
				else 
				{
					result = go.GetComponent<AudioSource>();
					if(result == null)
					{
						result = go.AddComponent<AudioSource>();
					}
				}
				return result;
			}
		}
	}
}
