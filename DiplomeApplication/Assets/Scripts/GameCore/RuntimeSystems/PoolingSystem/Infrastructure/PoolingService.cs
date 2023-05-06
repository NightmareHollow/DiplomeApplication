using System.Collections.Generic;
using GameCore.CustomExtensions.InstantiateExtensions.Infrastructure;
using GameCore.RuntimeSystems.Infrastructure;
using GameCore.RuntimeSystems.PoolingSystem.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.RuntimeSystems.PoolingSystem.Infrastructure
{
    public static class PoolingService
    {
        private static PoolingSettingsContainer settingsContainer;
        
        private static Dictionary<Component, Pool> pools;
        private static Transform poolsParent;

        private static IInstantiateProvider customInstantiateProvider;

        [RuntimeInitializeOnLoadMethod]
        private static void InitializePoolingService()
        {
            pools = new Dictionary<Component, Pool>();

            poolsParent = new GameObject("Pools").transform;
            Object.DontDestroyOnLoad(poolsParent);

            PoolingSettingsContainer poolingSettings = Resources.Load<PoolingSettingsContainer>(
                RuntimeSystemsResourcesPaths.PoolingSettingsPath);

            foreach (PoolSettings poolSettings in poolingSettings.PoolsSettingsList)
            {
                AddPool(poolSettings.Original, poolSettings.Amount);
            }
        }

        public static void ChangeInstantiateProvider(IInstantiateProvider newInstantiateProvider = null)
	        => customInstantiateProvider = newInstantiateProvider;
        
        public static T GetObject<T>(T original) where T : Component, IPoolComponent
		{
			Pool pool = GetPool(original);
			GetClone(original, pool, out T clone);
			
			clone.OriginalPrefab = original;

			return clone;
		}

		public static T GetObject<T>(T original, Vector3 position, Quaternion rotation) where T : Component, IPoolComponent
		{
			Pool pool = GetPool(original);
			GetClone(original, pool, out T clone);

			clone.transform.SetPositionAndRotation(position, rotation);
			clone.OriginalPrefab = original;

			return clone;
		}

		public static T GetObject<T>(T original, Transform parent, 
			bool worldPositionStays = false) where T : Component, IPoolComponent
		{
			Pool pool = GetPool(original);
			GetClone(original, pool, out T clone);

			clone.transform.SetParent(parent, worldPositionStays);
			clone.OriginalPrefab = original;

			return clone;
		}

		public static T GetObject<T>(T original, Transform parent, Vector3 position, Quaternion rotation) where T : Component, IPoolComponent
		{
			Pool pool = GetPool(original);
			GetClone(original, pool, out T clone);

			clone.transform.SetParent(parent);
			clone.transform.SetPositionAndRotation(position, rotation);
			clone.OriginalPrefab = original;

			return clone;
		}

		public static void ReturnObject<T>(T clone) where T : Component, IPoolComponent
		{
			clone.gameObject.SetActive(false);
			
			Pool pool = GetPool(clone.OriginalPrefab);
			clone.transform.SetParent(pool.PoolParent);
			
			pool.Objects.Push(clone);
		}

		public static void AddPool<T>(T original, int amount) where T : Component, IPoolComponent
		{
			Pool pool = GetPool(original);

			for (int i = 0; i < amount; i++)
			{
				InstantiateClone(original, pool.PoolParent, out T clone);

				clone.OriginalPrefab = original;
				pool.Objects.Push(clone);
			}
		}

		public static int GetPoolSize<T>(T original) where T : Component, IPoolComponent 
		=> pools.TryGetValue(original, out Pool pool) ? pool.Objects.Count : 0;

		public static void RemovePool<T>(T original) where T : Component, IPoolComponent
		{
			if (pools.TryGetValue(original, out Pool pool))
			{
				DestroyGameObjectsFromPool(pool, pool.Objects.Count);

				Object.Destroy(pool.PoolParent.gameObject);

				pools.Remove(original);
			}
		}

		public static void DestroyPool<T>(T original) where T : Component, IPoolComponent
		{
			if (pools.TryGetValue(original, out Pool pool))
			{
				DestroyGameObjectsFromPool(pool, pool.Objects.Count);
			}
		}

		public static void DestroyPool<T>(T original, int amount) where T : Component, IPoolComponent
		{
			if (pools.TryGetValue(original, out Pool pool))
			{
				DestroyGameObjectsFromPool(pool, amount);
			}
		}

		private static void AddPool(Component original, int amount)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			if (original is not IPoolComponent)
				return;
			
			Pool pool = GetPool(original);
			for (int i = 0; i < amount; i++)
			{
				InstantiateClone(original, pool.PoolParent, out Component clone);
				
				// ReSharper disable once SuspiciousTypeConversion.Global
				((IPoolComponent) clone).OriginalPrefab = original;
				pool.Objects.Push(clone);
			}
		}

		private static Pool GetPool<T>(T original) where T : Component
		{
			Pool foundPool;
			if (pools.TryGetValue(original, out Pool pool))
			{
				foundPool = pool;
			}
			else
			{
				foundPool = new Pool(poolsParent, original.name);
				pools.Add(original, foundPool);
			}

			return foundPool;
		}

		private static void GetClone<T>(T original, Pool pool, out T clone) where T : Component, IPoolComponent
		{
			if (pool.Objects.Count > 0)
			{
				clone = (T) pool.Objects.Pop();
			}
			else
			{
				InstantiateClone(original, pool.PoolParent, out T createdClone);
				clone = createdClone;
			}
		}

		private static void InstantiateClone<T>(T original, Transform parent, out T clone) where T : Component
		{
			clone = Instantiate(original, parent);
			clone.gameObject.SetActive(false);
		}

		private static T Instantiate<T>(T original, Transform parent) where T : Component
		{
			T clone = customInstantiateProvider != null ?
				customInstantiateProvider.Instantiate(original, parent) :
				Object.Instantiate(original, parent);

			return clone;
		}

		private static void DestroyGameObjectsFromPool(Pool pool, int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				Object.Destroy(pool.Objects.Pop().gameObject);
			}
		}
    }
}