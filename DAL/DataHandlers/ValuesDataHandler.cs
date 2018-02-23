using Microsoft.Extensions.Configuration;
using MyWebApp.models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace MyWebApp.DAL.DataHandlers
{
    public class ValuesDataHandler
    {
        private readonly IConfiguration configuration;


        public ValuesDataHandler(IConfiguration config)
        {
            configuration = config;
        }

        public string GetValues() {
            //ModelClass objModel = new ModelClass();
            DataSet ds = new DataSet();
            DALCommon objDALCommon = new DALCommon();
            Dictionary<string, string> StoredProcedureParams = new Dictionary<string, string>();
            StoredProcedureParams.Add("@Parameter1", "Parameter1_Value");
            ds = objDALCommon.DBCall(configuration.GetConnectionString("NewBusiness"), "STORED_PROCEDURE_NAME", StoredProcedureParams);
            objDALCommon.AssignTableName(ref ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //assign to Model
                //objModel.SomeProperty = ds.Tables["Table1Name"].Rows[0]["ColumnName"].ToString();
            }

            return JsonConvert.SerializeObject("objModel");
        }
    }
}
