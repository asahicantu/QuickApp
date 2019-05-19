// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;
        ISvcRepository _svcs;
        


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public DbSet<ApplicationUser> Users
        {
            get
            {
                return _context.Users;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }



        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }



        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }

        public ISvcRepository  Svcs
        {
            get
            {
                if (_svcs == null)
                    _svcs = new SvcRepository(_context);

                return _svcs;
            }
        }

        IApplicationUserRepository IUnitOfWork.Users => throw new NotImplementedException();

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
