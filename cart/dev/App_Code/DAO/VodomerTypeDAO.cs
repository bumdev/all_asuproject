using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;


/// <summary>
/// Summary description for VodomerTypeDAO
/// </summary>
/// 

namespace DAO
{
    public class VodomerTypeDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                VodomerType ent = new VodomerType();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public VodomerType createEntityFromReader(SqlDataReader dr)
        {
            VodomerType ent = new VodomerType();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("conventional_signth")))
                ent.ConventionalSignth = dr["conventional_signth"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("diameter")))
                ent.Diameter= Convert.ToInt32(dr["diameter"]);

            if (!dr.IsDBNull(dr.GetOrdinal("gear_ratio")))
                ent.GearRatio = Convert.ToDouble(dr["gear_ratio"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Active")))
                ent.IsActive = Convert.ToBoolean(dr["Active"]);

            if (!dr.IsDBNull(dr.GetOrdinal("id_seller")))
                ent.SellerID = Convert.ToInt32(dr["id_seller"]);

            if (!dr.IsDBNull(dr.GetOrdinal("description")))
                ent.Description = dr["description"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("GovRegister")))
                ent.GovRegister = dr["GovRegister"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DateProduced")))
                ent.DateProduced = dr["DateProduced"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("CheckInterval")))
                ent.CheckInterval = Convert.ToInt32(dr["CheckInterval"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Approve")))
                ent.Approve = Convert.ToBoolean(dr["Approve"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}