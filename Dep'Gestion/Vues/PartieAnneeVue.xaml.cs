using Dep_Gestion.Model;
using Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections;
using DAO;
using Metier;
using System.Diagnostics;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PartieAnneeVue : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<PartieAnnee> pannee = factoSQL.getPartieAnneeDAO();

        private ObjetBase nodeSelectionne;
        private PartieAnnee partieAnneeSelectionne;

        public PartieAnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                nodeSelectionne = (ObjetBase)e.Parameter;
                partieAnneeSelectionne = (PartieAnnee)nodeSelectionne;
                nodeSelectionne.Visibility = true;
                if (nodeSelectionne.GetType() == typeof(Annee))
                {
                    //this.textBoxAnnee.Text = nodeSelectionne.Nom;
                    //this.textBoxDescription.Text = anneeSelectionne.Description;
                }
                base.OnNavigatedTo(e); ;

            }

            base.OnNavigatedTo(e);
        }

        private void TextBlockPartieAnnee_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            nodeSelectionne.Visibility = false;
            textBoxPartieAnnee.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBoxPartieAnnee_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                nodeSelectionne.Visibility = true;
            }
        }

        private void TextBoxPartieAnnee_LostFocus(object sender, RoutedEventArgs e)
        {
            nodeSelectionne.Visibility = true;
        }

        private void TextBoxPartieAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            partieAnneeSelectionne.Nom = this.textBoxPartieAnnee.Text;
            pannee.update(partieAnneeSelectionne.Id, new PartieAnnee(partieAnneeSelectionne.Nom, partieAnneeSelectionne.Annee, partieAnneeSelectionne.Description));
        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            partieAnneeSelectionne.Description = this.textBoxDescription.Text;
            pannee.update(partieAnneeSelectionne.Id, new PartieAnnee(partieAnneeSelectionne.Nom, partieAnneeSelectionne.Annee, partieAnneeSelectionne.Description.Replace("\'", "\'\'")));
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void AjouterEnseignement_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void SupprimerEnseignementTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}