using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        DbSet<UserSubscription> UserSubscriptions { get; set; }

        void SaveChanges();
    }
}
