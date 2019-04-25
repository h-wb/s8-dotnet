using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Model;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dep_Gestion.Vues
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AnneeVue : Page
    {

        private TextBlockModel tbmodel;

        public AnneeVue()
        {
            this.InitializeComponent();

            tbmodel = new TextBlockModel();
            DataContext = tbmodel;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {

                this.tbmodel.Text = e.Parameter.ToString();

            }

            base.OnNavigatedTo(e);
        }
    }

}

