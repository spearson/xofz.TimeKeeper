namespace xofz.TimeKeeper.UI.Forms
{
    using System;
    using System.Threading;
    using xofz.UI.Forms;

    public partial class UserControlHomeNavUi : UserControlUi, HomeNavUi
    {
        public UserControlHomeNavUi()
        {
            this.InitializeComponent();
        }

        public event Action StatisticsKeyTapped;

        public event Action ExitKeyTapped;

        private void statisticsKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.StatisticsKeyTapped?.Invoke()).Start();
        }

        private void exitKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.ExitKeyTapped?.Invoke()).Start();
        }
    }
}
