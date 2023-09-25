using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism; 

namespace ConveyorDoc.Model
{
    public class InstructinSubItem 
    {
        public string Label { get; }

        public string Icon { get; }

        public string ViewName { get; }

        public object Parent { get; }

        public InstructinSubItem(string lable, string iconPath, string viewName, object parent)
        {
            Label = lable;
            Icon = iconPath;
            ViewName = viewName;
            Parent = parent;
        }

    }
}
