using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazvSF
{
    class WorkDescriptionEventArgs : EventArgs
    {
        public string WorkDescription { get; set; }

        public WorkDescriptionEventArgs(string workDescription)
        {
            WorkDescription = workDescription ?? throw new ArgumentNullException(nameof(workDescription));
        }
    }
}
