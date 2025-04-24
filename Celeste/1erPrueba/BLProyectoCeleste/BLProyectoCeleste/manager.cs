using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;


namespace BLProyectoCeleste
{
    public class manager
    {
        #region Atributos

        // string mconnStr = "Data Source=ALB--PC01;Initial Catalog=TFI;Integrated Security=True";
        string mconnstr = "Data Source=LAPTOP-G5ORBE1P\\SQLEXPRESS;Initial Catalog=pruebaFrontBackend;Integrated Security=True;";

        SqlConnection mConn;

        #endregion

        #region Metodos

        public DataSet ExecuteDataSet(string comando)
        {
            DataSet mDataSet = new DataSet();

            try
            {
                mConn = new SqlConnection(mconnstr);
                mConn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(comando, mConn);
                sqlAdapter.Fill(mDataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mConn.Close();
                mConn.Dispose();
            }

            return mDataSet;
        }

        public int ExecuteNonQuery(string comando)
        {
            try
            {
                mConn = new SqlConnection(mconnstr);
                mConn.Open();
                SqlCommand sqlCommand = new SqlCommand(comando, mConn);
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mConn.Close();
                mConn.Dispose();
            }

            return 0;
        }

        public int ExecuteScalar(string comando)
        {
            try
            {
                mConn = new SqlConnection(mconnstr);
                mConn.Open();
                SqlCommand sqlCommand = new SqlCommand(comando, mConn);
                var result = sqlCommand.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mConn.Close();
                mConn.Dispose();
            }

            return 0;
        }

        public void ProbarConexion()
        {
            try
            {
                mConn = new SqlConnection(mconnstr);
                mConn.Open();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void CrearBackUp(string strFilePathAndName)
        {
            SqlConnection mconn = new SqlConnection(mconnstr);

            try
            {
                mConn.Open();
                SqlCommand com = new SqlCommand(strFilePathAndName, mconn);
                com.ExecuteReader();
            }
            finally
            {
                mConn.Close();
                mConn.Dispose();
            }
        }

        public void HacerRestore(string strRestore)
        {
            string mconnStr = "Data Source=ALB--PC01;Initial Catalog=master;Integrated Security=True";
            SqlConnection mConn = new SqlConnection(mconnStr);

            try
            {
                mConn.Open();
                SqlCommand com = new SqlCommand(strRestore, mConn);
                com.ExecuteNonQuery();
            }
            finally
            {
                mConn.Close();
                mConn.Dispose();
            }
        }

        #endregion
    }

}

