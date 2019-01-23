using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;

namespace DomainObjects
{
    public class InventDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(InventoryDirect ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@TypeFurnID", ent.TypeFurnID);
            sc.Parameters.Add("@DepartID", ent.DepartID);
            sc.Parameters.Add("@ModelID", ent.ModelID);
            sc.Parameters.Add("@GroupID", ent.GroupID);
            sc.Parameters.Add("@DateExploitation", ent.DateExploitation);
            sc.Parameters.Add("@DateBalance", ent.DateBalance);
            sc.Parameters.Add("@Wear", ent.Wear);
            sc.Parameters.Add("@InitialCost", ent.InitialCost);
            sc.Parameters.Add("@LiquidationCost", ent.LiquidationCost);
            sc.Parameters.Add("@Weariness", ent.Weariness);
            sc.Parameters.Add("@Comment", ent.Comment);
            sc.Parameters.Add("@MethodCalculation", ent.MethodCalculation);
            sc.Parameters.Add("@CountImplements", ent.CountImplements);
            sc.Parameters.Add("@TermOfUse", ent.TermOfUse);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@InventoryNumber", ent.InventoryNumber);
            sc.Parameters.Add("@DepreciationRate", ent.DepreciationRate);
            sc.Parameters.Add("@ManufacturerID", ent.ManufacturerID);
            sc.Parameters.Add("@NomenclativeNumber", ent.NomenlativeNumber);
        }

        void addParameters(InventoryDirect ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateInventory(InventoryDirect ent)
        {
            int createid = 0;
            InventoryDirectDAO entDAO = new InventoryDirectDAO();
            UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateInventory");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }
    }
}