using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using CounselingAI.Services;

namespace CounselingAI.Business.ViewModels;
public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly ICounselingService _counselingService;
    private readonly IPromptService _promptService;
    private readonly IProfileService _profileService;
    public MainPageViewModel(IPromptService promptService, 
                             ICounselingService counselingService,
                             IProfileService profileService)
    {
        _counselingService = counselingService;
        _promptService = promptService;
        _profileService = profileService;

        AskCounselorCommand = new AsyncRelayCommand(AskCounselorStreaming);

    }

    public MainPageViewModel()
    {
        _counselingService = new CounselingService("", "");
        _promptService = new PromptService();
        _profileService = new ProfileService();

        AskCounselorCommand = new AsyncRelayCommand(AskCounselor);

    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void NotifyPropertyChange([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ICommand AskCounselorCommand { get; }

    private string career;
    public string Career
    {
        get { return career; }
        set
        {
            career = value;
            NotifyPropertyChange();
        }
    }

    public string CareerLabel => "Career";

    private string location;
    public string Location
    {
        get { return location; }
        set
        {
            location = value;
            NotifyPropertyChange();
        }
    }

    private string response;
    public string Response
    {
        get { return response; }
        set
        {
            response = value;
            NotifyPropertyChange();
        }
    }

    private async Task AskCounselor()
    {
        var prompts = await _promptService.GetPromptAsync();
        var prompt = prompts.First().PromptText;
        prompt = prompt.Replace("[career]", Career).Replace("[location]", Location);
        var profiles = await _profileService.GetProfilesAsync();

        CounselingRequest request = new CounselingRequest()
        {
            Profile = profiles.First().ProfileText,
            Prompt = prompt,
        };

        var airesponse = await _counselingService.QueryAIAsync(request);
        Response = airesponse.Content;
    }

    private async Task AskCounselorStreaming()
    {
        var prompts = await _promptService.GetPromptAsync();
        var prompt = prompts.First().PromptText;
        prompt = prompt.Replace("[career]", Career).Replace("[location]", Location);
        var profiles = await _profileService.GetProfilesAsync();

        CounselingRequest request = new CounselingRequest()
        {
            Profile = profiles.First().ProfileText,
            Prompt = prompt,
        };

        Response = "";

       // var writer = new System.IO.StreamWriter(data);
        var openAIClient = new OpenAIClient(new Uri("https://cds-ai.openai.azure.com/"), new AzureKeyCredential("86b5c867a68c48e8854e1212c82f4760"));

        try
        {
            var startTime = DateTime.Now;
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "counselingai35",
                Messages =
            {
                new ChatRequestSystemMessage(request.Profile),
                new ChatRequestUserMessage(request.Prompt),
            }
            };
            var stream = await openAIClient.GetChatCompletionsStreamingAsync(chatCompletionsOptions);
            await foreach (var chatCompletion in stream)
            {
                Response += chatCompletion.ContentUpdate;
            }
        }
        catch (Exception exception)
        {
           // await writer.WriteAsync("event: error\ndata: error\n\n");
        }
        finally
        {
           // await writer.FlushAsync();
        }
    }
}
