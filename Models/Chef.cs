using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Chefs_n_Dishes.Models
{
    public class RestrictedDate : ValidationAttribute
    {
        public override bool IsValid(object date) 
        {
            return (DateTime)date < DateTime.Now;
        }
    }
    public class Chef
    {
        [Key]
        public int ChefId {get; set;}

        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get; set;}
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [RestrictedDate]
        public DateTime Dob {get;set;}

        public List<Dish> Dishes {get; set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}