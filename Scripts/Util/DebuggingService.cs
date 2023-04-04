namespace MouseAndKeyboard.Util;

public class DebuggingService
{
    public static bool IsDebugMode
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
