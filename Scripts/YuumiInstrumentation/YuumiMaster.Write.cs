using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

partial class YuumiMaster
{
    private void SendPacket()
    {
        socket.MySocket.Send(ypacket, out _);
        ypacket.Reset();
    }

    #region Events Funcs

    private void WhenConnect(bool isServer)
    {
        var width = MouseAndKeyboard.Native.PrimaryMonitor.Instance.Width;
        var height = MouseAndKeyboard.Native.PrimaryMonitor.Instance.Height;
        Logger.WriteLine($"SEND: Screen {width} {height}");
        ypacket.WriteScreen(width, height);
        SendPacket();
    }

    private void WhenKeyDown(KeyEventData ev) => UnifyKey(PressState.Down, ev);
    private void WhenKeyUp(KeyEventData ev) => UnifyKey(PressState.Up, ev);

    private void WhenMouseDown(MouseEventData ev) => UnifyMouse(PressState.Down, ev);
    private void WhenMouseUp(MouseEventData ev) => UnifyMouse(PressState.Up, ev);

    private void WhenMouseMove(MouseEventData ev)
    {
        Logger.WriteLine($"SEND: MMove {ev.X} {ev.Y}");
        ypacket.WriteMouseMove(ev.X, ev.Y);
        SendPacket();
    }

    private void WhenMouseScroll(MouseEventData ev)
    {
        Logger.WriteLine("SEND: MScroll " + ev.SrollDelta);
        ypacket.WriteMouseScroll(ev.SrollDelta);
        SendPacket();
    }

    #endregion

    #region Unify
    private void UnifyKey(PressState pressed, KeyEventData ev)
    {
        Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev}");
        if (ev.Shift && MirrorWhenShiftKeys.Contains(ev.KeyCode))
        {
            //Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} | Shift");
            ypacket.WriteKey(ev.KeyCode, pressed);
            SendPacket();
        }
        else if (ev.Control && KeyWithShiftWhenControlShift.TryGetValue(ev.KeyCode, out var key))
        {
            //Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} => {key} | Control");
            ypacket.WriteKeyModifier(key, InputModifiers.Control, pressed);
            SendPacket();
        }
    }
    private void UnifyMouse(PressState pressed, MouseEventData ev)
    {
        Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev}");
        if (MouseToKey.TryGetValue(ev.Button, out var key))
        {
            //Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button} => {key}");
            ypacket.WriteKey(key, pressed);
            SendPacket();
        }
        else
        {
            //Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button}");
            ypacket.WriteMouseClick(ev.Button, pressed);
            SendPacket();
        }
    }
    #endregion

}