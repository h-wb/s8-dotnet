using DAO;
using Dep_Gestion.Model;
using Dep_Gestion.Vues;
using Metier;
using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppGestion
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {


        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);


        private static DAO<Annee> annee = factoSQL.getAnneeDAO();
        private static DAO<PartieAnnee> partieAnnee = factoSQL.getPartieAnneeDAO();
        private static DAO<Departement> depart = factoSQL.getDepartementDAO();


        private static DAO<Enseignement> enseignement = factoSQL.getEnseignementDAO();
        private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();
        private static DAO<Categorie> categ = factoSQL.getCategorieDAO();
        private static DAO<Departement> dep = factoSQL.getDepartementDAO();


        private Departement<ItemDepartement> dpt = new Departement<ItemDepartement>();


        private ItemDepartement nodeSelectionneItem;

        public MainPage()
        {
            this.InitializeComponent();
            this.reload_Treeview();

        }

        public void reload_Treeview()
        {
            



            foreach (Annee annee in annee.findAll())
            {
                ItemDepartement nodeAnnee = new ItemDepartement { Text = annee.nom, Objet = annee, Children = new ObservableCollection<ItemDepartement>(), NavigationDestination = typeof(AnneeVue), NavigationParameter = annee};
                dpt.Add(nodeAnnee);
                foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                {
                    if (annee.id == partieAnnee.annee.id)
                    {
                        ItemDepartement nodePartieAnnee = new ItemDepartement { Text = partieAnnee.nom, Objet = partieAnnee, Children = new ObservableCollection<ItemDepartement>(), Parent = nodeAnnee, NavigationDestination= typeof(PartieAnneeVue) };
                        
                        /*Paramètres PartieAnnée*/
                        ArrayList paramsPartieAnnee = new ArrayList();
                        //Nom du parent
                        paramsPartieAnnee.Add(nodePartieAnnee.Parent.Text);
                        //Nom de la partie annee Selectionné
                        paramsPartieAnnee.Add(nodePartieAnnee.Text);
                        //Id de la partie année selectionné
                        paramsPartieAnnee.Add(partieAnnee.id);
                        //Id du parent
                        paramsPartieAnnee.Add(nodePartieAnnee.Parent.Objet.id);

                        nodePartieAnnee.NavigationParameter = paramsPartieAnnee;

                        nodeAnnee.Children.Add(nodePartieAnnee);
                        foreach (Enseignement enseignement in enseignement.findAll())
                        {
                            if (partieAnnee.id == enseignement.partAnnee.id)
                            {
                                ItemDepartement nodeEnseignement = new ItemDepartement { Text = enseignement.nom, Objet = enseignement, Children = new ObservableCollection<ItemDepartement>(), Parent = nodePartieAnnee };
                                nodePartieAnnee.Children.Add(nodeEnseignement);
                            }
                        }

                    }
                }
            }
        }

        private void TreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            nodeSelectionneItem = (ItemDepartement)args.InvokedItem;
            if (nodeSelectionneItem.NavigationDestination != null)
            {
                Navigate(nodeSelectionneItem.NavigationDestination, nodeSelectionneItem.NavigationParameter);
            }
        }

        public bool Navigate(Type sourcePageType, object parameter = null)
        {
            return Frame.Navigate(sourcePageType, parameter);
        }

        private void Frame_OnNavigated(object sender, NavigationEventArgs e)
        {
            TreeView.SelectionMode = TreeViewSelectionMode.None;
            TreeView.SelectionMode = TreeViewSelectionMode.Single;
        }

        private void Add_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionneItem == null)
            {

                Annee nouvelleAnnee = new Annee("Nouvelle annee", dep.find(1));
                annee.create(nouvelleAnnee);
                dpt.Add(new ItemDepartement { Text = nouvelleAnnee.nom, Objet = nouvelleAnnee });
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(Annee))
            {
                PartieAnnee nouvellePartieAnnee = new PartieAnnee("Nouveau semestre", (Annee)nodeSelectionneItem.Objet);
                partieAnnee.create(nouvellePartieAnnee);
                nodeSelectionneItem.Children.Add(new ItemDepartement { Text = nouvellePartieAnnee.nom, Objet = nouvellePartieAnnee, Parent = nodeSelectionneItem });
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(PartieAnnee))
            {
                Enseignement nouvelEnseignement = new Enseignement("Nouveau enseignement", (PartieAnnee)nodeSelectionneItem.Objet);
                enseignement.create(nouvelEnseignement);
                nodeSelectionneItem.Children.Add(new ItemDepartement { Text = nouvelEnseignement.nom, Objet = nouvelEnseignement, Parent = nodeSelectionneItem });
            }

        }

        private void Clear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (nodeSelectionneItem.Objet.GetType() == typeof(Annee))
            {
                annee.delete((Annee)nodeSelectionneItem.Objet);
                dpt.Remove(nodeSelectionneItem);
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(PartieAnnee))
            {

                partieAnnee.delete((PartieAnnee)nodeSelectionneItem.Objet);
                nodeSelectionneItem.Parent.Children.Remove(nodeSelectionneItem);
            }
            else if (nodeSelectionneItem.Objet.GetType() == typeof(Enseignement))
            {
                enseignement.delete((Enseignement)nodeSelectionneItem.Objet);
                nodeSelectionneItem.Parent.Children.Remove(nodeSelectionneItem);

            }



        }

    }
}