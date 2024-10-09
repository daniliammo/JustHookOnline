using UnityEngine;

namespace Utils
{
    public struct FindGameObject 
    {
	    public static bool FindParentGameObjectWithTag(Transform childGameObject, string targetTag, out GameObject parent)
		{
			var currentParent = childGameObject.parent;

			if (childGameObject.CompareTag(targetTag))
			{
				parent = childGameObject.gameObject;
				return true;
			}
			
			// Проходим по всем родителям, пока не дойдем до корня сцены
			while (currentParent! != null)
			{
				if(currentParent != null && currentParent.CompareTag(targetTag))
				{
					parent = currentParent.gameObject;
					return true;
				}
				currentParent = currentParent.parent;
			}

			parent = null;
			return false;
		}

	    public static bool FindParentGameObjectWithTag(Transform childGameObject, string targetTag)
		{
			return FindParentGameObjectWithTag(childGameObject, targetTag, out var unused);
		}
		
		public static bool FindChildGameObjectWithTag(Transform parentGameObject, string targetTag, out GameObject child)
		{
			// Проверяем, не совпадает ли сам родительский объект с искомым тегом
			if (parentGameObject!.CompareTag(targetTag))
			{
				child = parentGameObject.gameObject;
				return true;
			}

			// Проходим по всем дочерним объектам
			foreach (Transform childTransform in parentGameObject)
			{
				if (childTransform.CompareTag(targetTag))
				{
					child = childTransform.gameObject;
					return true;
				}

				// Рекурсивный вызов для проверки вложенных дочерних объектов
				if (FindChildGameObjectWithTag(childTransform, targetTag, out child))
					return true;
			}

			child = null;
			return false;
		}
		
		public static bool FindChildGameObjectWithTag(Transform parentGameObject, string targetTag)
		{
			return FindChildGameObjectWithTag(parentGameObject, targetTag, out _);
		}

		public static bool Find(Transform parentGameObject, string targetTag)
		{
			return FindChildGameObjectWithTag(parentGameObject, targetTag) ||
			       FindParentGameObjectWithTag(parentGameObject, targetTag);
		}
		
		public static bool Find(Transform parentGameObject, string targetTag, out GameObject x)
		{
			if (FindChildGameObjectWithTag(parentGameObject, targetTag, out var g))
			{
				x = g;
				return true;
			}
			if (FindParentGameObjectWithTag(parentGameObject, targetTag, out var g2))
			{
				x = g2;
				return true;
			}

			x = null;
			return false;
		}
    }
}
    