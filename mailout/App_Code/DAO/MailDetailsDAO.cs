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
    public class MailDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                MailDetails ent = new MailDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public MailDetails createEntityFromReader(SqlDataReader dr)
        {
            MailDetails ent = new MailDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("MailSendID")))
                ent.MailSendID = Convert.ToInt32(dr["MailSendID"]);

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