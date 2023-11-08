using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaturationValue : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] Image cursorImage;

    ColorPickerControl colorPickerControl;
    RectTransform rectTransform;
    RectTransform cursorTransform;

    float xNorm;
    float yNorm;

    private void Awake()
    {
        colorPickerControl = FindObjectOfType<ColorPickerControl>();
        rectTransform = GetComponent<RectTransform>();
        cursorTransform = cursorImage.GetComponent<RectTransform>();
        cursorTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    public void UpdateColor(PointerEventData eventData)
    {
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        if (pos.x < -deltaX)
        {
            pos.x = -deltaX;
        }
        else if (pos.x > deltaX)
        {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY)
        {
            pos.y = -deltaY;
        }
        else if (pos.y > deltaY)
        {
            pos.y = deltaY;
        }

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        xNorm = x / rectTransform.sizeDelta.x;
        yNorm = y / rectTransform.sizeDelta.y;

        cursorTransform.localPosition = pos;
        cursorImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);

        colorPickerControl.SetSaturationAndValue(xNorm, yNorm);
    }

    public void UpdateColorAfterHueChange()
    {
        colorPickerControl.SetSaturationAndValue(xNorm, yNorm);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }
}
