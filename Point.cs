//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPOpenHouseAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Point
    {
        public int ID { get; set; }
        public string userIDFK { get; set; }
        public int points { get; set; }
    
        public virtual User User { get; set; }
    }
}
