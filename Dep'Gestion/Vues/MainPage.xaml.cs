using DAO;
using Dep_Gestion.Model;
using Dep_Gestion.Vues;
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


        private TreeViewNode nodeSelectionne;
        private NavigationMenuItem nodeSelectionneItem;

        public MainPage()
        {
            this.InitializeComponent();
            foreach (Annee annee in annee.findAll())
            {
                NavigationMenuItem nodeAnnee = new NavigationMenuItem { Text = annee.nom, Objet = annee, Children = new ObservableCollection<MenuItem>(), NavigationDestination = typeof(AnneeVue) };
                annees.Add(nodeAnnee);
                foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                {
                    if (annee.id == partieAnnee.annee.id)
                    {
                        NavigationMenuItem nodePartieAnnee = new NavigationMenuItem { Text = partieAnnee.nom, Objet = partieAnnee, Children = new ObservableCollection<MenuItem>(), NavigationDestination = typeof(PartieAnneeVue) };
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
            nodeSelectionne = (TreeViewNode)args.InvokedItem;
            if (args.InvokedItem is TreeViewNode node)
            {
                if (node.Content is NavigationMenuItem menuItem)
                {
                    nodeSelectionneItem = menuItem;
                    if (nodeSelectionneItem.NavigationDestination != null)
                    {
                        Debug.WriteLine(menuItem.NavigationDestination);
                        Navigate(menuItem.NavigationDestination);
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

            if (nodeSelectionne == null)
            {
                Annee nouvelleAnnee = new Annee("Nouvelle annee");
                NavigationMenuItem nouveauNode = new NavigationMenuItem { Text = nouvelleAnnee.nom, Objet = nouvelleAnnee };
                NavigationTree.RootNodes.Add(nouveauNode.AsTreeViewNode());
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(Annee))
            {
                PartieAnnee partieAnnee = new PartieAnnee("Nouveau semestre", (Annee)nodeSelectionneItem.Objet);
                NavigationMenuItem nouveauNode = new NavigationMenuItem { Text = partieAnnee.nom, Objet = partieAnnee };
                nodeSelectionne.Children.Add(nouveauNode.AsTreeViewNode());
            }

        }


    }
}