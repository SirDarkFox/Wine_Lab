using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wine_Lab.ViewModels.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Введите название статьи")]
        [StringLength(50, ErrorMessage = "Слишком длинное название статьи")]
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgPath { get; set; }
        public IFormFile Image { get; set; }
    }
}
