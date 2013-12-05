using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WinNoteLib
{
    public class AssistDll //包含字符串构造和数据转换等辅助函数的支持库
    {
        public static String DeleteConstructor(String situation,String tableName){  //输入删除的表和条件，输出删除SQL语句
       
            String sql = "DELETE ";
            sql += " FROM " + tableName + " WHERE " + situation;
            return sql;
        }

        public static String SelectConstructor(String[] targetFields, String situation, String tableName) //输入查询的字段名，条件和表名，输出查询SQL语句
        {

            String sql = "SELECT ";
            foreach (String field in targetFields)
            {
                sql += field + ",";
            }
            sql = sql.Remove(sql.LastIndexOf(','), 1);
            sql += " FROM " + tableName + " WHERE " + situation;
            return sql;
        }
       
        public static String SelectConstructor( String situation, String tableName) //构造查询全字段的SQL语句
        {

            String sql = "SELECT *";
            sql += " FROM " + tableName + " WHERE " + situation;
            return sql;
        }
        public static String ChangeIDToName( String name, int id,String tableName) //输入表名，转换的字段名，和ID号，将对应ID号转换成该表的另一字段
        {
            String targetName = "";
            String sql = "SELECT " + name + " FROM "+tableName+" WHERE id="+id;
            BaseConnection conn=new BaseConnection(tableName);
            try
            {
                targetName = conn.BaseSelect(sql).Rows[0].ItemArray[0].ToString();
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            return targetName;

        }
        public static int ChangeNameToID(String fieldName, String name, String tableName) //输入表名，输入的字段名，和字段值，获取对应的ID值
        {
            int targetID = -1;
            String sql = "SELECT " + "id" + " FROM " + tableName + " WHERE "+fieldName+"=\'"+name+"\'";
            BaseConnection conn = new BaseConnection(tableName);
            try
            {
                targetID = (int)conn.BaseSelect(sql).Rows[0].ItemArray[0];
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            return targetID;
        }
        public static OleDbParameter[] ParaConstructor(String[] paraName, Object[] para) //将输入的存储过程的变量装箱
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
                CommonPara.ErrorMessage = e.Message;
            }
            return paras;
        }

        public static String tagsEncapsulation(String[] inputTags) //将标签组连接成一个标签字段
        {
            String result = "";
            foreach(string tempTag in inputTags)
            {
                result += tempTag+"-";
            }
            return result;
        }


        public static String[] tagsDearchive(String inputTags) //从一个字段中提取整个标签组
        {
            String[] result;
            result = inputTags.Split('-');
            return result;
        }
        public static String TopSelectConstructor(String[] targetFields,String situation,String tableName,int topNumber,bool order,string orderField){ //构建选取前指定项的SQL语句
       
            String sql = "SELECT TOP "+topNumber.ToString()+ " ";
            foreach (String field in targetFields)
            {
                sql+=field+",";
            }
            sql=sql.Remove(sql.LastIndexOf(','),1);
            sql += " FROM " + tableName + " WHERE " + situation;
            if (order) sql += " ORDER BY " + orderField + " DESC";
            else sql += " ORDER BY ASC";
    
            return sql;
        }
        public static byte[] Serialize(Object obj) //对象序列化函数
        {
            Stream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(stream, obj);  //序列化
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            byte[] array = null;

            array = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(array, 0, (int)stream.Length);
            stream.Close();
            return array;
        }

        public static Object Deserialize(byte[] biObj) //对象反序列化
        {

            MemoryStream stream = new MemoryStream(biObj);
            BinaryFormatter bf = new BinaryFormatter();
            Object result=new Object();
            while (stream.Position != stream.Length)
            {
                result=bf.Deserialize(stream);
            }
            stream.Close();
            return result;
        }
    } 
}
