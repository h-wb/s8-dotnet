using DAO;
using Metier;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Model;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EnseignementVue : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Enseignement> enseignement = factoSQL.getEnseignementDAO();
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();

        private ArrayList array = new ArrayList();

        private EC ecSelect;
        private InfosAssignation infosAssignationSelect;
        private Enseignement enseignementSelect;

        private ObservableCollectionExt<InfosAssignation> infosAssignations = new ObservableCollectionExt<InfosAssignation>();
        private ObservableCollectionExt<EC> ECs = new ObservableCollectionExt<EC>();
        private ObservableCollectionExt<ObjetBase> tCs = new ObservableCollectionExt<ObjetBase>();
        private ObservableCollectionExt<ObjetBase> enseignants = new ObservableCollectionExt<ObjetBase>();
        private ObservableCollectionExt<Enseignant> enseignantsAttribues = new ObservableCollectionExt<Enseignant>();

        public EnseignementVue()
        {
            this.InitializeComponent();
            tCs = GetTypeCours();
            enseignants = GetEnseignants();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                enseignementSelect = (Enseignement)e.Parameter;
                enseignementSelect.Visibility = true;
                ECs = GetECs(enseignementSelect);
                enseignantsAttribues = GetEnseignantsAttribues(enseignementSelect);
                base.OnNavigatedTo(e); ;
            }
        }


        private void TextBlockEnseignement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            enseignementSelect.Visibility = false;
            textBoxEnseignement.Focus(FocusState.Programmatic);
            textBoxEnseignement.Select(textBoxEnseignement.Text.Length, 0);
        }

        private void TextBoxEnseignement_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    enseignementSelect.Nom = textBox.Text;
                    enseignement.update(enseignementSelect.Id, enseignementSelect);
                }
                enseignementSelect.Visibility = true;
            }

        }

        private void TextBoxEnseignement_LostFocus(object sender, RoutedEventArgs e)
        {
            enseignementSelect.Visibility = true;
        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            enseignementSelect.Description = textBox.Text;
            enseignement.update(enseignementSelect.Id, enseignementSelect);
        }

        private void Enseignant_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Enseignant_Drop(object sender, DragEventArgs e)
        {

            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var source = sender as Grid;
                InfosAssignation infosAssignation = source.DataContext as InfosAssignation;

                var id = await e.DataView.GetTextAsync();
                infosAssignation.Enseignant = enseignant.find(Convert.ToInt32(id));
                InfosAssignation.update(infosAssignation.Id, infosAssignation);
            }
        }

        private void AjouterInfosAssignation_Button(object sender, RoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            ecSelect = (EC)source.DataContext;

            InfosAssignation nouvelleInfosAssignation = new InfosAssignation { Nom = "Nouveau cours", EC = ecSelect, Enseignant = null, TypeCours = null, NbHeures = 0, Children = tCs, Enseignants = enseignants };
            InfosAssignation.create(nouvelleInfosAssignation);
            ecSelect.Children.Add(nouvelleInfosAssignation);

        }

        private void SupprimerInfosAssignation_Button(object sender, RoutedEventArgs e)
        {
            if (infosAssignationSelect != null)
            {
                InfosAssignation.delete(infosAssignationSelect);
                ecSelect.Children.Remove(infosAssignationSelect);
            }
        }

        private void TextBloxEC_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ecSelect.Visibility = false;
        }

        private void TextBoxEC_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var source = (FrameworkElement)e.OriginalSource;
            ecSelect = (EC)source.DataContext;

            if (e.Key == VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    ecSelect.Nom = textBox.Text;
                    EC.update(ecSelect.Id, ecSelect);
                }
                ecSelect.Visibility = true;
            }
        }

        private void TabEC_Click(object sender, RoutedEventArgs e)
        {
            EC nouvelEc = new EC { Nom = "Nouvel EC", Enseignement = enseignementSelect };
            EC.create(nouvelEc);
            ECs.Add(nouvelEc);
        }

        private void TabEC_TabClosing(object sender, Microsoft.Toolkit.Uwp.UI.Controls.TabClosingEventArgs e)
        {
            EC ec = (EC)e.Item;
            EC.delete(ec);
        }

        private void TabEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabView tabView = sender as TabView;
            ecSelect = (EC)tabView.SelectedItem;
        }

        private void DataGridIA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            infosAssignationSelect = (InfosAssignation)dataGrid.SelectedItem;
            Debug.WriteLine(infosAssignationSelect);


        }

        private void ComboBoxTypeCours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            TypeCours typeCoursSelectionne = (TypeCours)comboBox.SelectedItem;
            if (typeCoursSelectionne.Id == -1)
            {
                typeCoursSelectionne.Nom = "Nouveau type de cours";
                TypeCours.create(typeCoursSelectionne);
                infosAssignationSelect.TypeCours = typeCoursSelectionne;
                InfosAssignation.update(infosAssignationSelect.Id, infosAssignationSelect);
                tCs.Add(new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 });
            }
            else
            {
                infosAssignationSelect.TypeCours = typeCoursSelectionne;
                InfosAssignation.update(infosAssignationSelect.Id, infosAssignationSelect);
            }
        }

        private void ComboBoxTypeCours_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            infosAssignationSelect.TypeCours.Nom = args.Text;
            TypeCours.update(infosAssignationSelect.TypeCours.Id, infosAssignationSelect.TypeCours);
        }


        private void ModidiferNomCours_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    infosAssignationSelect.Nom = textBox.Text;
                    InfosAssignation.update(infosAssignationSelect.Id, infosAssignationSelect);
                }
            }
        }

        private void ModifierNbHeures_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    infosAssignationSelect.NbHeures = Convert.ToDouble(textBox.Text);
                    InfosAssignation.update(infosAssignationSelect.Id, infosAssignationSelect);
                }
            }
        }

        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }


        private void ComboBoxEnseignant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Enseignant enseignantSelectionne = (Enseignant)comboBox.SelectedItem;
            infosAssignationSelect.Enseignant = enseignantSelectionne;
            InfosAssignation.update(infosAssignationSelect.Id, infosAssignationSelect);
            enseignantsAttribues.Replace(GetEnseignantsAttribues(enseignementSelect));
        }


    }
}
