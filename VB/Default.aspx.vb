Imports DevExpress.Web
Imports DevExpress.Data.Filtering
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        TestDateEdit.PickerType = DirectCast(System.Enum.Parse(GetType(DatePickerType), ComboBox.Value.ToString()), DatePickerType)
        TestDateEdit.Date = New Date(2018, 9, 1)
        TestDateEdit.MinDate = New Date(2018, 9, 1)
        TestDateEdit.MaxDate = (New Date(2018, 9, 1)).AddDays(1000)
        testGrid.DataSource = Data.GetData()
        testGrid.DataBind()
    End Sub
    Public Class Data
        Public Property OrderID() As Integer
        Public Property OrderDate() As Date
        Public Property OrderName() As String
        Public Shared Function GetData() As List(Of Data)
            Return Enumerable.Range(0, 1000).Select(Function(i) New Data() With { _
                .OrderID = i, _
                .OrderDate = (New Date(2018, 9, 1)).AddDays(i), _
                .OrderName = "Order#" & i _
            }).ToList()
        End Function
    End Class

    Public Sub ComboBoxSelected(ByVal sender As Object, ByVal e As EventArgs)
        TestDateEdit.PickerType = DirectCast(System.Enum.Parse(GetType(DatePickerType), ComboBox.Value.ToString()), DatePickerType)
    End Sub
    Protected Sub testGrid_ProcessColumnAutoFilter(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAutoFilterEventArgs)
        If e.Value = "|" Then
            Return
        End If
        If e.Column.FieldName <> "OrderDate" Then
            Return
        End If
        If e.Kind = GridViewAutoFilterEventKind.CreateCriteria Then
            Dim dates() As String = e.Value.Split("|"c)
            Session("DateFilterText") = dates(0) & " - " & dates(1)
            Dim dateFrom As Date = Convert.ToDateTime(dates(0)), dateTo As Date = Convert.ToDateTime(dates(1))
            e.Criteria = (New OperandProperty("OrderDate") >= dateFrom) And (New OperandProperty("OrderDate") < dateTo)
        End If
    End Sub
End Class