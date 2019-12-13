using System.Data;

namespace DataAccess.Interfaces
{
    public interface IConfigurationDbRepository
    {
        DataSet GetApplicationSettings(int id);
    }
}