//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace csce361project1145.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class location
    {
        public location()
        {
            this.pictures = new HashSet<picture>();
        }
    
        public int locationId { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<double> latitude { get; set; }
    
        public virtual ICollection<picture> pictures { get; set; }
    }
}
