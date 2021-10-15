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

        
        Task.Delay(1500).ContinueWith(t =>
        {
            Mouse.LinearGradualMove(1000, 500);
            //Mouse.GetCursorPosition(out int x, out int y);
            //Console.WriteLine(x + " " + y);
            //Console.WriteLine(Mouse.PositionToAbsolutePrint(x, y));
            //for (int i = 1350; i < 1500; i += 10)
            //{
            //    Mouse.MoveAbsolute(i, 500);
            //    Mouse.GetCursorPosition(out x, out y);
            //    Console.WriteLine(x + " " + y);
            //}
            //Mouse.MoveAbsolute(0, 0);
            //Mouse.GetCursorPosition(out x, out y);
            //Console.WriteLine(x + " " + y);
        });
    }

    private void OnExit(object sender, EventArgs e)
    {
        inputListener.Dispose();
        ThreadExit -= OnExit;
    }
}
