using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Dep_Gestion.Model
{
    internal static class NavigationMenuItemExtensions
    {
        internal static TreeViewNode AsTreeViewNode(this NavigationMenuItem menuItem)
        {
            var result = new TreeViewNode
            {
                Content = menuItem
            };

            foreach (NavigationMenuItem subItem in menuItem.Children)
            {
                result.Children.Add(subItem.AsTreeViewNode());
            }

            return result;
        }
    }
}
