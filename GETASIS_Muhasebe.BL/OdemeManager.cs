using GETASIS_Muhasebe.Entities;
using Microsoft.EntityFrameworkCore;

namespace GETASIS_Muhasebe.BL
{
    public class OdemeManager : Repository<Odeme>
    {
        public List<Odeme> GetAllData()
        {
            return context.Odeme.Include(x => x.Hesap).Include(x => x.Cari).Include(x => x.OdemeTip).Include(x => x.Hesap.Doviz).ToList().FindAll(x => x.Silindi != 1);
        }
        public int InsteadDelete(int id)
        {
            if (id >= 0)
            {
                GetT(id).Silindi = 1;
                context.Update(GetT(id));

            }
            return context.SaveChanges();
        }
    }
}
