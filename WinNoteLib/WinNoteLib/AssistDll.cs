using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;
namespace WinNoteLib
{
    public class AssistDll
    {
        public static String SelectConstructor(String[] targetFields,String situation,String tableName){
       
            String sql = "SELECT ";
            foreach (String field in targetFields)
            {
                sql+=field+",";
            }
            sql=sql.Remove(sql.LastIndexOf(','),1);
            sql += " FROM " + tableName + " WHERE " + situation;
            return sql;
        }
       
        public static String SelectConstructor( String situation, String tableName)
        {

            String sql = "SELECT *";
            sql += " FROM " + tableName + " WHERE " + situation;
            return sql;
        }
        public static OleDbParameter[] ParaConstructor(String[] paraName, Object[] para)
        {
            OleDbParameter[] paras = new OleDbParameter[para.Length];
            try
            {
                
                for (int i = 0; i < para.Length; i++)
                {
                    OleDbParameter tempPara = new OleDbParameter();
                    tempPara.ParameterName = paraName[i];
                    tempPara.Value = para[i];
                    paras.SetValue(tempPara, i);
                }

            }
            catch (Exception e)
            {



            }
            return paras;
        }
    }
}
