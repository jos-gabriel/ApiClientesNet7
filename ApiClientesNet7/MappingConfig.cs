using ApiClientesNet7.Models;
using ApiClientesNet7.Models.Dto;
using AutoMapper;

namespace ApiClientesNet7
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration( config =>
            {
                config.CreateMap<ClientDto, Client>();
                config.CreateMap<Client, ClientDto>();

            });

            return mappingConfig;
        }

    }
}
