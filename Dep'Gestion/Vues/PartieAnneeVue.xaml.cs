using DAO;
using Metier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class PartieAnneeVue : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();
        private static DAO<Enseignement> enseignement = factoSQL.getEnseignementDAO();

        private Enseignement enseignementSelect;
        private PartieAnnee partieAnneeSelect;

        public PartieAnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                partieAnneeSelect = (PartieAnnee)e.Parameter;
                partieAnneeSelect.Visibility = true;
                base.OnNavigatedTo(e); ;

            }
        }

        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return Frame.Navigate(sourcePageType, parameter);
        }

        private void TextBlockPartieAnnee_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            partieAnneeSelect.Visibility = false;
            textBoxPartieAnnee.Focus(FocusState.Programmatic);
            textBoxPartieAnnee.Select(textBoxPartieAnnee.Text.Length, 0);
        }

        private void TextBoxPartieAnnee_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    partieAnneeSelect.Nom = textBox.Text;
                    partieAnnee.update(partieAnneeSelect.Id, partieAnneeSelect);
                }
                partieAnneeSelect.Visibility = true;
            }
        }

        private void TextBoxPartieAnnee_LostFocus(object sender, RoutedEventArgs e)
        {
            partieAnneeSelect.Visibility = true;
        }


        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            partieAnneeSelect.Description = textBox.Text;
            partieAnnee.update(partieAnneeSelect.Id, partieAnneeSelect);
        }

        private void AjouterEnseignement_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Enseignement nouvelleEnseignement = new Enseignement { Nom = "Nouvel enseignement", PartieAnnee = partieAnneeSelect, Parent = partieAnneeSelect, NavigationDestination = typeof(EnseignementVue)};
            enseignement.create(nouvelleEnseignement);
            partieAnneeSelect.Children.Add(nouvelleEnseignement);
        }

        private void SupprimerEnseignementTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            enseignementSelect.Parent.Children.Remove(enseignementSelect);
            enseignement.delete(enseignementSelect);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            enseignementSelect = (Enseignement)e.ClickedItem;
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (enseignementSelect.NavigationDestination != null)
            {
                Navigate(enseignementSelect.NavigationDestination, enseignementSelect);
            }
        }
    }
}
