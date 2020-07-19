using core.Interface;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Unitofwork
{
    public class Unitofwork<T> : IUnitofwork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;
        public Unitofwork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
