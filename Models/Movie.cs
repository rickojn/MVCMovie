using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMovie.Models
{
    public class ClassicMovieAttribute : ValidationAttribute
    {
        private int _year;

        public ClassicMovieAttribute(int year)
        {
            _year = year;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            Movie movie = (Movie)validationContext.ObjectInstance;

            if (movie.Genre == "Classic" && movie.ReleaseDate.Year > _year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Classic movies must have a release year earlier than {_year}.";
        }

    }
        public class Movie
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        [ClassicMovie(year:1960)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }


    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }

}