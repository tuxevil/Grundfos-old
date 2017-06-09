using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerNet.Domain
{
    public class Identifier
    {
        protected int id;
        private int creationUser;
        private DateTime creationDate;
        private int modificationUser;
        private DateTime modificationDate;

        public virtual int CreationUser
        {
            get { return creationUser; }
            set { creationUser = value; }
        }

        public virtual DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
 
        }

        public virtual int ModificationUser
        {
            get { return modificationUser; }
            set { modificationUser = value; }
 
        }

        public virtual DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
