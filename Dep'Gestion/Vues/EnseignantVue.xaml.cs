using DAO;
using Metier;
using Model;
using System.Diagnostics;
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
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();
        private static DAO<Categorie> categorie = factoSQL.getCategorieDAO();
        private static DAO<EquivalentTD> equivalentTD = factoSQL.getEquivalentTDDao();
        private static DAO<TypeCours> typeCours = factoSQL.getTypeCoursDao();

        public Enseignant enseignantSelectionne;
        public Categorie categorieSelectionne;

        private ObservableCollectionExt<Categorie> categories;
        private ObservableCollectionExt<EquivalentTD> equivalentTDs;

        public EnseignantVue()
        {
            this.InitializeComponent();
            categories = GetCategories();
        }

        private ObservableCollectionExt<Categorie> GetCategories()
        {
            ObservableCollectionExt<Categorie> categories = new ObservableCollectionExt<Categorie>();
            foreach (Categorie categorie in categorie.findAll())
            {
                categories.Add(new Categorie { Id = categorie.Id, Nom = categorie.Nom, Heures = categorie.Heures });
            }
            return categories;
        }

        private ObservableCollectionExt<EquivalentTD> GetEquivalentTDs(Categorie categorieSelectionnee)
        {
            ObservableCollectionExt<EquivalentTD> equivalentTDs = new ObservableCollectionExt<EquivalentTD>();


            Debug.WriteLine(categorieSelectionnee);

            /*foreach (EquivalentTD eqTD in equivalentTD.findAll())
            {
                if (eqTD.idCategorieEnseignant == categorieSelectionne.Id)
                {

                    foreach (TypeCours typeCours in typeCours.findAll())
                    {
                        if (typeCours.Id == eqTD.idTypeCours)
                        {
                            Debug.WriteLine(eqTD.Id);
                            EquivalentTD equivalent = new EquivalentTD { Id = eqTD.Id, idCategorieEnseignant = eqTD.idCategorieEnseignant, TypeCours = typeCours, ratiosCoursTD = eqTD.ratiosCoursTD };
                        }
                    }
                }
            }*/
            foreach (EquivalentTD eqTD in equivalentTD.findAll())
            {
                if (eqTD.Categorie.Id == categorieSelectionne.Id)
                {

                    foreach (TypeCours typeCours in typeCours.findAll())
                    {
                        if (typeCours.Id == eqTD.TypeCours.Id)
                        {
                            Debug.WriteLine(eqTD.Id);
                            EquivalentTD equivalent = new EquivalentTD { Id = eqTD.Id, Categorie = eqTD.Categorie, TypeCours = typeCours, Ratio = eqTD.Ratio };
                        }
                    }
                }
            }
            return equivalentTDs;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            enseignantSelectionne = (Enseignant)e.Parameter;
            enseignantSelectionne.Visibility = true;
            if (enseignantSelectionne.GetType() == typeof(Enseignant))
            {
                this.textBoxPrenom.Text = enseignantSelectionne.Prenom;
                this.textBoxNom.Text = enseignantSelectionne.Nom;

            }
            base.OnNavigatedTo(e);
        }

        private void TextBlockNom_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            enseignantSelectionne.Visibility = false;
            textBoxNom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBlockPrenom_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            enseignantSelectionne.Visibility = false;
            textBoxPrenom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            enseignantSelectionne.Nom = this.textBoxNom.Text;
            enseignantSelectionne.Prenom = this.textBoxPrenom.Text;
            enseignant.update(enseignantSelectionne.Id, new Enseignant(enseignantSelectionne.Nom, enseignantSelectionne.Prenom));
        }

        private void TextBox_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            enseignantSelectionne.Visibility = true;
        }

        private void TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                enseignantSelectionne.Visibility = true;
            }
        }

        private void CategorieSelectionne(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            categorieSelectionne = (Categorie)comboBox.SelectedItem;
            equivalentTDs = GetEquivalentTDs(categorieSelectionne);
        }
    }
}
