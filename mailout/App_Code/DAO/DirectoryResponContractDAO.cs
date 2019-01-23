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
    public class DirectoryResponContractDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                DirectoryResponContract ent = new DirectoryResponContract();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public DirectoryResponContract createEntityFromReader(SqlDataReader dr)
        {
            DirectoryResponContract ent = new DirectoryResponContract();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ResponName")))
                ent.ResponName = dr["ResponName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Position")))
                ent.Position = dr["Position"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DepartamentID")))
                ent.DepartamentID = Convert.ToInt32(dr["DepartamentID"]);

            

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}