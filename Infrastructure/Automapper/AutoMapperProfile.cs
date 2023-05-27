using AutoMapper;
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public static Mapper InitializeAutoMapperProfile()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                //.ForMember(dest => dest.Images, act => act.Ignore())
                //                    .ForMember(dest => dest.Categories, opt => opt.Ignore())
                //                    .ForMember(dest => dest.Subcategories, opt => opt.Ignore());

                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
