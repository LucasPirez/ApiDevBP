using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;

namespace ApiDevBP.Mappers
{
    public class AutoMapping:Profile
    {

        public AutoMapping()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}
