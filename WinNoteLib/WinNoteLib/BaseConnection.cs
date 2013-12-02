using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
namespace WinNoteLib
{
    public class BaseConnection
    {
        private SqlConnection Conn;
        [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl),]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, string filePath);
        public BaseConnection(String tableName)
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                String tempString = "";
                StreamReader txtStreamReader = new StreamReader("NetConfig.ini");
                tempString = txtStreamReader.ReadLine();
                // GetPrivateProfileString("ConnectionString",tempString , "对应项不存在", temp, "NetConfig.ini");
                //   CommonPara.ConnectionString = "server="+tempString+";uid = sa; pwd =; database = Note";

                CommonPara.ConnectionString = tempString;


            }
            catch (Exception e)
            {
                CommonPara.ErrorMessage = "找不到配置文件";
            }
        }
        public DataTable BaseSelect(String sql)
        {

            DataSet tempSet = new DataSet();
            try
            {

                using (OleDbConnection connection = new OleDbConnection(CommonPara.ConnectionString))
                {
                    //打开连接
                    connection.Open();
                    OleDbCommand cmmd = new OleDbCommand();
                    cmmd.CommandText = sql;
                    cmmd.Connection = connection;
                    OleDbDataAdapter tempAdapter = new OleDbDataAdapter();
                    tempAdapter.SelectCommand = cmmd;
                    tempAdapter.Fill(tempSet);
                    return tempSet.Tables[0];
                }
            }
            catch (Exception e)
            {

            }
            return tempSet.Tables[0];
            /*   String message = "";
               DataSet tempSet = new DataSet();
               SqlConnection conn;
 
               try
               {
                   conn = new SqlConnection(CommonPara.ConnectionString);
                   conn.Open();      
                   SqlDataAdapter tempAdapter = new SqlDataAdapter();
                   tempAdapter.SelectCommand=new SqlCommand(sql, conn);
                   tempAdapter.Fill(tempSet);
                   conn.Close();
                
               }
               catch (Exception e)
               {
                   message = e.Message;
                   CommonPara.ErrorMessage="数据库连接错误";
               }
        
               return tempSet.Tables[0];*/
        }
        public int BaseSP(String spName, OleDbParameter[] para)
        {
            int result=-1;
            try
            {

                using (OleDbConnection connection = new OleDbConnection(CommonPara.ConnectionString))
                {
                    //打开连接
                    connection.Open();
                    OleDbCommand cmmd = new OleDbCommand();
                    cmmd.CommandType = CommandType.StoredProcedure;
                    cmmd.CommandText = spName;
                    foreach (OleDbParameter Apara in para)
                    {
                        cmmd.Parameters.Add(Apara);
                    }
                    cmmd.Connection = connection;
                    result = cmmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
