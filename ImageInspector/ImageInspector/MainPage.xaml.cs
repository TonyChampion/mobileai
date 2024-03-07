using ImageInspector.ViewModels;

namespace ImageInspector
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPageViewModel MainPageViewModel => new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();
            MainPageViewModel.OutputLabel = "tony";
        }

    }

}
