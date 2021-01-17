using UnityEngine;

public static class JumpInput
{
    private static float _previousInput;

    public static float GetJumpInput(float threshold)
    {
        float input = Input.GetAxis("Vertical");

        bool isReleased = input < _previousInput;
        _previousInput = input;

        if (input < threshold && isReleased )
            return threshold;

        if (input >= threshold)
            return 1.0f;


        return 0f;
    }
}