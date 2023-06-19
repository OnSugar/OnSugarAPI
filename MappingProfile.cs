using System;
using AutoMapper;
using OnSugarAPI.Models;
using OnSugarAPI.ViewModels;

namespace OnSugarAPI
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<BloodSugarViewModel, BloodSugarModel>().ReverseMap();
		}
	}
}

