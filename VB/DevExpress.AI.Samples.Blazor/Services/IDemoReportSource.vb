Imports DevExpress.XtraReports.UI

Namespace DevExpress.AI.Samples.Blazor.Services

    Public Interface IDemoReportSource

        Function GetReport(ByVal reportName As String) As XtraReport

        Function GetReportList() As Dictionary(Of String, String)

    End Interface
End Namespace
