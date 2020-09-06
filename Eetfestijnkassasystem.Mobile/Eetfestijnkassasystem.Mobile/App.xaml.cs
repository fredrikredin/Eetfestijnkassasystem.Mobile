using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eetfestijnkassasystem.Mobile.Services;
using Eetfestijnkassasystem.Mobile.Views;

namespace Eetfestijnkassasystem.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
