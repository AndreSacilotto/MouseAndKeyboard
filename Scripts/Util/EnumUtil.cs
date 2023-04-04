using System.Numerics;
using System.Runtime.CompilerServices;

namespace MouseAndKeyboard.Util;

public class EnumUtil
{
    public static string EnumTitle<T>() where T : Enum => typeof(T).Name;
    public static string[] EnumToString<T>() where T : Enum => typeof(T).GetEnumNames();

    public static T StringToEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);
    public static T StringToEnum<T>(string value, bool ignoreCase) where T : Enum => (T)Enum.Parse(typeof(T), value, ignoreCase);

    public static int EnumCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;
    public static T[] EnumToArray<T>() where T : Enum => (T[])typeof(T).GetEnumValues();

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
}
