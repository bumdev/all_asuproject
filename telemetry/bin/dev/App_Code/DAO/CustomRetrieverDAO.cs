using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Entities;

/// <summary>
/// Summary description for CustomRetrieverDAO
/// </summary>
namespace DAO
{
    public class CustomRetrieverDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();
            return ue;
        }
        public List<Book> RetrieveWPTypeDevice(SqlCommand command)
        {
            List<Book> ue = new List<Book>();
            createConnection();
            command.Connection = sq;

            try
            {
                Book book;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    book = new Book();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        book.ID=(Utilities.ConvertToInt(dr["ID"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                        book.Title=(dr["Title"].ToString());

                    ue.Add(book);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }
        public UniversalEntity RetrieveDiameters(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("Diameter")))
                        al.Add(Utilities.ConvertToInt(dr["Diameter"].ToString()));

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveDistricts(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        al.Add(Utilities.ConvertToInt(dr["ID"].ToString()));
                    
                    if (!dr.IsDBNull(dr.GetOrdinal("DistrictName")))
                        al.Add(dr["DistrictName"].ToString());

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        //RetrieveFActByOrderID
        //RetrieveUserLocation
        public UniversalEntity RetrieveUserLocation(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("id")))
                        al.Add(Utilities.ConvertToInt(dr["id"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("Prefix")))
                         al.Add(dr["Prefix"].ToString());

                    if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                        al.Add(dr["Phone"]);

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveUserLocationByFOrder(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                string t = string.Empty;
                dr = command.ExecuteReader();
                while (dr.Read())
                {


                    if (!dr.IsDBNull(dr.GetOrdinal("UserLocation")))
                        t = dr["UserLocation"].ToString();



                    ue.Add(t);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveUserLocationBySurname(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                string t=string.Empty;
                dr = command.ExecuteReader();
                while (dr.Read())
                {


                    if (!dr.IsDBNull(dr.GetOrdinal("UserLocation")))
                        t = dr["UserLocation"].ToString();

                    ue.Add(t);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }
        public UniversalEntity GetRateByDateAndWP(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("R")))
                        al.Add(Utilities.ConvertToDouble(dr["R"].ToString()));

                   /* if (!dr.IsDBNull(dr.GetOrdinal("d")))
                        al.Add(Utilities.ConvertToInt(dr["d"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                        al.Add(Convert.ToDateTime(dr["DateIn"]));*/

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveFActByOrderID5Low(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("diameter")))
                        al.Add(Utilities.ConvertToInt(dr["diameter"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("nom_zavod")))
                        al.Add(dr["nom_zavod"].ToString());

                    if (!dr.IsDBNull(dr.GetOrdinal("StartValue")))
                        al.Add(dr["StartValue"].ToString());

                    if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                        al.Add(dr["Price"].ToString());

                    al.Add(1);
                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }
        public UniversalEntity RetrieveFActByOrderID5High(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("diameter")))
                        al.Add(Utilities.ConvertToInt(dr["diameter"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                        al.Add(dr["Price"].ToString());

                    if (!dr.IsDBNull(dr.GetOrdinal("summa")))
                        al.Add(dr["summa"].ToString());

                    if (!dr.IsDBNull(dr.GetOrdinal("ocount")))
                        al.Add(dr["ocount"].ToString());

                    al.Add(1);
                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }
        public UniversalEntity RetrieveGroups(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        al.Add(Utilities.ConvertToInt(dr["ID"].ToString()));

                    if (!dr.IsDBNull(dr.GetOrdinal("UserLocation")))
                        al.Add(dr["UserLocation"].ToString());

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }
        /*
        public UniversalEntity RetrieveContractors(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("ContractorID")))
                        al.Add(Utilities.ConvertToInt(dr["ContractorID"].ToString()));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        al.Add(dr["Name"].ToString());

                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveContracts(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("ContractID")))
                        al.Add(Utilities.ConvertToInt(dr["ContractID"].ToString()));
                    if (!dr.IsDBNull(dr.GetOrdinal("Number")))
                        al.Add(dr["Number"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("Contractor")))
                        al.Add(dr["Contractor"].ToString());
                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveTransporters(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("TransporterID")))
                        al.Add(Utilities.ConvertToInt(dr["TransporterID"].ToString()));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        al.Add(dr["Name"].ToString());
                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

        public UniversalEntity RetrieveMarksbyName(SqlCommand command)
        {
            UniversalEntity ue = new UniversalEntity();
            createConnection();
            command.Connection = sq;

            try
            {
                ArrayList al;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    al = new ArrayList();

                    if (!dr.IsDBNull(dr.GetOrdinal("MarkID")))
                        al.Add(Utilities.ConvertToInt(dr["MarkID"].ToString()));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        al.Add(dr["Name"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("Ammount")))
                       al.Add(Convert.ToInt32(dr["Ammount"]));
                    if (!dr.IsDBNull(dr.GetOrdinal("MassKG")))
                        al.Add(Convert.ToDecimal(dr["MassKG"]));
                    if (!dr.IsDBNull(dr.GetOrdinal("MassTotalKG")))
                        al.Add(Convert.ToDecimal(dr["MassTotalKG"]));
                    if (!dr.IsDBNull(dr.GetOrdinal("PricePerTN")))
                        al.Add(Convert.ToDecimal(dr["PricePerTN"]));
                    ue.Add(al);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {


            }
            finally
            {
                closeConnection();
            }

            return ue;
        }

       */

    }
}