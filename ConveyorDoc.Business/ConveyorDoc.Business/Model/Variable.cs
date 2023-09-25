
using ConveyorDoc.Business.Constants;
using DocumentFormat.OpenXml.Wordprocessing;
using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace ConveyorDoc.Business.Model
{

    public class Variable : BindableBase
    {

        private string _name = string.Empty;
        [Required(AllowEmptyStrings =false)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }


        private string _value = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }


        private string _default = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string Default
        {
            get { return _default; }
            set { SetProperty(ref _default, value); }
        }

        public override string ToString()
        {
            return _name.ToString();
        }


        public Variable() { }


        public Variable(Run run)
        {
            _value = run.InnerText;

            _name = Regex.Match(run.InnerText, RegexPatternsConstants.NO_TAGS).Value;

            _default = run.InnerText;
        }

    }
}
