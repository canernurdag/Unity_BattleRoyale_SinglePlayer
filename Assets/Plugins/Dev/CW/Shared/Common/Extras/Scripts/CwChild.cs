﻿using UnityEngine;

namespace CW.Common
{
	/// <summary>This is the base class for all components that are created as children of another component, allowing them to be more easily managed.</summary>
	public abstract class CwChild : MonoBehaviour
	{
		public interface IHasChildren
		{
			bool HasChild(CwChild child);
		}

		public void DestroyGameObjectIfInvalid()
		{
			var parent = GetParent();

			if (parent == null || parent.HasChild(this) == false)
			{
#if UNITY_EDITOR
				UnityEditor.Undo.DestroyObjectImmediate(gameObject);
#else
				DestroyImmediate(gameObject);
#endif
			}
		}

		protected abstract IHasChildren GetParent();

		protected virtual void Start()
		{
			//DestroyGameObjectIfInvalid();
		}
	}
}