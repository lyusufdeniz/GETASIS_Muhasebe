using GETASIS_Muhasebe.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GETASIS_Muhasebe.BL
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        public Context context;
        private DbSet<T> _objectSet;
        public Repository()
        {
            if (context == null)
            {
                context = new Context();
                _objectSet = context.Set<T>();
            }

        }
        public int Add(T entity)
        {
            _objectSet.Add(entity);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id >= 0)
            {
                _objectSet.Remove(GetT(id));
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    return -1;
                }

            }
            return context.SaveChanges();


        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefault(expression);

        }



        public List<T> GetAll()
        {
            return _objectSet.ToList();
        }


        public T GetT(int id)
        {
            return _objectSet.Find(id);
        }

        public int Update(T entity)
        {
            _objectSet.Update(entity);
            return context.SaveChanges();
        }

        List<T> IRepository<T>.GetAll(Expression<Func<T, bool>> expression)
        {
            return _objectSet.Where(expression).ToList();
        }
    }
}
