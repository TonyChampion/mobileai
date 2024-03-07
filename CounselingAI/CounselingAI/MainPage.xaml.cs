using CounselingAI.Business.ViewModels;
using CounselingAI.Services;

namespace CounselingAI;

public sealed partial class MainPage : Page
{
    public MainPageViewModel ViewModel = new MainPageViewModel();
    public MainPage()
    {
        this.InitializeComponent();
    }
}
