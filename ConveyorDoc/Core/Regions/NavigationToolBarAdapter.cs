using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConveyorDoc.Core.Regions
{
    public class NavigationToolBarAdapter : RegionAdapterBase<StackPanel>
    {
        public NavigationToolBarAdapter(IRegionBehaviorFactory factor)
            : base(factor)
        {

        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += ((x, y) =>
            {
                switch (y.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            foreach (StackPanel group in y.NewItems)
                            {
                                regionTarget.Children.Add(group);


                            }
                            break;
                        }
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            foreach (StackPanel group in y.OldItems)
                            {
                                regionTarget.Children.Remove(group);
                            }
                            break;
                        }
                }
            });
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
