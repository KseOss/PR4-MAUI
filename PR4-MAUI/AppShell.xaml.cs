namespace PR4_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        public void EnableManualTab()
        {
            var manualTab = this.FindByName<Tab>("ManualTab");
            if (manualTab != null)
            {
                manualTab.IsEnabled = true;
            }
        }
    }
}
