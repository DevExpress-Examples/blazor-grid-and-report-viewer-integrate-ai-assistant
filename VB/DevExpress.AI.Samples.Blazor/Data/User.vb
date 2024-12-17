Namespace DevExpress.AI.Samples.Blazor.Data

    Public Partial Class User

        Public Property ID As Long

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Country As String

        Public Property City As String

        Public Property Address As String

        Public Property Phone As String

        Public Property Email As String

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
    End Class
End Namespace
