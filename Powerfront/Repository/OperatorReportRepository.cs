using Powerfront.Database;
using Powerfront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Repository
{
    public class OperatorReportRepository 
    {

        public OperatorReportItems GetOperatorsReport()
        {
            OperatorReportItems ProductivityReport = new OperatorReportItems();
            ProductivityReport.OperatorProductivity = new List<OperatorReportViewModel>();
            ProductivityReport.Visitors = new List<Visitor>();
            ProductivityReport.WebSite = new List<string>();
            ProductivityReport.DateModels = new List<DateModel>();

            try
            {

                //SqlConnection conn = new SqlConnection("Data Source=FAISALHABIB\\SQLEXPRESS;Initial Catalog=chat;User id=chat;Password=chat;");
                //SqlCommand sqlcomm = new SqlCommand("exec dbo. ", conn);

                using (var db = new chatEntities())
                {
                    GetProductivityReport(ProductivityReport, db);
                    GetVisitors(ProductivityReport, db);
                    GetWebsites(ProductivityReport, db);
                    GetDates(ProductivityReport);
                    


                }
            }
            catch (Exception e)
            {
                //some message on window
            }
            return ProductivityReport;
        }

        private void GetDates(OperatorReportItems productivityReport)
        {
            var tw=DateRangeRepository.ThisWeek(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "this week", startDate = tw.Start, endDate = tw.End });
            var lastweek = DateRangeRepository.LastWeek(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "last week", startDate = lastweek.Start, endDate = lastweek.End });
            var thisMonth = DateRangeRepository.ThisMonth(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "this month", startDate = thisMonth.Start, endDate = thisMonth.End });
            var lastMonth = DateRangeRepository.LastMonth(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "last month", startDate = lastMonth.Start, endDate = lastMonth.End });
            var thisYear = DateRangeRepository.ThisYear(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "this year", startDate = thisYear.Start, endDate = thisYear.End });
            var lastYear = DateRangeRepository.LastYear(DateTime.Now);
            productivityReport.DateModels.Add(new DateModel() { name = "last year", startDate = lastYear.Start, endDate = lastYear.End });
            

        }
       

        private void GetWebsites(OperatorReportItems productivityReport, chatEntities db)
        {
            productivityReport.WebSite = db.Conversation.Select(x => x.Website).Distinct().ToList();
        }

        private void GetVisitors(OperatorReportItems productivityReport, chatEntities db)
        {
            var visitors = db.Visitor.ToList();
            productivityReport.Visitors = visitors; 
        }

        private static void GetProductivityReport(OperatorReportItems ProductivityReport, chatEntities db)
        {
            var reports = db.OperatorProductivity(website: null, visitorID: null, startDate: null, endDate: null).ToList();

            //   var starters = db.Messages.GroupBy(x => x.ConversationID);

            // SqlDataReader dr = sqlcomm.ExecuteReader();
            //  if (dr.Read())
            foreach (var dr in reports)
            {
                OperatorReportViewModel opVM = new Models.OperatorReportViewModel();
                opVM.ID = Convert.ToInt32(dr.OperatorID);
                opVM.Name = Convert.ToString(dr.Name);
                opVM.ProactiveAnswered = Convert.ToInt32(dr.ProactiveAnswered);
                opVM.ProactiveSent = Convert.ToInt32(dr.ProactiveSent);
                opVM.ProactiveResponseRate = Convert.ToInt32(dr.ProactiveResponseRate);
                opVM.ReactiveAnswered = Convert.ToInt32(dr.ReactiveAnswered);
                opVM.ReactiveReceived = Convert.ToInt32(dr.ReactiveReceived);
                opVM.ReactiveResponseRate = Convert.ToInt32(dr.ReactiveResponseRate);
                opVM.AverageChatLength = Convert.ToString(dr.AverageChatLength)+"mm";
              //  opVM.TotalChatLength = Convert.ToString(dr.TotalChatLength)+"mm";
                TimeSpan t = TimeSpan.FromMinutes(Convert.ToDouble(dr.TotalChatLength));

                string answer = string.Format("{0:D2}d:{1:D2}h:{2:D2}m",
                                t.Days,
                                t.Hours,
                                t.Minutes                               );
                opVM.TotalChatLength = answer;
                ProductivityReport.OperatorProductivity.Add(opVM);
            }
        }
    }
}