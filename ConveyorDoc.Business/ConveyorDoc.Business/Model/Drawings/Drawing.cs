using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Extension;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConveyorDoc.Business.Model
{
	public class Drawing : BindableBase
    {

        #region Fields
        private string _plotName;
        private string _copyName;
        private string _templatePath;
        private string _templateTxt;
        private List<Variable> _variables;
        #endregion

        #region Properties
        /// <summary>
        /// DXF source text read from template
        /// </summary>
        public string TemplateTxt
        {
            get { return _templateTxt; }
            set { SetProperty(ref _templateTxt, value); }
        }


        /// <summary>
        /// List of variables found in template
        /// </summary>
        public List<Variable> Variables
        {
            get { return _variables; }
            set { SetProperty(ref _variables, value); }
        }

        /// <summary>
        /// Template path
        /// </summary>
        public string TemplatePath
        {
            get { return _templatePath; }
            set { SetProperty(ref _templatePath, value); }
        }


        /// <summary>
        /// DXF copy name
        /// </summary>
        public string DxfName
        {
            get { return _copyName; }
            set { SetProperty(ref _copyName, value); }
        }


        /// <summary>
        /// Plot name (PDF)
        /// </summary>
        public string PlotName
        {
            get { return _plotName; }
            set { SetProperty(ref _plotName, value); }
        }
        #endregion

        public Drawing() { }

        public Drawing(string templatePath, string programNumber)
        {

            _variables = new List<Variable>();

            _templatePath = templatePath;

            string name = Regex.Match(programNumber, RegexPatternsConstants.PROGRAM_NO_MACHINE_ENDING).Value + Path.GetFileNameWithoutExtension(templatePath);

            _copyName = String.Join("_", DrawingConstants.DRAWING_PREFIX , name) + DrawingConstants.DXF_EXTENSION;

            _plotName = String.Join("_", DrawingConstants.DRAWING_PREFIX, name) + DrawingConstants.PDF_EXTENSION;

            _templateTxt = File.ReadAllText(_templatePath);

            ReadVariables();

        }

        /// <summary>
        /// Finding tagged text in dxf txt
        /// </summary>
        /// <param name="template">Template location</param>
        /// <returns>List of found variables</returns>
        public void ReadVariables()
        {
            foreach (Match match in Regex.Matches(TemplateTxt, RegexPatternsConstants.TAGGED_WORD))
            {
                Variables.AddVariable(match.Value);
            }           
        }

        /// <summary>
        /// Save current drawing as dxf in seprate Folder
        /// Plot saved dxf to pdf
        /// </summary>
        /// <param name="saveDirectory">Save direcory</param>
        public void Plot(string saveDirectory)
        {

            //Save DXF
            string dxfPath = SaveDXF(saveDirectory, FindAndReplace(), DxfName);


            //Plot DXF to PDF
            using (var dxfGenerator = new DrawingPlotter()) 
            {
                dxfGenerator.PlotDXF(dxfPath, Path.Combine(saveDirectory, PlotName));
            }
           
        }


        /// <summary>
        /// Based on current drawing creates pdf preview
        /// </summary>
        public void PlotPreview()
        {
            using(var dxfGenerator = new DrawingPlotter())
            {
                dxfGenerator.PlotPreview(FindAndReplace());
            }
        }


        /// <summary>
        /// Save DXF
        /// </summary>
        /// <param name="directory">saving loaction</param>
        /// <param name="saveName">dxf save name</param>
        /// <param name="dxfText">dxf txt</param>
        /// <returns>Saved DXF location</returns>
        public string SaveDXF(string directory, string dxfText, string saveName)
        {
            string dxfFolder = Directory.CreateDirectory(Path.Combine(directory, DrawingConstants.DXF_FOLDER_NAME)).FullName;

            string dxfPath = Path.Combine(dxfFolder, saveName);

            File.WriteAllText(dxfPath, dxfText);

            return dxfPath;

        }


        /// <summary>
        /// Replace Variables with new values 
        /// </summary>
        /// <param name="vars">List of variables from template</param>
        private string FindAndReplace()
        {
            string result = TemplateTxt;

            foreach (Variable var in Variables)
            {
              result = result.Replace(var.Default, var.Value);
            }

            return result;
        }

    }
}
