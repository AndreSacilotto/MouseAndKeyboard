namespace MouseAndKeyboard.InputListener;

/// <param name="Timestamp">The system tick count of when the event occurred</param>
public record class EventData(int Timestamp)
{
    #region Handled
    private bool handled = false;
    public bool Handled => handled;
    /// <summary>SetFlags this event as handled, used to prevent further processing of this event</summary>
    public void StopPropagation() => handled = true;
    #endregion
}
