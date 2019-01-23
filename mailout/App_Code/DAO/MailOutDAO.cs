using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;
using ExcelLibrary.BinaryFileFormat;


namespace DAO
{
    public class MailOutDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                MailOut ent = new MailOut();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public MailOut createEntityFromReader(SqlDataReader dr)
        {
            MailOut ent = new MailOut();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("RegNumber")))
                ent.RegNumber = dr["RegNumber"].ToString();

           /* if (!dr.IsDBNull(dr.GetOrdinal("DateRegistration")))
                ent.DateRegistration = Convert.ToDateTime(dr["DateRegistration"]);*/

            /*if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);*/

            if (!dr.IsDBNull(dr.GetOrdinal("About")))
                ent.About = dr["About"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AdresatID")))
                ent.AdresatID = Convert.ToInt32(dr["AdresatID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Whom")))
                ent.Whom = dr["Whom"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AnswerAbout")))
                ent.AnswerAbout = dr["AnswerAbout"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AnswerDate")))
                ent.AnswerDate = dr["AnswerDate"].ToString();

            /*if (!dr.IsDBNull(dr.GetOrdinal("Notation")))
                ent.Notation = dr["Notation"].ToString();*/

            if (!dr.IsDBNull(dr.GetOrdinal("PersonalAccount")))
                ent.PersonalAccount = dr["PersonalAccount"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}