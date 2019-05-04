using DAO;
using Metier;
using Model;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using System;

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
        public TypeCours typeCoursSelectionne;
        public EquivalentTD equivalentTDSelectionne;
  

        private ObservableCollectionExt<Categorie> categories;
        public ObservableCollectionExt<EquivalentTD> equivalentTDs;
        public ObservableCollectionExt<ObjetBase> tCs;

        public EnseignantVue()
        {
            this.InitializeComponent();
            categories = GetCategories();
            tCs = GetTypeCours();

        }

        private ObservableCollectionExt<Categorie> GetCategories()
        {
            ObservableCollectionExt<Categorie> categories = new ObservableCollectionExt<Categorie>();
            foreach (Categorie categorie in categorie.findAll())
            {
                categories.Add(new Categorie { Id = categorie.Id, Nom = categorie.Nom.TrimEnd(), Heures = categorie.Heures });
            }
            categories.Add(new Categorie { Nom = "Créer une catégorie...", Heures = 0 });
            return categories;
        }

        private ObservableCollectionExt<ObjetBase> GetTypeCours()
        {
            ObservableCollectionExt<ObjetBase> tCs = new ObservableCollectionExt<ObjetBase>();
            foreach (TypeCours tC in typeCours.findAll())
            {
                tCs.Add(new TypeCours { Id = tC.Id, Nom = tC.Nom.TrimEnd(), Groupes = tC.Groupes });
            }
            tCs.Add(new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 });
            return tCs;
        }

        private ObservableCollectionExt<EquivalentTD> GetEquivalentTDs(Categorie categorieSelectionnee)
        {
            ObservableCollectionExt<EquivalentTD> equivalentTDs = new ObservableCollectionExt<EquivalentTD>();

            foreach (EquivalentTD eqTD in equivalentTD.findAll())

            {
                if (eqTD.Categorie.Id == categorieSelectionnee.Id)
                {
                    EquivalentTD equivalent = new EquivalentTD { Id = eqTD.Id, Categorie = categorieSelectionnee, TypeCours = eqTD.TypeCours, tCs = tCs, Nom = "", Ratio = eqTD.Ratio };
                    equivalentTDs.Add(equivalent);

                }
            }
            return equivalentTDs;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            enseignantSelectionne = (Enseignant)e.Parameter;
            enseignantSelectionne.Visibility = true;
            equivalentTDs = GetEquivalentTDs(enseignantSelectionne.Categorie);
            categoriesComboxBox.SelectedItem = categories.Where(p => p.Id == enseignantSelectionne.Categorie.Id).FirstOrDefault();


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

        private void TextBlockCategorie_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            enseignantSelectionne.Visibility = false;
            //textBoxPrenom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
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

            categorieSelectionne = (Categorie)e.AddedItems[0];

            if (categories.IndexOf(categorieSelectionne) == categories.Count - 1)
            {
                categorieSelectionne.Nom = "Nouvelle catégorie";  
                categorie.create(categorieSelectionne);
                enseignantSelectionne.Categorie = categorieSelectionne;
                enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
                Categorie nouvelleCategorie = new Categorie { Nom = "Créer une catégorie...", Heures = 0 };
                categories.Add(nouvelleCategorie); 
            }
            else
            {
                
                equivalentTDs.Replace(GetEquivalentTDs(categorieSelectionne));
                enseignantSelectionne.Categorie = categorieSelectionne;
                enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);

               
            } 

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
            equivalentTDSelectionne = (EquivalentTD)dataGrid.SelectedItem;
        }

        private void CategoriesComboxBox_CharacterReceived(Windows.UI.Xaml.UIElement sender, CharacterReceivedRoutedEventArgs args)
        {
            //Debug.WriteLine(args.Character);
            //categorieSelectionne.Nom = categoriesComboxBox.Text;
            //categorie.update(categorieSelectionne.Id, categorieSelectionne);

        }

        private void CategoriesComboxBox_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
        {

        }

        private void CategoriesComboxBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            //Debug.WriteLine(args.Character);
            //categorieSelectionne.Nom = categoriesComboxBox.Text;
            //categorie.update(categorieSelectionne.Id, categorieSelectionne);

        }

        private void TypeCoursSelectionne(object sender, SelectionChangedEventArgs e)
        {
            
            typeCoursSelectionne = (TypeCours)e.AddedItems[0];

            Debug.WriteLine(equivalentTDSelectionne.TypeCours);

            if (tCs.IndexOf(typeCoursSelectionne) == tCs.Count - 1 && equivalentTDSelectionne.TypeCours.Nom != "Choisir un type de cours")
            {
                typeCoursSelectionne.Nom = "Nouveau type de cours";
                typeCours.create(typeCoursSelectionne);
                //equivalentTDs.
                //enseignantSelectionne.Categorie = categorieSelectionne;
               // equivalentTD.GetType
                //equivalentTD.update()
                
               // enseignant.update(enseignantSelectionne.Id, enseignantSelectionne);
                TypeCours nouveauTypeCours = new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 };
                tCs.Add(nouveauTypeCours);
            }
            else if (equivalentTDs.Any(a => a.Categorie == categorieSelectionne && a.TypeCours.Id == typeCoursSelectionne.Id && a.TypeCours.Nom != equivalentTDSelectionne.TypeCours.Nom))
            {
                Debug.WriteLine("EXISTE DEJA");

            }
            else
            {
                equivalentTDSelectionne.TypeCours = typeCoursSelectionne;
                equivalentTD.update(equivalentTDSelectionne.Id, equivalentTDSelectionne);
            }
        }

        private void NomTypeCours(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            equivalentTDSelectionne.TypeCours.Nom = args.Text;
            typeCours.update(equivalentTDSelectionne.TypeCours.Id, equivalentTDSelectionne.TypeCours);
        }

        private void SupprimerEquivalentTD_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AjouterEquivalentTD_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TypeCours typeCoursRestant = (TypeCours)tCs.Where(a => !equivalentTDs.Any(b => b.TypeCours.Id == a.Id) && a.Id != -1).FirstOrDefault();
            Debug.WriteLine(typeCoursRestant);
            if(typeCoursRestant != null)
            {
                EquivalentTD nouveauEquivalentTD = new EquivalentTD { Categorie = enseignantSelectionne.Categorie, TypeCours = typeCoursRestant, Ratio = 1, tCs = tCs, Nom = "" };
                equivalentTD.create(nouveauEquivalentTD);
                equivalentTDs.Add(nouveauEquivalentTD);
            }


        }


        private void Ratio_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text != "")
            {
               equivalentTDSelectionne.Ratio = Convert.ToDouble(textBox.Text);
               equivalentTD.update(equivalentTDSelectionne.Id, equivalentTDSelectionne);
            }
        }

        private void TextBox_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }

        private void CategoriesComboxBox_DropDownOpened(object sender, object e)
        {
           
        }
    }
}
