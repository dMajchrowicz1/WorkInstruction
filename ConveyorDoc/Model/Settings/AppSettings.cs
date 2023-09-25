using ConveyorDoc.Business.Constants;
using Newtonsoft.Json;
using Prism.Mvvm;
using System.IO;

namespace ConveyorDoc.Model.Settings
{
    public class AppSettings : BindableBase
    {
        private GeneralSettings _generalSettings = new GeneralSettings();
        public GeneralSettings GeneralSettings
        {
            get { return _generalSettings; }
            set { SetProperty(ref _generalSettings, value); }
        }

        private DirectoriesSettings _directoriesSettings = new DirectoriesSettings();
        public DirectoriesSettings DirectoriesSettings
        {
            get { return _directoriesSettings; }
            set { SetProperty(ref _directoriesSettings, value); }
        }

        private InstructionSettings _instructionSettings =  new InstructionSettings();
        public InstructionSettings InstructionSettings
        {
            get { return _instructionSettings; }
            set { SetProperty(ref _instructionSettings, value); }
        }


        public void Save(string savePath)
        {
           if(savePath != null && Directory.Exists(savePath))
           {
              var json =  JsonConvert.SerializeObject(this);
              File.WriteAllText(Path.Combine(savePath, GeneralConstants.CONFIG_NAME), json);
           }
        }


        public static AppSettings Load()
        {
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(GeneralConstants.CONFIG_FILE_DIR));
        }

        public static AppSettings Load(string filePath)
        {
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(filePath));
        }


    }
}
