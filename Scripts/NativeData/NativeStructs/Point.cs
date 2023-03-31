using System.Numerics;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

/// <summary>Point is represents a position</summary>
/// <remarks>
/// https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-point
/// A point is a special, it uses long on its definition everyone just use int for it for some reason
/// <remarks/>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Point(int X, int Y) :
    IMultiplyOperators<Point, Point, Point>, IMultiplyOperators<Point, int, Point>,
    IDivisionOperators<Point, Point, Point>, IDivisionOperators<Point, int, Point>,
    IAdditionOperators<Point, Point, Point>, IAdditionOperators<Point, int, Point>,
    ISubtractionOperators<Point, Point, Point>, ISubtractionOperators<Point, int, Point>,
    IUnaryNegationOperators<Point, Point>
{
    public static Point Zero => new(0, 0);

    public static Point operator *(Point a, Point b) => new(a.X * b.X, a.Y * b.Y);
    public static Point operator *(int a, Point b) => new(a * b.X, a * b.Y);
    public static Point operator *(Point a, int b) => new(a.X * b, a.Y * b);

    public static Point operator /(Point a, Point b) => new(a.X / b.X, a.Y / b.Y);
    public static Point operator /(int a, Point b) => new(a / b.X, a / b.Y);
    public static Point operator /(Point a, int b) => new(a.X / b, a.Y / b);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator +(int a, Point b) => new(a + b.X, a + b.Y);
    public static Point operator +(Point a, int b) => new(a.X + b, a.Y + b);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator -(int a, Point b) => new(a - b.X, a - b.Y);
    public static Point operator -(Point a, int b) => new(a.X - b, a.Y - b);

    public static Point operator -(Point a) => new(-a.X, -a.Y);

}
