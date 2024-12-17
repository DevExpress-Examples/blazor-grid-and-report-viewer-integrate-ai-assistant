using DevExpress.XtraReports.UI;

namespace DevExpress.AI.Samples.Blazor.Services {
    public class DemoReportSource : IDemoReportSource {
        readonly Dictionary<string, Func<XtraReport>> predefinedReports = new Dictionary<string, Func<XtraReport>> {
            ["Drill-Down Report"] = () => new XtraReportsDemos.DrillDownReport.DrillDownReport(),
            ["Market Share Report"] = () => new XtraReportsDemos.HierarchicalReport.Report(),
            ["Restaurant Menu"] = () => new XtraReportsDemos.RestaurantMenu.Report(),
        };

        public Dictionary<string, string> GetReportList() {
            return predefinedReports.ToDictionary(i => i.Key, i => i.Key);
        }

        public XtraReport GetReport(string reportName) {
            Func<XtraReport> ctor;
            if(predefinedReports.TryGetValue(reportName, out ctor))
                return ctor();
            return null;
        }
    }
}
