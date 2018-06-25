#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameStandard.Extensions.Platform
{
    using Utilities;

    /// <summary>
    /// 计划表中间件。
    /// </summary>
    public class ScheduleMiddleware
    {
        /// <summary>
        /// 构造一个计划表中间件实例。
        /// </summary>
        /// <param name="next">给定的下一步请求委托。</param>
        public ScheduleMiddleware(RequestDelegate next)
        {
            Next = next.NotNull(nameof(next));
        }


        /// <summary>
        /// 下一步请求委托。
        /// </summary>
        protected RequestDelegate Next { get; private set; }


        /// <summary>
        /// 异步执行。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        public Task Invoke(HttpContext context)
        {
            var schedule = context.RequestServices.GetRequiredService<ISchedulePlatform>();
            //schedule.WatchingAssemblies = true;
            schedule.StartJobs().Wait();

            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals("/Schedules", StringComparison.Ordinal))
            {
                return Next.Invoke(context);
            }
            
            var allJobs = new List<IJobDetail>();

            var jobGroups = schedule.Scheduler.GetJobGroupNames().Result;

            foreach (string str in jobGroups)
            {
                var jobKeys = schedule.Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(str)).Result;

                foreach (var job in jobKeys)
                {
                    var jobDetail = schedule.Scheduler.GetJobDetail(job).Result;

                    if (jobDetail != null)
                    {
                        allJobs.Add(jobDetail);
                    }
                }
            }

            var json = allJobs.Select(jd => new
            {
                type_name = jd.JobType.AsAssemblyQualifiedNameWithoutVCP(),
                key = jd.Key.ToString(),
                description = jd.Description
            });

            // 输出当前所有工作
            return context.Response.WriteJsonAsync(json);
        }

    }
}
