using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Constants
{
    public class RegexPatternsConstants
    {
        public const string MACHINE_NAME = "(?=[A-Z]).*(?=_)";

        public const string PROGRAM_PREFX = ".+?(?=_[A-Z])";

        public const string TOOL_NUMBER = "T([0-9]{4}?)";

        public const string TOOL_HUB_NUMBER = @"T=""([0-9]{1,3})""";

        public const string HUB_OFFSET_NUMBER = "\\d+";

        public const string NO_TAGS = @"\w+";

        public const string WORD = @"([A-Z])\w+";

        public const string PROGRAM_NO_MACHINE_ENDING = "^[\\w\\W]+?(?=([A-Z]))";

        public const string WORD_VARIABLE = $"[<]*[>]";

        public const string TAGGED_WORD = $"<[a-zA-Z]+>";
    }
}
