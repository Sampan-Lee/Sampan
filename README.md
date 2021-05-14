# Sampan
基于RBAC的权限管理后端单体架构应用  
领域驱动设计


# 项目结构
src   
Sampan.Application:应用服务实现，依赖于 Domain 包和 Application.Contracts 包  
Sampan.Application.Contracts:应用服务规约，包含应用服务接口和相关的数据传输对象(DTO) 依赖于 Domain.Shared 包.  
Sampan.Domain:领域层，包含实体,领域服务和领域异常等领域对象 依赖于 Domain.Shared 包.
Sampan.Domain:领域共享层，包含领域常量,领域枚举和其他类型, 不依赖任何包，在其他所有层中使用.
Sampan.Infrastructure:基础设施，为应用提供某种能力，如数据持久化，分布式缓存，短信服务，聚合支付等 依赖Public包  
Sampan.Public:公共层，包含基础类型，接口定义，常量，C#拓展及工具类  
Sampan.Tasks:任务调度，实现定时任务等功能
Sampan.WebApi.Admin:后台管理系统的API接口  
Sampan.WebApi.Front:前台用户的API接口
Sampan.WebExtension:.Net Core Web服务的拓展封装，包括JWT服务，各种依赖注入，过滤器，中间件，及自定义权限认证

test  
对仓储，应用服务或工具类进行测试，暂未使用
