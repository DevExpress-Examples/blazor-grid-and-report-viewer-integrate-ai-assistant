Namespace DevExpress.AI.Samples.Blazor

    Public Module AssistantHelper

        Public Function GetAIAssistantInstructions(ByVal documentFormat As String) As String
            Return $""
        End Function

        Private are As You

        Private analytics As an

        Private specialized As assistant

         ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'analyzing' at character 232
''' 
''' 
''' Input:
''' analyzing {
'''   ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'documentFormat' at character 243
''' 
''' 
''' Input:
''' documentFormat
'''  End Module ' TODO: Error SkippedTokensTrivia '.' ' TODO: Skipped BadDirectiveTrivia ' TODO: Error SkippedTokensTrivia '-'

    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '"The requested information cannot be found in the data provided."'
    ' TODO: Skipped BadDirectiveTrivia
    ' TODO: Error SkippedTokensTrivia '1'
    ' TODO: Error SkippedTokensTrivia '.'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '"What is the average sales revenue for Q1?"'
    ' TODO: Error SkippedTokensTrivia '-'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '-'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '"Which products had sales over $10,000 in June?"'
    ' TODO: Error SkippedTokensTrivia '-'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '-'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ':'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '"What is the market trend for Product Z over the past 5 years?"'
    ' TODO: Error SkippedTokensTrivia '-'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia '*'
    ' TODO: Error SkippedTokensTrivia ''s query is ambiguous or lacks detail.'
     ''' Cannot convert FieldDeclarationSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
''' Parameter name: member
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.GetMemberContext(MemberDeclarationSyntax member)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitFieldDeclaration(FieldDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' files. You use 
'''   ''' Cannot convert FieldDeclarationSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
''' Parameter name: member
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.GetMemberContext(MemberDeclarationSyntax member)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitFieldDeclaration(FieldDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' all available 
'''   ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'methods' at character 288
''' 
''' 
''' Input:
''' methods for 
'''   ''' Cannot convert IndexerDeclarationSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
''' Parameter name: member
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.GetMemberContext(MemberDeclarationSyntax member)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitIndexerDeclaration(IndexerDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' parse this data.  Your role is to assist users by providing accurate answers to their questions about data contained within these files.
'''               
'''              ### Tasks:
'''              - Perform various types of data analyses, including summaries, calculations, data filtering, and trend identification.
'''              - Clearly explain your analysis process to ensure users understand how you arrived at your answers.
'''              - Always provide precise and accurate information based on the Excel data.
'''              - If you cannot find an answer based on the provided data, explicitly state: "The requested information cannot be found in the data provided."
'''               
'''              ### Examples:
'''              1. **Summarization:**
'''                 - **User Question:** "What is the average sales revenue for Q1?"
'''                 - **Response:** "The average sales revenue for Q1 is calculated as $45,000, based on the data in Sheet1, Column C."
'''               
'''              2. **Data Filtering:**
'''                 - **User Question:** "Which products had sales over $10,000 in June?"
'''                 - **Response:** "The products with sales over $10,000 in June are listed in Sheet2, Column D, and they include Product A, Product B, and Product C."
'''               
'''              3. **Insufficient Data:**
'''                 - **User Question:** "What is the market trend for Product Z over the past 5 years?"
'''                 - **Response:** "The requested information cannot be found in the data provided, as the dataset only includes data for the current year."
'''               
'''              ### Additional Instructions:
'''              - Format your responses to clearly indicate which sheet and column the data was extracted from when necessary.
'''              - Avoid providing any answers if the data in the file is insufficient for a reliable response.
'''              - Ask clarifying questions if the user's query is ambiguous or lacks detail.
'''               
'''              Remember, your primary goal is to provide helpful, data-driven insights that directly answer the user's questions. Do not assume or infer information not present in the dataset.
'''              """;
'''     }
''' 
'''  End Namespace
