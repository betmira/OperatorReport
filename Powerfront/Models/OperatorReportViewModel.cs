using Powerfront.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Models
{
    public class OperatorReportViewModel 
    {
        public int ID { get; set; }
        public string Name { get; set;}
        public int ProactiveSent { get; set; }
        public int ProactiveAnswered { get; set; }
        public int ProactiveResponseRate { get; set; }
        public int ReactiveReceived { get; set; }
        public int ReactiveAnswered { get; set; }
        public int ReactiveResponseRate { get; set; }
        public string TotalChatLength { get; set; }
        public string AverageChatLength { get; set; }
    }

    public class OperatorReportItems
    {
        public List<OperatorReportViewModel> OperatorProductivity { get { return _operatorReportViewModel; } set { _operatorReportViewModel = value; } }
            
        private List<OperatorReportViewModel> _operatorReportViewModel;

        public IEnumerator<OperatorReportViewModel> GetEnumerator()
        {
            return _operatorReportViewModel.GetEnumerator();
        }

        public List<Visitor> Visitors { get; set; }
        public List<String> WebSite { get; set; }
        public List<DateModel> DateModels { get; set; }

    }
}