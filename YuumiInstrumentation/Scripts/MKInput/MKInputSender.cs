using System;

namespace MouseKeyboard.MKInput
{
    public abstract class MKInputSender : IDisposable
    {
        public abstract void Dispose();
    }
}