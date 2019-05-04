using DAO;
using Dep_Gestion.Model;
using AppGestion;
using Metier;
using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Model;
using Windows.UI.Xaml;
using System.Linq;

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
        private static DAO<Categorie> categ = factoSQL.getCategorieDAO();
        private static DAO<EC> ecs = factoSQL.getECDAO();
        private static DAO<TypeCours> tps = factoSQL.getTypeCoursDao();
        private static DAO<InfosAssignation> IA = factoSQL.getInfosAssignationDAO();

        private ObservableCollectionExt<Departement> departements = new ObservableCollectionExt<Departement>();
        private ObservableCollectionExt<Annee> annees = new ObservableCollectionExt<Annee>();
        private ObservableCollectionExt<Enseignant> enseignants = new ObservableCollectionExt<Enseignant>();





        

        public ObjetBase nodeSelectionne;
        private ObjetBase departementSelectionne;
        private ObjetBase enseignantSelectionne;



        public MainPage()
        {
            this.InitializeComponent();
            departements = GetDepartements();
            enseignants = GetEnseignants();
        }

        private ObservableCollectionExt<Departement> GetDepartements()
        {
            ObservableCollectionExt<Departement> departements = new ObservableCollectionExt<Departement>();
            foreach (Departement dpt in depart.findAll())
            {
                departements.Add(new Departement { Id = dpt.Id, Nom = dpt.Nom });
            }
            return departements;
        }

        private ObservableCollectionExt<Enseignant> GetEnseignants()
        {
            ObservableCollectionExt<Enseignant> enseignants = new ObservableCollectionExt<Enseignant>();
            foreach (Enseignant ens in enseignant.findAll())
            {
                enseignants.Add(new Enseignant { Id = ens.Id, Categorie = ens.Categorie, Prenom =  ens.Prenom.TrimEnd(), Nom = ens.Nom.TrimEnd(), NavigationDestination = typeof(EnseignantVue) });
            }
            return enseignants;
        }

        private ObservableCollectionExt<Annee> GetAnnees(int idDepartement)
        {
            ObservableCollectionExt<Annee> annees = new ObservableCollectionExt<Annee>();

            foreach (Annee annee in annee.findAll())
            {
                if (idDepartement == annee._departement.Id)
                {
                    Annee nodeAnnee = new Annee { Id = annee.Id, Nom = annee.Nom.TrimEnd(), Description = annee._description, Children = new ObservableCollectionExt<ObjetBase>(), NavigationDestination = typeof(AnneeVue), Departement = annee.Departement };
                    Debug.WriteLine(nodeAnnee.Description);
                    annees.Add(nodeAnnee);
                    foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                    {
                        if (annee.Id == partieAnnee.Annee.Id)
                        {
                            PartieAnnee nodePartieAnnee = new PartieAnnee { Id = partieAnnee.Id, Nom = partieAnnee.Nom.TrimEnd(), Description = partieAnnee.Description, Annee = annee, Children = new ObservableCollectionExt<ObjetBase>(), Parent = nodeAnnee, NavigationDestination=typeof(PartieAnneeVueReset) };
                            nodeAnnee.Children.Add(nodePartieAnnee);
                            foreach (Enseignement enseignement in enseignement.findAll())
                            {
                                if (partieAnnee.Id == enseignement.PartieAnnee.Id)
                                {
                                    Enseignement nodeEnseignement = new Enseignement { Id = enseignement.Id, Nom = enseignement.Nom.TrimEnd(), PartieAnnee=partieAnnee, Description=enseignement.Description, Children = new ObservableCollectionExt<ObjetBase>(), Parent = nodePartieAnnee, NavigationDestination=typeof(EnseignementVue) };
                                    nodePartieAnnee.Children.Add(nodeEnseignement);
                                }
                            }
                        }

                    }
                }
            }
            return annees;
        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {

            nodeSelectionne = (ObjetBase)args.InvokedItem;
            if (nodeSelectionne.NavigationDestination != null && nodeSelectionne != null) 
            {
                Debug.WriteLine(nodeSelectionne.NavigationDestination);
                Navigate(nodeSelectionne.NavigationDestination, nodeSelectionne);
            }

        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            departementSelectionne = (ObjetBase)args.InvokedItem;
            annees.Replace(GetAnnees(departementSelectionne.Id));

        }

        private void EnseignantSelection(object sender, ItemClickEventArgs e)
        {
            enseignantSelectionne = (ObjetBase)e.ClickedItem;
            if (enseignantSelectionne.NavigationDestination != null)
            {
                Navigate(enseignantSelectionne.NavigationDestination, enseignantSelectionne);
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
            if (nodeSelectionne == null)
            {
                Annee nouvelleAnne = new Annee { Nom = "Nouvelle annee", Departement = depart.find(1), NavigationDestination = typeof(AnneeVue), Description= ""};
                annee.create(nouvelleAnne);
                annees.Add(nouvelleAnne);
            }
            else if (nodeSelectionne.GetType() == typeof(Annee))
            {
                PartieAnnee nouvellePartieAnnee = new PartieAnnee { Nom = "Nouveau semestre", Annee = (Annee)nodeSelectionne, NavigationDestination = typeof(PartieAnneeVueReset), Description = "", Parent = nodeSelectionne};
                partieAnnee.create(nouvellePartieAnnee);
                nodeSelectionne.Children.Add(nouvellePartieAnnee);
            }
            else if (nodeSelectionne.GetType() == typeof(PartieAnnee))
            {
                Enseignement nouvelEnseignement = new Enseignement { Nom= "Nouveau enseignement", PartieAnnee = (PartieAnnee)nodeSelectionne, NavigationDestination = typeof(EnseignementVue), Description = "", Parent = nodeSelectionne };
                enseignement.create(nouvelEnseignement);
                nodeSelectionne.Children.Add(nouvelEnseignement);
            }

        }

        private void Clear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionne.GetType() == typeof(Annee))
            {
                annee.delete((Annee)nodeSelectionne);
                annees.Remove((Annee)nodeSelectionne);     
            }
            else if (nodeSelectionne.GetType() == typeof(PartieAnnee))
            {
                partieAnnee.delete((PartieAnnee)nodeSelectionne);
                nodeSelectionne.Parent.Children.Remove((PartieAnnee)nodeSelectionne);
            }
            else if (nodeSelectionne.GetType() == typeof(Enseignement))
            {
                enseignement.delete((Enseignement)nodeSelectionne);
                nodeSelectionne.Parent.Children.Remove(nodeSelectionne);
            }



        }

        private void AddDepartement_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Departement departement = new Departement { Nom = "Nouveau département" };
            depart.create(departement);
            departements.Add(departement);
        }

        private void AjouterEnseignant(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Enseignant nouvelEnseignant = new Enseignant { Nom = "Nouvel", Prenom = "enseignant", Categorie = categ.find(1), NavigationDestination = typeof(EnseignantVue) };
            enseignant.create(nouvelEnseignant);
            enseignants.Add(nouvelEnseignant);
        }

        private void SupprimerEnseignant(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            enseignant.delete((Enseignant)enseignantSelectionne);
            enseignants.Remove((Enseignant)enseignantSelectionne);
        }

        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            int EnseignantId = e.Items.Cast<Enseignant>().Select(i => i.Id).FirstOrDefault();
            e.Data.SetText(EnseignantId.ToString());
        }

        private void ListeEnseignants_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            Debug.WriteLine("sdfsdfsdfsdfsd");
            var test = (FrameworkElement)e.OriginalSource;
            var test2 = (Enseignant)test.DataContext;

            Debug.WriteLine(test2.ToString());
        }

        private void ListeEnseignants_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            enseignantSelectionne = (Enseignant)source.DataContext;
        }
    }
}