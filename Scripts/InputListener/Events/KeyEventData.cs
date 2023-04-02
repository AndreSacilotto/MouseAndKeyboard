using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <param name="KeyCode">OS key code</param>
/// <param name="ScanCode">Hardware key code</param>
/// <param name="IsKeyDown">The click event is a mouse down</param>
/// <param name="IsKeyUp">The click event is a mouse up</param>
/// <param name="Control">Was Control key pressed</param>
/// <param name="Shift">Was Shift key pressed</param>
/// <param name="Alt">Was Alt key pressed</param>
/// <param name="IsExtendedKey">If true the ScanCode consists of a sequence of two bytes, where the first byte has a Value of 0xE0</param>
/// <param name="Timestamp">The system tick count of when the event occurred</param>
public record class KeyEventData(VirtualKey KeyCode, ScanCode ScanCode, bool IsKeyDown, bool IsKeyUp, bool Control, bool Shift, bool Alt, bool IsExtendedKey, int Timestamp) : EventData(Timestamp)
{
}