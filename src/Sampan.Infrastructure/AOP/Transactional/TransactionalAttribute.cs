using System;
using System.Data;
using FreeSql;

namespace Sampan.Infrastructure.AOP.Transactional
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;
    }
}