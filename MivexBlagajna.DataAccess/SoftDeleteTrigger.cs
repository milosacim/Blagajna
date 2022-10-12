using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data;

namespace MivexBlagajna.DataAccess
{
    public class SoftDeleteTrigger : IBeforeSaveTrigger<ISoftDeletable>
    {
        private readonly MivexBlagajnaDbContext _context;
        public SoftDeleteTrigger(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public async Task BeforeSave(ITriggerContext<ISoftDeletable> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                var entry = _context.Entry(context.Entity);
                context.Entity.Obrisano = true;
                entry.State = EntityState.Modified;
            }

            await Task.CompletedTask;
        }
    }
}
