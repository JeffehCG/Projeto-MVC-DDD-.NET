using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Classe para fazer o mapeamento das classes (Projeto Domain e MVC)
namespace ProjetoModeloDDD.MVC.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}