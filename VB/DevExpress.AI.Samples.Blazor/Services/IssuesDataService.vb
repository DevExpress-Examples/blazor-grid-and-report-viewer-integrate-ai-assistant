Imports DevExpress.AI.Samples.Blazor.Data
Imports Microsoft.EntityFrameworkCore

Namespace DevExpress.AI.Samples.Blazor.Services

    Public Class IssuesDataService

        Private DbFactory As IDbContextFactory(Of IssuesContext)

        Public Sub New(ByVal DbFactory As IDbContextFactory(Of IssuesContext))
            Me.DbFactory = DbFactory
        End Sub

        Public Function GetIssuesAsync(ByVal Optional ct As CancellationToken = DirectCast(Nothing, CancellationToken)) As Task(Of List(Of Issue))
            Return DbFactory.CreateDbContext().Items.ToListAsync()
        End Function

        Public Function GetProjectsAsync(ByVal Optional ct As CancellationToken = DirectCast(Nothing, CancellationToken)) As Task(Of List(Of Project))
            Return DbFactory.CreateDbContext().Projects.ToListAsync()
        End Function

        Public Function GetUsersAsync(ByVal Optional ct As CancellationToken = DirectCast(Nothing, CancellationToken)) As Task(Of List(Of User))
            Return DbFactory.CreateDbContext().Users.ToListAsync()
        End Function
    End Class
End Namespace
