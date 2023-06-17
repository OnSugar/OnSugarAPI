using System;
using System.ComponentModel.DataAnnotations;

namespace OnSugarAPI.ViewModels
{
	public class BloodSugarViewModel
	{
        public float Value { get; set; }

        public DateTime Date { get; set; }
    }
}

