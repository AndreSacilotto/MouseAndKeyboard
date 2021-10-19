using System.Windows.Forms;

namespace MouseKeyboard.MKInput.Default
{

    public class MkInputListenDefault : MKInputListen
    {
        public MkInputListenDefault() : base()
        {
            Subscribe();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #region SUB
        public override void Subscribe()
        {
            base.Subscribe();

            inputEvents.MouseMove += OnMouseMove;
            inputEvents.MouseDown += OnMouseDown;
            inputEvents.MouseUp += OnMouseUp;
            inputEvents.MouseDoubleClick += OnMouseDoubleClick;
            inputEvents.MouseWheel += OnMouseScroll;

            inputEvents.KeyDown += OnKeyDown;
            inputEvents.KeyUp += OnKeyUp;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            inputEvents.MouseMove -= OnMouseMove;
            inputEvents.MouseDown -= OnMouseDown;
            inputEvents.MouseDoubleClick -= OnMouseDoubleClick;
            inputEvents.MouseWheel -= OnMouseScroll;

            inputEvents.KeyDown -= OnKeyDown;
            inputEvents.KeyUp -= OnKeyUp;
        }
        #endregion

        #region SEND

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            MKEventHandleUtil.Print(e);
        }

        #endregion
    }
}
