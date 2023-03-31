namespace MouseAndKeyboard;

public class MultiFormContext : ApplicationContext
{
    private int openForms;
    public MultiFormContext(params Form[] forms)
    {
        openForms = forms.Length;
        foreach (var form in forms)
        {
            form.FormClosed += WhenFormClosed;
            form.Show();
        }

        void WhenFormClosed(Object? sender, FormClosedEventArgs e)
        {
            if (--openForms == 0)
                ExitThread();
        }
    }


}