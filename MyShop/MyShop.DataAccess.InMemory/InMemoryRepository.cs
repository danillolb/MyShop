using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        public string Id;
        public string className;
        public List<T> items;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if(items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }
        
        public void Insert(T t)
        {
            items.Add(t);
        }

        public T Find(string Id)
        {
            T itemToFind = items.Find(i => i.Id == Id);
            if(itemToFind != null)
            {
                return itemToFind;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
        
        public  IQueryable<T> Collection(){
            return items.AsQueryable<T>();
        }

        public void Delete(string Id)
        {
            T itemToDelete = items.Find(i => i.Id == Id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
    }
}
