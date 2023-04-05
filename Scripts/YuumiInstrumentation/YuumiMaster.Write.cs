using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;
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
    private void SendScreen()
    {
        var width = PrimaryMonitor.Instance.Width;
        var height = PrimaryMonitor.Instance.Height;
        Logger.WriteLine($"SEND: Screen {width} {height}");
        ypacket.WriteScreen(width, height);
        SendPacket();
    }

    private void UnifyKeyboardPress(PressState pressed, KeyboardEventData ev)
    {
        var vk = ev.KeyCode;

        if (ev.Shift && MirrorWhenShiftVK.Contains(vk))
        {
            if (ev.Control) // Mirror Control when Shift Control
            {
                Logger.WriteLine($"SEND: KKey {pressed,-5}: {vk} | Control");
                ypacket.WriteKeyPressWithModifier(vk, InputModifiers.Control, pressed);
                SendPacket();
            }
            else
            {
                Logger.WriteLine($"SEND: KKey {pressed,-5}: {vk} | Shift");
                ypacket.WriteKeyPress(vk, pressed);
                SendPacket();
            }
        }
        else if (vk == FOCUS_KEY)
        {
            ypacket.WriteKeyPress(FOCUS_KEY, pressed);
            SendPacket();
        }
        else if (!ev.Shift && !ev.Control)
        {
            ypacket.WriteKeyPress(FOCUS_KEY, pressed);
            SendPacket();
            ypacket.WriteKeyPress(CAMERA_KEY, pressed);
            SendPacket();
            if (vk == SCREEN_KEY)
                SendScreen();
        }

    }

    private void UnifyMousePress(PressState pressed, MouseEventData ev)
    {
        //Logger.WriteLine(ev);
        if (ev.Button == MouseButtonsF.XButton1)
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5} {ev.Button} | Left");
            ypacket.WriteMousePress(MouseButtonsF.Left, pressed);
            SendPacket();
        }
        else if (ev.Button == MouseButtonsF.XButton2)
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5} {ev.Button} | Right");
            ypacket.WriteMousePress(MouseButtonsF.Right, pressed);
            SendPacket();
        }
    }
    #endregion


}