using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCJob.Models.Jobs
{
    public class AddMassagejob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            var reportDirectory = string.Format("~/text/{0}/", DateTime.Now.ToString("yyyy-MM-ssss"));
            reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            var dailyReportFullPath = string.Format("{0}text_{1}.log", reportDirectory, DateTime.Now.Day);
            var logContent = string.Format("{0}-{1}-{2}", DateTime.Now, "滴 滴滴", Environment.NewLine);
            if (logContent == null)
            {
                JobExecutionException jobex = new JobExecutionException("写入失败");

            }
            File.AppendAllText(dailyReportFullPath, logContent);

        }
    }
}