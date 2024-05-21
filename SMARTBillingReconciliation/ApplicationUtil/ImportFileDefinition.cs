using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMARTBillingReconciliation.ApplicationUtil
{
    public class ImportFileDefinition
    {
        public string ticketNumber { get; set; }
        public DateTime transactionDate { get; set; }
        public decimal transactionAmount { get; set; }
    }
}
