using Powerfront.Database;
using Powerfront.Models;
using Powerfront.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Powerfront.Controllers
{
    public class HomeController : Controller
    {   
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }

            public ActionResult OperatorReport()
            {
            ViewBag.Message = "Operator Productivity Report";
            OperatorReportRepository operatorReportRepository = new OperatorReportRepository();          
            
            return View(operatorReportRepository.GetOperatorsReport());
        }

            public ActionResult OperatorProductivityData()
            {
                return View();
            }
        }
    }