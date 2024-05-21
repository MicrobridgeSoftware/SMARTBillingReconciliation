using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMARTBillingReconciliation.ApplicationUtil
{
    public class ExceptionListDefinition
    {
        public string fileName { get; set; }
        public string region { get; set; }
        public string ticketNo { get; set; }
        public DateTime transactionDate { get; set; }
        public decimal transactionValue { get; set; }
    }
}