using UnityEngine;
using System.Collections;

public enum InputDirection
{
    Left, Right, Up, Down, NoDirection
}

public class InputHelperP2D {

    public static InputDirection getDirection(Vector2 startPosition, Vector2 endPosition)
    {

        Vector2 direction = endPosition - startPosition;

        if (direction.magnitude < 30)
            return InputDirection.NoDirection;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -30 && angle <= 30)
        {
            return InputDirection.Right;
        }
        else if (angle >= 60 && angle <= 120)
        {
            return InputDirection.Up;
        }
        else if (angle > 150 && angle <= 180 || angle < -150)
        {
            return InputDirection.Left;
        }
        else if (angle < -60 && angle >= -120)
        {
            return InputDirection.Down;
        }

        return InputDirection.NoDirection;
    }
}
