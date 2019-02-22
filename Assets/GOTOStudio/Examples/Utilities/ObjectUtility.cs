using UnityEngine;

namespace GOTO
{
	public static class ObjectUtility
	{
		public static void Enable(Object thing, bool enable)
		{
			if (!thing)
				return;
			if (thing is GameObject)
			{
				var go = thing as GameObject;
				go.SetActive(enable);
			}
			else if (thing is Behaviour)
			{
				var comp = thing as Behaviour;
				comp.enabled = enable;
			}
			else if (thing is Collider)
			{
				var collider = thing as Collider;
				collider.enabled = enable;
			}
			else if (thing is ParticleSystem)
			{
				var parSys = thing as ParticleSystem;
				var emission = parSys.emission;
				emission.enabled = enable;
			}
			else if (thing is OcclusionPortal)
			{
				var portal = thing as OcclusionPortal;
				portal.open = enable;
			}
			else if (thing is Renderer)
			{
				var renderer = thing as Renderer;
				renderer.enabled = enable;
			}
			else 
			{
				Debug.LogError("Trying to toggle unsupported type!");
			}
		}

		public static bool GetEnabled(Object thing)
		{
			if (thing is GameObject)
			{
				var go = thing as GameObject;
				return go.activeSelf;
			}
			else if (thing is Behaviour)
			{
				var comp = thing as Behaviour;
				return comp.enabled;
			}
			else if (thing is Collider)
			{
				var collider = thing as Collider;
				return collider.enabled;
			}
			else if (thing is ParticleSystem)
			{
				var parSys = thing as ParticleSystem;
				var emission = parSys.emission;
				return emission.enabled;
			}
			else if (thing is OcclusionPortal)
			{
				var portal = thing as OcclusionPortal;
				return portal.open;
			}
			else if (thing is Renderer)
			{
				var renderer = thing as Renderer;
				return renderer.enabled;
			}
			return false;
		}

		public static void Toggle(Object thing)
		{
			if (thing is GameObject)
			{
				var go = thing as GameObject;
				go.SetActive(!go.activeSelf);
			}
			else if (thing is Behaviour)
			{
				var comp = thing as Behaviour;
				comp.enabled = !comp.enabled;
			}
			else if (thing is Collider)
			{
				var collider = thing as Collider;
				collider.enabled = !collider.enabled;
			}
			else if (thing is ParticleSystem)
			{
				var parSys = thing as ParticleSystem;
				var emission = parSys.emission;
				emission.enabled = !emission.enabled;
			}
			else if (thing is OcclusionPortal)
			{
				var portal = thing as OcclusionPortal;
				portal.open = !portal.open;
			}
			else if (thing is Renderer)
			{
				var renderer = thing as Renderer;
				renderer.enabled = !renderer.enabled;
			}
			else 
			{
				Debug.LogError("Trying to toggle unsupported type!");
			}
		}

		public static Transform GetTransform(Object thing)
		{
			if (!thing)
				return null;
			if (thing is GameObject)
			{
				var go = thing as GameObject;
				return go.transform;
			}
			else if (thing is Component)
			{
				var comp = thing as Component;
				return comp.transform;
			}
			return null;
		}
	}
}
