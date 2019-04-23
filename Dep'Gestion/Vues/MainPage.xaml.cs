
using DAO;
using Dep_Gestion.Model;
using Metier;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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


        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();


        private ObservableCollection<NavigationMenuItem> annees = new ObservableCollection<NavigationMenuItem>();
        private ObservableCollection<MenuItem> test = new ObservableCollection<MenuItem>();


        public MainPage()
        {
            this.InitializeComponent();
            foreach (Annee annee in annee.findAll())
            {
                NavigationMenuItem nodeAnnee = new NavigationMenuItem { Text = annee.nom, Id = annee.id, Children = new ObservableCollection<MenuItem>() };
                annees.Add(nodeAnnee);
                foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                {
                    if (annee.id == partieAnnee.annee.id)
                    {
                        NavigationMenuItem nodePartieAnnee = new NavigationMenuItem { Text = partieAnnee.nom, Id = partieAnnee.id, Children = new ObservableCollection<MenuItem>() };
                        nodeAnnee.Children.Add(nodePartieAnnee);
                    }
                }
            }


            foreach (var item in annees)
            {
                NavigationTree.RootNodes.Add(item.AsTreeViewNode());

            }



        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {

            Debug.WriteLine(((TreeViewNode)args.InvokedItem).Content);

            switch (((TreeViewNode)args.InvokedItem).Depth)
            {
                case 0:
                    Debug.WriteLine("Annee " + ((TreeViewNode)args.InvokedItem).Content);
                    break;
                case 1:
                    Debug.WriteLine("Partie Annee " + ((TreeViewNode)args.InvokedItem).Content);
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


            /*  if (args.InvokedItem is TreeViewNode node)
              {
                  if (node.Content is NavigationMenuItem menuItem)
                  {
                      var target = menuItem.NavigationDestination;
                      if (target != null)
                      {
                          Navigate(menuItem.NavigationDestination, menuItem.NavigationParameter);
                      }
                  }
              }
     */

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
           // categSQL.create(nouveauType);
            NavigationMenuItem test = new NavigationMenuItem { Text = nouveauType.nom };
            NavigationTree.RootNodes.Add(test.AsTreeViewNode());
        }
    }
}