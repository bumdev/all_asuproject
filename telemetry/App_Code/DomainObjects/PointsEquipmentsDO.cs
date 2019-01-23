using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

/// <summary>
/// Summary description for VodomerDO
/// </summary>
/// 

namespace DomainObjects
{
    public class PointsEquipmentsDO:UniversalDO
    {
        void AddParametersToSqlCommand(PointsEquipments ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@EquipmentID", ent.EquipmentID);
            sc.Parameters.Add("@InventoryNumber", ent.InventoryNumber);
            sc.Parameters.Add("@State", ent.State);
            sc.Parameters.Add("@Repairs", ent.Repairs);
        }
        void addParameters(PointsEquipments ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
        public int Create(PointsEquipments ent)
        {
            int createdid = 0;
            PointsEquipmentsDAO entDAO = new PointsEquipmentsDAO();
            sc = new SqlCommand("CreatePointsEquipments");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createdid = entDAO.createEntity(sc);
            return createdid;
        }
    }
}