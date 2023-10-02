using ConveyorDoc.Business.Constants;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConveyorDoc.Business.Model
{
    public class NcProgram 
    {

        public string Machine { get; set; } = string.Empty;

        public string ProgramNumber { get; set; } = string.Empty;

        public string ProgramPath { get; set; } = string.Empty;

        public string ControlType { get; set; } = string.Empty;

        public bool IsSelected { get; set; } = false;

        //Default ctor
        public NcProgram() { }

        public NcProgram(string programLocation)
        {

            ProgramPath = programLocation;

            ProgramNumber = Path.GetFileName(programLocation);

            Machine = Regex.Match(ProgramNumber, RegexPatternsConstants.MACHINE_NAME).Value;

            ControlType = FindControlType(Path.GetExtension(programLocation));
        }


        private string FindControlType(string programExtension)
        {
            if (programExtension.Equals(NcProgramConstants.FANUC_EXTENSIONS, System.StringComparison.CurrentCultureIgnoreCase))
                return "Fanuc";
            else if (programExtension.Equals(NcProgramConstants.SINUMERIK_EXTENSION, System.StringComparison.CurrentCultureIgnoreCase))
                return "Sinumerik";
            else
                throw new System.Exception("Invalid file format");
        }
    }
}
