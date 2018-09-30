using UnityEngine;
using System.Collections;
using System;

public class ColorfulEffectScript : MonoBehaviour {

    private enum ColorState
    {
        Red,
        RedGreen,
        Green,
        GreenBlue,
        Blue,
        BlueRed,
    }

    public enum LightState
    {
        TurnOn,
        Normal,
        TurnOff,
        Off
    }

    const float changeColorScaleFactor = 0.1f;
    const float turnOffScaleFactor = 0.2f;
    const float turnOnScaleFactor = 0.2f;

    SpriteRenderer myRenderer;

    LightState lightState = LightState.TurnOn;

    ColorState colorState = ColorState.Red;
    float red =1;
    float green = 0.3f;
    float blue = 0.3f;
    float alpha = 1f;
	// Use this for initialization
	void Start ()
    {
        myRenderer = GetComponent<SpriteRenderer>();
       
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        switch (lightState)
        {
            case LightState.TurnOn:
                TransitionLightOn();
                SwitchColor();
                break;
            case LightState.Normal:
                SwitchColor();

                break;
            case LightState.TurnOff:
                TransitionLightOff();
                SwitchColor();
                break;
            case LightState.Off:
                break;
            default:
                break;
        }
    }

    private void SwitchColor()
    {
        switch (colorState)
        {
            case ColorState.Red:
                if (blue <= 0.3f)
                {
                    blue = 0.3f;
                    colorState = ColorState.RedGreen;
                }
                else
                {
                    blue -= Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            case ColorState.RedGreen:
                if (green >= 1)
                {
                    green = 1;
                    colorState = ColorState.Green;
                }
                else
                {
                    green += Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            case ColorState.Green:
                if (red <= 0.3f)
                {
                    red = 0.3f;
                    colorState = ColorState.GreenBlue;
                }
                else
                {
                    red -= Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            case ColorState.GreenBlue:
                if (blue >= 1)
                {
                    blue = 1;
                    colorState = ColorState.Blue;
                }
                else
                {
                    blue += Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            case ColorState.Blue:
                if (green <= 0.3f)
                {
                    green = 0.3f;
                    colorState = ColorState.BlueRed;
                }
                else
                {
                    green -= Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            case ColorState.BlueRed:
                if (red >= 1)
                {
                    red = 1;
                    colorState = ColorState.Red;
                }
                else
                {
                    red += Time.fixedDeltaTime * changeColorScaleFactor;
                    SetColor();
                }
                break;
            default:
                break;
        }
    }

    private void SetColor()
    {
        myRenderer.color = new Color(red, green, blue,alpha);
    }

    private void TransitionLightOff()
    {
        alpha -= Time.fixedDeltaTime * turnOffScaleFactor;

        if(alpha <=0)
        {
            alpha = 0;
            lightState = LightState.Off;
            gameObject.SetActive(false);

        }

    }

    private void TransitionLightOn()
    {
        alpha += Time.fixedDeltaTime * turnOnScaleFactor;

        if (alpha >= 1f)
        {
            alpha = 1f;
            lightState = LightState.Normal;
        }

    }

    public void TurnLightOn()
    {
        gameObject.SetActive(true);
        lightState = LightState.TurnOn;
    }

    public void TurnLightOff()
    {
        lightState = LightState.TurnOff;
    }

}
