using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatweer.Core.Events
{
    public abstract class DomainEvent
    {
        public DateTime PublishDate { get; private set; }

        public DomainEvent()
        {
            PublishDate = DateTime.Now;
        }
    }
}
