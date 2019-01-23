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
    public class ResponsibleContractorDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                ResponsibleContractor ent = new ResponsibleContractor();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public ResponsibleContractor createEntityFromReader(SqlDataReader dr)
        {
            ResponsibleContractor ent = new ResponsibleContractor();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("District")))
                ent.District = dr["District"].ToString();*/

            if (!dr.IsDBNull(dr.GetOrdinal("id_directory_respon_contract")))
                ent.ResponContractDirectory = Convert.ToInt32(dr["id_directory_respon_contract"]);

            if (!dr.IsDBNull(dr.GetOrdinal("date_registration")))
                ent.DateRegister = Convert.ToDateTime(dr["date_registration"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}