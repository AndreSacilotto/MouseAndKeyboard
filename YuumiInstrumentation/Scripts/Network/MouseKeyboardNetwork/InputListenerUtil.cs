using System;
using System.Text;
using System.Windows.Forms;

public static class InputListenerUtil
{
    public static void Print(MouseEventArgs ev)
    {
        var str = new StringBuilder();
        str.AppendLine("Button: " + ev.Button.ToString());
        str.AppendLine("Clicks: " + ev.Clicks.ToString());
        str.AppendLine("Delta: " + ev.Delta.ToString());
        str.AppendLine("Location: " + ev.Location.ToString());
        Console.WriteLine(str.ToString());
    }

    public static void Print(KeyEventArgs ev)
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
        Console.WriteLine(str.ToString());
    }

    public static void Print(KeyPressEventArgs ev)
    {
        var str = new StringBuilder();
        str.AppendLine("Alt: " + ev.Handled.ToString());
        str.AppendLine("Control: " + ev.KeyChar.ToString());
        Console.WriteLine(str.ToString());
    }

}
