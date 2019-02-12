//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int id { get; set; }
        
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<int> productId { get; set; }
        public Nullable<int> productStockId { get; set; }
        public Nullable<int> unitId { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> paidAmount { get; set; }
        public string addedBy { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Unit Unit { get; set; }
    }
}