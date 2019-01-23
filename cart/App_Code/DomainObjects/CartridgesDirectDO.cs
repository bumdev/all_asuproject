﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Entities;
using DAO;

/// <summary>
/// Summary description for VodomerTypeDO
/// </summary>
/// 
namespace DomainObjects
{
    public class CartridgesDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(CartridgesDirect ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@NameCartridges", ent.NameCartridge);
            sc.Parameters.Add("@Number", ent.Number);
            sc.Parameters.Add("@Information", ent.Information);
            sc.Parameters.Add("@RefuelingCondition", ent.RefuelingCondition);
            sc.Parameters.Add("@Comment", ent.Comment);
            sc.Parameters.Add("@TypeCrtridgesID", ent.TypeCrtridgesID);
            sc.Parameters.Add("@DepartamentID", ent.DepartamentID);
            sc.Parameters.Add("@CartDirectID", ent.CartDirectID);
            sc.Parameters.Add("@InTheWork", ent.InTheWork);
            sc.Parameters.Add("@DateInWork", ent.DateInWork);
        }
        void addParameters(CartridgesDirect ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }
      

        public int CreateCartridgesDirect(CartridgesDirect ent)
        {
            int createid = 0;
            CartridgesDirectDAO entDAO = new CartridgesDirectDAO();
            sc = new SqlCommand("CreateCartridges");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

      /*  public bool UpdateVodomerType(VodomerType ent)
        {
            bool success = true;
            VodomerTypeDAO entDAO = new VodomerTypeDAO();
            sc = new SqlCommand("UpdateRegisty");
            sc.CommandType = CommandType.StoredProcedure;
           sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@Approve", ent.Approve);
            sc.Parameters.Add("@conventional_signth", ent.ConventionalSignth);
            sc.Parameters.Add("@diameter", ent.Diameter);
            sc.Parameters.Add("@id_seller", ent.SellerID);
            sc.Parameters.Add("@Active", ent.IsActive);
            sc.Parameters.Add("@description", ent.Description);
            sc.Parameters.Add("@GovRegister", ent.GovRegister);
            sc.Parameters.Add("@DateProduced", ent.DateProduced);
            sc.Parameters.Add("@CheckInterval", ent.CheckInterval);
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }*/
        public UniversalEntity RetrieveVodomerTypeBySellerIdAndDiameter(int id,int d)
        {
            VodomerTypeDAO entDAO = new VodomerTypeDAO();
            sc = new SqlCommand("RetrieveVodomerTypeBySellerIdAndDiameter");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            sc.Parameters.Add("@Diameter", d);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveVodomerTypeBySellerId(int id)
        {
            VodomerTypeDAO entDAO = new VodomerTypeDAO();
            sc = new SqlCommand("RetrieveVodomerTypeBySellerId");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveVodomerById(int id)
        {
            VodomerTypeDAO entDAO = new VodomerTypeDAO();
            sc = new SqlCommand("RetrieveVodomerTypeById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
        public UniversalEntity RetrieveVodomerTypeByVodomerId(int id)
        {
            VodomerTypeDAO entDAO = new VodomerTypeDAO();
            sc = new SqlCommand("RetrieveVodomerTypeByVodomerId");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }
}