using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DomainObjects;
using Entities;
using DAO;

namespace DomainObjects
{
    public class OtherMailDetailsDO : UniversalDO
    {
        void AddParametersToSqlCommand(OtherMailDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@OtherMailSendID", ent.OtherMailSendID);
            //sc.Parameters.Add("@IsDeleted", ent.IsDeleted);
            sc.Parameters.Add("@ResponsibleContractorID", ent.ResponsibleContractorID);
        }

        void addParameters(OtherMailDetails ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(OtherMailDetails ent)
        {
            int createid = 0;
            OtherMailDetailsDAO entDAO = new OtherMailDetailsDAO();
            sc = new SqlCommand("CreateOtherMailDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }
    }
}