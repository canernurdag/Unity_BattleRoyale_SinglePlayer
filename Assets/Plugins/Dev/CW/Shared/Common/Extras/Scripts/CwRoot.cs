using UnityEngine;
using System.Collections.Generic;

namespace CW.Common
{
	/// <summary>If your scene is massive, then you can add this component to an empty GameObject. Any components that support this feature will add GameObjects to this as children and transform them in a way that makes them render properly using a floating origin system.</summary>
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-100)]
	[HelpURL(CwShared.HelpUrlPrefix + "CwRoot")]
	[AddComponentMenu(CwShared.ComponentMenuPrefix + "Root")]
	public class CwRoot : MonoBehaviour
	{
		private static List<CwRoot> instances = new List<CwRoot>();

		public static bool Exists
		{
			get
			{
				return instances.Count > 0;
			}
		}

		public static Transform Root
		{
			get
			{
				if (instances.Count > 0)
				{
					return instances[0].transform;
				}

				return null;
			}
		}

		public static Transform GetRoot()
		{
			if (instances.Count == 0)
			{
				new GameObject("CwRoot").AddComponent<CwRoot>();
			}

			return instances[0].transform;
		}

		protected virtual void OnEnable()
		{
			if (instances.Count > 0)
			{
				Debug.LogWarning("Your scene already contains an instance of CwRoot!", instances[0]);
			}

			instances.Add(this);
		}

		protected virtual void OnDisable()
		{
			instances.Remove(this);
		}
	}
}

#if UNITY_EDITOR
namespace CW.Common
{
	using UnityEditor;
	using TARGET = CwRoot;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class CwRoot_Editor : CwEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			Info("If your scene is massive, then you can add this component to an empty GameObject. Any components that support this feature will add GameObjects to this as children and transform them in a way that makes them render properly using a floating origin system.");
		}
	}
}
#endif