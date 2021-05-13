using System;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Sampan.Common.Util;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Infrastructure.DistributedCache;
using Sampan.Service.Contract;

namespace Sampan.Application
{
    public abstract class Service : IService
    {
        public IServiceProvider ServiceProvider { get; set; }

        protected readonly object ServiceProviderLock = new object();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef) ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        private ICurrentUser _currentUser;
        public ICurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);

        private IMapper _mapper;
        public IMapper Mapper => LazyGetRequiredService(ref _mapper);

        private UnitOfWorkManager unitOfWorkManager;
        public UnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref unitOfWorkManager);

        private ILogger _logger;
        public ILogger Logger => LazyGetRequiredService(ref _logger);

        public IAuthorizationService AuthorizationService => LazyGetRequiredService(ref _authorizationService);
        private IAuthorizationService _authorizationService;

        private IDistributedCache _cache;
        public IDistributedCache Cache => LazyGetRequiredService(ref _cache);

        protected void ThrowIf(bool condition, BusinessException exception)
        {
            BusinessExceptionUtil.ThrowIf(condition, exception);
        }
    }
}