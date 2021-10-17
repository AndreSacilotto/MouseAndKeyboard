/// <summary> 
/// http://sol.gfxile.net/interpolation/index.html#s3 
/// https://stackoverflow.com/questions/33044848/c-sharp-lerping-from-position-to-position
/// </summary>
public static class Interpolation
{
    public static float CubicInterpolation(float v0, float v1, float v2, float v3, float t)
    {
        var p = v3 - v2 - v0 - v1;
        var q = v0 - v1 - p;
        var r = v2 - v0;
        var s = v1;
        return (p * t * 3) + (q * t * 2) + (r * t) + s;
    }
    public static float QuadraticInterpolation(float v0, float v1, float v2, float t)
    {
        var v01 = Lerp(v0, v1, t);
        var v12 = Lerp(v1, v2, t);
        return Lerp(v01, v12, t);
    }
    public static float Lerp(float v0, float v1, float t)
    {
        return v0 + t * (v1 - v0);
    }
    public static float PerlinSmoothStep(float t)
    {
        // Ken Perlin's version
        return t * t * t * ((t * ((6 * t) - 15)) + 10);
    }
    public static float SmoothStep(float edge0, float edge1, float x)
    {
        var t = Clamp01((x - edge0) / (edge1 - edge0));
        return t * t * (3f - 2f * t);
    }

    public static float SmootherStep(float edge0, float edge1, float x)
    {
        x = Clamp01((x - edge0) / (edge1 - edge0));
        return x * x * x * (x * (x * 6 - 15) + 10);
    }

    public static float Slerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static float Clamp01(float value)
    {
        if (value < 0f) return 0f;
        if (value > 1f) return 1f;
        return value;
    }
}
