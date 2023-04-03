using System.Runtime.CompilerServices;

namespace MouseAndKeyboard.Util.Log;

public static class Logger
{
    public static bool Enabled => OnLog == null;

    public static event Action<string>? OnLog;

    //[Conditional("DEBUG")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Write(string log) => OnLog?.Invoke(log);

    //[Conditional("DEBUG")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Write(object log) => OnLog?.Invoke(log.ToString()!);

    //[Conditional("DEBUG")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteLine(string log) => OnLog?.Invoke(log + Environment.NewLine);
    public static void WriteLine(ReadOnlySpan<string> log) => OnLog?.Invoke(log.ToString() + Environment.NewLine);

    //[Conditional("DEBUG")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteLine(params object[] logs) => OnLog?.Invoke(string.Join(' ', logs) + Environment.NewLine);

}
