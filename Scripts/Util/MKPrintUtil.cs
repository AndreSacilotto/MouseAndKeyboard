using System.Text;

namespace MouseAndKeyboard.Util;

internal static class MKPrintUtil
{
    public static string Print(MouseEventArgs ev)
    {
        var str = new StringBuilder();
        str.AppendLine("Button: " + ev.Button.ToString());
        str.AppendLine("Clicks: " + ev.Clicks.ToString());
        str.AppendLine("Delta: " + ev.Delta.ToString());
        str.AppendLine("Location: " + ev.Location.ToString());
        return str.ToString();
    }

    public static string Print(KeyEventArgs ev)
    {
        var str = new StringBuilder();
        str.AppendLine("Alt: " + ev.Alt.ToString());
        str.AppendLine("Control: " + ev.Control.ToString());
        str.AppendLine("Shift: " + ev.Shift.ToString());
        str.AppendLine("Handled: " + ev.Handled.ToString());
        str.AppendLine("KeyCode: " + ev.KeyCode.ToString());
        str.AppendLine("KeyData: " + ev.KeyData.ToString());
        str.AppendLine("KeyValue: " + ev.KeyValue.ToString());
        str.AppendLine("Modifiers: " + ev.Modifiers.ToString());
        str.AppendLine("SuppressKeyPress: " + ev.SuppressKeyPress.ToString());
        return str.ToString();
    }

    public static string Print(KeyPressEventArgs ev)
    {
        var str = new StringBuilder();
        str.AppendLine("Alt: " + ev.Handled.ToString());
        str.AppendLine("Control: " + ev.KeyChar.ToString());
        return str.ToString();
    }
}