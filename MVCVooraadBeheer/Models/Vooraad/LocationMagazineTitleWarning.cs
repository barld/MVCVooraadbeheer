//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCVooraadBeheer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationMagazineTitleWarning
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> ActiveTo { get; set; }
        public string value { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual MagazineTitle MagazineTitle { get; set; }
    }
}
