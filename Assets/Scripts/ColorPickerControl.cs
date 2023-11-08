using UnityEngine;
using UnityEngine.UI;

public class ColorPickerControl : MonoBehaviour
{
    [SerializeField] Image parentImage;
    [SerializeField] Team parentTeam;
    [SerializeField] bool isPrimary;

    public float currentHue;
    public float currentSaturation;
    public float currentValue;

    [SerializeField] SaturationValue saturationValue;
    [SerializeField] RawImage hueImage;
    [SerializeField] RawImage saturationValueImage;
    [SerializeField] Slider hueSlider;

    Texture2D hueTexture;
    Texture2D saturationValueTexture;

    [SerializeField] ColorPickerControl[] otherColorPickers = new ColorPickerControl[3];

    private void Start()
    {
        CreateHueImage();
        CreateSaturationValueImage();
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1));
        }

        hueTexture.Apply();
        currentHue = 0;
        hueImage.texture = hueTexture;
    }

    private void CreateSaturationValueImage()
    {
        saturationValueTexture = new Texture2D(16, 16);
        saturationValueTexture.wrapMode = TextureWrapMode.Clamp;
        saturationValueTexture.name = "SaturationValueTexture";

        for (int i = 0; i < saturationValueTexture.height; i++)
        {
            for (int j = 0; j < saturationValueTexture.width; j++)
            {
                saturationValueTexture.SetPixel(i, j, Color.HSVToRGB(currentHue, (float)i / saturationValueTexture.width, (float)j / saturationValueTexture.height));
            }
        }

        saturationValueTexture.Apply();
        currentSaturation = 0;
        currentValue = 0;
        saturationValueImage.texture = saturationValueTexture;
    }

    public Color GetCurrentColor()
    {
        return Color.HSVToRGB(currentHue, currentSaturation, currentValue);
    }

    public void SetSaturationAndValue(float saturation, float value)
    {
        currentSaturation = saturation;
        currentValue = value;
        parentImage.color = GetCurrentColor();
        if (isPrimary)
        {
            parentTeam.SetPrimaryColor(parentImage.color);
        }
        else
        {
            parentTeam.SetSecondaryColor(parentImage.color);
        }
    }

    public void UpdateSaturationValueImage()
    {
        currentHue = hueSlider.value;

        for (int i = 0; i < saturationValueTexture.height; i++)
        {
            for (int j = 0; j < saturationValueTexture.width; j++)
            {
                saturationValueTexture.SetPixel(i, j, Color.HSVToRGB(currentHue, (float)i / saturationValueTexture.width, (float)j / saturationValueTexture.height));
            }
        }

        saturationValueTexture.Apply();
        saturationValue.UpdateColorAfterHueChange();
    }

    public void ToggleColorPicker()
    {
        for (int i = 0; i < otherColorPickers.Length; i++)
        {
            otherColorPickers[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(!gameObject.activeSelf);
    }
}
