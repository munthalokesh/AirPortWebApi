//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirPortWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int BookingId { get; set; }
        public string HangerId { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    
        public virtual HangerDetail HangerDetail { get; set; }
    }
}
