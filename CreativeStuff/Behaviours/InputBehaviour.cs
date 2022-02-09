using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CreativeStuff
{
    public class InputBehaviour : MonoBehaviour
    {
        public static InputBehaviour Create(string labelText, UnityAction<string> callback)
        {
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            //Create and configure parent Object
            var inputBehaviourGameObject = new GameObject("InputBehaviour", typeof(RectTransform), typeof(InputBehaviour), typeof(CanvasRenderer), typeof(Image), typeof(HorizontalLayoutGroup));
            var inputBehaviour = inputBehaviourGameObject.GetComponent<InputBehaviour>();
            var image = inputBehaviourGameObject.GetComponent<Image>();
            var rectTransform = inputBehaviourGameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 40);

            inputBehaviour.callback = callback;

            Color backgroundColor;
            ColorUtility.TryParseHtmlString("#292929", out backgroundColor);
            image.color = backgroundColor;

            //Create Label
            var labelGameObject = new GameObject("Label", typeof(Text));
            var label = labelGameObject.GetComponent<Text>();
            label.rectTransform.SetParent(inputBehaviourGameObject.transform);
            label.font = ArialFont;
            label.text = labelText;
            label.color = Color.white;
            label.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, label.preferredWidth);
            label.alignment = TextAnchor.MiddleRight;
            label.fontSize = 32;
            label.resizeTextForBestFit = true;

            //Create Inputfield
            var inputfieldGameObject = new GameObject("Input", typeof(InputField), typeof(CanvasRenderer), typeof(Image));
            var inputfield = inputfieldGameObject.GetComponent<InputField>();
            var inputfieldImage = inputfieldGameObject.GetComponent<Image>();
            inputfieldImage.color = Color.gray;

            var inputTextDisplay = new GameObject("Text", typeof(Text));
            inputfield.textComponent = inputTextDisplay.GetComponent<Text>();
            inputfield.textComponent.color = Color.white;
            inputfield.textComponent.font = ArialFont;
            inputfield.textComponent.rectTransform.SetParent(inputfield.transform, false);
            inputfield.textComponent.rectTransform.anchorMax = Vector2.one;
            inputfield.textComponent.rectTransform.anchorMin = Vector2.zero;
            inputfield.textComponent.rectTransform.sizeDelta = Vector2.zero;
            inputfield.textComponent.rectTransform.offsetMin = new Vector2(5, 2);
            inputfield.textComponent.alignment = TextAnchor.MiddleCenter;
            inputfield.textComponent.fontSize = 32;
            inputfield.textComponent.supportRichText = false;

            inputfieldGameObject.transform.SetParent(inputBehaviourGameObject.transform);
            inputBehaviour.input = inputfield;

            return inputBehaviour;
        }

        private UnityAction<string> callback;
        private InputField input;

        private void Start()
        {
            input.onEndEdit.AddListener(callback);
        }
    }
}