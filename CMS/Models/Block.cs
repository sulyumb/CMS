using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Block
    {
         
         
        [Display(Name = "Block Sub")]
        [Key]
        public int ID { get; set; }
        
        public string Name {  get; set; }
        
        public virtual ICollection<Session> Sessinos { get; set; }
    }
}