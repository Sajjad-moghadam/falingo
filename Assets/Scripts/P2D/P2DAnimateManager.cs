using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimateType
{
    linear,
    endRadical,
    startRadical,
	pulse
}


/// <summary>
/// 
/// </summary>
public class P2DAnimateManager:SingletonMahsa<P2DAnimateManager>
{
        
    List<AnimateStructP2D> animations = new List<AnimateStructP2D>();

    protected P2DAnimateManager(){ }

    public void Add(IP2DAnimate animateObject, Vector2 startPosition, Vector2 endPosition,Vector2 startScale,Vector2 endScale, float time,AnimateType Type = AnimateType.linear,float startDelay = 0)
    {
        //CheckExistingAnimate(animateObject);
        animations.Add(new AnimateStructP2D(animateObject, startPosition, endPosition, time, Type,startDelay));
    }

    private void CheckExistingAnimate(IP2DAnimate animateObject)
    {
        //for (int i = 0; i < animations.Count; i++)
        //{
        //    if (animations[i].hasObject(animateObject))
        //    {
        //        if (!animations[i].isEnd)
        //        {
        //            animations[i].immediateEnd();
        //            Debug.Log("ImmediateEnd");
        //        }
        //        animations.RemoveAt(i);
        //    }
        //}
    }

    public void Add(IP2DAnimate animateObject, Vector2 startPosition, Vector2 endPosition, float time, AnimateType Type = AnimateType.linear, float startDelay = 0)
    {
        CheckExistingAnimate(animateObject);

        animations.Add(new AnimateStructP2D(animateObject, startPosition, endPosition, time, Type, startDelay));
    }


    public void Add(AnimateStructP2D animateStruct)
    {
        CheckExistingAnimate(animateStruct.animateObject);


        animations.Add(animateStruct);
    }

    public void Update()
    {

        for (int i = 0; i < animations.Count; i++)
        {
            try
            {
                animations[i].Update();

                if (animations[i].isEnd)
                {
                    animations.RemoveAt(i);
                }
            }
            catch
            {
                Debug.LogWarning("Animation Deleted");
                animations.RemoveAt(i);

            }

        }
    }

    public void removeAllAnimate()
    {
        for (int i = 0; i < animations.Count; i++)
        {
            animations[i].immediateEnd();
            animations.RemoveAt(i);
        }
    }

    public void remove(P2DGameObject p2dObject)
    {
        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i].animateObject == p2dObject)
            {
                animations.RemoveAt(i);
            }
        }
    }

}
