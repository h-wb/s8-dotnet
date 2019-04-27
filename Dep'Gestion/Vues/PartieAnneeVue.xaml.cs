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

        public PartieAnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                nodeSelectionne = (ObjetBase)e.Parameter;
                if (e.Parameter != null)
                {
                    this.textboxNomVue.Text = nodeSelectionne.Nom;

                }
                base.OnNavigatedTo(e);

            }

            base.OnNavigatedTo(e);
        }

        private void TextboxNomVue_TextChanged(object sender, TextChangedEventArgs e)
        {
            nodeSelectionne.Nom = this.textboxNomVue.Text;
            pannee.update(nodeSelectionne.Id, new PartieAnnee(nodeSelectionne.Nom, (Annee)nodeSelectionne.Parent));
        }
    }
}