using ApiClientesNet7.Data;
using ApiClientesNet7.Models;
using ApiClientesNet7.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiClientesNet7.Repository
{
    public class ClientRepository : IClientRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ClientRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ClientDto> CreateUpdate(ClientDto clientDto)
        {
            Client client = _mapper.Map<ClientDto, Client>(clientDto);
            if (client.Id > 0)
            {
                _db.clients.Update(client);
            }
            else
            {
                await _db.clients.AddAsync(client);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<Client, ClientDto>(client);
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                Client client = await _db.clients.FindAsync(id);
                if (client == null)
                {
                    return false;
                }
                _db.clients.Remove(client);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ClientDto> GetClientById(int id)
        {
            Client client = await _db.clients.FindAsync(id);
            return _mapper.Map<ClientDto>(client);

        }

        public async Task<List<ClientDto>> GetClients()
        {
            List<Client> list = await _db.clients.ToListAsync();
            return _mapper.Map<List<ClientDto>>(list);
        }
    }
}
