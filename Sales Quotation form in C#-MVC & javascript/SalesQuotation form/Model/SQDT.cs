using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class SQDT
    {
        public Int64 QuoteID { get; set; }

        public String CompCode { get; set; }

        public String CostCentreCode { get; set; }

        public Int64 DocNum { get; set; }

        public String CustomerCode { get; set; }

        public String CustomerName { get; set; }

        public DateTime DocDate { get; set; }

        public String RefNo { get; set; }

        public String ItemNo { get; set; }

        public String ItemDescription { get; set; }

        public Single Quantity { get; set; }

        public Single PackSize { get; set; }

        public Decimal UnitCost { get; set; }

        public Decimal Discount { get; set; }

        public Decimal? DiscountAmount { get; set; }

        public String TaxCode { get; set; }

        public Decimal TotalBeforeTax { get; set; }

        public Single TaxSum { get; set; }

        public Decimal LineTotal { get; set; }

        public Int32 LineNumber { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public String WorkStation { get; set; }

        public Decimal AVGCost { get; set; }

        public Decimal SellingPrice { get; set; }

        public Decimal TotalCOS { get; set; }

        public String Pack { get; set; }

        public Single Margin { get; set; }
     
    }
}
