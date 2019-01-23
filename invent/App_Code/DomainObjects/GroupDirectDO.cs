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
    public class GroupDirectDO:UniversalDO
    {
        void AddParametersToSqlCommand(GroupDirect ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@GroupName", ent.GroupName);
        }

        void addParameters(GroupDirect ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int CreateGroup(GroupDirect ent)
        {
            int createid = 0;
            GroupDirectDAO entDAO = new GroupDirectDAO();
            UniversalEntity ue = new UniversalEntity();
            sc = new SqlCommand("CreateGroup");
            sc.CommandType = CommandType.StoredProcedure;
            addParameters(ent);
            createid = (entDAO.createEntity(sc));
            return createid;
        }

        public UniversalEntity RetrieveGroupDirect()
        {
            GroupDirectDAO entDAO = new GroupDirectDAO();
            sc = new SqlCommand("RetrieveGroup");
            sc.CommandType = CommandType.StoredProcedure;
            return (entDAO.retrieveEntity(sc));
        }
    }
}