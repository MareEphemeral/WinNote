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
    public class BaseConnection //数据库连接类
    {
        [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl),]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, string filePath);
        public BaseConnection(String tableName) //从配置文件中读取连接字符串
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                String tempString = "";
                StreamReader txtStreamReader = new StreamReader("NetConfig.ini");
                tempString = txtStreamReader.ReadLine();
                CommonPara.ConnectionString = "Provider=SQLOLEDB.1;Password=mare;Persist Security Info=True;User ID=sa;Initial Catalog=Note;Data Source="+tempString;


            }
            catch (Exception e)
            {
               
                CommonPara.ErrorMessage = "找不到配置文件";
                CommonPara.ErrorMessage += e.Message;
            }
        }
        public DataTable BaseSelect(String sql) //执行一条SQL语句，返回结果集
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
                CommonPara.ErrorMessage = e.Message;
            }
            return tempSet.Tables[0];
        }

        public DataTable BaseInsert(String[] fieldsList,Object[] value,String tableName) //执行一条插入语句
        {

            DataSet tempSet = new DataSet();
            try
            {

                using (OleDbConnection connection = new OleDbConnection(CommonPara.ConnectionString))
                {
                    //打开连接
                    connection.Open();
                    OleDbCommand cmmd = new OleDbCommand();
                    cmmd.CommandText = "Select * from "+tableName;
                    cmmd.Connection = connection;
                    OleDbDataAdapter tempAdapter = new OleDbDataAdapter();
                    tempAdapter.SelectCommand = cmmd;
                    tempAdapter.Fill(tempSet,tableName);
                    tempSet.Tables[0].Columns["id"].AutoIncrement = true;
                    tempSet.Tables[0].Columns["id"].AutoIncrementSeed = 1;
                    tempSet.Tables[0].Columns["id"].AutoIncrementStep = 1;
                    DataRow tempRow = tempSet.Tables[0].NewRow();
                    for (int i = 0; i < fieldsList.Length;i++ )
                    {
                        tempRow[fieldsList[i]] = value[i];
                    }
                    tempSet.Tables[0].Rows.Add(tempRow);
                    tempAdapter.Update(tempSet, tableName);
                    return tempSet.Tables[0];
                }
            }
            catch (Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            return tempSet.Tables[0];
        }

        public int BaseSP(String spName, OleDbParameter[] para) //执行一个过程函数
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
                CommonPara.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
