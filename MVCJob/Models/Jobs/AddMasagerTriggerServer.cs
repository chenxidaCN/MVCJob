﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCJob.Models.Jobs
{
    public class AddMasagerTriggerServer
    {
        public ITrigger AddMasagerTrigger()
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity("添加消息到日志", "作业触发器")
                .WithSimpleSchedule(x => x
                    //.WithIntervalInSeconds(5)
                    // .WithIntervalInHours(5)
                    .WithIntervalInMinutes(5) //每五分钟执行一次
                    .RepeatForever())
                .Build();
            return trigger;
        }
    }
}