using DAO;
using Metier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PartieAnneeVueReset : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<PartieAnnee> pannee = factoSQL.getPartieAnneeDAO();
        private static DAO<Enseignement> ens = factoSQL.getEnseignementDAO();

        private ObjetBase nodeSelectionne;
        public ObjetBase enseignementSelectionne;
        private PartieAnnee partieAnneeSelectionne;

        public PartieAnneeVueReset()
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
                if (nodeSelectionne.GetType() == typeof(PartieAnnee))
                {
                    this.textBoxPartieAnnee.Text = nodeSelectionne.Nom;
                    this.textBoxDescription.Text = partieAnneeSelectionne.Description;
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
            enseignementSelectionne = (Enseignement)e.ClickedItem;
        }

        private void AjouterEnseignement_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Enseignement nouvelleEnseignement = new Enseignement { Nom = "Nouvelle Enseignement", PartieAnnee = (PartieAnnee)nodeSelectionne, Parent = nodeSelectionne };
            ens.create(nouvelleEnseignement);
            nodeSelectionne.Children.Add(nouvelleEnseignement);
        }

        private void SupprimerEnseignementTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            enseignementSelectionne.Parent.Children.Remove((Enseignement)enseignementSelectionne);
            ens.delete((Enseignement)enseignementSelectionne);
        }
    }
}
