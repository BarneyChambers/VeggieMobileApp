using BottomBar.XamarinForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace VeggieMobile21062018
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BottomBarPage bottomBar = new BottomBarPage
            {
                    
            };

            NavigationPage.SetHasNavigationBar(bottomBar, false);
            var tab1 = new MainPage();
            var tab2 = new ReceivePage(null);
            var tab3 = new SendPage(null);
            var tab4 = new SendPage(null);
            var tab5 = new InfoPage(null);

            bottomBar.Children.Add(tab1);
            bottomBar.Children.Add(tab2);
            bottomBar.Children.Add(tab3);
            bottomBar.Children.Add(tab4);
            bottomBar.Children.Add(tab5);

            bottomBar.Children[0].Icon = (FileImageSource)ImageSource.FromFile("homeFA.png");
            bottomBar.Children[0].Title = "Home";
            bottomBar.Children[1].Icon = (FileImageSource)ImageSource.FromFile("QRFA.png");
            bottomBar.Children[1].Title = "Receive";
            bottomBar.Children[2].Icon = (FileImageSource)ImageSource.FromFile("scanFA.png");
            bottomBar.Children[2].Title = "Scan";
            bottomBar.Children[3].Icon = (FileImageSource)ImageSource.FromFile("paperplaneFA.png");
            bottomBar.Children[3].Title = "Send";
            bottomBar.Children[4].Icon = (FileImageSource)ImageSource.FromFile("cogFA.png");
            bottomBar.Children[4].Title = "Settings";

            bottomBar.CurrentPage = bottomBar.Children[0];
            Application.Current.MainPage = new NavigationPage(bottomBar);


            bottomBar.CurrentPageChanged += (object sender, EventArgs e) => {
                var i = bottomBar.Children.IndexOf(bottomBar.CurrentPage);
                if (i == 2) //Scan Page
                {
                    ZXingScannerPage z = new ZXingScannerPage();
                    App.Current.MainPage.Navigation.PushAsync(z);

                    z.OnScanResult += (ZXing.Result result) => { App.Current.MainPage.Navigation.PushAsync(new SendPage(result)); };
                }
            };
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
