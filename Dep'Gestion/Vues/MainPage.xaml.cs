using DAO;
using Dep_Gestion.Metier;
using Dep_Gestion.Vues;
using Metier;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Model;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        private ObservableCollectionExt<ObjetBase> annees = new ObservableCollectionExt<ObjetBase>();
        private ObservableCollectionExt<Enseignant> enseignants = new ObservableCollectionExt<Enseignant>();







        public ObjetBase nodeSelectionne;
        private ObjetBase departementSelectionne;
        private ObjetBase enseignantSelectionne;



        public MainPage()
        {
            this.InitializeComponent();
            departements = GetDepartements();
            enseignants = GetEnseignants();


            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(AppTitleBar);

        }



        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            nodeSelectionne = (ObjetBase)args.InvokedItem;
            if (nodeSelectionne.NavigationDestination != null && nodeSelectionne != null)
            {
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


        private void Add_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionne == null)
            {
                Annee nouvelleAnne = new Annee { Nom = "Nouvelle annee", Departement = depart.find(departementSelectionne.Id), NavigationDestination = typeof(AnneeVue), Description = "" };
                annee.create(nouvelleAnne);
                annees.Add(nouvelleAnne);
            }
            else if (nodeSelectionne.GetType() == typeof(Annee))
            {
                PartieAnnee nouvellePartieAnnee = new PartieAnnee { Nom = "Nouveau semestre", Annee = (Annee)nodeSelectionne, NavigationDestination = typeof(PartieAnneeVue), Description = "", Parent = nodeSelectionne };
                partieAnnee.create(nouvellePartieAnnee);
                nodeSelectionne.Children.Add(nouvellePartieAnnee);
            }
            else if (nodeSelectionne.GetType() == typeof(PartieAnnee))
            {
                Enseignement nouvelEnseignement = new Enseignement { Nom = "Nouveau enseignement", PartieAnnee = (PartieAnnee)nodeSelectionne, NavigationDestination = typeof(EnseignementVue), Description = "", Parent = nodeSelectionne };
                enseignement.create(nouvelEnseignement);
                nodeSelectionne.Children.Add(nouvelEnseignement);
            }

            // nodeSelectionne.Nom = "fffsdfds";

        }

        private void Clear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionne != null)
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

        private void TabDepartement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabView tabView = sender as TabView;
            departementSelectionne = (Departement)tabView.SelectedItem;
            annees.Replace(GetAnnees(departementSelectionne.Id));
        }

        private void TabDepartement_TabClosing(object sender, Microsoft.Toolkit.Uwp.UI.Controls.TabClosingEventArgs e)
        {
            if(departements.Count() == 1)
            {
                Departement nouveauDepart = new Departement { Nom = "Nouveau département" };
                depart.create(nouveauDepart);
                departements.Add(nouveauDepart);
                Tabs.SelectedItem = nouveauDepart;
                departementSelectionne = nouveauDepart;
            }
            Departement departement = (Departement)e.Item;
            depart.delete(departement);
        }


        private void TabDepartement_Click(object sender, RoutedEventArgs e)
        {
            Departement departement = new Departement { Nom = "Nouveau département" };
            depart.create(departement);
            departements.Add(departement);
        }

        private void TreeView_LostFocus(object sender, RoutedEventArgs e)
        {
            nodeSelectionne = null;
            TreeView.SelectionMode = TreeViewSelectionMode.None;
            TreeView.SelectionMode = TreeViewSelectionMode.Single;
        }

        private void Reglages_Click(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ReglagesVue), new Reglages { Categorie = GetCategories(), TypeCours = GetTypeCours() });
        }

        private void TextBloxEC_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            departementSelectionne.Visibility = false;
        }

        private void TextBoxEC_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var source = (FrameworkElement)e.OriginalSource;
            departementSelectionne = (Departement)source.DataContext;

            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    departementSelectionne.Nom = textBox.Text;
                    depart.update(departementSelectionne.Id, (Departement)departementSelectionne);
                }
                departementSelectionne.Visibility = true;
            }
        }

        private void AutoSuggetBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            enseignantSelectionne = (Enseignant)args.ChosenSuggestion;
            Navigate(enseignantSelectionne.NavigationDestination, enseignantSelectionne);
        }
    }
}