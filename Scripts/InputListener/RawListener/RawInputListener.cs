using MouseAndKeyboard.Native;
using MouseAndKeyboard.Native.RawInput;

namespace MouseAndKeyboard.InputListener.RawInput;

public class RawInputListener
{

    public static void Create() 
    {
        var inputDevices = new RawInputDevice[2];

        inputDevices[0] = new(HIDUsagePage.Generic, HIDUsage.Mouse, RawInputDeviceFlags.NoLegacy, 0);
        inputDevices[1] = new(HIDUsagePage.Generic, HIDUsage.Keyboard, RawInputDeviceFlags.NoLegacy, 0);

        if (!RawInputAPI.RegisterRawInputDevices(inputDevices))
            throw new Exception("Fail to Register MK");
    }


}
