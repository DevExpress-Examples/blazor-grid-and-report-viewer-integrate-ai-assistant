using DevExpress.Blazor.Reporting.Models;
using DevExpress.XtraReports.UI;

namespace DevExpress.AI.Samples.Blazor.Models
{
    public class UserAssistantTabContentModel : ITabContentModel
    {
        public TabContentKind Kind => TabContentKind.Custom;
        Func<XtraReport> GetReport;
        bool reportReady = false;
        public bool GetVisible() => reportReady && (GetReport()?.PrintingSystem?.PageCount ?? 0) > 0;

        public UserAssistantTabContentModel(Func<XtraReport> getReport)
        {
            GetReport = getReport;
        }

        public MemoryStream GetReportData()
        {
            var ms = new MemoryStream();
            GetReport()?.PrintingSystem.ExportToPdf(ms);
            ms.Position = 0;
            return ms;
        }

        public Task InitializeAsync()
        {
            reportReady = false;
            return Task.CompletedTask;
        }

        public void Update() {
            reportReady = true;
        }
    }
}

