using MVCJob.Models.Jobs;
using Quartz.Simpl;
using Quartz.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCJob.Models
{
    public class JobManager
    {
        public static void State()
        {
            //开启调度
            JobBase.Scheduler.Start();
            XMLSchedulingDataProcessor processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
            processor.ProcessFileAndScheduleJobs("~/Jobs.xml", JobBase.Scheduler);
            // 第一个参数是你要执行的工作(job)  第二个参数是这个工作所对应的触发器(Trigger)(例如:几秒或几分钟执行一次)
            //JobBase.AddSchedule(new JobServer<AddMassagejob>(),
            //    new AddMasagerTriggerServer().AddMasagerTrigger(), "每隔五分钟向文本中写入文字", "消息工作");

            
        }
    }
}