using DAO;
using Metier;
using System;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace AppGestion
{
    /// <summary>
    /// Vue de l'année
    /// </summary>
    public sealed partial class AnneeVue : Page
    {

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();

        private ObjetBase semestreSelect;
        private Annee anneeSelect;



        public AnneeVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            anneeSelect = (Annee)e.Parameter;
            anneeSelect.Visibility = true;
            base.OnNavigatedTo(e);
        }


        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return Frame.Navigate(sourcePageType, parameter);
        }

        private void TextBlockAnnee_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            anneeSelect.Visibility = false;
            textBoxAnnee.Focus(FocusState.Programmatic);
            textBoxAnnee.Select(textBoxAnnee.Text.Length, 0);
        }

        private void TextBoxAnnee_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            anneeSelect.Visibility = true;
        }

        private void TextBoxAnnee_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    anneeSelect.Nom = textBox.Text;
                    annee.update(anneeSelect.Id, anneeSelect);
                }
                anneeSelect.Visibility = true;
            }

        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            anneeSelect.Description = textBox.Text;
            annee.update(anneeSelect.Id, anneeSelect);
        }

        private void AjouterSemestre_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            PartieAnnee nouvellePartieAnnee = new PartieAnnee { Nom = "Nouveau semestre", Annee = anneeSelect, Parent = anneeSelect, NavigationDestination = typeof(PartieAnneeVue) };
            partieAnnee.create(nouvellePartieAnnee);
            anneeSelect.Children.Add(nouvellePartieAnnee);
        }

        private void SupprimerSemestre_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            semestreSelect.Parent.Children.Remove((PartieAnnee)semestreSelect);
            partieAnnee.delete((PartieAnnee)semestreSelect);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            semestreSelect = (PartieAnnee)e.ClickedItem;
        }

        private void ListView_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (semestreSelect.NavigationDestination != null)
            {
                Navigate(semestreSelect.NavigationDestination, semestreSelect);
            }
        }
    }

}

