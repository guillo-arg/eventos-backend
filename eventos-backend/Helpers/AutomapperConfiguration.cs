﻿using AutoMapper;
using eventos_backend.DTOs.Account;
using eventos_backend.DTOs.Persona;
using eventos_backend.Models;
using System.Reflection;

namespace eventos_backend.Helpers
{
    public class AutomapperConfiguration
    {
        private static MapperConfiguration instance;

        private AutomapperConfiguration()
        {

        }


        public static MapperConfiguration Instance()
        {
            if (instance == null)
            {
                instance = new MapperConfiguration(conf => {
                    conf.CreateMap<User, UserDTO>().ReverseMap();
                    conf.CreateMap<Role, RoleDTO>().ReverseMap();
                    conf.CreateMap<Persona, PostPersonaDTO>()
                    .ReverseMap();


                });
            }

            return instance;
        }
    }
}
