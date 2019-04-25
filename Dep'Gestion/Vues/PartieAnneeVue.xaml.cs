﻿using Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dep_Gestion.Vues
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PartieAnneeVue : Page
    {
        private TextBlockModel tbmodel { get; set; }
        private ButtonModel bmmodel { get; set; }
        private TextBoxModel tboxmodel { get; set; }

        public PartieAnneeVue()
        {
            this.InitializeComponent();

            this.tbmodel = new TextBlockModel();
            this.bmmodel = new ButtonModel { Content = "Ecrit dans la txtbox et appuie stp" };
            this.tboxmodel = new TextBoxModel();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.tbmodel.Text = e.Parameter.ToString();
            }

            base.OnNavigatedTo(e);
        }

        private void btnPartAnnee_Click(object sender, RoutedEventArgs e)
        {
            this.bmmodel.Content = txtbox.Text;
        }
    }
}