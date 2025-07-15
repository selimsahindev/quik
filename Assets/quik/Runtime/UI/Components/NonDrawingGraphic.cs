using UnityEngine;
using UnityEngine.UI;

namespace quik.Runtime.UI.Components
{
    /// <summary>
    /// A UI component that does not render anything but still participates in UI layout, raycasting, and masking.
    /// Useful as an invisible interaction zone or layout helper within a Canvas hierarchy.
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class NonDrawingGraphic : Graphic
    {
#pragma warning disable 0649
#if UNITY_EDITOR
        [SerializeField] private bool visualize;
#endif
#pragma warning restore 0649

#if !UNITY_EDITOR
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
#endif

        protected override void OnPopulateMesh(VertexHelper vertexHelper)
        {
            vertexHelper.Clear();

#if UNITY_EDITOR
            if (!visualize)
            {
                return;
            }
            
            var rect = GetPixelAdjustedRect();
            var width = rect.width;
            var height = rect.height;

            var pivot = rectTransform.pivot;
            var bottomLeftX = -width * pivot.x;
            var bottomLeftY = -height * pivot.y;

            vertexHelper.AddVert(new Vector3(bottomLeftX, bottomLeftY, 0f), color, Vector2.zero);
            vertexHelper.AddVert(new Vector3(bottomLeftX, bottomLeftY + height, 0f), color, Vector2.zero);
            vertexHelper.AddVert(new Vector3(bottomLeftX + width, bottomLeftY + height, 0f), color, Vector2.zero);
            vertexHelper.AddVert(new Vector3(bottomLeftX + width, bottomLeftY, 0f), color, Vector2.zero);

            vertexHelper.AddTriangle(0, 1, 2);
            vertexHelper.AddTriangle(2, 3, 0);
#endif
        }
    }
}
