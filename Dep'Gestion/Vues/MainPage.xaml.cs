using System;
using System.Collections.Generic;
using System.IO;
using Dep_Gestion.Model;
using Metier;
using DAO;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Categorie> categSQL = factoSQL.getCategorieDAO();
        private static DAO<EquivalentTD> equivalentTD = factoSQL.getEquivalentTDDao();
        private static DAO<Annee> an = factoSQL.getAnneeDAO();
        private static DAO<PartieAnnee> pan = factoSQL.getPartieAnneeDAO();

        private ObservableCollection<NavigationMenuItem> MainMenu = new ObservableCollection<NavigationMenuItem>();


        public MainPage()
        {
            this.InitializeComponent();

            foreach (Categorie categ in categSQL.findAll())
                MainMenu.Add(new NavigationMenuItem { Text = categ.nom, Id = categ.id });

            /*foreach (var item in MainMenu)
            {
                NavigationTree.RootNodes.Add(item.AsTreeViewNode());
                item.Children.Add("oui".Astre);
                
                
            }*/

            //-----------------//

        

            foreach(Annee anne in an.findAll())
            {
                TreeViewNode root = new TreeViewNode() { Content = anne.nom };
                foreach(PartieAnnee panne in pan.findAll())
                {
                    if(panne.annee.id == anne.id)
                    {
                        root.Children.Add(new TreeViewNode() { Content = panne.nom });
                    }
                }


                NavigationTree.RootNodes.Add(root);
            }

            
        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            Debug.WriteLine(((TreeViewNode)args.InvokedItem).Content);

            switch(((TreeViewNode)args.InvokedItem).Depth)
            {
                case 0: Debug.WriteLine("Annee " + ((TreeViewNode)args.InvokedItem).Content);
                    break;
                case 1: Debug.WriteLine("Partie Annee " + ((TreeViewNode)args.InvokedItem).Content);
                    break;
            }
                /*if (args.InvokedItem is TreeViewNode node)
                {
                    if (node.Content is NavigationMenuItem menuItem)
                    {
                        var target = menuItem.NavigationDestination;
                        if (target != null)
                        {
                            Navigate(menuItem.NavigationDestination, menuItem.NavigationParameter);
                        }
                    }
                }*/

        }

        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return SplitViewFrame.Navigate(sourcePageType, parameter);
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            NavigationTree.SelectionMode = TreeViewSelectionMode.None;
            NavigationTree.SelectionMode = TreeViewSelectionMode.Single;
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Categorie nouveauType = new Categorie("Nouveau type de prof", 240);
            categSQL.create(nouveauType);
            NavigationMenuItem test = new NavigationMenuItem { Text = nouveauType.nom };
            NavigationTree.RootNodes.Add(test.AsTreeViewNode());
        }
    }
}