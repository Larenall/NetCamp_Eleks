using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDataService
    {
        DbSet<UserSubscription> UserSubscriptions { get; set; }

        void SaveChanges();
    }
}
