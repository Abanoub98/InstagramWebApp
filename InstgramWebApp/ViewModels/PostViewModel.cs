using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; } = new Post();
        [StringLength(255)]
        [DisplayName("Write a caption..")]
        public string Caption { get; set; }
        public HttpPostedFileBase[] ImgFiles { get; set; }
    }
}