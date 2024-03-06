using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepo<T> where T: Entity
    {
        //Technically a read as well
        List<T> GetAll();
        //Create
        T CreateEntity(T entity);
        //Read
        T FindById(int id);
        //Update
        void Update(T entity);  
        //Delete
        void DeleteById(int id);
        void CommitChanges();

    }
}
