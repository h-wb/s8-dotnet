using DAO;
using Metier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
    public sealed partial class EnseignementVue : Page
    {
        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Enseignement> ens = factoSQL.getEnseignementDAO();
        private static DAO<EC> ecs = factoSQL.getECDAO();
        private ArrayList array = new ArrayList();

        private ObjetBase nodeSelectionne;
        public ObjetBase ecSelectionne;
        private Enseignement enseignementSelectionne;

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
                nodeSelectionne.Visibility = true;
                if (nodeSelectionne.GetType() == typeof(Enseignement))
                {
                    this.textBoxEnseignement.Text = nodeSelectionne.Nom;
                    this.textBoxDescription.Text = enseignementSelectionne.Description;

                    this.initialiserArray();
                }
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
            ecSelectionne = (EC)e.ClickedItem;
        }

        private void AjouterEC_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            EC nouvelleEc = new EC { Nom = "Nouvelle EC", Enseignement = (Enseignement)nodeSelectionne, Parent = nodeSelectionne };
            //array.Add(nouvelleEc);
            ecs.create(nouvelleEc);
            //nodeSelectionne.Children.Add(nouvelleEc);
        }

        private void SupprimerECTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //ecSelectionne.Parent.Children.Remove((EC)ecSelectionne);
            //array.RemoveAt(((int)ecSelectionne.Id) - 1);
            ecs.delete((EC)ecSelectionne);
        }


        //Temporaire
        public void initialiserArray()
        {
            using (SqlCommand command_f = new SqlCommand("SELECT * FROM ec WHERE id_enseignement=" + enseignementSelectionne.Id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            array.Add(new EC { Id=reader_f.GetInt32(0), Nom = reader_f.GetString(1), Enseignement = (Enseignement)nodeSelectionne, Parent = nodeSelectionne });
                        }
                    }

                }
            }
        }
    }
}
