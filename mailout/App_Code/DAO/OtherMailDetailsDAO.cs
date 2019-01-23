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
    public class OtherMailDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                OtherMailDetails ent = new OtherMailDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public OtherMailDetails createEntityFromReader(SqlDataReader dr)
        {
            OtherMailDetails ent = new OtherMailDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("OtherMailSendID")))
                ent.OtherMailSendID = Convert.ToInt32(dr["OtherMailSendID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                ent.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

            if (!dr.IsDBNull(dr.GetOrdinal("ResponsibleContractorID")))
                ent.ResponsibleContractorID = Convert.ToInt32(dr["ResponsibleContractorID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}