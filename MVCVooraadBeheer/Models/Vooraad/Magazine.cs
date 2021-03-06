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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Magazine
    {
        public Magazine()
        {
            //zorgt dat er niet een standaart waarde van 1 januarie het jaar null komt te staan
            Verschijning = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Nullable<short> Jaargang { get; set; }
        public Nullable<short> nummer { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime Verschijning { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> UitSchappen { get; set; }
        public int BarCode { get; set; }
        public int Price { get; set; }

        [ForeignKey("MagazineTitle")]
        public int MagazineTitleId { get; set; }

        public virtual MagazineTitle MagazineTitle { get; set; }
        public virtual ICollection<MagazineTransaction> MagazineTransaction { get; set; }
    }
}
