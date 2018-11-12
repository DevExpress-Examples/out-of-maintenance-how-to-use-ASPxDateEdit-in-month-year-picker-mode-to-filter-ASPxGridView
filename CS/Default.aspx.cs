using DevExpress.Web;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TestDateEdit.PickerType = (DatePickerType)Enum.Parse(typeof(DatePickerType), ComboBox.Value.ToString());
        TestDateEdit.Date = new DateTime(2018, 9, 1);
        TestDateEdit.MinDate = new DateTime(2018, 9, 1);
        TestDateEdit.MaxDate = (new DateTime(2018, 9, 1)).AddDays(1000);
        testGrid.DataSource = Data.GetData();
        testGrid.DataBind();
    }
    public class Data
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public static List<Data> GetData()
        {
            return Enumerable.Range(0, 1000).Select(i => new Data() { OrderID = i, OrderDate = (new DateTime(2018, 9, 1)).AddDays(i), OrderName = "Order#" + i }).ToList<Data>();
        }
    }

    public void ComboBoxSelected(object sender, EventArgs e)
    {
        TestDateEdit.PickerType = (DatePickerType)Enum.Parse(typeof(DatePickerType), ComboBox.Value.ToString());
    }
    protected void testGrid_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridViewAutoFilterEventArgs e)
    {
        if (e.Value == "|") return;
        if (e.Column.FieldName != "OrderDate") return;
        if (e.Kind == GridViewAutoFilterEventKind.CreateCriteria)
        {
            String[] dates = e.Value.Split('|');
            Session["DateFilterText"] = dates[0] + " - " + dates[1];
            DateTime dateFrom = Convert.ToDateTime(dates[0]), dateTo = Convert.ToDateTime(dates[1]);
            e.Criteria = (new OperandProperty("OrderDate") >= dateFrom) & (new OperandProperty("OrderDate") < dateTo);
        }
    }
}