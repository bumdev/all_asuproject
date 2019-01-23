using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Entities;
using DomainObjects;
using System.Collections;


namespace DAO
{
	/// <summary>
	/// Summary description for UniversalDAO.
	/// </summary>

	public abstract class UniversalDAO
	{
		protected string connInfo;
		protected SqlConnection sq;
		protected SqlCommand sc;
		protected SqlDataReader dr;
		protected ArrayList drColNames;
		protected string errorMessage;
		protected bool _ignoreMultipleDatasets = false;

		public bool IgnoreMultipleDatasets
		{
			get { return _ignoreMultipleDatasets; }
			set { _ignoreMultipleDatasets = value; }
		}

		public UniversalDAO()
		{
            
		}

		public abstract UniversalEntity createEntity();

        /* This method creates and opens the connection to the db
        */ 
		protected void createConnection()
		{
            connInfo = ConfigurationManager.ConnectionStrings["ConnectionInfo"].ConnectionString;
			sq = new SqlConnection(connInfo);
			sq.Open();	
		}
		
		/*
		 * This method closes the connection to the db and also closes the SqlDataReader that have been used, if any
		 */ 
		protected void closeConnection()
		{	
			if (dr != null)
				dr.Close();			
			
            sq.Close();
		}

		public int createEntity(SqlCommand command)
		{
			int createdID = 0;
			SqlParameter id;

			id = command.Parameters.Add( "@ID",SqlDbType.Int);
			id.Direction = ParameterDirection.Output; 
			
			createConnection();
			command.Connection = sq;

			/*try
			{*/
				command.ExecuteNonQuery();
				createdID = Convert.ToInt32(command.Parameters["@ID"].Value);
			/*}
			catch (System.Data.SqlClient.SqlException ex )
			{
                     
			}
			finally
			{*/
				closeConnection();
				command.Dispose();
			//}

			return createdID;
		}

        public void createEntityWithNoReturn(SqlCommand command)
        {
            createConnection();
            command.Connection = sq;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
               
            }
            finally
            {
                closeConnection();
                command.Dispose();
            }
        }

        public UniversalEntity retrieveEntity(SqlCommand command)
		{
			UniversalEntity ue = new UniversalEntity();
			/*try
			{*/
				createConnection();
				command.Connection = sq;
				dr = command.ExecuteReader();
				ue = createEntity();
			/*}
			catch (System.Data.SqlClient.SqlException ex )
			{
                
			}
			finally
			{*/
				closeConnection();
				command.Dispose();
			//}

			return ue;
		}

        public int retrieveCount(SqlCommand command)
        {
            int count = 0;
            try
            {
                createConnection();
                command.Connection = sq;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    count = Utilities.ConvertToInt(dr[0].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                
            }
            finally
            {
                closeConnection();
                command.Dispose();
            }

            return count;
        }

		public bool updateEntity(SqlCommand command)
		{
			bool worked = true;

			createConnection();
			command.Connection = sq;

			try
			{
				command.ExecuteNonQuery();
			}
			catch ( System.Data.SqlClient.SqlException ex)
			{				
				worked = false;
                
			}
			finally
			{
				closeConnection();
				command.Dispose();
			}
			return worked;
		}

        public bool deleteEntity(SqlCommand command)
		{
			bool deleted = true;
			createConnection();
			command.Connection = sq;
			try
			{
				command.ExecuteNonQuery();
			}
			catch ( System.Data.SqlClient.SqlException ex)
			{
				deleted = false;
               
			}
			finally
			{
				closeConnection();
				command.Dispose();
			}
			return deleted;
		}

        public byte[] retrieveImage(SqlCommand command)
        {
			byte[] data = null;
            try
            {
                createConnection();
                command.Connection = sq;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("IMAGE")))
                        data = (byte[])dr["IMAGE"];
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

               

            }
            finally
            {
                closeConnection();
                command.Dispose();
            }

            return data;

        }

		public bool ColumnExistsInReader(string columnName)
		{
			if (drColNames == null)
			{
				drColNames = new ArrayList();
				DataTable schemaTable;
				//Retrieve column schema into a DataTable.

				schemaTable = dr.GetSchemaTable();

				foreach (DataRow dc in schemaTable.Rows)
				{
					drColNames.Add(dc[0].ToString().ToLower());
						
				}
			}
			if (drColNames.Contains(columnName.ToLower()))
				return true;
			else
				return false;
			
		}

	}
}