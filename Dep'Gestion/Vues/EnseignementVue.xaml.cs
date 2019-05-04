using DAO;
using Metier;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class EnseignementVue : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Enseignement> ens = factoSQL.getEnseignementDAO();
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();

        private ArrayList array = new ArrayList();

        private ObjetBase nodeSelectionne;
        public ObjetBase ecSelectionne;
        public InfosAssignation infosAssignationSelectionne;
        private Enseignement enseignementSelectionne;

        private ObservableCollectionExt<InfosAssignation> infosAssignations = new ObservableCollectionExt<InfosAssignation>();
        private ObservableCollectionExt<EC> ECs = new ObservableCollectionExt<EC>();

        public EnseignementVue()
        {
            this.InitializeComponent();

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                nodeSelectionne = (ObjetBase)e.Parameter;
                enseignementSelectionne = (Enseignement)nodeSelectionne;
                Debug.WriteLine(enseignementSelectionne);
                nodeSelectionne.Visibility = true;
                if (nodeSelectionne.GetType() == typeof(Enseignement))
                {
                    this.textBoxEnseignement.Text = nodeSelectionne.Nom;
                    this.textBoxDescription.Text = enseignementSelectionne.Description;

                    // this.initialiserArray();
                }
                //  infosAssignations = GetInfosAssignations(enseignementSelectionne);
                ECs = GetECs(enseignementSelectionne);

                base.OnNavigatedTo(e); ;

            }

            base.OnNavigatedTo(e);


        }

        private void TextBoxEnseignement_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                nodeSelectionne.Visibility = true;
            }
        }

        private void TextBoxEnseignement_LostFocus(object sender, RoutedEventArgs e)
        {
            nodeSelectionne.Visibility = true;
        }

        private void TextBoxEnseignement_TextChanged(object sender, TextChangedEventArgs e)
        {
            enseignementSelectionne.Nom = this.textBoxEnseignement.Text;
            ens.update(enseignementSelectionne.Id, new Enseignement(enseignementSelectionne.Nom, enseignementSelectionne.PartieAnnee, enseignementSelectionne.Description));
        }

        private void TextBlockEnseignement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            nodeSelectionne.Visibility = false;
            textBoxEnseignement.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            enseignementSelectionne.Description = this.textBoxDescription.Text;
            ens.update(enseignementSelectionne.Id, new Enseignement(enseignementSelectionne.Nom, enseignementSelectionne.PartieAnnee, enseignementSelectionne.Description.Replace("\'", "\'\'")));
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
          //  infosAssignationSelectionne = (InfosAssignation)e.ClickedItem;
        }

        private void AjouterEC_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void SupprimerECTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }



        private void Enseignant_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
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

        private void Ajouter_InfosAssignation(object sender, RoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            ecSelectionne = (EC)source.DataContext;

            InfosAssignation nouvelleInfosAssignation = new InfosAssignation { Nom = "Nouveau cours", EC = (EC)ecSelectionne, Enseignant = null, TypeCours = null, NbHeures = 0, Children = GetTypeCours(), Enseignants = GetEnseignants() };
            InfosAssignation.create(nouvelleInfosAssignation);
            ecSelectionne.Children.Add(nouvelleInfosAssignation);

        }

        private void ModifierAssignationEnseignant(object sender, SelectionChangedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            Debug.WriteLine(infosAssignationSelectionne.Enseignant);
            Debug.WriteLine((Enseignant)e.AddedItems[0]);

            Enseignant enseignantSelectionne = (Enseignant)e.AddedItems[0];

            infosAssignationSelectionne.Enseignant = enseignantSelectionne;
            InfosAssignation.update(infosAssignationSelectionne.Id, infosAssignationSelectionne);

            //var source = (FrameworkElement)e.OriginalSource;
            //var enseignantSelectionne = (Enseignant)source.DataContext;

            //Debug.WriteLine(enseignantSelectionne);
        }

        private void InfosAssignationSelectionne(object sender, SelectionChangedEventArgs e)
        {
            infosAssignationSelectionne = (InfosAssignation)e.AddedItems[0];
            Debug.WriteLine(infosAssignationSelectionne);
        }
    }
}
