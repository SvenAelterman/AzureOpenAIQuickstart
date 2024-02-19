using Azure;
using Azure.AI.OpenAI;
using static System.Environment;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string key = GetEnvironmentVariable("AZURE_OPENAI_KEY");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));
#pragma warning restore CS8604 // Possible null reference argument.

var chatCompletionsOptions = new ChatCompletionsOptions()
{
    DeploymentName = "gpt-35-turbo", //This must match the custom deployment name you chose for your model
    Messages =
    {
        new ChatRequestSystemMessage("You are a helpful assistant."),
        new ChatRequestUserMessage("Does Azure OpenAI support customer managed keys?"),
        new ChatRequestAssistantMessage("Yes, customer managed keys are supported by Azure OpenAI."),
        new ChatRequestUserMessage("Do other Azure AI services support this too?"),
    },
    MaxTokens = 100
};

Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);

Console.WriteLine(response.Value.Choices[0].Message.Content);

Console.WriteLine();