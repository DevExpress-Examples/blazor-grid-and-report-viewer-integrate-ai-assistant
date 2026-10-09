using Azure.AI.OpenAI;
using DevExpress.AIIntegration.Agents;
using DevExpress.AIIntegration.Chat;
using Microsoft.Extensions.AI;
using OpenAI.Files;
using OpenAI.Responses;
using OpenAI.VectorStores;

namespace DevExpress.AI.Samples.Blazor.Services {
    // The OpenAI.Responses API is for evaluation purposes only and is subject to change or removal in a future update.
    // The following code suppresses the OPENAI001 diagnostic.
#pragma warning disable OPENAI001
    public class AIAgentFactory {
        readonly OpenAIFileClient fileClient;
        readonly VectorStoreClient vectorStoreClient;
        readonly ResponsesClient responsesClient;
        readonly string deployment;
        readonly ILogger<AIAgentFactory> logger;

        public AIAgentFactory(AzureOpenAIClient client, string deployment, ILogger<AIAgentFactory> logger) {
            fileClient = client.GetOpenAIFileClient();
            vectorStoreClient = client.GetVectorStoreClient();
            responsesClient = client.GetResponsesClient();
            this.deployment = deployment;
            this.logger = logger;
        }

        // Uploads a file to OpenAI and returns an IChatResponseProvider backed by a Responses API agent.
        // The agent uses the Code Interpreter tool to analyze the file. If useFileSearchTool is true, the file is also
        // added to a short-lived vector store for the File Search tool (vector stores do not support XLSX files).
        // The cleanup delegate removes the uploaded file and the vector store.
        public async Task<(IChatResponseProvider Provider, Func<Task> Cleanup)> CreateAgentWithFileAsync(
            Stream data, string fileName, string instructions, bool useFileSearchTool = true, CancellationToken ct = default) {

            if(data.CanSeek)
                data.Position = 0;

            OpenAIFile file = (await fileClient.UploadFileAsync(data, fileName, FileUploadPurpose.Assistants, ct)).Value;
            VectorStore vectorStore = null;

            async Task Cleanup() {
                if(vectorStore != null) {
                    try { await vectorStoreClient.DeleteVectorStoreAsync(vectorStore.Id); }
                    catch(Exception ex) { logger.LogError(ex, "Error deleting vector store {Id}", vectorStore.Id); }
                }
                try { await fileClient.DeleteFileAsync(file.Id); }
                catch(Exception ex) { logger.LogError(ex, "Error deleting file {Id}", file.Id); }
            }

            try {
                var tools = new List<AITool> {
                    new HostedCodeInterpreterTool { Inputs = [new HostedFileContent(file.Id)] }
                };

                if(useFileSearchTool) {
                    vectorStore = (await vectorStoreClient.CreateVectorStoreAsync(
                        new VectorStoreCreationOptions {
                            ExpirationPolicy = new VectorStoreExpirationPolicy(VectorStoreExpirationAnchor.LastActiveAt, 1)
                        }, ct)).Value;
                    await vectorStoreClient.AddFileToVectorStoreAsync(vectorStore.Id, file.Id, ct);
                    tools.Add(new HostedFileSearchTool { Inputs = [new HostedVectorStoreContent(vectorStore.Id)] });
                }

                var aiAgent = responsesClient.AsAIAgent(
                    instructions: instructions,
                    tools: tools,
                    name: $"Data Analysis Agent {Guid.NewGuid():N}",
                    model: deployment);

                // The session keeps the conversation history.
                var session = await aiAgent.CreateSessionAsync(ct);
                return (aiAgent.AsIChatResponseProvider(session), Cleanup);
            }
            catch {
                await Cleanup();
                throw;
            }
        }
    }
#pragma warning restore OPENAI001
}
