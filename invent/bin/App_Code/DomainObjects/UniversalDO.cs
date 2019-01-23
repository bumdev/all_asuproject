using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DAO;
using Entities;

namespace DomainObjects
{

	public class UniversalDO
	{
		protected SqlCommand sc;
        //protected Error InnerError = null;

		public UniversalDO()
		{
			
		}

        /*protected void ClearInnerError()
        {
            InnerError = null;
        }
        /// <summary>
        /// Returns the last error as set by the DAO layer
        /// </summary>
        /// <returns>Error object or null as appropriate</returns>
        public Error GetLastError()
        {
            return InnerError;
        }

        /// <summary>
        /// Clears the last error from the object
        /// </summary>
        public void ClearLastError()
        {
            ClearInnerError();
        }*/

        protected SqlParameter createParm(string name, int value)
        {
            SqlParameter parm;
            if (value == 0)
                parm = new SqlParameter(name, DBNull.Value);
            
            else
                parm = new SqlParameter(name, value);
            
            parm.SqlDbType = SqlDbType.Int;
            return (parm);
        }        
        
        protected SqlParameter createParm(string name, string value)
        {
            SqlParameter parm;

            if (value == "&nbsp;")
                value = "";

            if ((value == null) || (value == ""))
            {
                parm = new SqlParameter(name, DBNull.Value);
            }
            else
            {
                
                parm = new SqlParameter(name, value);
            }
            parm.SqlDbType = SqlDbType.VarChar;
            return (parm);
        }        
        
        protected SqlParameter createParm(string name, DateTime value)
        {
            SqlParameter parm;
            if (value == DateTime.MinValue)
                parm = new SqlParameter(name, DBNull.Value);
            else if (value.Year == 1900 && value.Day == 1 && value.Month == 1)
                parm = new SqlParameter(name, DBNull.Value);
            else
                parm = new SqlParameter(name, value);
            
            parm.SqlDbType = SqlDbType.DateTime;
            return (parm);
        }        
        
        protected SqlParameter createParm(string name, Decimal value)
        {
            SqlParameter parm;
            if (value.Equals(0))
            {
                parm = new SqlParameter(name, DBNull.Value);
            }
            else
            {
                parm = new SqlParameter(name, value);
            }
            parm.SqlDbType = SqlDbType.Decimal;
            return (parm);
        }

        protected SqlParameter createParm(string name, Byte[] value)
        {
            SqlParameter parm;
            parm = new SqlParameter(name, value);
           
            parm.SqlDbType = SqlDbType.Binary;
            return (parm);
        }

        protected SqlParameter createParm(string name, bool value)
        {
            SqlParameter parm;
            parm = new SqlParameter(name, value);
            
            parm.SqlDbType = SqlDbType.Bit;
            return (parm);
        }             
	}
}
