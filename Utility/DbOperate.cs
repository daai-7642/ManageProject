using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public  class DbOperate
    {
        public static bool BackDB(string fileName)
        {
            string cmdText = @"backup database " + System.Configuration.ConfigurationManager.AppSettings["DbName"] + " to disk='" + fileName + "'";
            return BakReductSql(cmdText, true);
        }
        public static bool ReductDB(string fileName)
        {
            string cmdText = @"backup database " + System.Configuration.ConfigurationManager.AppSettings["DbName"] + " to disk='" + fileName + "'";
            return BakReductSql(cmdText, true);
        }
        /// <summary>
        /// 对数据库的备份和恢复操作，Sql语句实现
        /// </summary>
        /// <param name="cmdText">实现备份或恢复的Sql语句</param>
        /// <param name="isBak">该操作是否为备份操作，是为true否，为false</param>
        private static bool BakReductSql(string cmdText, bool isBak)
        {
            bool result = false;
            SqlCommand cmdBakRst = new SqlCommand();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                conn.Open();
                cmdBakRst.Connection = conn;
                cmdBakRst.CommandType = CommandType.Text;
                if (!isBak)     //如果是恢复操作
                {
                    string setOffline = "Alter database GroupMessage Set Offline With rollback immediate ";
                    string setOnline = " Alter database GroupMessage Set Online With Rollback immediate";
                    cmdBakRst.CommandText = setOffline + cmdText + setOnline;
                }
                else
                {
                    cmdBakRst.CommandText = cmdText;
                }
                cmdBakRst.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException sexc)
            {

                Log4net.ErrorLogHelper.WriteLog("失败，可能是对数据库操作失败",sexc);
            }
            catch (Exception ex)
            {
                Log4net.ErrorLogHelper.WriteLog("对不起，操作失败，可能原因", ex);
            }
            finally
            {
                cmdBakRst.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
    }
}
