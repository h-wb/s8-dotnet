using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DAO;
using Metier;


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

        public ObjetBase nodeSelectionne;

        public AnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {     
            nodeSelectionne = (ObjetBase)e.Parameter;
            if (e.Parameter != null)
            {
                this.textboxNomVue.Text = nodeSelectionne.Nom;

            }
            base.OnNavigatedTo(e);
        }

        private void TextboxNomVue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Annee anneeSelectionne = (Annee)nodeSelectionne;
            anneeSelectionne.Nom = this.textboxNomVue.Text;
            annee.update(annee.find(anneeSelectionne.Id), new Annee(anneeSelectionne.Nom, anneeSelectionne.Departement));
        }
    }

}

