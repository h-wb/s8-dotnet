using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Model
{
    internal static class NavigationMenuItemExtensions
    {
        internal static TreeViewNode AsTreeViewNode(this Item menuItem)
        {
            var result = new TreeViewNode
            {
                Content = menuItem
            };

            foreach (Item subItem in menuItem.Children)
            {
                result.Children.Add(subItem.AsTreeViewNode());
            }

            return result;
        }
    }
}
