namespace xofz.TimeKeeper.Root
{
    using System;
    using System.Windows.Forms;

    static class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var bootstrapper = new FormsBootstrapper();
            bootstrapper.Bootstrap();

            Application.Run(bootstrapper.MainForm);
        }
    }
}
