using InsideAutoManagement.Data;
using InsideAutoManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.DAO
{
    /// <summary>
    /// used for crud operations on Configurations
    /// </summary>
    public class ConfigurationsDAO
    {
        private readonly InsideAutoManagementContext _context = null!;

        public ConfigurationsDAO(InsideAutoManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Configuration>> GetConfigurations()
        {
            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                return new List<Configuration>();

            return await _context.Configurations.ToListAsync();
        }

        public async Task<Configuration?> GetConfiguration(Guid id)
        {
            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                return null;

            var configuration = await _context.Configurations.FindAsync(id);            

            return configuration;
        }

        public async Task<Configuration?> GetConfiguration(string name)
        {
            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                return null;

            var configuration = await _context.Configurations.Where(c=>c.Name == name).FirstOrDefaultAsync();

            return configuration;
        }

        public async Task EditConfiguration(Guid id, Configuration configuration)
        {

            if (id != configuration.Id)
                throw new ArgumentException("Invalid configuration");

            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                throw new InvalidOperationException("There are no configurations");

            if(ConfigurationExists(configuration.Name) == false)
                throw new ArgumentException($"The input configuration plate {configuration.Name} doesn't exist");

            try
            {
                _context.Entry(configuration).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
       
        public async Task SaveConfiguration(Configuration configuration)
        {
            try
            {
                if(configuration==null)
                    throw new ArgumentNullException();

                var existingConfigurations = await GetConfiguration(configuration.Name);

                if (existingConfigurations != null)
                {
                    Guid lastId = existingConfigurations.Id;
                    existingConfigurations = configuration;
                    existingConfigurations.Id = lastId;

                    _context.Entry(existingConfigurations).State = EntityState.Modified;
                }
                else
                    _context.Configurations.Add(configuration);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }           
        }
       
        public async Task DeleteConfiguration(Guid id)
        {
            var configuration = await _context.Configurations.FindAsync(id);
            if (configuration == null)
                throw new InvalidOperationException($"there aren't configurations with id = {id}");

            _context.Configurations.Remove(configuration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConfiguration(string name)
        {
            var configuration = await _context.Configurations.Where(cd=> cd.Name == name).FirstAsync();
            if (configuration == null)
                throw new InvalidOperationException($"there aren't configurations with name = {name}");

            _context.Configurations.Remove(configuration);
            await _context.SaveChangesAsync();
        }

        public bool ConfigurationExists(Guid id)
        {
            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                return false;

            return _context.Configurations.Any(e => e.Id == id);
        }

        public bool ConfigurationExists(string name)
        {
            if (_context.Configurations == null || _context.Configurations.Count() == 0)
                return false;

            return _context.Configurations.Any(e => e.Name == name);
        }
    }
}