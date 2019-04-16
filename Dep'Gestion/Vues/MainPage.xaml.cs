using Dep_Gestion.Model;
using Metier;
using System;
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

        private ObservableCollection<NavigationMenuItem> MainMenu = new ObservableCollection<NavigationMenuItem>();


        public MainPage()
        {



            this.InitializeComponent();


            foreach (Categorie categ in categSQL.findAll())
                MainMenu.Add(new NavigationMenuItem {Text = categ.nom, Id = categ.id });

  


            foreach (var item in MainMenu)
            {
                NavigationTree.RootNodes.Add(item.AsTreeViewNode());

              /*  foreach(EquivalentTD eq in equivalentTD.findAll())
                {
                    if(eq.idCategorieEnseignant == item.Id)
                    {
                        item.Add(new NavigationMenuItem { Text = "dd" });
                    }

                }*/

      
                
            }

            


        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is TreeViewNode node)
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
