//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MilesPerGallon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class MPGTracker
    {
        public int ID { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Display(Name = "Miles Driven")]
        public double MilesDriven { get; set; }

        [Display(Name = "Gallons Filled")]
        public int GallonsFilled { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime FillUpDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> StartDateUseOfCar { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> EndDateUseOfCar { get; set; }

        [Display(Name = "Miles Per Gallon")]
        public double MilesPerGallon
        {
            get
            {
                double mpg = (MilesDriven / GallonsFilled);
                return mpg;
            }
        }
    }
}
