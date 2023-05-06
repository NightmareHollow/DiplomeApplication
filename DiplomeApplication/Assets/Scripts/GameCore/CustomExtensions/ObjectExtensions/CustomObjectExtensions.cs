using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameCore.CustomExtensions.ObjectExtensions
{
	public static class CustomObjectExtensions
	{
		public static void ClearTransformChildren(this Transform parentTransform)
		{
			for (int i = parentTransform.childCount - 1; i >= 0; i--)
			{
				SmartDestroy(parentTransform.GetChild(i).gameObject);
			}
		}

		public static void RefreshCollider(this PolygonCollider2D polygonCollider2D, Sprite sprite)
		{
			if (!polygonCollider2D || !sprite)
				return;

			int physicsShapesCount = sprite.GetPhysicsShapeCount();
			polygonCollider2D.pathCount = physicsShapesCount;
			List<Vector2> shapePaths = new List<Vector2>(physicsShapesCount);

			for (int i = 0; i < physicsShapesCount; i++)
			{
				shapePaths.Clear();
				sprite.GetPhysicsShape(i, shapePaths);
				polygonCollider2D.SetPath(i, shapePaths);
			}
		}

#if UNITY_EDITOR
		public static void SaveAsset(this Object obj)
		{
			EditorUtility.SetDirty(obj);
			AssetDatabase.SaveAssetIfDirty(obj);
		}
#endif
		
		public static void SmartDestroy<T>(T objectToDestroy) where T : Object
		{
			if (Application.isPlaying)
			{
				Object.Destroy(objectToDestroy);
			}
#if UNITY_EDITOR
			else
			{
				Object.DestroyImmediate(objectToDestroy);
			}
#endif
		}
		
		#region SmartInstantiate

		public static T SmartInstantiate<T>(T prefab, Transform parent = null) where T : Object
			=> Instantiate(prefab, parent, false);
		
		public static T SmartInstantiate<T>(T prefab, Transform parent, bool instantiateInWorldSpace) where T : Object
			=> Instantiate(prefab, parent, instantiateInWorldSpace);
		
		public static T SmartInstantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Object
			=> Instantiate(prefab, parent, true, position, rotation, true);
		
		public static T SmartInstantiate<T>(T prefab, UnityEngine.SceneManagement.Scene destinationScene) where T : Object
			=> Instantiate(prefab, destinationScene);

		private static T Instantiate<T>(T prefab, Transform parent = null, bool inWorldSpace = true,
			Vector3 position = default, Quaternion rotation = default, bool usePosRotParams = false) where T : Object
		{

			T clone = null;

			if (Application.isPlaying)
			{
				clone = Object.Instantiate(prefab, parent);
			}
#if UNITY_EDITOR
			else
			{
				clone = (T) UnityEditor.PrefabUtility.InstantiatePrefab(prefab, parent);
			}
#endif
			Transform cloneTransform = ConvertObjectToTransform(clone);
			
			if (clone == null || cloneTransform == null)
				return null;

			if (usePosRotParams)
			{
				cloneTransform.position = position;
				cloneTransform.rotation = rotation;
			}
			else if (inWorldSpace)
			{
				cloneTransform.position = cloneTransform.localPosition;
				cloneTransform.rotation = cloneTransform.localRotation;
			}

			return clone;

		}

		private static T Instantiate<T>(T prefab, UnityEngine.SceneManagement.Scene destinationScene) where T : Object
		{
			T clone = null;
			
#if UNITY_EDITOR
			clone = (T) UnityEditor.PrefabUtility.InstantiatePrefab(prefab, destinationScene);
#endif

			return clone;
		}

		private static Transform ConvertObjectToTransform<T>(T obj) where T : Object
		{
			return obj switch
			{
				GameObject gameObject => gameObject.transform,
				Component component => component.transform,
				_ => null
			};
		}
		
		#endregion
	}
}