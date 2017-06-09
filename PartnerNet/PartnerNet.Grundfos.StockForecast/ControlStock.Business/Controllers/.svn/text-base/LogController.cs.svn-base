using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Common;
using PartnerNet.Domain;
using ProjectBase.Data;

namespace PartnerNet.Business
{
    public class LogController : AbstractNHibernateDao<Log, int>
    {
        public LogController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public bool IsExecuting(string processName, ExecutionStatus executionStatus)
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("ProcessName", processName));
            crit.Add(new EqExpression("ExecutionStatus", executionStatus));
            crit.Add(new EqExpression("CreationDate", Config.CurrentDate));
            return crit.UniqueResult<Log>() != null;
        }

        public void Add(string processName, ExecutionStatus executionStatus, string observations)
        {
            Log lg = new Log();

            if (executionStatus == ExecutionStatus.Start)
                lg.CreationDate = Config.CurrentDate;
            else
                lg.CreationDate = DateTime.Now;

            lg.ExecutionStatus = executionStatus;
            lg.Observations = observations;
            lg.ProcessName = processName;
            Save(lg);
        }
        public Log GetLastForecast()
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("ProcessName", "Forecast Processor"));
            crit.AddOrder(new Order("CreationDate", false));
            
            crit.SetMaxResults(1);

            return crit.UniqueResult<Log>();
        }
    }
}
