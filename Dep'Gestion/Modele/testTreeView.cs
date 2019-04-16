using AppGestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace Dep_Gestion.Modele
{
    class testTreeView
    {
        MainPage mp;

        public testTreeView(MainPage mp)
        {
            this.mp = mp;
        }

        public void Init()
        {
            TreeViewNode rootNode = new TreeViewNode() { Content = "Flavors" };
            rootNode.IsExpanded = true;
            rootNode.Children.Add(new TreeViewNode() { Content = "Vanilla" });
            rootNode.Children.Add(new TreeViewNode() { Content = "Strawberry" });
            rootNode.Children.Add(new TreeViewNode() { Content = "Chocolate" });

            mp.NavigationTree.RootNodes.Add(rootNode);
        }
    }
}
