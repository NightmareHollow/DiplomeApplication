using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.CustomExtensions.Utilities
{
	public static class CustomUtilities
	{
		public static void LoadComponentsOfTypeFromParent<T>(this ICollection<T> targetCollection, Transform objectsParent) where T : Object
		{
			targetCollection ??= new List<T>();
			targetCollection.Clear();
			
			ICollection<T> collection = objectsParent.GetComponentsInChildren<T>();
			foreach (T collectionElement in collection)
			{
				targetCollection.Add(collectionElement);
			}
		}
		
		public static void ReplaceSourceValues<T>(this List<T> source, T replacingValue, T valueToReplace)
		{
			for (int i = 0; i < source.Count; i++)
			{
				if (EqualityComparer<T>.Default.Equals(source[i], replacingValue))
				{
					source[i] = valueToReplace;
				}
			}
		}

		public static List<int> FindSourceReplacingIndexes<T>(this IList<T> source, T replacingValue)
		{
			List<int> replacedIndexes = new List<int>();
			
			for (int i = 0; i < source.Count; i++)
			{
				if (EqualityComparer<T>.Default.Equals(source[i], replacingValue))
				{
					replacedIndexes.Add(i);
				}
			}

			return replacedIndexes;
		}

		public static bool Equals(this Vector3 vector, Vector3 other, float epsilon = 0.01f)
		{
			return Vector3.Distance(other, vector) <= epsilon;
		}

		public static bool Equals(this float firstNumber, float secondNumber, float epsilon = 0.01f)
		{
			return Mathf.Abs(secondNumber - firstNumber) <= epsilon;
		}
		
		public static float NextFloat(this System.Random random, float min, float max)
		{
			double result = random.NextDouble() * (max - min) + min;
			return (float) result;
		}

		public static Vector3 RandomizeVector3(Vector3 minValue, Vector3 maxValue)
		{
			float randomX = Random.Range(minValue.x, maxValue.x);
			float randomY = Random.Range(minValue.y, maxValue.y);
			float randomZ = Random.Range(minValue.z, maxValue.z);

			return new Vector3(randomX, randomY, randomZ);
		}

		public static float ClampLowerValueLimit(float value, float lowerLimit = 0)
		{
			if (value < lowerLimit)
				value = lowerLimit;

			return value;
		}
		
		public static float ClampHigherValueLimit(float value, float higherLimit)
		{
			if (value > higherLimit)
				value = higherLimit;

			return value;
		}

		public static void SetTransparency(this Graphic graphicElement, float percentage)
		{
			percentage = Mathf.Clamp01(percentage);

			Color graphicColor = graphicElement.color;
			graphicColor.a = percentage;
			graphicElement.color = graphicColor;
		}

		public static bool DeactivateCoroutine(this MonoBehaviour containsBehaviour, ref Coroutine routine)
		{
			if (!containsBehaviour)
				return false;
			
			if (routine != null)
			{
				containsBehaviour.StopCoroutine(routine);
				routine = null;

				return true;
			}

			return false;
		}

		public static void ResetAllTriggers(this Animator animator)
		{
			if (!animator)
				return;

			foreach (AnimatorControllerParameter parameter in animator.parameters.
				Where(param => param.type == AnimatorControllerParameterType.Trigger))
			{
				animator.ResetTrigger(parameter.name);
			}
		}

		public static void AddOrApplyElement<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV element)
		{
			if (dict.ContainsKey(key))
			{
				dict[key] = element;
			}
			else
			{
				dict.Add(key, element);
			}
		}

		public static Vector3 ConvertLocalToWorldPos(this Transform target, Vector3 localPosition)
		{
			if (!target || !target.parent)
				return localPosition;

			Vector3 result = target.parent.TransformPoint(localPosition);
			return result;
		}
		
		public static void Shuffle<T>(this IList<T> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				T temp = list[i];
				int randomIndex = Random.Range(i, list.Count);
				list[i] = list[randomIndex];
				list[randomIndex] = temp;
			}
		}

		public static void Swap<T>(this IList<T> list, int i, int j) 
			=> (list[i], list[j]) = (list[j], list[i]);

		public static bool In<T>(this T val, params T[] values) 
			=> values.Contains(val);
	}
}