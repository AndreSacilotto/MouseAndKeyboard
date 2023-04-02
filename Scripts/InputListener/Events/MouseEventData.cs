using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <param name="Button">Indicates which MouseButtonVK was pressed</param>
/// <param name="IsButtonDown">True if event signals mouse button down</param>
/// <param name="IsButtonUp">True if event signals mouse button up</param>
/// <param name="X">The X coordinate/position of the mouse</param>
/// <param name="Y">The Y coordinate/position of the mouse</param>
/// <param name="Clicks">The number of times a mouse button was pressed</param>
/// <param name="SrollDelta">A signed count of the number of detents the wheel has rotated</param>
/// <param name="IsHorizontalWheel">True if event signals horizontal wheel action</param>
/// <param name="Timestamp">The system tick count when the event occurred</param>
public record class MouseEventData(MouseButtonsF Button, bool IsButtonDown, bool IsButtonUp, int Clicks, int X, int Y, int SrollDelta, bool IsHorizontalWheel, int Timestamp) : EventData(Timestamp)
{
    /// <summary>True if event contains information about wheel scroll</summary>
    public bool IsScrollEvent => SrollDelta != 0;

    public bool IsClickEvent => Clicks > 0;

    /// <summary>Gets the location of the mouse during MouseEvent</summary>
    public Point GetPosition() => new(X, Y);

    public MouseEventData ToDoubleClickEventArgs() => new(Button, IsButtonDown, IsButtonUp, 2, X, Y, SrollDelta, IsHorizontalWheel, Timestamp);
}