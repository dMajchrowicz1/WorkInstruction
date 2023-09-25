using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Queries
{
    public class DescriptionDto
    {
        private string moduleType;
        private string machine;
        private string size;
        private string operationNumber;
        private string text;

        public string ModuleType { get => moduleType ?? (moduleType = string.Empty); set => moduleType = value; }

        public string Machine { get => machine ?? (machine = string.Empty); set => machine = value; }

        public string Size { get => size ?? (size = string.Empty); set => size = value; }

        public string OperationNumber { get => operationNumber ?? (operationNumber= string.Empty); set => operationNumber = value; }

        public string Text { get => text ?? (text = string.Empty); set => text = value; }

        public bool IsSelected { get; set; }    

    }
}
