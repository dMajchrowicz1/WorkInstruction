using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Queries
{
    public class ToolDto
    {
        private string type;
        private string machine;
        private string offset;

        public int ItemId { get; set; }

        public string Type { get => type ?? (type = string.Empty); set => type = value; }

        public string Position { get; set; }

        public string Machine { get => machine ?? (machine = string.Empty); set => machine = value; }

        public string Offset { get => offset ?? (offset = string.Empty); set => offset = value; }

        public string CuttingPart { get; set; }

        public string Name { get; set; }

        public string PDF { get; set; }

        public bool IsSelected { get; set; }


        public string FullName
        {
            get { return $"T{Position}{Offset}"; }
        }


        public ToolPartsDto Parts { get; set; }


        public ToolDimensionsDto Dimensions { get; set; }


        public override string ToString()
        {
            return FullName;
        }
    }
}
