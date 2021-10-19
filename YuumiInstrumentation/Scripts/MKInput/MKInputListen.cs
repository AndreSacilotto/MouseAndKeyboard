using Gma.System.MouseKeyHook;
using System;

namespace MouseKeyboard.MKInput
{
    public abstract class MKInputListen : IDisposable
    {
        protected readonly IKeyboardMouseEvents inputEvents;
        protected bool enabled;

        protected MKInputListen() =>
            inputEvents = Hook.GlobalEvents();

        public bool IsEnabled => enabled;

        public virtual void Start()
        {
            if (!IsEnabled)
                Subscribe();
        }

        public virtual void Subscribe()
        {
            enabled = true;
        }

        public virtual void Unsubscribe()
        {
            enabled = false;
        }

        public virtual void Dispose()
        {
            Unsubscribe();
            inputEvents.Dispose();
        }

    }
}
