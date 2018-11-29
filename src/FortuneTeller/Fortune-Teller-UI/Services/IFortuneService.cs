using System.Threading.Tasks;

namespace Fortune_Teller_UI.Services
{
    public interface IFortuneService
    {
        Task<Fortune> RandomFortuneAsync();

        Task SetFortuneInCacheAsync();

        Task<Fortune> GetFortuneInCacheAsync();
    }
}
