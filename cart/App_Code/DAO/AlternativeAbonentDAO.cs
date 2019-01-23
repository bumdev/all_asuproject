using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAO;
using Entities;


namespace DAO
{
    public class AlternativeAbonentDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                AlternativeAbonent ent = new AlternativeAbonent();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public AlternativeAbonent createEntityFromReader(SqlDataReader dr)
        {
            AlternativeAbonent ent = new AlternativeAbonent();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("FAbonentID")))
                ent.FAbonentID = Convert.ToInt32(dr["FAbonentID"]);*/

            if (!dr.IsDBNull(dr.GetOrdinal("FirstName")))
                ent.FirstName = dr["FirstName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Surname")))
                ent.Surname = dr["Surname"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("LastName")))
                ent.LastName = dr["LastName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                ent.Phone = dr["Phone"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("NumberJournal")))
                ent.PhysicalNumberJournal = dr["NumberJournal"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictID")))
                ent.DistrictID = Convert.ToInt32(dr["DistrictID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}