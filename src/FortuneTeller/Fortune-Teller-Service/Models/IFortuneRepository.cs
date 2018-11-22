using System.Collections.Generic;

namespace Fortune_Teller_Service.Models
{
    public interface IFortuneRepository
    {
        IEnumerable<Fortune> GetAll();

        Fortune RandomFortune();
    }
}
