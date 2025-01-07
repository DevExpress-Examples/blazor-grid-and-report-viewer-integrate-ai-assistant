<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1268742)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Blazor Grid and Report Viewer — Incorporate an AI Assistant (Azure OpenAI) in your next DevExpress-powered Blazor app 

This example adds a Copilot-inspired chat window (DevExpress [`DxAIChat`](http://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat) component) to a DevExpress-powered Blazor application (using both the DevExpress Report Viewer and Blazor Grid component). Our chat implementation utilizes [Azure OpenAI Assistant](https://learn.microsoft.com/en-us/azure/ai-services/openai/how-to/assistant) to answer user questions based on information displayed in the report and/or data grid. 

To integrate AI-powered chat capabilities to your next great Blazor application, please follow the steps below:  

1. Register AI Services within the application. 
2. Add the DevExpress Chat component (`DxAIChat`). 
3. Export component data and pass it to the AI Assistant. 

The following DevExpress Blazor Components were used in this sample project:

- [DevExpress Blazor Grid](https://www.devexpress.com/blazor/data-grid/)

    Our Blazor Grid component displays project management data. You can use the AI Assistant to:

    - Prioritize tasks.
    - Count tasks using status data.
    - Filter tasks by owner or priority. 

    Implementation details: [Use an AI Assistant with the DevExpress Blazor Grid](#use-an-ai-assistant-with-the-devexpress-blazor-grid).

- [Report Viewer](https://www.devexpress.com/subscriptions/reporting/)

    Our Report Viewer includes several reports bound to different data. Use the AI Assistant to interact with report data as follows: 

    - *Drill-Down Report*: Analyze invoice totals, delivery status, and averages. 
    - *Market Share Report*: Compare market share changes across regions.
    - *Restaurant Menu*: Examine price range and categorize vegetarian options. 

    Implementation details: [Add an AI Assistant to the DevExpress Blazor Report Viewer](#add-an-ai-assistant-to-the-devexpress-report-viewer).

>[!NOTE]
> Open AI Assistant initialization may take time. `DxAIChat` is ready for use once Microsoft Azure OpenAI completes its source document scan.

## Implementation Details

### Register AI Services

> [!NOTE]  
> DevExpress AI-powered extensions follow the "bring your own key" principle. DevExpress does not offer a REST API and does not ship any built-in LLMs/SLMs. You need an active Azure/Open AI subscription to obtain the REST API endpoint, key, and model deployment name. These variables must be specified at application startup to register AI clients and enable DevExpress AI-powered Extensions in your application.

Add the following code snippet to the *Program.cs* file to register AI Services and incorporate an [OpenAI Assistant](https://platform.openai.com/docs/assistants/overview) in your application:

```cs
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using Microsoft.Extensions.AI;
using System.ClientModel;
//...
string azureOpenAIEndpoint = "AZURE_OPENAI_ENDPOINT";
string azureOpenAIKey = "AZURE_OPENAI_API_KEY";
string deploymentName = "YOUR_MODEL_NAME";
//...
var azureClient = new AzureOpenAIClient(
    new Uri(azureOpenAIEndpoint),
    new ApiKeyCredential(azureOpenAIKey));
builder.Services.AddChatClient(config => 
    config.Use(azureClient.AsChatClient(deploymentName))
);
builder.Services.AddDevExpressAI((config) => {
    config.RegisterOpenAIAssistants(azureClient, deploymentName); 
});
```

For additional information on the use of AI Assistants with `DxAIChat` and managing messages with custom RAG (Retrieval-Augmented Generation) solutions, refer to the following topic: [AI Service Assistants in the DxAIChat component](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat#ai-service-assistants).

>[!NOTE]
> The availability of Azure Open AI Assistants depends on region. For additional guidance in this regard, refer to the following document: [Azure OpenAI Service models -- Assistants (Preview)](https://learn.microsoft.com/en-us/azure/ai-services/openai/concepts/models?tabs=global-standard%2Cstandard-chat-completions#assistants-preview).

**Files to Review:**

- [Program.cs](./DevExpress.AI.Samples.Blazor/Program.cs)

### Use an AI Assistant with the DevExpress Blazor Grid

This example includes a page with both the DevExpress Blazor Grid (`DxGrid`) and Blazor Chat component (`DxAIChat`): 

![Blazor Grid and Integrated AI Assistant](images/data-grid.png)

To configure our Blazor Grid (data binding and customizations), review the following code file: [Grid.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/Grid.razor).

#### Add AI Chat to the Grid Page

The following code snippet adds the [`DxAIChat`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat) component to the page:

```razor
@using DevExpress.AIIntegration.Blazor.Chat
@using Markdig

<DxGrid @ref="grid" Data="@DataSource" CssClass="my-grid" ShowGroupPanel="true" TextWrapEnabled="false">
    @* ... *@
</DxGrid>
<DxAIChat @ref="chat" CssClass="my-grid-chat">
    <MessageContentTemplate>
        <div class="my-chat-content">
            @ToHtml(context.Content)
        </div>
    </MessageContentTemplate>
</DxAIChat>

@code {
    MarkupString ToHtml(string text) {
        return (MarkupString)Markdown.ToHtml(text);
    }
}
```

Use the [`MessageContentTemplate`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat.MessageContentTemplate) property to display rich-formatted messages. Use a markdown processor to convert response content to HTML code.

**Files to Review:**

- [Grid.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/Grid.razor)

#### Set Up the AI Assistant

Handle the `OnAfterRenderAsync` event and call the [`SetupAssistantAsync`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.IAIChat.SetupAssistantAsync(DevExpress.AIIntegration.Services.Assistant.AIAssistantOptions)) method to create your AI assistant and provide it with data and instructions. This example calls our Blazor Grid's [`ExportToXlsxAsync`](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid.ExportToXlsxAsync.overloads) method to generate data for the AI Assistant. 

```razor
@using DevExpress.AIIntegration.OpenAI.Services

@* ... *@
@code {
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if(firstRender) {
            using(MemoryStream ms = new MemoryStream()) {
                grid.BeginUpdate();
                grid.ShowGroupedColumns = true;
                await grid.ExportToXlsxAsync(ms, new GridXlExportOptions() {
                        ExportDisplayText = true
                    });
                await chat.SetupAssistantAsync(new OpenAIAssistantOptions("grid_data.xlsx", ms) {
                    Instructions = AssistantHelper.GetAIAssistantInstructions("xlsx"),
                    UseFileSearchTool = false
                });
                grid.ShowGroupedColumns = false;
                grid.EndUpdate();
            }
        }
        await base.OnAfterRenderAsync(firstRender);
    }
}
```

You can review and tailor AI assistant instructions in the following file: [Instructions.cs](./DevExpress.AI.Samples.Blazor/Instructions.cs).

For information on OpenAI Assistants, refer to the following document: [Assistants API overview](https://platform.openai.com/docs/assistants/overview).

**Files to Review:**

- [Grid.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/Grid.razor)
- [Instructions.cs](./DevExpress.AI.Samples.Blazor/Instructions.cs)

###  Add an AI Assistant to the DevExpress Blazor Report Viewer

As you can see in the following image, this sample uses our Blazor Report Viewer alongside the DevExpress Chat component (the AI Assistant tab uses the `DxAIChat` component to display requests and responses): 

![Blazor Report Viewer and Integrated AI Assistant](images/report-viewer.png)

#### Add New Tab for Your AI Assistant

Use the [`OnCustomizeTabs`](https://docs.devexpress.com/XtraReports/DevExpress.Blazor.DxViewer.OnCustomizeTabs) event to add a new tab: 

```razor
@using DevExpress.AI.Samples.Blazor.Components.Reporting
@using DevExpress.AI.Samples.Blazor.Models
@using DevExpress.Blazor.Reporting.Models

@* ... *@
<DxReportViewer @ref="Viewer" CssClass="my-report" OnCustomizeTabs="OnCustomizeTabs">
</DxReportViewer>
@* ... *@

@code {
    // ...
    void OnCustomizeTabs(List<TabModel> tabs) {
        tabs.Add(new TabModel(new UserAssistantTabContentModel(() => CurrentReport), "AI", "AI Assistant") {
            TabTemplate = (tabModel) => {
                return (builder) => {
                    builder.OpenComponent<AITabRenderer>(0);
                    builder.AddComponentParameter(1, "Model", tabModel.ContentModel);
                    builder.CloseComponent();
                };
            }
        });
    }
}
```

A new [`TabModel`](https://docs.devexpress.com/XtraReports/DevExpress.Blazor.Reporting.Models.TabModel._members) object is added to the tab list. The [`UserAssistantTabContentModel`](https://github.com/DevExpress-Examples/blazor-grid-and-report-viewer-integrate-ai-assistant/blob/24.2.3%2B/CS/DevExpress.AI.Samples.Blazor/Models/UserAssistantTabContentModel.cs#L6) class implements the [`ITabContentModel`](https://docs.devexpress.com/XtraReports/DevExpress.Blazor.Reporting.Models.ITabContentModel) interface that specifies AI Assistant tab visibility. The tab is only visible when the report is initialized and contains at least one page.

The `TabTemplate` property specifies tab content. It dynamically renders the `DxAIChat` component inside the tab and passes `ContentModel` as a parameter to control tab content.

The content for the AI Assistant tab is defined in the following file: [AITabRenderer.razor](./DevExpress.AI.Samples.Blazor/Components/Reporting/AITabRenderer.razor). 

```razor
@using DevExpress.AI.Samples.Blazor.Models
@using DevExpress.AIIntegration.Blazor.Chat
@using System.Text.RegularExpressions
@using Markdig

<DxAIChat CssClass="my-report-chat">
    <MessageContentTemplate>
        <div class="my-chat-content">
            @ToHtml(context.Content)
        </div>
    </MessageContentTemplate>
</DxAIChat>

@code {
    [Parameter] public UserAssistantTabContentModel Model { get; set; }
    string ClearAnnotations(string text) {
    //To clear out annotations in a response from the assistant.
        return Regex.Replace(text, @"\【.*?】", "");
    }
    
    MarkupString ToHtml(string text) {
        text = ClearAnnotations(text);
        return (MarkupString)Markdown.ToHtml(text);
    }
}
```

Use the [`MessageContentTemplate`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat.MessageContentTemplate) property to display rich-formatted messages. Use a markdown processor to convert response content to HTML code.

**Files to Review:**

- [ReportViewer.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/ReportViewer.razor)
- [AITabRenderer.razor](./DevExpress.AI.Samples.Blazor/Components/Reporting/AITabRenderer.razor)
- [UserAssistantTabContentModel.cs](./DevExpress.AI.Samples.Blazor/Models/UserAssistantTabContentModel.cs)

#### Set Up the AI Assistant

Handle the [`Initialized`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat.Initialized) event and call the [`SetupAssistantAsync`](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.IAIChat.SetupAssistantAsync(DevExpress.AIIntegration.Services.Assistant.AIAssistantOptions)) method to create your AI assistant and provide it with data and instructions. This example calls the [`ExportToPdf`](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.PrintingSystemBase.ExportToPdf(System.IO.Stream)) method to generate data for the AI Assistant:

```razor
@using DevExpress.AIIntegration.Blazor.Chat
@using DevExpress.AIIntegration.OpenAI.Services

<DxAIChat CssClass="my-report-chat" Initialized="ChatInitialized">
    @* ... *@
</DxAIChat>

@code {
    // ...
    async Task ChatInitialized(IAIChat aIChat) {
        using (MemoryStream ms = Model.GetReportData()) {
            await aIChat.SetupAssistantAsync(new OpenAIAssistantOptions("report.pdf", ms) {
                Instructions = AssistantHelper.GetAIAssistantInstructions("pdf")
            });
        }
    }
}
```

You can review and tailor AI assistant instructions in the following file: [Instructions.cs](./DevExpress.AI.Samples.Blazor/Instructions.cs).

For information on OpenAI Assistants, refer to the following article: [Assistants API overview](https://platform.openai.com/docs/assistants/overview).

**Files to Review:**

- [ReportViewer.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/ReportViewer.razor)
- [AITabRenderer.razor](./DevExpress.AI.Samples.Blazor/Components/Reporting/AITabRenderer.razor)
- [UserAssistantTabContentModel.cs](./DevExpress.AI.Samples.Blazor/Models/UserAssistantTabContentModel.cs)
- [Instructions.cs](./DevExpress.AI.Samples.Blazor/Instructions.cs)

## Files to Review

- [Program.cs](./DevExpress.AI.Samples.Blazor/Program.cs)
- [Instructions.cs](./DevExpress.AI.Samples.Blazor/Instructions.cs)
- [Grid.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/Grid.razor)
- [ReportViewer.razor](./DevExpress.AI.Samples.Blazor/Components/Pages/ReportViewer.razor)
- [AITabRenderer.razor](./DevExpress.AI.Samples.Blazor/Components/Reporting/AITabRenderer.razor)
- [UserAssistantTabContentModel.cs](./DevExpress.AI.Samples.Blazor/Models/UserAssistantTabContentModel.cs)

## Documentation

- [Blazor AI Chat](https://docs.devexpress.com/Blazor/DevExpress.AIIntegration.Blazor.Chat.DxAIChat)
- [Demo: Blazor AI Chat](https://demos.devexpress.com/blazor/AI/Chat#Overview)
- [Blazor Grid](https://docs.devexpress.com/Blazor/403143/components/grid)
- [Blazor Report Viewer](https://docs.devexpress.com/XtraReports/403594/web-reporting/blazor-reporting/server/blazor-report-viewer-native)
- [AI-powered Extensions for DevExpress Reporting](https://docs.devexpress.com/XtraReports/405211/ai-powered-functionality/ai-for-devexpress-reporting)

## More Examples

- [Reporting for Blazor - Integrate AI-powered Summarize and Translate Features based on Azure OpenAI](https://github.com/DevExpress-Examples/blazor-reporting-ai/)
- [Reporting for ASP.NET Core - Integrate AI Assistant based on Azure OpenAI](https://github.com/DevExpress-Examples/web-reporting-integrate-ai-assistant)
- [AI Chat for Blazor - How to add DxAIChat component in Blazor, MAUI, WPF, and WinForms applications](https://github.com/DevExpress-Examples/devexpress-ai-chat-samples)
- [Rich Text Editor and HTML Editor for Blazor - How to integrate AI-powered extensions](https://github.com/DevExpress-Examples/blazor-ai-integration-to-text-editors)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-and-report-viewer-integrate-ai-assistant&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-and-report-viewer-integrate-ai-assistant&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
