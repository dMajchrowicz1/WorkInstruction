using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Constants
{
    public static class GeneralConstants
    {
        public const string APP_NAME = "Work Instruction";

        public const string CONFIG_NAME = "Config.json";

        public const string TEMP_FOLDER = "TempFiles";

        public const string OPEN_XML_FOLDER = "OpenXML";

        public const string DATE_TIME_FORMAT = "MM.dd.yyyy HH:mm:ss";


        public const string CONFIG_DIALOG_FILTER = "Config files (*.json)|*.json";

        public const string ALL_DIALOG_FILTER = "All files (*.*)|*.*|All files (*.*)|*.*";


        public const string WORD_TEMPLATE_NAME = "Template.dotx";

        public static string USER_DIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), APP_NAME);

        public static string TEMP_FOLDER_DIR = Path.Combine(USER_DIR, TEMP_FOLDER);

        public static string CONFIG_FILE_DIR = Path.Combine(USER_DIR, CONFIG_NAME);

        public static string OPEN_XML_FILES_DIR = Path.Combine(USER_DIR, OPEN_XML_FOLDER);


        public static string DETAILED_TOOL_HEADER_PATH = Path.Combine(OPEN_XML_FILES_DIR, "DetailedToolHeaderTableRow.xml");

        public static string STANDARD_TOOL_HEADER_PATH = Path.Combine(OPEN_XML_FILES_DIR, "StandardToolHeaderTableRow.xml");

        public static string FIXTURE_HEADER_TEMPLATE_PATH = Path.Combine(OPEN_XML_FILES_DIR, "FixtureHeaderTableRow.xml");

        public static string WORD_TEMPLATE_PATH = Path.Combine(OPEN_XML_FILES_DIR, "Template.dotx");

        public static string DESCRIPTION_TEMPLATE_PATH = Path.Combine(OPEN_XML_FILES_DIR, "DescriptionTemplate.txt");
    }
}
