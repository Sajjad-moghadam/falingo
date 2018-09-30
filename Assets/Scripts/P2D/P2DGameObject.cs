using UnityEngine;
using System.Collections;

public class P2DGameObject :MonoBehaviour, IP2DAnimate
{
    RectTransform rectTransform;
    bool UIRect = false;
	// Use this for initialization
	void Start () 
    {
        rectTransform = GetComponent<RectTransform>();
        
        if (rectTransform != null)
            UIRect = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector2 getPosition()
    {
        if (UIRect)
        {
            return rectTransform.anchoredPosition;
        }
        else
        {
            return transform.position;
        }
    }

    public void setPosition(Vector2 Position)
    {
        if (UIRect)
        {
            rectTransform.anchoredPosition = Position;
            UIHealper.SetAnchorOnObject(rectTransform, rectTransform.parent.GetComponent<RectTransform>());
        }
        else
        {
            transform.position = Position;
        }
    }

    public Vector2 getScale()
    {
        return transform.localScale;
    }

    public void setScale(Vector2 Scale)
    {
        transform.localScale = new Vector3(Scale.x,Scale.y,1);
    }

    public float getRotation()
    {
        return transform.rotation.z;
    }

    public void setRotation(float Rotation)
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Rotation,transform.rotation.w);
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

}
