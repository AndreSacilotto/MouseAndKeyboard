using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.InputSimulator;
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

    #region Event Funcs

    private void WhenConnect(bool isServer)
    {
        var width = PrimaryMonitor.Instance.Width;
        var height = PrimaryMonitor.Instance.Height;
        Logger.WriteLine($"SEND: Screen {width} {height}");
        ypacket.WriteScreen(width, height);
        SendPacket();
    }

    private void WhenKeyDown(KeyHookEventArgs ev) => UnifyKey(PressState.Down, ev);
    private void WhenKeyUp(KeyHookEventArgs ev) => UnifyKey(PressState.Up, ev);

    private void WhenMouseDown(MouseHookEventArgs ev) => UnifyMouse(PressState.Down, ev);
    private void WhenMouseUp(MouseHookEventArgs ev) => UnifyMouse(PressState.Up, ev);

    private void WhenMouseMove(MouseHookEventArgs ev)
    {
        Logger.WriteLine($"SEND: MMove {ev.X} {ev.Y}");
        ypacket.WriteMouseMove(ev.X, ev.Y);
        SendPacket();
    }

    private void WhenMouseScroll(MouseHookEventArgs ev)
    {
        Logger.WriteLine("SEND: MScroll " + ev.Delta);
        ypacket.WriteMouseScroll(ev.Delta);
        SendPacket();
    }

    #endregion

    #region Unify
    private void UnifyKey(PressState pressed, KeyHookEventArgs ev)
    {
        if (ev.Shift && MirrorWhenShiftKeys.Contains(ev.KeyCode))
        {
            Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} | Shift");
            ypacket.WriteKey(ev.KeyCode, pressed);
            SendPacket();
        }
        else if (ev.Control && SkillUpKeys.TryGetValue(ev.KeyCode, out var key))
        {
            Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} => {key} | Control");
            ypacket.WriteKeyModifier(key, Keys.LControlKey, pressed);
            SendPacket();
        }
    }
    private void UnifyMouse(PressState pressed, MouseHookEventArgs ev)
    {
        if (MouseToKey.TryGetValue(ev.Button, out var key))
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button} => {key}");
            ypacket.WriteKey(key, pressed);
            SendPacket();
        }
        else
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button}");
            ypacket.WriteMouseClick(ev.Button, pressed);
            SendPacket();
        }
    }
    #endregion

}