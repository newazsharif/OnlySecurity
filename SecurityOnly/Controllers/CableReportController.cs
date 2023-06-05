using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using PCOHRApp.DA;
using PCOHRApp.Models;

namespace PCOHRApp.Controllers
{
    public class CableReportController : Controller
    {
        private ReportDA _da;
       
        private ReportDataSource rd;
        public CableReportController()
        {
            _da = new ReportDA();
        }
        // GET: CableReport
        [CustomSessionFilterAttribute]
        public ActionResult AllDishCustomerDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllInternetCustomerDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllDishCollectionDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllInternetCollectionDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishPaymentHistory()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetPaymentHistory()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetBillDue()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishBillDue()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult HostList()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult MonthWiseDishBill()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult MonthWiseInternetBill()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollectionPageWise()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetCollectionPageWise()
        {
            return View();
        }
        //------------- Report 

       [CustomSessionFilterAttribute]
        public ActionResult IntCollPageBetween()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult IntCollPageGroup()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollPageBetween()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollPageGroup()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult IntBillCard()
        {
            return View();
        }
         [CustomSessionFilterAttribute]
        public ActionResult DishBillCard()
        {
            return View();
        }
        
        public void ReportView(string reportType, int? cid,int? receivedBy, int? collectedBy, bool? isActive, int? zoneId, string fromDate = "", string toDate = "",string ctype = "",int pageNo = 0)
        {
            if (reportType == "AllDishCustomerDetails" || reportType == "AllInternetCustomerDetails")
            {
                Session["reportType"] = reportType;
                Session["isActive"] = isActive;
                Session["zoneId"] = zoneId;
            }
            else if (reportType == "DishBillDetails" || reportType == "InternetBillDetails")
            {
                Session["reportType"] = reportType;
                Session["fromDate"] = fromDate;
                Session["toDate"] = toDate;
                Session["cid"] = cid;
                Session["receivedBy"] = receivedBy;
                Session["collectedBy"] = collectedBy;
                Session["ctype"] = ctype;
            }
            else if (reportType == "DishBillDue" || reportType == "InternetBillDue")
            {
                Session["reportType"] = reportType;
                Session["zoneId"] = zoneId;
            }
            else if (reportType == "DishPaymentHistory" || reportType == "InternetPaymentHistory")
            {
                Session["reportType"] = reportType;
                Session["cid"] = cid;
            }
            
        }
        public ActionResult ReportViewer()
        {
            return View();
        }

        public ActionResult ShowReport(string fileType, string reportType, int? cid, int? receivedBy, int? collectedBy, bool? isActive, int? zoneId, int? custSerialPrefixId, int? assignedUserId, int? hostId, int? dueReportStatus, int? activeStatus, string fromDate = "", string toDate = "", string ctype = "", string month = "", int year = 0, int pageNo = 0, int topageNo = 0, string yearName = "", string receivedByString="")
        {
            LocalReport lr = new LocalReport();
            if (reportType == "AllDishCustomerDetails")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "AllDishCustomerDetails.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("isActive", isActive == null ? null : isActive.ToString()));
                reportParameters.Add(new ReportParameter("zoneId",zoneId + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCustomerList(isActive, zoneId?? 0, 0, custSerialPrefixId ?? 0, assignedUserId ?? 0, hostId ?? 0) ?? new List<CustomerVM>();
                rd = new ReportDataSource("CustomerDataSet", dataList);
                lr.DataSources.Add(rd);

            }

            string rptType = fileType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + fileType + "</OutputFormat>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                rptType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);

        }
    }
}