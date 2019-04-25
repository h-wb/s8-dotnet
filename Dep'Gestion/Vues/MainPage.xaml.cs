using DAO;
using Model;
using Dep_Gestion.Vues;
using Metier;
using System;
using System.Collections.ObjectModel;
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
        private static DAO<Departement> depart = factoSQL.getDepartementDAO();
        private static DAO<Enseignement> enseignement = factoSQL.getEnseignementDAO();
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();


        private ObservableCollectionExt<Item> departements = new ObservableCollectionExt<Item>();
        private ObservableCollectionExt<Item> annees = new ObservableCollectionExt<Item>();
        private ObservableCollectionExt<Item> enseignants = new ObservableCollectionExt<Item>();



        private static Departement departement = new Departement("Informatique");

        private Item nodeSelectionne;
        private Item departementSelectionne;

        public MainPage()
        {
            this.InitializeComponent();
            departements = GetDepartements();
            enseignants = GetEnseignants();
        }



        private ObservableCollectionExt<Item> GetDepartements()
        {
            ObservableCollectionExt<Item> departements = new ObservableCollectionExt<Item>();
            foreach (Departement dpt in depart.findAll())
            {
                departements.Add(new Item { Id = dpt.id, Text = dpt.nom, Objet = dpt });
            }
            return departements;
        }

        private ObservableCollectionExt<Item> GetEnseignants()
        {
            ObservableCollectionExt<Item> enseignants = new ObservableCollectionExt<Item>();
            foreach (Enseignant ens in enseignant.findAll())
            {
                enseignants.Add(new Item { Id = ens.id, Text = ens.nom, Objet = ens });
            }
            return enseignants;
        }

        private ObservableCollectionExt<Item> GetAnnees(int idDepartement)
        {
            ObservableCollectionExt<Item> departement = new ObservableCollectionExt<Item>();

            foreach (Annee annee in annee.findAll())
            {
                if (idDepartement == annee.dep.id)
                {
                    Item nodeAnnee = new Item { Id = annee.dep.id, Text = annee.nom, Objet = annee, Children = new ObservableCollection<Item>(), NavigationDestination = typeof(AnneeVue) };
                    departement.Add(nodeAnnee);
                    foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                    {
                        if (annee.id == partieAnnee.annee.id)
                        {
                            Item nodePartieAnnee = new Item { Text = partieAnnee.nom, Objet = partieAnnee, Children = new ObservableCollection<Item>(), Parent = nodeAnnee };
                            nodeAnnee.Children.Add(nodePartieAnnee);
                            foreach (Enseignement enseignement in enseignement.findAll())
                            {
                                if (partieAnnee.id == enseignement.partAnnee.id)
                                {
                                    Item nodeEnseignement = new Item { Text = enseignement.nom, Objet = enseignement, Children = new ObservableCollection<Item>(), Parent = nodePartieAnnee };
                                    nodePartieAnnee.Children.Add(nodeEnseignement);
                                }
                            }

                        }
                    }
                }
               
            }

            return departement;
        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            nodeSelectionne = (Item)args.InvokedItem;
            if (nodeSelectionne.NavigationDestination != null)
            {
                Navigate(nodeSelectionne.NavigationDestination, nodeSelectionne.NavigationParameter);
            }

        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            departementSelectionne = (Item)args.InvokedItem;
            annees.Replace(GetAnnees(departementSelectionne.Id));

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

            if (nodeSelectionne == null)
            {
                Annee nouvelleAnnee = new Annee("Nouvelle annee", depart.find(departementSelectionne.Id));
                annee.create(nouvelleAnnee);
                annees.Add(new Item { Text = nouvelleAnnee.nom, Objet = nouvelleAnnee });
            }
            else if (nodeSelectionne.Objet.GetType() == typeof(Annee))
            {
                PartieAnnee nouvellePartieAnnee = new PartieAnnee("Nouveau semestre", (Annee)nodeSelectionne.Objet);
                partieAnnee.create(nouvellePartieAnnee);
                nodeSelectionne.Children.Add(new Item { Text = nouvellePartieAnnee.nom, Objet = nouvellePartieAnnee, Parent = nodeSelectionne });
            }
            else if (nodeSelectionne.Objet.GetType() == typeof(PartieAnnee))
            {
                Enseignement nouvelEnseignement = new Enseignement("Nouveau enseignement", (PartieAnnee)nodeSelectionne.Objet);
                enseignement.create(nouvelEnseignement);
                nodeSelectionne.Children.Add(new Item { Text = nouvelEnseignement.nom, Objet = nouvelEnseignement, Parent = nodeSelectionne });
            }

        }

        private void Clear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionne.Objet.GetType() == typeof(Annee))
            {
                annee.delete((Annee)nodeSelectionne.Objet);
                annees.Remove(nodeSelectionne);
            }
            else if (nodeSelectionne.Objet.GetType() == typeof(PartieAnnee))
            {

                partieAnnee.delete((PartieAnnee)nodeSelectionne.Objet);
                nodeSelectionne.Parent.Children.Remove(nodeSelectionne);
            }
            else if (nodeSelectionne.Objet.GetType() == typeof(Enseignement))
            {
                enseignement.delete((Enseignement)nodeSelectionne.Objet);
                nodeSelectionne.Parent.Children.Remove(nodeSelectionne);

            }



        }

    }
}