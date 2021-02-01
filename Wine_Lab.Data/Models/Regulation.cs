using System;
using System.ComponentModel.DataAnnotations;

namespace Wine_Lab.Data.Models
{
    public class Regulation
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
