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
    public class CartridgesDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                CartridgesDirect ent = new CartridgesDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public CartridgesDirect createEntityFromReader(SqlDataReader dr)
        {
            CartridgesDirect ent = new CartridgesDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("NameCartridges")))
                ent.NameCartridge = dr["NameCartridges"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Number")))
                ent.Number = Convert.ToInt32(dr["Number"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Information")))
                ent.Information = dr["Information"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("RefuelingCondition")))
                ent.RefuelingCondition = Convert.ToBoolean(dr["RefuelingCondition"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Comment")))
                ent.Comment = dr["Comment"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("TypeCrtridgesID")))
                ent.TypeCrtridgesID = Convert.ToInt32(dr["TypeCrtridgesID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DepartamentID")))
                ent.DepartamentID = Convert.ToInt32(dr["DepartamentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateFueling")))
                ent.DateFueling = Convert.ToDateTime(dr["DateFueling"]);

            if (!dr.IsDBNull(dr.GetOrdinal("CartDirectID")))
                ent.CartDirectID = Convert.ToInt32(dr["CartDirectID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("InTheWork")))
                ent.InTheWork = Convert.ToBoolean(dr["InTheWork"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateInWork")))
                ent.DateInWork = Convert.ToDateTime(dr["DateInWork"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}