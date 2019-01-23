using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entities;


namespace DAO
{
    public class UAbonentDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                UAbonent ent = new UAbonent();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public UAbonent createEntityFromReader(SqlDataReader dr)
        {
            UAbonent ent = new UAbonent();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                ent.Title = dr["Title"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("OKPO")))
                ent.OKPO = dr["OKPO"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("MFO")))
                ent.MFO = dr["MFO"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("RS")))
                ent.RS = dr["RS"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Contract")))
                ent.Contract = dr["Contract"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Bank")))
                ent.Bank = dr["Bank"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("IsBudget")))
                ent.IsBudget = Convert.ToBoolean(dr["IsBudget"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DogID")))
                ent.DogID = Convert.ToInt32(dr["DogID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("VATPay")))
                ent.VATPay = dr["VATPay"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                ent.Phone = dr["Phone"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("ContactFace")))
                ent.ContactFace = dr["ContactFace"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Cause")))
                ent.Cause = dr["Cause"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("INN")))
                ent.INN = dr["INN"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("TaxType")))
                ent.TaxType = dr["TaxType"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }
}