using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAO
{
    public abstract class DAO<T>
    {
        public abstract T find(int id);

        public abstract T find(string nom);

        public abstract List<T> findAll();

        public abstract T create(T obj);
        
        public abstract T update(T objAupdate, T update);

        public abstract void delete(T obj);
    }

}