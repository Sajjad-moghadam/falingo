using UnityEngine;
using System.Collections;

public class P2DGUI : IP2DAnimate
{

    GUIStyle guiStyle ;
    GUIContent guiContent;
    Rect location;

    float effectDensity;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="content">Content.</param>
	/// <param name="style">Style.</param>
	/// <param name="position">World Position</param>
    public P2DGUI(GUIContent content, GUIStyle style, Vector2 WorldPosition)
    {
		Vector2 screenPosition = Camera.main.WorldToScreenPoint (WorldPosition);
		screenPosition.y  = Screen.height - screenPosition.y;
        guiStyle = style;
        guiContent = content;

        Vector2 size = guiStyle.CalcSize(guiContent);

		location = new Rect(screenPosition.x - size.x / 2, screenPosition.y - size.y / 2, size.x, size.y);
    }



	public void Draw()
    {
        GUI.Label(location, guiContent, guiStyle);

    }


    public Vector2 getPosition()
    {
        return location.position;
    }

    public void setPosition(Vector2 Position)
    {
        location = new Rect(Position.x, Position.y, location.width, location.height);
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
