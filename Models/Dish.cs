using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefs_n_Dishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public int Calories {get; set;}

        [Required]
        public int Tastiness {get;set;}

        [Required]
        public string Description {get;set;}

        public int ChefId {get;set;}

        public Chef Chef {get; set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    } 
}