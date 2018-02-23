using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyWebApp.DAL
{
    public class DALCommon
    {
        public DataSet DBCall(string ConnectionString, string StoredProcedureName, Dictionary<string, string> StoredProcedureParams)
        {

            DataSet ds = new DataSet();

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand(StoredProcedureName, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    foreach (KeyValuePair<string, string> item in StoredProcedureParams)
                    {
                        sqlCmd.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    try
                    {
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(ds);
                        }
                    }
                    catch (Exception)
                    {
                        sqlConn.Dispose();
                    }

                }
            }

            return ds;
        }

        /// <summary>
        /// Assign the Table Names to the DS table
        /// </summary>
        /// <param name="DS">Provide the DataSet returned the first table of this dataset must contain the table name in order</param>
        public void AssignTableName(ref DataSet DS)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int index = 1; index <= DS.Tables.Count - 1; index++)
                {
                    DS.Tables[index].TableName = DS.Tables[0].Rows[0].ItemArray[index - 1].ToString();
                }

                DS.Tables.Remove("table");
            }
        }

        /// <summary>
        /// Remove All DB Null for String value only.
        /// </summary>
        /// <param name="DS">Pass DataSet as a reference paramter</param>
        private static void RemoveDBNullValues(ref DataSet DS)
        {
            if (DS.Tables.Count > 0)
            {
                foreach (DataTable DSTable in DS.Tables)
                {
                    foreach (DataRow DSTableRow in DSTable.Rows)
                    {
                        foreach (DataColumn DSRowColumn in DSTable.Columns)
                        {
                            if (DSTableRow[DSRowColumn.ColumnName] == DBNull.Value)
                            {
                                switch (DSRowColumn.DataType.Name.ToUpper())
                                {
                                    case "STRING":
                                        DSTableRow[DSRowColumn.ColumnName] = "";
                                        break;
                                    case "BOOLEAN":
                                        DSTableRow[DSRowColumn.ColumnName] = 0;
                                        break;
                                    case "INT16":
                                        DSTableRow[DSRowColumn.ColumnName] = 0;
                                        break;
                                    case "DECIMAL":
                                        DSTableRow[DSRowColumn.ColumnName] = 0.0;
                                        break;
                                    default:
                                        DSTableRow[DSRowColumn.ColumnName] = "";
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }   //end of RemoveDBNullValues

    }   //end of class DALCommon
}   //end of namespace
