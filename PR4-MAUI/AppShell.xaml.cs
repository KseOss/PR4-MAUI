    namespace PR4_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            DisableManualTab();
        }
        public void EnableManualTab()
        {
            ManualTab.IsVisible = true;
        }
        public void DisableManualTab()
        {
            ManualTab.IsVisible = false;
        }
    }
}
