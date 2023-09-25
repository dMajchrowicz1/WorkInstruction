using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Extension;
using ConveyorDoc.Business.Queries;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConveyorDoc.Business.Model
{
    public class Instruction : BindableBussines
    {

        private string _moduleNumber;
        [Required(AllowEmptyStrings = false)]
        public string ModuleNumber
        {
            get { return _moduleNumber; }
            set { SetProperty(ref _moduleNumber,value); }
        }

        private string _moduleType;
        [Required(AllowEmptyStrings = false)]
        public string ModuleType
        {
            get { return _moduleType; }
            set { SetProperty(ref _moduleType,value); }
        }


        private string _moduleSize;
        [Required(AllowEmptyStrings = false)]
        public string ModuleSize
        {
            get { return _moduleSize; }
            set { SetProperty(ref _moduleSize , value); }
        }

        private string _moduleRev;
        [Required(AllowEmptyStrings = false)]
        public string ModuleRev
        {
            get { return _moduleRev; }
            set {SetProperty(ref _moduleRev, value); }
        }

        private string _programmerName = Environment.UserName;
        [Required(AllowEmptyStrings = false)]
        public string ProgrammerName

        {
            get { return _programmerName; }
            set {  SetProperty(ref _programmerName, value); }
        }

        
        private string _material;
        [Required(AllowEmptyStrings = false)]
        public string Material
        {
            get { return _material; }
            set {  SetProperty(ref _material, value); }
        }

        private string _blankNumber;
        [Required(AllowEmptyStrings = false)]
        public string BlankNumber
        {
            get { return _blankNumber; }
            set { SetProperty(ref _blankNumber, value); }
        }


        private string _date = DateTime.Now.ToString(GeneralConstants.DATE_TIME_FORMAT);
        [Required(AllowEmptyStrings = false)]
        public string Date
        {
            get { return _date; }
            set {  SetProperty(ref _date, value); }
        }

        private ObservableCollection<Word> _wordInstructions = new ObservableCollection<Word>();
        public ObservableCollection<Word> WordInstructions
        {
            get { return _wordInstructions; }
            set { SetProperty(ref _wordInstructions, value); }
        }



        public void AddWord(string ncProgramPath)
        {
            _wordInstructions.Add(new Word(this, new NcProgram(ncProgramPath)));
        }

        public void AddWord(NcProgram [] programs)
        {
            programs.ForEach(program => _wordInstructions.Add(new Word(this,program)));
        }

        public void GenerateWords(string savePath, bool standardHeader =true)
        {
            foreach (var item in WordInstructions)
            {
                item.SaveInstruction(savePath,standardHeader);
            }
        }

        public void Save(string savePath)
        {
            //Save all object's as json file with objectReference Id
            File.WriteAllText(savePath, JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
        }

        public static Instruction Load(string filePath)
        {
            var result = JsonConvert.DeserializeObject<Instruction>(File.ReadAllText(filePath),
                        new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });


            return result;

        }

    }
}
