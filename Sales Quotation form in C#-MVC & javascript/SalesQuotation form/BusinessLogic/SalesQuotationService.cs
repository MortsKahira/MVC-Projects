using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DQueris;
using DAL.Model;

namespace DAL.BusinessLogic
{
   public class SalesQuotationService
    {
        public SalesQuotationResponse SaveSaleQuotation(SQHD item)
        {
            SalesQuotationResponse _SalesQuotationResponse = new SalesQuotationResponse();
            try
            {
                //string result = "Fail";
                Int32 docnum = 0;
                //string policy = "";
                //Single AvailableQty = 0;
                //string errorMessage = "";
                if (item != null)
                {
                    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        db.Open();
                        using (var transaction = db.BeginTransaction())
                        {
                            try
                            {
                                docnum = db.ExecuteScalar<Int32>(SqlQuery.GenerateSalesQuotationDocNum, new { CompCode = "001", CostCentreCode = "Cost001" }, transaction, 180, CommandType.Text);
                                item.DocNum = docnum;
                                item.Detail.ForEach(f => f.DocNum = docnum);
                                db.Execute(SqlQuery.AddSalesQuotationSQHD, item, transaction, 180, CommandType.Text);

                                db.Execute(SqlQuery.AddSalesQuotationSQDT, item.Detail, transaction, 180, CommandType.Text);
                                transaction.Commit();
                                _SalesQuotationResponse.DocNum = docnum;
                                _SalesQuotationResponse.Message = "Success";
                                return _SalesQuotationResponse;
                            }
                            catch (Exception ex)
                            {

                                transaction.Rollback();
                                Logger.WriteLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                _SalesQuotationResponse.DocNum = 0;
                                _SalesQuotationResponse.Message = ex.Message;
                                return _SalesQuotationResponse;
                            }

                        }
                    }

                }
                _SalesQuotationResponse.DocNum = 0;
                _SalesQuotationResponse.Message = "Error saving Sales Quotation Invoice";
                return _SalesQuotationResponse;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                _SalesQuotationResponse.DocNum = 0;
                _SalesQuotationResponse.Message = ex.Message;
                return _SalesQuotationResponse;
            }
        }


        public List<Customers> GetCusts(string name)
        {
            try
            {


                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {


                    if (name == "*")
                    {
                        return db.Query<Customers>("SELECT CustomerCode,CustomerName FROM Customers").ToList();


                    }
                    else
                    {
                        return db.Query<Customers>("SELECT CustomerCode,CustomerName FROM Customers WHERE lower(CustomerCode) LIKE @name or lower(CustomerName) LIKE @name", new { name = "" + name.Trim() + "%" }).ToList();
                    }


                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }



    }
}
