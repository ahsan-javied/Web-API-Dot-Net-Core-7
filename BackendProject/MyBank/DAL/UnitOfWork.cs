using DAL.DBCore;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRegisterUserRepository RegisterUserRepository { get; }
        public void Save();
    }
        
    public class UnitOfWork : IUnitOfWork
    {
        private CoreDBContext _dbContext;
        public UnitOfWork(CoreDBContext context)
        {
            _dbContext = context;
        }

        private IRegisterUserRepository _IRegisterUserRepository;
        private IUserRepository _IUserRepository;

        public IRegisterUserRepository RegisterUserRepository
        {
            get
            {
                if (this._IRegisterUserRepository == null)
                {
                    this._IRegisterUserRepository = new RegisterUserRepository(_dbContext);
                }
                return _IRegisterUserRepository;
            }
        }
        
        public IUserRepository UserRepository
        {
            get
            {
                if (this._IUserRepository == null)
                {
                    this._IUserRepository = new UserRepository(_dbContext);
                }
                return _IUserRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
