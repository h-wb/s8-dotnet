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



        private ComboboxAnneeModel CAM;
        private PartieAnnee panneCourante;
        private Annee anneeParent;
        private ArrayList listeID;

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<PartieAnnee> pannee = factoSQL.getPartieAnneeDAO();

        public PartieAnneeVue()
        {
            this.InitializeComponent();


            CAM = new ComboboxAnneeModel();
            listeID = new ArrayList();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                //ArrayList parametres = (ArrayList)e.Parameter;
                //this.textblockAnnee.Text = (string)parametres[0];
                //this.tboxNom.Text = this.textblockPartieAnnee.Text = (string)parametres[1];
                
                //panneCourante = pannee.find((int)parametres[2]);
                //anneeParent = annee.find((int)parametres[3]);

                //foreach (Annee an in annee.findAll())
                //{
                //    if (an.Departement.Id == anneeParent.Departement.Id)
                //    {
                //        listeID.Add(an.Id);
                //        CAM.Items.Add(an.Nom);
                //    }
                //}

            }

            base.OnNavigatedTo(e);
        }

        private void BtnModifAnnee_Click(object sender, RoutedEventArgs e)
        {

            if (verif(sender))
            {
                ////On update la partie année courante
                //pannee.update(pannee.find(panneCourante.Id), new PartieAnnee(textboxNomVue.Text, annee.find((int)listeID[this.comboboxAnnee.SelectedIndex])));

                ////On met à jour la textbox de présentation dans la vue et on vide la textbox
                //Debug.WriteLine("Bloup");
                //Debug.WriteLine(this.comboboxAnnee.Text);
                //this.textblockAnnee.Text = this.comboboxAnnee.Text;
                //this.textblockPartieAnnee.Text = this.textboxNomVue.Text;
                //this.tboxNom.Text = "";
                //this.comboboxAnnee.SelectedIndex = 0;
            }
        }

        //Fonction de vérification du formulaire
        private bool verif(object sender)
        {
            var button = sender as Button;

            //Vérification Longueur de la chaîne
            if (textboxNomVue.Text.Trim().Length == 0)
            {
                //tbmodelFlyout.Text = "Le nom doit faire au moins 1 caractère";
                //MyFlyout.ShowAt(button);
                //return false;
            }

            //On parcourt la liste des années et on vérifie si il existe une année identique dans la BDD (même nom + même département)
            foreach (PartieAnnee pan in pannee.findAll())
            {
                //if ((this.textboxNomVue.Text.Trim() == pan.Nom.Trim())&&((int)listeID[this.comboboxAnnee.SelectedIndex] == pan.Annee.Id))
                //{
                    //tbmodelFlyout.Text = "Cette partie année existe déjà";
                    //MyFlyout.ShowAt(button);
                    //return false;
                //}
            }

            return true;
        }
    }
}