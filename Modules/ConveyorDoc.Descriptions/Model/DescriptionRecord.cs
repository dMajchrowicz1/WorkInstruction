using ConveyorDoc.Core.Extension;

namespace ConveyorDoc.Descriptions.Model
{
    public class DescriptionRecord : RecordBase
    {

        public int ID { get; set; }

        private string _moduleTyp = string.Empty;
        public string ModuleType
        {
            get { return _moduleTyp ?? (_moduleTyp = string.Empty); }
            set { SetProperty(ref _moduleTyp, value); }
        }

        private string _machine = string.Empty;
        public string Machine
        {
            get { return _machine ?? (_machine = string.Empty); }
            set { SetProperty(ref _machine, value); }
        }

        private string _size = string.Empty;
        public string Size
        {
            get { return _size ?? (_size = string.Empty); }
            set { SetProperty(ref _size, value); }
        }

        private string _operation = string.Empty;
        public string OperationNumber
        {
            get { return _operation ?? (_operation = string.Empty); }
            set { SetProperty(ref _operation, value); }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text ?? (_text = string.Empty); }
            set { SetProperty(ref _text, value); }
        }


        public override bool Contains(object obj)
        {
            if (obj != null && obj is DescriptionRecord description)
            {
                return
                    description.Machine.Filter(Machine) &&
                    description.ModuleType.Filter(ModuleType) &&
                    description.OperationNumber.Filter(OperationNumber) &&
                    description.Size.Filter(Size);
            }
            else return false;
        }
    }
}
