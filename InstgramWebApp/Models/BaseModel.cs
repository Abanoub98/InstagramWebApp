using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class BaseModel
    {
        [Required]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); //generate unique id
    }
}