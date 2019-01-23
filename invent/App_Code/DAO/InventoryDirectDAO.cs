using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entities;

namespace DAO
{
    public class InventoryDirectDAO:UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();
            while (dr.Read())
            {
                InventoryDirect ent = new InventoryDirect();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public InventoryDirect createEntityFromReader(SqlDataReader dr)
        {
            InventoryDirect ent = new InventoryDirect();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("TypeFurnID")))
                ent.TypeFurnID = Convert.ToInt32(dr["TypeFurnID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DepartID")))
                ent.DepartID = Convert.ToInt32(dr["DepartID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ModelID")))
                ent.ModelID = Convert.ToInt32(dr["ModelID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("GroupID")))
                ent.GroupID = Convert.ToInt32(dr["GroupID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateExploitation")))
                ent.DateExploitation = Convert.ToDateTime(dr["DateExploitation"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateBalance")))
                ent.DateBalance = Convert.ToDateTime(dr["DateBalance"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Wear")))
                ent.Wear = dr["Wear"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("InitialCost")))
                ent.InitialCost = Convert.ToDouble(dr["InitialCost"]);

            if (!dr.IsDBNull(dr.GetOrdinal("LiquidationCost")))
                ent.LiquidationCost = Convert.ToDouble(dr["LiquidationCost"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Weariness")))
                ent.Weariness = dr["Weariness"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Comment")))
                ent.Comment = dr["Comment"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("MethodCalculation")))
                ent.MethodCalculation = dr["MethodCalculation"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("CountImplements")))
                ent.CountImplements = Convert.ToInt32(dr["CountImplements"]);

            if (!dr.IsDBNull(dr.GetOrdinal("TermOfUse")))
                ent.TermOfUse = dr["TermOfUse"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictID")))
                ent.DistrictID = Convert.ToInt32(dr["DistrictID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("InventoryNumber")))
                ent.InventoryNumber = Convert.ToInt32(dr["InventoryNumber"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DepreciationRate")))
                ent.DepreciationRate = dr["DepreciationRate"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("ManufacturerID")))
                ent.ManufacturerID = Convert.ToInt32(dr["ManufacturerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("NomenclativeNumber")))
                ent.NomenlativeNumber = Convert.ToInt32(dr["NomenclativeNumber"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}