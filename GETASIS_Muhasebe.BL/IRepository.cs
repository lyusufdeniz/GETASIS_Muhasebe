using System.Linq.Expressions;

namespace GETASIS_Muhasebe.BL
{
    interface IRepository<T> //buradaki T Type anlamına gelir tüm entityleri kapsar
                             //örnek List<T> getAll()==List<Brand> getAll()
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression); //where ile filtrelenebilen kayıtları getirir
        T GetT(int id);
        T Find(Expression<Func<T, bool>> expression);
        int Add(T entity);
        int Update(T entity);
        int Delete(int id);

    }
}

