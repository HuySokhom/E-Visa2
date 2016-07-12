using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class ImageView
    {

        public int Id { get; set; }

        [AllowHtml]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        public int IsActive { get; set; }
        
        [Display(Name="CreatedDate")]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

    }



    public class ImageViewModel : ImageView
    {
        [Display(Name = "Image")]
        [Required]
        public HttpPostedFileBase Photo { get; set; }

    }

    public class ImageEditView : ImageView{
        [Display(Name = "Image")]
        public HttpPostedFileBase Photo { get; set; }


    }
}