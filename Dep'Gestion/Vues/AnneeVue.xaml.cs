using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Dep_Gestion.Model;
using Windows.Devices.Input;
using System.Diagnostics;
using DAO;
using Metier;
using System;
using Outils;
using AppGestion;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dep_Gestion.Vues
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AnneeVue : Page
    {

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<Departement> dep = factoSQL.getDepartementDAO();

        private TextBlockModel tbmodel;
        private TextBlockModel tbmodelFlyout;
        private TextBoxModel tboxNom;
        private Annee anneeCourante;

        public AnneeVue()
        {
            this.InitializeComponent();

            tbmodel = new TextBlockModel();
            tboxNom = new TextBoxModel();
            tbmodelFlyout = new TextBlockModel();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                anneeCourante = (Annee)e.Parameter;

                this.tboxNom.Text = this.tbmodel.Text = anneeCourante.nom;

            }

            base.OnNavigatedTo(e);
        }

        //Si clic sur le bouton de Modification
        private void BtnModifAnnee_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Si la vérification est ok
            if (verif(sender)) { 

                //On recupère les informations de l'année courante
                Annee an = annee.find(this.tbmodel.Text);
                //On update l'année courante (annee.find(an.id)) par sa nouvelle valeur (new Annee(...))
                annee.update(annee.find(an.id), new Annee(uppercaseFirst(textboxNomVue.Text), dep.find(1)));//Remplacer dep.find(1) par dep.find(Id du département courant)

                //On met à jour la textbox de présentation dans la vue et on vide la textbox
                this.tbmodel.Text = uppercaseFirst(this.textboxNomVue.Text);
                this.tboxNom.Text = "";

            }

            //Mise à jour treeview ?

        }

        //Fonction de vérification du formulaire
        private bool verif(object sender)
        {
            var button = sender as Button;

            //Vérification Longueur de la chaîne
            if (textboxNomVue.Text.Trim().Length == 0)
            {
                tbmodelFlyout.Text = "Le nom doit faire au moins 1 caractère";
                MyFlyout.ShowAt(button);
                return false;
            }

            //On parcourt la liste des années et on vérifie si il existe une année identique dans la BDD (même nom + même département)
            foreach(Annee an in annee.findAll())
            {
                Annee anne = annee.find(textboxNomVue.Text);

                if (anne != null)
                {
                    if (anne.dep.id == anneeCourante.dep.id)
                    {
                        tbmodelFlyout.Text = "Cette annee existe déjà";
                        MyFlyout.ShowAt(button);
                        return false;
                    }
                }
            }

            return true;
        }

        //Temporaire
        public static string uppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

}

