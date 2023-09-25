using ConveyorDoc.Core.Extension;
using ConveyorDoc.Fixtures.Model;
using Microsoft.VisualBasic;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.Fixtures.ViewModels
{
    public class FixtureViewModelBase : BindableBase
    {
        private FixtureRecord _selecteFixture;
        public FixtureRecord SelectedFixture
        {
            get { return _selecteFixture; }
            set { SetProperty(ref _selecteFixture, value); }
        }

        private ObservableCollection<FixtureRecord> _fixtures = new ObservableCollection<FixtureRecord>();
        public ObservableCollection<FixtureRecord> Fixtures
        {
            get { return _fixtures; }
            set { SetProperty(ref _fixtures, value); }
        }

        private ObservableCollection<string> _machines = new ObservableCollection<string>();
        public ObservableCollection<string> Machines
        {
            get { return _machines; }
            set { SetProperty(ref _machines, value); }
        }

        private ObservableCollection<string> _sizes = new ObservableCollection<string>();
        public ObservableCollection<string> Sizes
        {
            get { return _sizes; }
            set { SetProperty(ref _sizes , value); }
        }


        private ObservableCollection<string> _types = new ObservableCollection<string>();
        public ObservableCollection<string> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value);  }
        }


        public ICollectionView FixtureCollection { get; }

        public FixtureViewModelBase()
        {
            FixtureCollection = CollectionViewSource.GetDefaultView(_fixtures);

            Fixtures.CollectionChanged += Fixtures_CollectionChanged;
        }

        private void Fixtures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Machines.Replace(Fixtures.Select(x => x.Machine).Distinct());
            Sizes.Replace(Fixtures.Select(_x => _x.Size).Distinct());
            Types.Replace(Fixtures.Select(x => x.ItemType).Distinct());
        }

    }
}
