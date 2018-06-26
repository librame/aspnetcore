#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Quartz;
using System.Threading.Tasks;

namespace LibrameStandard.Extensions.Platform
{
    using Schedules;
    using Utilities;

    /// <summary>
    /// 环境监视工作。
    /// </summary>
    public class EnvironmentMonitoringJob : AbstractJob<EnvironmentMonitoringJob>
    {
        /// <summary>
        /// 构造一个 <see cref="EnvironmentMonitoringJob"/> 实例。
        /// </summary>
        /// <param name="environment">给定的 <see cref="IEnvironmentPlatform"/>。</param>
        public EnvironmentMonitoringJob(IEnvironmentPlatform environment)
        {
            Environment = environment.NotNull(nameof(environment));
        }


        /// <summary>
        /// 环境平台。
        /// </summary>
        public IEnvironmentPlatform Environment { get; }


        /// <summary>
        /// 执行工作。
        /// </summary>
        /// <param name="context">给定的 <see cref="IJobExecutionContext"/>。</param>
        /// <returns>返回一个异步任务。</returns>
        public override Task Execute(IJobExecutionContext context)
        {
            var path = @"wwwroot\logs\environment.txt".AppendBasePath();
            var json = Environment.EnvironInfo.AsJsonString();

            // 将当前区块链写入文本文件
            json.WriteContent(path);

            return Task.CompletedTask;
        }

    }
}
