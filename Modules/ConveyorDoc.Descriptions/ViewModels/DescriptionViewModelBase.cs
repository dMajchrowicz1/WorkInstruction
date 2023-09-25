using ConveyorDoc.Core.Extension;
using ConveyorDoc.Descriptions.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.Descriptions.ViewModels
{
    public class DescriptionViewModelBase : BindableBase
    {
        private DescriptionRecord _selectedDescription;
        public DescriptionRecord SelectedDescription
        {
            get { return _selectedDescription; }
            set { SetProperty(ref _selectedDescription, value); }
        }


        private ObservableCollection<DescriptionRecord> _descriptions =  new ObservableCollection<DescriptionRecord>();
        public ObservableCollection<DescriptionRecord> Descriptions
        {
            get { return _descriptions; }
            set { SetProperty(ref _descriptions, value); }
        }
     

        private ObservableCollection<string> _sizes;
        public ObservableCollection<string> Sizes
        {
            get { return _sizes; }
            set { SetProperty(ref _sizes, value); }
        }

        private ObservableCollection<string> _machines;
        public ObservableCollection<string> Machines
        {
            get { return _machines; }
            set { SetProperty(ref _machines, value); }
        }

        private ObservableCollection<string> _moduleTypes;
        public ObservableCollection<string> ModuleTypes
        {
            get { return _moduleTypes; }
            set { SetProperty(ref _moduleTypes, value); }
        }

        public ICollectionView DescriptionCollection { get; }

        public DescriptionViewModelBase()
        {
            DescriptionCollection = CollectionViewSource.GetDefaultView(_descriptions);

            //Descriptions.CollectionChanged += Descriptions_CollectionChanged;
        }

        //private void Descriptions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    Machines.Replace(Descriptions.Select(x => x.Machine).Distinct());
        //    Sizes.Replace(Descriptions.Select(x => x.Size).Distinct());
        //    ModuleTypes.Replace(Descriptions.Select(x => x.ModuleType).Distinct());
        //}
    }
}
