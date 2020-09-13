using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eetfestijnkassasystem.Mobile.Services;
using Eetfestijnkassasystem.Mobile.Views;
using Xamarin.Essentials;
using System.Net.Http.Headers;

namespace Eetfestijnkassasystem.Mobile
{
    public partial class App : Application
    {
        static App()
        {
            Settings = new AppSettings();
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<OrderRepository>();
            
            MainPage = new AppShell();
        }

        public static AppSettings Settings { get; }

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

    public class AppSettings
    {
        public AppSettings()
        {
            Parse();
        }

        public string BackendUrl { get; set; }

        private void Parse()
        {
            //BackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
            BackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        }
    }
}
