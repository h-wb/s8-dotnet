using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DAO;
using Metier;
using System.Diagnostics;
using System.Text;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AnneeVue : Page
    {

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<Departement> dep = factoSQL.getDepartementDAO();
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();

        public ObjetBase nodeSelectionne;
        public ObjetBase semestreSelectionne;
        public Annee anneeSelectionne;



        public AnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {     
            nodeSelectionne = (ObjetBase)e.Parameter;
            anneeSelectionne = (Annee)nodeSelectionne;
            nodeSelectionne.Visibility = true;
            if (nodeSelectionne.GetType() == typeof(Annee))
            {
                this.textBoxAnnee.Text = nodeSelectionne.Nom;
                this.textBoxDescription.Text = anneeSelectionne.Description;
            } 
            base.OnNavigatedTo(e);
        }

        private void TextBoxAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            nodeSelectionne.Nom = this.textBoxAnnee.Text;
            Debug.WriteLine(nodeSelectionne.Nom);
            annee.update(anneeSelectionne.Id, new Annee(nodeSelectionne.Nom, anneeSelectionne.Departement, anneeSelectionne.Description.Replace("\'", "\'\'")));
        }

        
        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            anneeSelectionne.Description = this.textBoxDescription.Text;
            annee.update(anneeSelectionne.Id, new Annee(nodeSelectionne.Nom, anneeSelectionne.Departement, anneeSelectionne.Description.Replace("\'", "\'\'")));

        }

        private void TextBlock_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            nodeSelectionne.Visibility = false;
            textBoxAnnee.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBoxAnnee_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            nodeSelectionne.Visibility = true;
        }

        private void TextBoxAnnee_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                nodeSelectionne.Visibility = true;
            }
        }

        private void AjouterSemestre_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            PartieAnnee nouvellePartieAnnee = new PartieAnnee { Nom = "Nouveau semestre", Annee = (Annee)nodeSelectionne, Parent = nodeSelectionne };
            partieAnnee.create(nouvellePartieAnnee);
            nodeSelectionne.Children.Add(nouvellePartieAnnee);
        }

        private void SupprimerSemestre_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            semestreSelectionne.Parent.Children.Remove((PartieAnnee)semestreSelectionne);
            partieAnnee.delete((PartieAnnee)semestreSelectionne);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            semestreSelectionne = (PartieAnnee)e.ClickedItem;
            Debug.WriteLine(semestreSelectionne);
        }
    }

}

