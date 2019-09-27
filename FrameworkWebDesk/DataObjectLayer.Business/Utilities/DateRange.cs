using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class DateRange
    {
        private DateTime? dateBegin;

        private DateTime? dateEnd;

        public DateTime? DateBegin
        {
            get { return dateBegin; }
            set { dateBegin = value; }
        }

        public DateTime? DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
        }

        public DateRange(string dateBegin, string dateEnd)
        {
            if (!string.IsNullOrEmpty(dateBegin))
            {
                this.dateBegin = Convert.ToDateTime(dateBegin);
            }

            if (!string.IsNullOrEmpty(dateEnd))
            {
                this.dateEnd = Convert.ToDateTime(dateEnd);
            }
        }
    }
}
