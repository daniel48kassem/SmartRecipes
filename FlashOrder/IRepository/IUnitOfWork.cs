using System;
using System.Threading.Tasks;
using FlashOrder.Data;

namespace FlashOrder.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Recipe> Recipes { get; }
        IGenericRepository<Item> Items { get; }
        Task save();
    }
}