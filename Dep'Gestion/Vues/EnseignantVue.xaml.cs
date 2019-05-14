using DAO;
using Metier;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Model;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EnseignantVue : Page
    {
        private const bool Collapsed = true;
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();
        private static DAO<Categorie> categorie = factoSQL.getCategorieDAO();
        private static DAO<EquivalentTD> equivalentTD = factoSQL.getEquivalentTDDao();
        private static DAO<TypeCours> typeCours = factoSQL.getTypeCoursDao();
        private static DAO<InfosAssignation> infoAssignations = factoSQL.getInfosAssignationDAO();
        private static DAO<EC> eC = factoSQL.getECDAO();
        private static DAO<Enseignement> enseignement = factoSQL.getEnseignementDAO();
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();
        private static DAO<Annee> annee = factoSQL.getAnneeDAO();

        public Enseignant enseignantSelectionne;
        public Categorie categorieSelectionne;
        public TypeCours typeCoursSelectionne;
        public EquivalentTD equivalentTDSelectionne;
        public Enseignement enseignementSelect;
        public Service service;






        private ObservableCollectionExt<Categorie> categories;
        public ObservableCollectionExt<EquivalentTD> equivalentTDs;
        public ObservableCollectionExt<ObjetBase> tCs;
        public ObservableCollectionExt<Enseignement> enseignementsAssignes;


        public EnseignantVue()
        {
            this.InitializeComponent();
            categories = GetCategories();
            tCs = GetTypeCours();
        }











        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            enseignantSelectionne = (Enseignant)e.Parameter;

            if(enseignantSelectionne.Categorie is null)
            {
                enseignantSelectionne.Categorie = categories[0];
            }
            equivalentTDs = GetEquivalentTDs(enseignantSelectionne.Categorie);
            service = GetService();
            enseignementsAssignes = GetEnseignementsAssignes();
            enseignantSelectionne.Visibility = true;

            categoriesComboxBox.SelectedItem = categories.Where(p => p.Id == enseignantSelectionne.Categorie.Id).FirstOrDefault();


            if (enseignantSelectionne.GetType() == typeof(Enseignant))
            {
                this.textBoxPrenom.Text = enseignantSelectionne.Prenom;
                this.textBoxNom.Text = enseignantSelectionne.Nom;

            }
            base.OnNavigatedTo(e);
        }

        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return Frame.Navigate(sourcePageType, parameter);
        }

        private void TextBlockNom_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Visibility = Visibility.Collapsed;
            textBoxNom.Visibility = Visibility.Visible;
            textBoxNom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBlockPrenom_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Visibility = Visibility.Collapsed;
            textBoxPrenom.Visibility = Visibility.Visible;
            textBoxPrenom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBoxNom_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Visibility = Visibility.Collapsed;
            textBlockNom.Visibility = Visibility.Visible;
        }

        private void TextBoxPrenom_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Visibility = Visibility.Collapsed;
            textBlockPrenom.Visibility = Visibility.Visible;
        }

        private void TextBoxNom_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    enseignantSelectionne.Nom = textBox.Text;
                    enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
                }
                textBox.Visibility = Visibility.Collapsed;
                textBlockNom.Visibility = Visibility.Visible;
            }
        }

        private void TextBoxPrenom_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    enseignantSelectionne.Prenom = textBox.Text;
                    enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
                }
                textBox.Visibility = Visibility.Collapsed;
                textBlockPrenom.Visibility = Visibility.Visible;
            }
        }

        private void ComboBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            categorieSelectionne = (Categorie)comboBox.SelectedItem;
            if (categorieSelectionne != null && categorieSelectionne.Id == -1)
            {
                categorieSelectionne.Nom = "Nouvelle catégorie";
                categorie.create(categorieSelectionne);
                enseignantSelectionne.Categorie = categorieSelectionne;
                enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
                categories.Add(new Categorie { Nom = "Créer une catégorie...", Heures = 0 });
            }
            else
            {
                equivalentTDs.Replace(GetEquivalentTDs(categorieSelectionne));
                enseignantSelectionne.Categorie = categorieSelectionne;
                enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
            }
        }


        private void ComboBoxTypeCours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            typeCoursSelectionne = (TypeCours)comboBox.SelectedItem;
            if (typeCoursSelectionne.Id == -1)
            {
                typeCoursSelectionne.Nom = "Nouveau type de cours";
                typeCours.create(typeCoursSelectionne);
                equivalentTDSelectionne.TypeCours = typeCoursSelectionne;
                equivalentTD.update(equivalentTDSelectionne.Id, equivalentTDSelectionne);
                tCs.Add(new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 });
            }
            else
            {
                equivalentTDSelectionne.TypeCours = typeCoursSelectionne;
                equivalentTD.update(equivalentTDSelectionne.Id, equivalentTDSelectionne);
            }

            service.Heures = GetHeures();
            service.Information = GetInformation(service.Heures);
        }

        private void NomCategorie(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            categorieSelectionne.Nom = args.Text;
            enseignantSelectionne.Categorie = categorieSelectionne;
            categorie.update(categorieSelectionne.Id, categorieSelectionne);
            enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            equivalentTDSelectionne = (EquivalentTD)dataGrid.SelectedItem;

        }

        private void NomTypeCours(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            equivalentTDSelectionne.TypeCours.Nom = args.Text;
            typeCours.update(equivalentTDSelectionne.TypeCours.Id, equivalentTDSelectionne.TypeCours);


        }

        private void SupprimerEquivalentTD_Tapped(object sender, TappedRoutedEventArgs e)
        {
            equivalentTD.delete(equivalentTDSelectionne);
            equivalentTDs.Remove(equivalentTDSelectionne);

        }

        private void AjouterEquivalentTD_Tapped(object sender, TappedRoutedEventArgs e)
        {

            EquivalentTD nouveauEquivalentTD = new EquivalentTD { Categorie = enseignantSelectionne.Categorie, TypeCours = null, Ratio = 1, tCs = tCs, Nom = "" };
            equivalentTD.create(nouveauEquivalentTD);
            equivalentTDs.Add(nouveauEquivalentTD);



        }



        private void Ratio_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    equivalentTDSelectionne.Ratio = Convert.ToDouble(textBox.Text);
                    equivalentTD.update(equivalentTDSelectionne.Id, equivalentTDSelectionne);
                }
            }

            service.Heures = GetHeures();
            service.Information = GetInformation(service.Heures);
        }

        private void TextBox_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }



        private void TextBoxHeures_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {

                enseignantSelectionne.Categorie.Heures = Convert.ToDouble(textBox.Text);
                categorie.update(enseignantSelectionne.Categorie.Id, enseignantSelectionne.Categorie);
            }
            service.Heures = GetHeures();
            service.Information = GetInformation(service.Heures);

        }

        private void TextBoxHeures_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => c != ',' & !char.IsDigit(c));
        }

        private void EnseignementsAssignes_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (enseignementSelect != null)
            {
                Navigate(enseignementSelect.NavigationDestination, enseignementSelect);
            }
        }

        private void EnseignementsAssignes_ItemClick(object sender, ItemClickEventArgs e)
        {
            enseignementSelect = (Enseignement)e.ClickedItem;
        }

    }
}
