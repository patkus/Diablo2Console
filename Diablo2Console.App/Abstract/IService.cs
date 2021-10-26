using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.App.Abstract
{
    public interface IService<T>
    {
        public List<T> Items { get; set; }

        List<T> GetAllItems();
        int CreateItem(T item);
        int UpdateItem(T item);
        void RemoveItem(T item);
    }
}
