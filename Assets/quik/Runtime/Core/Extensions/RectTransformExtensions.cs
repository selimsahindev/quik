using UnityEngine;

namespace quik.Runtime.Core.Extensions
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets both the min and max anchor values of the RectTransform.
        /// </summary>
        public static void SetAnchor(this RectTransform rectTransform, Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
        }

        /// <summary>
        /// Sets the pivot of the RectTransform.
        /// </summary>
        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            rectTransform.pivot = pivot;
        }

        /// <summary>
        /// Stretches the RectTransform to fill its parent container.
        /// </summary>
        public static void StretchToParent(this RectTransform rectTransform)
        {
            rectTransform.SetAnchor(Vector2.zero, Vector2.one);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        /// <summary>
        /// Sets the size of the RectTransform.
        /// </summary>
        public static void SetSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.sizeDelta = size;
        }

        /// <summary>
        /// Centers the RectTransform within its parent.
        /// </summary>
        public static void CenterInParent(this RectTransform rectTransform)
        {
            rectTransform.SetAnchor(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            rectTransform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Sets the local position of the RectTransform, adjusting only the X and Y axes.
        /// </summary>
        public static void SetLocalPositionXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.localPosition = new Vector3(x, y, rectTransform.localPosition.z);
        }
    }
}
