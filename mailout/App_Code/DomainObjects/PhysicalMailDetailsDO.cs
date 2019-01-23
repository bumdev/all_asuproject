using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace DomainObjects
{
    public class PhysicalMailDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(PhysicalMailDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@PhysicalMailSendID", ent.PhysicalMailSendID);
            sc.Parameters.Add("@ResponsibleContractorID", ent.ResponsibleContractorID);
            
            //sc.Parameters.Add("@DateOut", ent.DateOut);
            /*if (ent.DateRegister.HasValue)
                sc.Parameters.Add("@date_registration", ent.DateRegister.Value.ToShortDateString());*/
        }

        void addParameters(PhysicalMailDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(PhysicalMailDetails ent)
        {
            int createid = 0;
            PhysicalMailDetailsDAO msDAO = new PhysicalMailDetailsDAO();
            sc = new SqlCommand("CreatePhysicalMailDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = msDAO.createEntity(sc);
            return createid;
        }

        
    }
}