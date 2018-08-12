using UnityEngine;

public static class RectTransformExtensions
{
	public static bool Overlaps(this RectTransform rectA, RectTransform rectB)
	{
		if (!rectA || !rectB)
		{
			return false;
		}

		var corners = new Vector3[4];
		rectB.GetWorldCorners(corners);

		foreach (var corner in corners)
		{
			var localPoint = rectA.InverseTransformPoint(corner);
			if (rectA.rect.Contains(localPoint))
			{
				return true;
			}
		}

		return false;
	}
}
