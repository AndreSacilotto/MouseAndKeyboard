using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

public static class MarshalExt
{
    public static T ToStruct<T>(IntPtr lParam) where T : struct => (T)Marshal.PtrToStructure(lParam, typeof(T))!;
}
