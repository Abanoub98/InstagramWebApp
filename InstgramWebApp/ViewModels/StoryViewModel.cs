using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class StoryViewModel
    {
        public Story Story { get; set; } = new Story();

        public HttpPostedFileBase[] ImgFiles { get; set; }
    }
}