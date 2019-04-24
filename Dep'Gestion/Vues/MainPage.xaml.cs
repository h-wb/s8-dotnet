
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
        //private static DAO<Enseignement> enseignement = factoSQL.getE


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
                        NavigationMenuItem nodePartieAnnee = new NavigationMenuItem { Text = partieAnnee.nom, Objet = partieAnnee, Children = new ObservableCollection<MenuItem>() };
                        nodeAnnee.Children.Add(nodePartieAnnee);
                    }
                }
            }

            foreach (var item in annees)
            {
                TreeView.RootNodes.Add(item.AsTreeViewNode());

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
                        Navigate(menuItem.NavigationDestination, menuItem.NavigationParameter);
                    }
                }
            }
        }

        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return Frame.Navigate(sourcePageType, parameter);
        }

        private void Frame_OnNavigated(object sender, NavigationEventArgs e)
        {
            TreeView.SelectionMode = TreeViewSelectionMode.None;
            TreeView.SelectionMode = TreeViewSelectionMode.Single;
        }

        private void Add_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
            if(nodeSelectionne == null)
            {
                Annee nouvelleAnnee = new Annee("Nouvelle annee");
                annee.create(nouvelleAnnee);
                NavigationMenuItem nouveauNode = new NavigationMenuItem { Text = nouvelleAnnee.nom, Objet = nouvelleAnnee };
                TreeView.RootNodes.Add(nouveauNode.AsTreeViewNode());
            } else if(nodeSelectionneItem.Objet.GetType() == typeof(Annee))
            {
                PartieAnnee nouvellePartieAnnee = new PartieAnnee("Nouveau semestre", (Annee)nodeSelectionneItem.Objet);
                partieAnnee.create(nouvellePartieAnnee);
                NavigationMenuItem nouveauNode = new NavigationMenuItem { Text = nouvellePartieAnnee.nom, Objet = nouvellePartieAnnee };
                nodeSelectionne.Children.Add(nouveauNode.AsTreeViewNode());
            }
       
        }

        private void Clear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionneItem.Objet.GetType() == typeof(Annee))
            {
                annee.delete((Annee)nodeSelectionneItem.Objet);
                TreeView.RootNodes.Remove(nodeSelectionne);
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(PartieAnnee))
            {
                partieAnnee.delete((PartieAnnee)nodeSelectionneItem.Objet);
                //Etrange façon de supprimer mais remove n'est pas directement accessible via le node
                nodeSelectionne.Parent.Children.Remove(nodeSelectionne);
            }

            

        }

    }
}