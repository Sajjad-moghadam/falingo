using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void StartAnimateEventHandler(IP2DAnimate sender);
public delegate void FinishAnimateEventHandler(IP2DAnimate sender);


public class AnimateStructP2D
{

    public event StartAnimateEventHandler StartAnimate;
    public event FinishAnimateEventHandler FinishAnimate;

    public void OnStartAnimate()
    {
        if (StartAnimate != null)
        {
            StartAnimate(animateObject);
        }
    }

    private void OnFinishAnimate()
    {
        if (FinishAnimate != null)
        {
            FinishAnimate(animateObject);
        }
    }

    public enum AnimateState
    {
        Delay,
        Start,
        End
    }

    Vector2 startPosition;
    Vector2 changePosition = Vector2.zero;
    Vector2 startScale;
    Vector2 changeScale = Vector2.zero;
    float startDelay;
    float time;
    float elepsedTime = 0;

    float startRotation;
    float changeRotation = 0;

    /// <summary>
    /// for avoid some problem(if user want change variable during animate we lock Unnecessary variable)
    /// </summary>
    bool lockPosition = true;
    bool lockRotation = true;
    bool lockScale = true;

    public IP2DAnimate animateObject;
    AnimateType type;

    private AnimateState _animateState = AnimateState.Delay;

    public AnimateState animateState
    {
        get { return _animateState; }
        set
        {
            _animateState = value;

            switch (_animateState)
            {
                case AnimateState.Delay:
                    break;
                case AnimateState.Start:
                    OnStartAnimate();
                    break;
                case AnimateState.End:
                    OnFinishAnimate();
                    break;
                default:
                    break;
            }
        }
    }


    float variation; // this parameter set in every Update

    public bool isEnd
    {
        get
        {
            if (_animateState == AnimateState.End)
                return true;
            else
                return false;
        }
    }

    public AnimateStructP2D(IP2DAnimate AnimateObject, float AnimateTime, AnimateType Type = AnimateType.linear, float StartDelay = 0)
    {
        InitAnimate(AnimateObject, AnimateTime, Type, StartDelay);

    }

    

    public AnimateStructP2D(IP2DAnimate AnimateObject, Vector2 StartPosition, Vector2 EndPosition, float AnimateTime, AnimateType Type = AnimateType.linear, float StartDelay = 0)
    {
        InitAnimate(AnimateObject, AnimateTime, Type, StartDelay);

        AddMoveToAnimate(StartPosition,EndPosition);

    }

    private void InitAnimate(IP2DAnimate AnimateObject, float Time, AnimateType Type, float StartDelay)
    {
        animateObject = AnimateObject;
        time = Time;
        type = Type;
        startDelay = StartDelay;

        SetStartStatus();

    }

    private void SetStartStatus()
    {
        startPosition = animateObject.getPosition();
        startScale = animateObject.getScale();
        startRotation = animateObject.getRotation();
    }
    public void Update()
    {

        switch (_animateState)
        {
            case AnimateState.Delay: DelayUpdate();
                break;
            case AnimateState.Start: StartStateUpdate();
                break;
            case AnimateState.End:
                break;
            default:
                break;
        }


    }

    private void StartStateUpdate()
    {
         elepsedTime += Time.fixedDeltaTime;
         if (elepsedTime >= time)
         {
             elepsedTime = time;
         }

         ProcessAnimate();


        if (elepsedTime >= time) // after processAnimateDueToVariation we sholud call AnimateState.End to avoid problem
        {
            animateState = AnimateState.End;
        }
        
    }

    private void ProcessAnimate()
    {
        __SetVareationDueToElepsedTime();

        __processAnimateDueToVariation();
    }

    /// <summary>
    /// only call from processAnimate
    /// </summary>
    private void __SetVareationDueToElepsedTime()
    {
        switch (type)
        {
            case AnimateType.linear: SetVariationLinear();
                break;
            case AnimateType.endRadical: SetVariationEndRadical();
                break;
            case AnimateType.startRadical: SetVariationStartRadical();
                break;
            case AnimateType.pulse: SetVariationPulse();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// only call in processAnimate
    /// </summary>
    private void __processAnimateDueToVariation()
    {
        if (!lockPosition)
        {
            animateObject.setPosition(startPosition + (changePosition * variation));
        }
        if (!lockRotation)
        {
            animateObject.setRotation(startRotation + (changeRotation * variation));
        }
        if (!lockScale)
        {
            animateObject.setScale(startScale + (changeScale * variation));
        }
    }

    private void DelayUpdate()
    {
        startDelay -= Time.fixedDeltaTime;
        if (startDelay <= 0)
            animateState = AnimateState.Start;
    }

    public bool hasObject(IP2DAnimate testObject)
    {
        if (animateObject == testObject)
        {
            return true;
        }

        return false;
    }

    public void immediateEnd()
    {
        elepsedTime = time;

        ProcessAnimate();

        animateState = AnimateState.End;

    }
    
	private void SetVariationPulse()
	{
        if ( (elepsedTime / time) < 0.5f)
        {
            variation = elepsedTime / time;
        }
        else
        {
            variation = (time - elepsedTime) / time;
        }
	}

    private void SetVariationLinear()
    {
        variation = elepsedTime / time;

    }

    private void SetVariationStartRadical()
    {

        variation = (float)Math.Pow((elepsedTime / time), 3);



    }

    private void SetVariationEndRadical()
    {
        variation = (float)Math.Sqrt((elepsedTime / time));



    }

    public void AddResizingToAnimate(Vector2 scaleStart, Vector2 scaleEnd)
    {
        startScale = scaleStart;
        changeScale = scaleEnd - scaleStart;

        animateObject.setScale(startScale);

        lockScale = false;
    }

    public void AddMoveToAnimate(Vector2 StartPosition,Vector2 EndPosition)
    {
        startPosition = StartPosition;
        changePosition = EndPosition - startPosition;

        lockPosition = false;
    }

    /// <summary>
    /// add rotation to animate
    /// </summary>
    /// <param name="startRotation">in radian</param>
    /// <param name="changeRotation">in radian</param>
    public void AddRotationToAnimate(float StartRotation, float ChangeRotation)
    {
        this.startRotation = StartRotation;
        this.changeRotation = ChangeRotation;

        lockRotation = false;
    }
}