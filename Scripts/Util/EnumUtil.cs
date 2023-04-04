namespace MouseAndKeyboard.Util;

public class EnumUtil
{
    public static string EnumTitle<T>() where T : Enum => typeof(T).Name;
    public static string[] EnumToString<T>() where T : Enum => typeof(T).GetEnumNames();

    public static T StringToEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);
    public static T StringToEnum<T>(string value, bool ignoreCase) where T : Enum => (T)Enum.Parse(typeof(T), value, ignoreCase);

    public static int EnumCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;
    public static T[] EnumToArray<T>() where T : Enum => (T[])typeof(T).GetEnumValues();
}
