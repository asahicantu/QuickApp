// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ISvcRepository Svcs { get; }
        IProductRepository Products { get; }
        IOrdersRepository Orders { get; }
        DbSet<ApplicationUser> Users {get; }
        //IApplicationUserRepository Users { get; }

        int SaveChanges();
    }
}
