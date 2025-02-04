using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public  class SQHD
    {
        public Int32 SQID { get; set; }

        public String CompCode { get; set; }

        public String CostCentreCode { get; set; }

        public Int64 DocNum { get; set; }

        public String CustomerCode { get; set; }

        public String CustomerName { get; set; }

        public DateTime DocDate { get; set; }

        public String RefNo { get; set; }

        public String SalesManCode { get; set; }

        public Decimal DiscSum { get; set; }

        public Decimal TotalBeforeTax { get; set; }

        public Single TaxSum { get; set; }

        public Decimal LineTotal { get; set; }

        public String INVStatus { get; set; }

        public Decimal AmountPaid { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public String WorkStation { get; set; }

        public String QTStatus { get; set; }

        public Single INVQTY { get; set; }

        public Int64 INVDocNum { get; set; }
        public List<SQDT> Detail { get; set; } = new List<SQDT>();



    }
}
