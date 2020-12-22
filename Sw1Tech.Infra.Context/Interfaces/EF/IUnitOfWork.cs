using System;

namespace Sw1Tech.Infra.Context.Interfaces.EF
{
    public interface IUnitOfWork
    {
        Guid DoBeginTransaction();
        void DoSavePoint();
        void DoCommit();
        void DoRollback();
    }
}
