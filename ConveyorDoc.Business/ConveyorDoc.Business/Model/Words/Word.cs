
using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Extension;
using ConveyorDoc.Business.Extensions;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Business.UseCase;
using ConveyorDoc.Service;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConveyorDoc.Business.Model
{
    public class Word : BindableBase
    {
                     
        private string _name = string.Empty;
        private string _fullName = string.Empty;        
        private string _description = string.Empty;        
        private NcProgram _ncProgram;
        private Instruction _parent;
        private ObservableCollection<Drawing> _drawings;
        private ObservableCollection<Variable> _variables;
        private ObservableCollection<FixtureDto> _fixtures;
        private ObservableCollection<ToolDto> _tools;


        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }


        /// <summary>
        /// CNC Program for which word instruction will be created
        /// </summary>
        public NcProgram NcProgram
        {
            get { return _ncProgram; }
            set { SetProperty(ref _ncProgram, value); }
        }

        /// <summary>
        /// Variables found in word template as tagged node <>
        /// </summary>
        public ObservableCollection<Variable> Variables
        {
            get { return _variables; }
            set { SetProperty(ref _variables, value); }
        }


        /// <summary>
        /// Full word name with extension
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }


        /// <summary>
        /// Word name without extension
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }


       /// <summary>
       /// Tools found in nc program 
       /// All will be added to word instruction
       /// </summary>
        public ObservableCollection<ToolDto> Tools
        {
            get { return _tools; }
            set { SetProperty(ref _tools, value); }
        }

        /// <summary>
        /// List of fixtures which will be added to word instruction
        /// </summary>
        public ObservableCollection<FixtureDto> Fixtures
        {
            get { return _fixtures; }
            set { SetProperty(ref _fixtures, value); }
        }


        /// <summary>
        /// List of drawings templates
        /// </summary>
        public ObservableCollection<Drawing> Drawings
        {
            get { return _drawings; }
            set { SetProperty(ref _drawings, value); }
        }

        
        public Instruction Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }



        /// <summary>
        /// Default constructor
        /// </summary>
        public Word() 
        {
            
        }

        /// <summary>
        /// Create word instruction based on nc program
        /// </summary>
        /// <param name="program">Parent nc program</param>
        public Word(Instruction parent,NcProgram program) 
        {
            _ncProgram = program;

            Parent = parent;

            _fullName = String.Join("_", WordConstants.WORD_PREFIX , Path.GetFileNameWithoutExtension(_ncProgram.ProgramPath)) + WordConstants.WORD_EXTENSION;

            _name = String.Join("_", WordConstants.WORD_PREFIX, Path.GetFileNameWithoutExtension(_ncProgram.ProgramPath));

            _drawings = new ObservableCollection<Drawing>();

            _fixtures = new ObservableCollection<FixtureDto>();

            _tools = new ObservableCollection<ToolDto>();

            _variables = new ObservableCollection<Variable>();

        }

        public void GetDefaultDescription()
        {
            _description = File.ReadAllText(GeneralConstants.DESCRIPTION_TEMPLATE_PATH);
        }

        public void ReadVariables()
        {
            var result = Enumerable.Empty<Variable>();

            using (var openXML = new WordOpenXML())
            {
                _variables.Replace(openXML.FindVariables());
            }

            if (result != null)
            {
                //Finds variable values from other objects
                _variables.GetVariableValues(Parent);
                _variables.GetVariableValues(NcProgram);
            }
        }

        public void SaveInstruction(string path, bool detailedTools)
        {
            string filePath = Path.Combine(path, FullName);

            using (var openXML = new WordOpenXML())
            {
                //Replace varibales in document with values from word template variables
                openXML.ReplaceVariables(Variables);

                //Insert all fixtures to document
                openXML.AddFixtures(Fixtures);

                //Insert all tools to document
                openXML.AddTools(Tools, detailedTools);

                //Insert description
                openXML.AddDescription(Description);


                if (File.Exists(filePath))
                    File.Delete(filePath);

                openXML.Save(filePath);
            }

            if (Drawings.Count > 0)
            {
                foreach (var drawing in Drawings.ToList())
                {
                    drawing.Plot(path);
                }
            }
        }

        public void FindProgramTools(IGetToolQuery queryHandler)
        {
            var result = Enumerable.Empty<ToolDto>();

            switch (NcProgram.ControlType)
            {
                case "Sinumerik":
                    result = GetToolsByList(queryHandler, SinumericToolFinder(), NcProgram.Machine);
                    break;
                case "Fanuc":
                    result = GetToolsByList(queryHandler, FanucToolFinder(), NcProgram.Machine);
                    break;
                default:
                    throw new Exception("Cannot find tools");

            }

            _tools.Replace(result);

        }




        //privates
        private IEnumerable<ToolDto> GetToolsByList(IGetToolQuery queryHandler, IEnumerable<string> offsetList, string machine)
        {
            var result = new List<ToolDto>();

            foreach (var offset in offsetList)
            {
                result.Add(queryHandler.GetTool(offset, machine));
            }


            return result;
        }

        private IEnumerable<string> FanucToolFinder()
        {
            var result = new List<string>();

            if (NcProgram.ProgramPath != null)
            {
                //Read all lines of nc file, searching for tool pattern and return as list
                var foundtools = RegexTool.GetMatches(File.ReadAllLines(NcProgram.ProgramPath), RegexPatternsConstants.TOOL_NUMBER).Distinct().ToList();

                foreach (var tool in foundtools)
                {
                    result.Add(tool.Remove(0, 3));
                }
            }
            else
                throw new Exception($"Invalid location: {NcProgram.ProgramPath}");


            return result;

        }

        private IEnumerable<string> SinumericToolFinder()
        {

            var result = new List<string>();

            if (NcProgram.ProgramPath != null)
            {
                //Read all lines of nc file, searching for tool pattern and return as list
                var toolList = RegexTool.GetMatches(File.ReadAllLines(NcProgram.ProgramPath), RegexPatternsConstants.TOOL_HUB_NUMBER).Distinct().ToList();


                foreach (var tool in toolList)
                {
                    result.Add(Regex.Match(tool, RegexPatternsConstants.HUB_OFFSET_NUMBER).Value.OffsetFormat());
                }
            }
            else
                throw new Exception($"Invalid location: {NcProgram.ProgramPath}");

            return result;

        }
    }
}
