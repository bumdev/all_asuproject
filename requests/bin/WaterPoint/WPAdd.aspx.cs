using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainObjects;
using Entities;
using System.Text;

namespace kipia_web_application
{
    public partial class WPAdd : ULPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Devices"] = null;
                LoadTypeDevice();
                LoadTempDevices();
                LoadWPLocations();
                LoadWPTypes();
            }
        }
        private void LoadWPTypes()
        {
            List<Book> bl = new List<Book>();
            BookDO bDO = new BookDO();
            bl = bDO.RetrieveWPTypes().OfType<Book>().ToList<Book>();
            ddlWPType.DataSource = bl;
            ddlWPType.DataTextField = "Title";
            ddlWPType.DataValueField = "ID";
            ddlWPType.DataBind();

            radddlWPType.DataSource = bl;
            radddlWPType.DataTextField = "Title";
            radddlWPType.DataValueField = "ID";
            radddlWPType.DataBind();
        }
        private void LoadWPLocations()
        {
            List<Book> bl = new List<Book>();
            BookDO bDO = new BookDO();
            bl = bDO.RetrieveWPLocations().OfType<Book>().ToList<Book>();
            ddlWPLocation.DataSource = bl;
            ddlWPLocation.DataTextField = "Title";
            ddlWPLocation.DataValueField = "ID";
            ddlWPLocation.DataBind();

            radddlWPLocation.DataSource = bl;
            radddlWPLocation.DataTextField = "Title";
            radddlWPLocation.DataValueField = "ID";
            radddlWPLocation.DataBind();
        }
        private void LoadTempDevices()
        {
            List<int> dev = new List<int>();
            List<WPDevice> wpdl = new List<WPDevice>();
            WPDeviceDO wpddo = new WPDeviceDO();
            if (Session["Devices"] != null)
            {
                dev = Session["Devices"] as List<int>;
            }
            foreach (int id in dev)
            {
                wpdl.Add(wpddo.RetrieveDevicesByID(id).OfType<WPDevice>().ToList<WPDevice>()[0]);
            }
            repDevicesSaved.DataSource = wpdl;
            repDevicesSaved.DataBind();

        }
        private void LoadTypeDevice()
        {
            CustomRetrieverDO crdo = new CustomRetrieverDO();
            List<Book> lbook = crdo.RetrieveWPTypeDevice();

            radddlTypeDevice.DataSource = lbook;
            radddlTypeDevice.DataTextField = "Title";
            radddlTypeDevice.DataValueField = "ID";
            radddlTypeDevice.DataBind();
            /*foreach (Book b in lbook)
            {
                ddlTypeDevice.Items.Add(new ListItem(b.Title, b.ID.ToString()));
                radddlTypeDevice.Items.Add((new ListItem(b.Title, b.ID.ToString()));
            }*/
        }
        private void LoadDevices()
        {
            List<int> dev = new List<int>();
            List<WPDevice> wpdl = new List<WPDevice>();
            WPDeviceDO wpddo = new WPDeviceDO();
            wpdl = wpddo.RetrieveDevicesByNameAndType(Utilities.ConvertToInt(radddlTypeDevice.SelectedValue), tbDeviceTitle.Text.Trim()).OfType<WPDevice>().ToList<WPDevice>();
            if (Session["Devices"] != null)
            {
                dev = Session["Devices"] as List<int>;
            }
            foreach (int ii in dev)
            {
                int index=-1;
                for (int i = 0; i < wpdl.Count; i++)
                {
                    if (wpdl[i].ID == ii)
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    wpdl.RemoveAt(index);
                }
            }
            repDevice.DataSource = wpdl;
            repDevice.DataBind();
        }
        protected void lb_Click(object sender, EventArgs e)
        {
            List<WPDevice> wpdl = new List<WPDevice>();
            WPDeviceDO wpddo=new WPDeviceDO();
            wpdl = wpddo.RetrieveDevicesByNameAndType(Utilities.ConvertToInt(radddlTypeDevice.SelectedValue), tbDeviceTitle.Text.Trim()).OfType<WPDevice>().ToList<WPDevice>();
            repDevice.DataSource = wpdl;
            repDevice.DataBind();
        }

        protected void repDevice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int id=Utilities.ConvertToInt((e.Item.FindControl("hfDeviceID") as HiddenField).Value);
                List<int> dev = new List<int>();
                
                if (Session["Devices"] == null)
                {
                    dev.Add(id);
                    Session["Devices"] = dev;
                }
                else
                {
                    dev = Session["Devices"] as List<int>;
                    dev.Add(id);
                    Session["Devices"] = dev;
                }
                LoadTempDevices();
                LoadDevices();
            }
        }

        protected void repDevicesSaved_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id=Utilities.ConvertToInt((e.Item.FindControl("hfDeviceID") as HiddenField).Value);
                List<int> dev = new List<int>();
                if (Session["Devices"] != null)
                {
                    dev = Session["Devices"] as List<int>;
                }
                dev.Remove(id);
                Session["Devices"] = dev;
                LoadTempDevices();
                LoadDevices();
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            WaterPoint wp = new WaterPoint();
            wp.Title=tbTitle.Text.Trim();
            wp.LocationID = Utilities.ConvertToInt(radddlWPLocation.SelectedValue);
            wp.LineFirst = Utilities.ConvertToDouble(tbLF.Text.Trim());
            wp.LineSecond = Utilities.ConvertToDouble(tbLS.Text.Trim());
            wp.D = Utilities.ConvertToInt(tbD.Text);
            wp.DCalc = Utilities.ConvertToInt(tbDCalc.Text);
            wp.QMin = Utilities.ConvertToInt(tbQmin.Text);
            wp.QMax = Utilities.ConvertToInt(tbQmax.Text);
            wp.Wptype = Utilities.ConvertToInt(radddlTypeDevice.SelectedValue);
            wp.Comment = tbComment.Text.Trim();

            WaterPointDO wpDO = new WaterPointDO();
            int rez = 0;
            StringBuilder xml = new StringBuilder();
            List<int> dev = new List<int>();
            if (Session["Devices"] != null)
            {
                xml.Append("<devices>");
                dev = Session["Devices"] as List<int>;
                foreach (int i in dev)
                {
                    xml.Append("<id>"+i.ToString()+"</id>");                        
                }
                xml.Append("</devices>");
                
            }

            rez = wpDO.Create(wp, xml.ToString());
            if (rez > 0)
            {
                nlWP.SetCleanNotification("Водовод успешно добавлен.");
            }
            else
            {
                nlWP.SetDirtyNotification("Произошла ошибка при добавлении водовода.");
            }
        }
    }
}