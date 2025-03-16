using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class JobRepository : EntityRepository<Job>
    {
        public JobRepository(DbContext context) : base(context)
        {
        }

        // Špecifická metóda pre získanie naplanovaneho jobu
        public async Task<Job?> GetNaplanovanyJobAsync()
        {
            return await _context.Set<Job>().FirstOrDefaultAsync(j => j.ScheduledTime <= DateTime.Now && j.JobStateId.ToString() == ConstansJobState.NaplanovanyJobId);
        }

        public async Task EndJobAndCreateJob(Job jobsToRun)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {                
                jobsToRun.JobStateId = Guid.Parse(ConstansJobState.SpracovanyJobId);
                jobsToRun.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(jobsToRun);
                var inputParam = JsonConvert.DeserializeObject<ImputParameter>(jobsToRun.InputParameters);
                int minuty = 10;
                if (inputParam != null) minuty = int.Parse(inputParam.Minutes);
                var newJob = new Job
                {
                    Id = Guid.NewGuid(),
                    Name = jobsToRun.Name,
                    JobCodeId = jobsToRun.JobCodeId,
                    JobStateId = Guid.Parse(ConstansJobState.NaplanovanyJobId),
                    ScheduledTime = DateTime.UtcNow.AddMinutes(minuty),
                    InputParameters = jobsToRun.InputParameters,
                    Description = jobsToRun.Description,
                    CreatedAt = DateTime.UtcNow
                };
                await AddAsync(newJob);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Chyba pri ukončovaní jobu a vytváraní nového", ex);
            }

        }
    }
}
