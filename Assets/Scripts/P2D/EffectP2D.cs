using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[Flags]
public enum EffectType
{

    fade = 1,
    scale = 2,
}

public enum effectStatus
{
    None,Enter,Exit
}

public class EffectP2D:IP2DAnimate
{
    Vector2 originalPosition;
    private EffectType effectType;
    private effectStatus effectStatus;
    float effectTime;
    float elepsedTime = 0;
    int maxFontSize = 36;

    GUIStyle guiStyle = new GUIStyle();
    GUIContent guiContent;
    Rect location;


        bool end = false;

        public bool End
        {
            get { return end; }
        }

        float delay = 0;
    float effectDensity;

   
    public EffectP2D(GUIContent guiContentEffect, Vector2 WorldPosition, effectStatus status, EffectType type,GUIStyle guiStyleEffect, float effectTime = 0.6f,float delay =0)
    {
		this.originalPosition = Camera.main.WorldToScreenPoint(WorldPosition);
		originalPosition.y = Screen.height - originalPosition.y;
        effectStatus = status;
        effectType = type;
        this.effectTime = effectTime;
        this.delay = delay;

        guiStyle = guiStyleEffect;
        maxFontSize = guiStyle.fontSize;
        guiContent = guiContentEffect;

        Vector2 size = guiStyle.CalcSize(guiContent);

        location = new Rect(originalPosition.x - size.x/2, originalPosition.y - size.y/2, size.x, size.y);


    }


    public void Update()
    {
        if (delay > 0)
        {
            delay -= Time.fixedDeltaTime;
            return;
        }
        if (!end)
        {

            elepsedTime += Time.fixedDeltaTime;


            switch (effectStatus)
            {
                case effectStatus.None:
                    effectDensity = 1;
                    break;
                case effectStatus.Enter:
                    effectDensity = elepsedTime / effectTime;

                    break;
                case effectStatus.Exit:
                    effectDensity = 1 - (elepsedTime / effectTime);
                    break;
                default:
                    break;
            }

            if (elepsedTime >= effectTime)
                end = true;
        }
    }
        

    public void Draw()
    {

        if (!end && delay <= 0)
        {
            
            switch (effectStatus)
            {
                case effectStatus.None:
                    break;
                case effectStatus.Enter: // exit and enter are same and parameter only is deferent
                case effectStatus.Exit:

                    if ((effectType & EffectType.fade) == EffectType.fade)
                    {
						Color orginal = guiStyle.normal.textColor;
						guiStyle.normal.textColor = new Color(orginal.r,orginal.g,orginal.b,effectDensity);
                    }
                    if ((effectType & EffectType.scale) == EffectType.scale)
                    {
                        guiStyle.fontSize = (int)(effectDensity * maxFontSize);
                    }

                    break;
                default:
                    break;
            }

        GUI.Label(location, guiContent,guiStyle);

            
        }

    }


    public Vector2 getPosition()
    {
        return location.position;
    }

    public void setPosition(Vector2 Position)
    {
		location = new Rect (Position.x, Position.y, location.width, location.height);
    }

    public Vector2 getScale()
    {
        return Vector2.one;
    }

    public void setScale(Vector2 Scale)
    {
        return;
    }

    public float getRotation()
    {
        return 1;
    }

    public void setRotation(float Rotation)
    {
        return;
    }

   
}
