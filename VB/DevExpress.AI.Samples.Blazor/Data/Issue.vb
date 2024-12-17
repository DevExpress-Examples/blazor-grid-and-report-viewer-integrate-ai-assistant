Namespace DevExpress.AI.Samples.Blazor.Data

    Public Enum IssueType
        Request
        Bug
    End Enum

    Public Enum IssueStatus
        [New]
        Postponed
        Fixed
        Rejected
    End Enum

    Public Enum IssuePriority
        Low
        Medium
        High
    End Enum

    Public Partial Class Issue

        Public Property ID As Long

        Public Property Name As String

        Public Property Type As IssueType

        Public Property ProjectID As Nullable(Of Long)

        Public Property Priority As IssuePriority

        Public Property Status As IssueStatus

        Public Property CreatorID As Nullable(Of Long)

        Public Property CreatedDate As Nullable(Of System.DateTime)

        Public Property OwnerID As Nullable(Of Long)

        Public Property ModifiedDate As Nullable(Of System.DateTime)

        Public Property FixedDate As Nullable(Of System.DateTime)

        Public Property Description As String

        Public Property Resolution As String
    End Class
End Namespace
