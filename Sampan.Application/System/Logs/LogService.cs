using FreeSql;
using Sampan.Common.Extension;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Service.Contract.System.Logs;

namespace Sampan.Application.System.Logs
{
    public class LogService : ReadSerivce<Log, LogDto, LogListDto, GetLogListDto>,
        ILogService
    {
        public LogService(IRepository<Log> repository) : base(repository)
        {
        }

        protected override ISelect<Log> CreateFilteredQuery(GetLogListDto input)
        {
            return Repository.Select
                .WhereIf(!input.Application.IsNullOrWhiteSpace(), a => a.Application == input.Application)
                .WhereIf(!input.TraceId.IsNullOrWhiteSpace(), a => a.TraceId == input.TraceId);
        }
    }
}