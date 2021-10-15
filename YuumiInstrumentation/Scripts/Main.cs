using System;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Main : ApplicationContext
{
    private readonly InputListener inputListener;
    //private readonly NetworkManager network;

    public Main()
    {
        ThreadExit += OnExit;

        inputListener = new InputListener(false);
        //network = new NetworkManager();

        Mouse.DragAndDrop(1000, 500);
        //Task.Delay(1500).ContinueWith(t =>
        //{
            //Mouse.GetCursorPosition(out uint x, out uint y);
            //Console.WriteLine(x + " " + y);
            //Mouse.MoveAbsoluteVirtual(2000, 500);
            //Mouse.GetCursorPosition(out x, out y);
            //Console.WriteLine(x + " " + y);
        //});
    }

    private void OnExit(object sender, EventArgs e)
    {
        inputListener.Dispose();
        ThreadExit -= OnExit;
    }
}
