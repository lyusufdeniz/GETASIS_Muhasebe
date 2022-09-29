using GETASIS_Muhasebe.Entities;
using Microsoft.EntityFrameworkCore;

namespace GETASIS_Muhasebe.BL
{
    public class HesapManager : Repository<Hesap>
    {
        public List<Hesap> GetAllData()
        {
            return context.Hesap.Include(x => x.Doviz).ToList();

        }
    }
}
