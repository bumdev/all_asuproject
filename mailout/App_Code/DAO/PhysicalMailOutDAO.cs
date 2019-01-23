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
    public class PhysicalMailOutDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                PhysicalMailOut ent = new PhysicalMailOut();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public PhysicalMailOut createEntityFromReader(SqlDataReader dr)
        {
            PhysicalMailOut ent = new PhysicalMailOut();


            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PhysicalAdresatID")))
                ent.PhysicalAdresatID = Convert.ToInt32(dr["PhysicalAdresatID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("RegNumber")))
                ent.RegNumber = dr["RegNumber"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Whom")))
                ent.Whom = dr["Whom"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("About")))
                ent.About = dr["About"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AnswerAbout")))
                ent.AnswerAbout = dr["AnswerAbout"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("AnswerDate")))
                ent.AnswerDate = dr["AnswerDate"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Notation")))
                ent.Notation = dr["Notation"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("ContractNumber")))
                ent.ContractNumber = dr["ContractNumber"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("SenderID")))
                ent.SenderID = Convert.ToInt32(dr["SenderID"]);
           
            return ent;
        }

        
        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}