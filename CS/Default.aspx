<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.18.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function DateChanged(s, e) {
            var startDate = new Date(s.GetValue());
            var date1 = startDate.toLocaleDateString();
            var changedDate = null;
            switch (combo.GetValue()) {
                case "Months":
                    changedDate = startDate.setMonth(startDate.getMonth() + 1);
                    break;
                case "Years":
                    changedDate = startDate.setMonth(startDate.getMonth() + 12);
                    break;
            }
            var date2 = new Date(changedDate).toLocaleDateString();
            document.getElementById("dateText").innerText = s.GetValue().toLocaleDateString();
            grid.AutoFilterByColumn("OrderDate", date1 + "|" + date2);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%">
            <Items>
                 <dx:LayoutGroup Width="100%" ShowCaption="False" GroupBoxDecoration="None" ColCount="2">
                    <Items>
                        <dx:LayoutGroup Width="500px" Caption="Pick data in specific range to filter data in grid">
                            <Items>
                                <dx:LayoutItem Caption="Show data for" VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit runat="server" ID="TestDateEdit">
                                                <ClientSideEvents DateChanged="DateChanged" Init="DateChanged" />
                                            </dx:ASPxDateEdit>
                                            <span style="color:red">Value of DateEdit is </span><span style="color:red" id="dateText"></span>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Select range scale" VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox runat="server" ID="ComboBox" OnSelectedIndexChanged="ComboBoxSelected" AutoPostBack="true" ClientInstanceName="combo">
                                                <Items>
                                                    <dx:ListEditItem Value="Months" Text="Month range" Selected="true"></dx:ListEditItem>
                                                    <dx:ListEditItem Value="Years" Text="Year range"></dx:ListEditItem>
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
        <dx:ASPxGridView ID="testGrid" runat="server" AutoGenerateColumns="False" KeyFieldName="OrderID"
            ClientInstanceName="grid" OnProcessColumnAutoFilter="testGrid_ProcessColumnAutoFilter" Width="550px">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="OrderID" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="OrderDate" VisibleIndex="3">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="OrderName" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
    </form>
</body>
</html>
