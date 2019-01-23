using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Telerik.Charting;
using Entities;
using DomainObjects;
using System.Data;
using Telerik.Web.UI;



namespace outcomming_mail
{
    public partial class ReportRate : ULPage
    {
        private void AddNewSeries(UniversalEntity ue)
        {
            
            ChartSeries cs = new ChartSeries(radWP.SelectedText,ChartSeriesType.Spline);
            RadChart1.Series.Add(cs);
            int i = 0;
            List<ChartSeriesItem> csc = new List<ChartSeriesItem>();
            foreach (ArrayList al in ue)
            {
                ChartSeriesItem csi=new ChartSeriesItem();

                csi.YValue=(Convert.ToDouble(al[0]));
                csi.XValue = (i);
                //csi.XValue=(Convert.ToDateTime(al[1]).Ticks);
                                
                //cs.AddItem((Convert.ToDouble(al[0]));
                i++;
                
                csc.Add(csi);
            }
            
            cs.AddItem(csc);
            
            RadChart1.DataBind();
            RadChart1.PlotArea.XAxis.LayoutMode = Telerik.Charting.Styles.ChartAxisLayoutMode.Normal;
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                radWP.DataSource = (DataView)dsWP.Select(DataSourceSelectArguments.Empty);

                dFrom.SelectedDate = DateTime.Now.AddMonths(-1);
                dTo.SelectedDate = DateTime.Now.AddMonths(1);
                radWP.DataTextField = "Title";
                radWP.DataValueField = "ID";
                radWP.DataBind();
                radWP.Items.Add(new DropDownListItem("Все", "0"));
            }
        }

        protected void btSet_Click(object sender, EventArgs e)
        {
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            UniversalEntity ue = new UniversalEntity();

            ue = crdo.GetRateByDateAndWP(Utilities.ConvertToInt(radWP.SelectedValue),Utilities.ConvertToInt(radddlUnits.SelectedValue), dFrom.SelectedDate.Value, dTo.SelectedDate.Value);

           
            /*Random r = new Random();

            List<Double> c = new List<double>();
            c.Add(Math.Round((r.NextDouble() * 100)));
            c.Add(Math.Round((r.NextDouble() * 100)));
            c.Add(Math.Round((r.NextDouble() * 100)));
            c.Add(Math.Round((r.NextDouble() * 100)));
            c.Add(Math.Round((r.NextDouble() * 100)));
            c.Add(Math.Round((r.NextDouble() * 100)));
             * */
            AddNewSeries(ue);
        }

        protected void btCleare_Click(object sender, EventArgs e)
        {

            RadChart1.Clear();
        }
    }
}