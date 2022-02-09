using UnityEngine;
using UnityEngine.UI;

namespace CreativeStuff
{
    internal class CanvasBehaviour : MonoBehaviour
    {
        internal static CanvasBehaviour Create()
        {
            var canvasGameObject = new GameObject("InputBehaviour", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(RectTransform), typeof(CanvasBehaviour));
            var canvas = canvasGameObject.GetComponent<Canvas>();
            var canvasScaler = canvasGameObject.GetComponent<CanvasScaler>();
            var canvasBehaviour = canvasGameObject.GetComponent<CanvasBehaviour>();

            canvas.sortingOrder = int.MaxValue;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.matchWidthOrHeight = 1f;

            return canvasBehaviour;
        }

        private RectTransform contentContainer;

        public void AddContent(Transform content)
        {
            content.SetParent(contentContainer);
        }

        private void Awake()
        {
            var contentContainer = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            var layoutGroup = contentContainer.GetComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childControlHeight = false;
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandHeight = false;

            var sizeFitter = contentContainer.GetComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var layoutRect = contentContainer.GetComponent<RectTransform>();
            layoutRect.transform.SetParent(transform, false);
            layoutRect.anchorMax = layoutRect.anchorMin = layoutRect.pivot = new Vector2(0, 1);
            layoutRect.anchoredPosition = new Vector2(50, -300);
            layoutRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);

            this.contentContainer = layoutRect;
        }
    }
}