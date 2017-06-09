using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class AlertTotal
    {
        private int id;
        private int alert;
        private int total;

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual int Alert
        {
            get { return alert; }
            set { alert = value; }
        }

        public virtual int Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
