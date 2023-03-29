namespace MouseAndKeyboard;

public class MultiFormContext : ApplicationContext
{
    private int openForms;
    public MultiFormContext(params Form[] forms)
    {
        openForms = forms.Length;
        foreach (var form in forms)
        {
            form.FormClosed += (s, args) =>
            {
                if (--openForms == 0)
                    ExitThread();
            };
            form.Show();
        }
    }


}