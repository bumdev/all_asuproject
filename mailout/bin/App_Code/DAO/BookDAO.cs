using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.Data.SqlClient;

namespace DAO
{
    public class BookDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                Book ent = new Book();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public Book createEntityFromReader(SqlDataReader dr)
        {
            Book ent = new Book();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                ent.Title = dr["Title"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Description")))
                ent.Description = dr["Description"].ToString();

            //Description



            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}