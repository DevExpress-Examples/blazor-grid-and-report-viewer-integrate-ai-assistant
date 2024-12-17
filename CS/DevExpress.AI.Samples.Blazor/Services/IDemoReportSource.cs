using DevExpress.XtraReports.UI;

namespace DevExpress.AI.Samples.Blazor.Services {
    public interface IDemoReportSource {
        XtraReport GetReport(string reportName);
        Dictionary<string, string> GetReportList();
    }
}
