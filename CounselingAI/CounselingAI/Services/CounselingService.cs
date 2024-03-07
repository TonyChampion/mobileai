using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Azure;
using Windows.ApplicationModel.Chat;
using Microsoft.Extensions.Configuration;
using Windows.Media.Protection.PlayReady;

namespace CounselingAI.Services;
public class CounselingService : ICounselingService
{
    private readonly OpenAIClient _openAIClient;

    public CounselingService(IConfiguration configuration)
    {
        _openAIClient = new OpenAIClient(
        new Uri(configuration["OpenAIEndpoint"]),
        new AzureKeyCredential(configuration["OpenAIKey"]));
    }

    public CounselingService(string endpoint, string key)
    {
        _openAIClient = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
    }
    public async Task<CounselingResponse> QueryAIAsync(CounselingRequest request, CancellationToken cancellationToken = default)
    {
        CounselingResponse response = new CounselingResponse();

        var chatCompletionsOptions = new ChatCompletionsOptions()
        {
            DeploymentName = "counselingai35",
            Messages =
            {
                new ChatRequestSystemMessage(request.Profile),
                new ChatRequestUserMessage(request.Prompt),
            }
        };
        Response<ChatCompletions> chatCompletions = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
        ChatResponseMessage responseMessage = chatCompletions.Value.Choices[0].Message;
        _openAIClient.GetChatCompletionsStreamingAsync(chatCompletionsOptions);
        response.Success = true;
        response.Content = responseMessage.Content;

        return response;
    }

}

