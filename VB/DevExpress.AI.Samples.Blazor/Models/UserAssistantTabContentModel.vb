Imports DevExpress.Blazor.Reporting.Models
Imports DevExpress.XtraReports.UI

Namespace DevExpress.AI.Samples.Blazor.Models

    Public Class UserAssistantTabContentModel
        Inherits ITabContentModel

        Public ReadOnly Property Kind As TabContentKind
            Get
                Return TabContentKind.Custom
            End Get
        End Property

        Private GetReport As Func(Of XtraReport)

        Private reportReady As Boolean = False

        Public Function GetVisible() As Boolean
            Return reportReady AndAlso (If(GetReport()?.PrintingSystem?.PageCount, 0)) > 0
        End Function

        Public Sub New(ByVal getReport As Func(Of XtraReport))
            Me.GetReport = getReport
        End Sub

        Public Function GetReportData() As MemoryStream
            Dim ms = New MemoryStream()
            GetReport()?.PrintingSystem.ExportToPdf(ms)
            ms.Position = 0
            Return ms
        End Function

        Public Function InitializeAsync() As Task
            reportReady = False
            Return Task.CompletedTask
        End Function

        Public Sub Update()
            reportReady = True
        End Sub
    End Class
End Namespace
