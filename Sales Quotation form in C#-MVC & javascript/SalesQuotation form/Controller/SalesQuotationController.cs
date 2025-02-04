using System;
using System.Collections.Generic;
using System.Linq;
using DAL.BusinessLogic;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Threading.Tasks;
using DAL.ViewModel;
using DAL.Model;
using System.Net;
using System.Collections;
//using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using Angle.Reports;
namespace Angle.Controllers
{
    public class SalesQuotationController : Controller
    {
        SalesQuotationService _SalesQuotationService = new SalesQuotationService();
        UdcService _UdcService = new UdcService();
        // ReceiptService _ReceiptService = new ReceiptService();
        Response _Response = new Response();
        PolicyService _PolicyService = new PolicyService();
        // GET: SalesQuotation
        public ActionResult Index()
        {
            return View();
        }

        //HpGet SalesQuotation Create
        public ActionResult Create()
        {
            try
             {
                string CaptureSalesMan = "";
                string CaptureReference = "";
                List<UDCMst> saleslist = new List<UDCMst>();
                saleslist = _UdcService.GetUDCPerCategory("SalesEmployees");
                // ViewBag.SalesManCode = new SelectList(saleslist, "Code", "Description");             
                ViewBag.DocNum = 0; // _SupplierInvoiceService.GetCSaleDocum();
                ViewBag.CostCenter = "Cost001";
                ViewBag.CompCode = "001";

                ViewBag.UserID = "001";
                CaptureSalesMan = _PolicyService.GetPolicyValue("Disable Capture SalesMan on Sales Order");
                if (CaptureSalesMan == null)
                    CaptureSalesMan = "N";
                if (CaptureSalesMan.ToUpper() == "Y")
                {
                    ViewBag.CaptureSalesMan = true;
                    ViewBag.RefNo = "";
                }
                else
                {
                    ViewBag.CaptureSalesMan = false;
                    ViewBag.RefNo = "Cost001" + "SI" + DateTime.Now.ToString("yyyyMMddHHmmss");
                }
                CaptureReference = _PolicyService.GetPolicyValue("Disable Capture Reference on supplierinvoice");
                if (CaptureReference == null)
                    CaptureReference = "N";
                if (CaptureReference.ToUpper() == "Y")
                {
                    ViewBag.CaptureReference = true;
                }
                else
                {
                    ViewBag.CaptureReference = false;
                }
                SQHD __SQHD = new SQHD();
                __SQHD.Detail = new List<SQDT>();
                //_SupplierInvoiceHD.Receipt = new List<CSReceipts>();
                return View(__SQHD);
            }
            catch (Exception ex)
            {

                Logger.WriteLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("Error500", "Misc");
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create(SQHD model)
        {
            try
            {
                SalesQuotationResponse result = new SalesQuotationResponse();
                if (ModelState.IsValid)
                {
                    result = await Task.FromResult(_SalesQuotationService.SaveSaleQuotation(model));
                    if (result.DocNum > 0)
                    {
                        // _Response = new Response();
                        Session["DocNum"] = result.DocNum;
                        _Response.Code = 200;
                        _Response.Message = "Sales Quotation saved successfully";
                        return Json(new { data = _Response }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        // _Response = new Response();
                        _Response.Code = 500;
                        _Response.Message = result.Message;
                        return Json(new { data = _Response }, JsonRequestBehavior.AllowGet);
                    }
                }
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                //_Response = new Response();
                _Response.Code = 500;
                _Response.Message = string.Format("Error {0} ." + errors);
                return Json(new { data = _Response }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //_Response = new Response();
                Logger.WriteLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                _Response.Code = 500;
                _Response.Message = "Error " + ex.Message;
                return Json(new { data = _Response }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetCustomers(string q)
        {
            try
            {

                List<Customers> custList = new List<Customers>();
                if (q == null)
                {
                    q = "*";
                }
                custList = _SalesQuotationService.GetCusts(q.ToLower());
                var customer = (from c in custList
                              select new { id = c.CustomerCode, text = c.CustomerName });
                return Json(new { items = customer }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                List<Customers> custList = new List<Customers>();
                custList = null;
                var customer = (from c in custList
                                select new { id = c.CustomerCode, text = c.CustomerName });
                return Json(new { items = customer }, JsonRequestBehavior.AllowGet);

            }
        }


    }
}