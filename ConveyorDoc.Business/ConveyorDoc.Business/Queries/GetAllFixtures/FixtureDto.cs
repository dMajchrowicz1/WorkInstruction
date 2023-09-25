using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Queries
{
    public class FixtureDto
    {
        private string itemNumber;
        private string size;
        private string machine;
        private string itemType;
        private string pDF;
        private string details;

        public string ItemNumber { get => itemNumber ?? (itemNumber = string.Empty); set => itemNumber = value; }

        public string Size { get => size ?? (size = string.Empty); set => size = value; }

        public string Machine { get => machine ?? (machine = string.Empty); set => machine = value; }

        public string ItemType { get => itemType ?? (itemType= string.Empty); set => itemType = value; }

        public string PDF { get => pDF ?? (pDF = string.Empty); set => pDF = value; }

        public string Details { get => details ?? (details = string.Empty); set => details = value; }

        public bool IsSelected { get; set; }

    }
}
