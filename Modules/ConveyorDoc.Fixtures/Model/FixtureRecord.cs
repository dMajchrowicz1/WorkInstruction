using ConveyorDoc.Core.Extension;
using System.Diagnostics;
using System.IO;


namespace ConveyorDoc.Fixtures.Model
{
    public class FixtureRecord : RecordBase
    {

        public int Id { get; set; }

        private string _itemNumber = string.Empty;
        public string ItemNumber
        {
            get { return _itemNumber ?? (_itemNumber = string.Empty); }
            set { SetProperty(ref _itemNumber, value); }
        }

        private string _size = string.Empty;
        public string Size
        {
            get { return _size ?? (_size = string.Empty); }
            set { SetProperty(ref _size, value); }
        }

        private string _machine = string.Empty;
        public string Machine
        {
            get { return _machine ?? (_machine = string.Empty); }
            set { SetProperty(ref _machine, value); }
        }

        private string _itemType = string.Empty;
        public string ItemType
        {
            get { return _itemType ?? (_itemType = string.Empty); }
            set { SetProperty(ref _itemType, value); }
        }

        private string _pdf = string.Empty;
        public string PDF
        {
            get { return _pdf; }
            set { SetProperty(ref _pdf, value); }
        }

        private string _details = string.Empty;
        public string Details
        {
            get { return _details; }
            set { SetProperty(ref _details, value); }
        }

        public string CreatedBy { get; set; }  

        public string ModifiedBy { get; set; }  

        public string CreateDate { get; set; }

        public string ModificationDate { get; set; }

        public string Workspace { get; set; }

        public void OpenPdf()
        {
            Process.Start("explorer", PDF);
        }

        public void OpenWorkspace()
        {
            Process.Start("explorer", Path.GetDirectoryName(PDF));
        }

        public override bool Contains(object obj)
        {
            if (obj is FixtureRecord fixture)
            {
                return
                    fixture.ItemNumber.Filter(ItemNumber) &&
                    fixture.Size.Filter(Size) &&
                    fixture.Machine.Filter(Machine) &&
                    fixture.ItemType.Filter(ItemType);                   
            }
            else
                return false;
        }

        
    }
}
