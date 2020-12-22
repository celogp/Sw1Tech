using Sw1Tech.Infra.Context.Interfaces.EF;
using Sw1Tech.Infra.Repository.EF.Context;
using System;

namespace Sw1Tech.Infra.Context.EF
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Sw1TechContext _context;

        public UnitOfWork(Sw1TechContext context)
        {
            _context = context;
        }
        public void DoSavePoint()
        {
            _context.SaveChanges();
        }
        public void DoCommit()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.SaveChanges();
                _context.Database.CurrentTransaction.Commit();
            }
        }

        public void DoRollback()
        {
            _context.Database.CurrentTransaction.Rollback();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public Guid DoBeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.BeginTransaction();
            }
            return _context.Database.CurrentTransaction.TransactionId;
        }
    }
}