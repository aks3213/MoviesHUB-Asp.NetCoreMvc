using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHUB.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name for the movie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please details about the movie")]
        public string Details { set; get; }

        [Required(ErrorMessage = "Please enter genre of the movie")]
        public string Genre { set; get; }

        public string path1 { get; set; }
        [NotMapped]
        public IFormFile ScreenShot1 { get; set; }

        public string path2 { get; set; }
        [NotMapped]
        public IFormFile ScreenShot2 { get; set; }

        public string path3 { get; set; }
        [NotMapped]
        public IFormFile ScreenShot3 { get; set; }

        public string path4 { get; set; }
        [NotMapped]
        public IFormFile ScreenShot4 { get; set; }

        public string path { get; set; }
        [NotMapped]
        public IFormFile MovieFile { get; set; }
    }
}
