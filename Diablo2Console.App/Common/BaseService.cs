using Diablo2Console.App.Abstract;
using Diablo2Console.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace Diablo2Console.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public int GetNextId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(x => x.Id).LastOrDefault().Id + 1;
            }
            else
            {
                lastId = 1;
            }
            return lastId;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }
        public int CreateItem(T item)
        {
            Items.Add(item);

            return item.Id;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(x => x.Id == item.Id);
            if(entity != null)
            {
                entity = item;
            }

            return entity.Id;
        }
    }
}
