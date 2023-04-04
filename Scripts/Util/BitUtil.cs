using System.Numerics;
using System.Runtime.CompilerServices;

namespace MouseAndKeyboard.Util;

public static class BitUtil
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T UnsetFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value &= ~flags;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value |= flags;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ToggleFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value ^= flags;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlag<T>(T value, T flags) where T : IBinaryInteger<T> => (value & flags) == flags;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasAnyFlag<T>(T value, T flags) where T : IBinaryInteger<T> => (value & flags) != T.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetHighOrderBit<T>(T value) where T : IBinaryInteger<T> => value & ~(T.AllBitsSet >>> 1);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetLowOrderBit<T>(T value) where T : IBinaryInteger<T> => value & T.One;
}