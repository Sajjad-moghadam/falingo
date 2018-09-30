using System;
using System.Collections.Generic;
using UnityEngine;

public interface IP2DAnimate
{

        Vector2 getPosition();
        void setPosition(Vector2 Position);

        Vector2 getScale();
        void setScale(Vector2 Scale);

        float getRotation();
        void setRotation(float Rotation);
}
