using UnityEngine;

public static class AnimationCurveExtensions
{
    public static void BuildCurve(this AnimationCurve animationCurve, EasingType easingType)
    {
        animationCurve.keys = null;
        float i = 0.0f;
        while(Mathf.Approximately(i, 1.0f) == false)
        {
            switch (easingType)
            {
                case EasingType.EaseInElastic:
                    animationCurve.AddKey(i, EaseInElastic(i));
                    break;
                case EasingType.EaseOutElastic:
                    animationCurve.AddKey(i, EaseOutElastic(i));
                    break;
                case EasingType.EaseInOutElastic:
                    animationCurve.AddKey(i, EaseInOutElastic(i));
                    break;
                case EasingType.EaseInBounce:
                    animationCurve.AddKey(i, EaseInBounce(i));
                    break;
                case EasingType.EaseOutBounce:
                    animationCurve.AddKey(i, EaseOutBounce(i));
                    break;
                case EasingType.EaseInOutBounce:
                    animationCurve.AddKey(i, EaseInOutBounce(i));
                    break;
            }
            i += 0.01f;
        }
    }
    private static float EaseInElastic(float x)  
    {
        float c4 = (2 * Mathf.PI) / 3;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
    }

    private static float EaseOutElastic(float x) 
    {
        float c4 = (2 * Mathf.PI) / 3;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : -Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 10.75f) * c4) + 1;
    }

    private static float EaseInOutElastic(float x) 
    {
        float c5 = (2 * Mathf.PI) / 4.5f;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : x < 0.5
          ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
          : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }

    private static float EaseInBounce(float x)
    {
        return 1 - EaseOutBounce(1 - x);
    }

    private static float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    private static float EaseInOutBounce(float x)
    {
        return x < 0.5
            ? (1 - EaseOutBounce(1 - 2 * x)) / 2
            : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }
}
