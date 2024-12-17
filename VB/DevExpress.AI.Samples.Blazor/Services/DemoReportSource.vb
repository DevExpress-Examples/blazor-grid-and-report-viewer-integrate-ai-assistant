Imports DevExpress.XtraReports.UI

Namespace DevExpress.AI.Samples.Blazor.Services

    Public Class DemoReportSource
        Implements IDemoReportSource

         ''' Cannot convert FieldDeclarationSyntax, System.InvalidCastException: Unable to cast object of type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax' to type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.ExpressionSyntax'.
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitFieldDeclaration(FieldDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''         readonly Dictionary<string, Func<XtraReport>> predefinedReports = new Dictionary<string, Func<XtraReport>> {
'''             ["Drill-Down Report"] = () => new XtraReportsDemos.DrillDownReport.DrillDownReport(),
'''             ["Market Share Report"] = () => new XtraReportsDemos.HierarchicalReport.Report(),
'''             ["Restaurant Menu"] = () => new XtraReportsDemos.RestaurantMenu.Report(),
'''         };
''' 
'''  Public Function GetReportList() As Dictionary(Of String, String) Implements IDemoReportSource.GetReportList
            Return Me.predefinedReports.ToDictionary(Function(i) i.Key, Function(i) i.Key)
        End Function

        Public Function GetReport(ByVal reportName As String) As XtraReport Implements IDemoReportSource.GetReport
            Dim ctor As Func(Of XtraReport)
            If Me.predefinedReports.TryGetValue(reportName, ctor) Then Return ctor()
            Return Nothing
        End Function
    End Class
End Namespace
