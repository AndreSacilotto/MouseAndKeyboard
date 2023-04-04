using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;
using static YuumiInstrumentation.InputData;

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
        var width = PrimaryMonitor.Instance.Width;
        var height = PrimaryMonitor.Instance.Height;
        Logger.WriteLine($"SEND: Screen {width} {height}");
        ypacket.WriteScreen(width, height);
        SendPacket();
    }

    private void WhenKeyDown(KeyboardEventData ev) => UnifyKeyboardPress(PressState.Down, ev);
    private void WhenKeyUp(KeyboardEventData ev) => UnifyKeyboardPress(PressState.Up, ev);

    private void WhenMouseDown(MouseEventData ev) => UnifyMousePress(PressState.Down, ev);
    private void WhenMouseUp(MouseEventData ev) => UnifyMousePress(PressState.Up, ev);

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

    int funtionKey;

    private void UnifyKeyboardPress(PressState pressed, KeyboardEventData ev)
    {
        var vk = ev.KeyCode;

        if (FunctionKeys.TryGetValue(vk, out int mask)) 
        {
            if (EnumUtil.HasFlag(funtionKey, mask))
            {
                ypacket.WriteKeyPress(ev.KeyCode, PressState.Down);
                funtionKey = EnumUtil.UnsetFlags(funtionKey, mask);
            }
            else 
            {
                ypacket.WriteKeyPress(ev.KeyCode, PressState.Up);
                funtionKey = EnumUtil.SetFlags(funtionKey, mask);
            }
            SendPacket();
        }

        Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev}");
        if (ev.Shift && MirrorWhenShiftKeys.Contains(ev.KeyCode))
        {
            //Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} | Shift");
            ypacket.WriteKeyPress(ev.KeyCode, pressed);
            SendPacket();
        }
        else if (ev.Control && KeyWithShiftWhenControlShift.TryGetValue(ev.KeyCode, out var key))
        {
            //Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} => {key} | Control");
            ypacket.WriteKeyPressWithModifier(key, InputModifiers.Control, pressed);
            SendPacket();
        }
    }
    private void UnifyMousePress(PressState pressed, MouseEventData ev)
    {

        if (ev.Button == MouseButtonsF.XButton1)
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5} {ev.Button} | Right");
            ypacket.WriteMousePress(MouseButtonsF.Right, pressed);
            SendPacket();
        }
        else if (ev.Button == MouseButtonsF.XButton2)
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5} {ev.Button} | Left");
            ypacket.WriteMousePress(MouseButtonsF.Left, pressed);
            SendPacket();
        }

    }
    #endregion


}