using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;

namespace Nutrinfo.Models
{
    public class GeminiModel
    {
        private readonly Kernel _kernel;

        public GeminiModel()
        {
            var builder = Kernel.CreateBuilder();

            builder.AddGoogleAIGeminiChatCompletion(
                modelId: "gemini-2.5-flash",
                apiKey: "AIzaSyAAeKCkRolm4R7NtSV--FgvlIhTUSHen3U"
            );

            _kernel = builder.Build();
        }

        public async Task<string> PreguntarAsync(PerfilesPersonalizados perfilPersonalizado, Productos producto)
        {
            var chat = _kernel.GetRequiredService<IChatCompletionService>();

            var history = new ChatHistory();
            history.AddSystemMessage("Vas a generar un breve texto de entre 300 y 400 caractéres en el cual según las siguientes características las cuales te voy a poner en una lista, vas a decidir que tan bueno es un producto alimenticio para la persona con dichas características. Las características van a estar listadas pero en caso de que una, varias o todas no digan nada vas a ignorarla y generar el texto como que en cada característica que falta, el usuario es una persona común y sana.");
            history.AddUserMessage("Características: Alergias: " + perfilPersonalizado.Alergias + ". Intolerancias: " + perfilPersonalizado.Intolerancias + ". Enfermedades: " + perfilPersonalizado.Enfermedades + ". Cultura: " + perfilPersonalizado.Cultura + ". Estilo de Vida: " + perfilPersonalizado.EstiloDeVida + ". Dieta: " + perfilPersonalizado.Dieta);

            var settings = new GeminiPromptExecutionSettings
            {
                Temperature = 0.2,
                TopP = 0.2
            };

            var result = await chat.GetChatMessageContentAsync(history, settings);

            return result.Content;
        }
    }
}