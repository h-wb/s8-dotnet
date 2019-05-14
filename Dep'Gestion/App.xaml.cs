using AppGestion;
using DAO;
using Metier;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Dep_Gestion
{
    /// <summary>
    /// Fournit un comportement spécifique à l'application afin de compléter la classe Application par défaut.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initialise l'objet d'application de singleton.  Il s'agit de la première ligne du code créé
        /// à être exécutée. Elle correspond donc à l'équivalent logique de main() ou WinMain().
        /// </summary>
        /// 



        public App()
        {
            Connexion connexion = new Connexion();
            connexion.creerConnexion();

            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
            DAO<Categorie> categorie = factoSQL.getCategorieDAO();



            Categorie categorieDefaut = new Categorie { Id = 1, Nom = "Catégorie par défaut", Heures = 0 };

            if(!(categorie.find(categorieDefaut.Id).Id == 1))
                categorie.create(categorieDefaut);

            //TypeCours CM = new TypeCours { Id = 1, Nom = "CM", Groupes = 1};
            //if (!(typeCours.find(CM.Nom) is TypeCours))
            //    typeCours.create(CM);

            //TypeCours TD = new TypeCours { Id = 2, Nom = "TD", Groupes = 2 };
            //if (!(typeCours.find(TD.Nom) is TypeCours))
            //    typeCours.create(TD);

            //TypeCours TP = new TypeCours { Id = 3, Nom = "TP", Groupes = 2 };
            //if (!(typeCours.find(TP.Nom) is TypeCours))
            //    typeCours.create(TP);

            /*TypeCours tp = new TypeCours("TP", true);
            Categorie maitreDeConference = new Categorie("maitre de conférences", 240);
            Departement dpt = new Departement("Informatique");
            Departement dpt2 = new Departement("Informatique2");
            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);

            DAO<Departement> depart = factoSQL.getDepartementDAO();
            //depart.create(dpt2);

            DAO<Annee> an = factoSQL.getAnneeDAO();  
            an.create(new Annee("M1", depart.find(1)));
            an.create(new Annee("M2", depart.find(1)));
            an.create(new Annee("M3", depart.find(2)));

            DAO<PartieAnnee> pan = factoSQL.getPartieAnneeDAO();
            PartieAnnee test = new PartieAnnee("Semestre 1", an.find(1));
            pan.create(test);
            pan.create(new PartieAnnee("Semestre 2", an.find(1)));
            pan.create(new PartieAnnee("Semestre 3", an.find(2)));

            DAO<Enseignement> en = factoSQL.getEnseignementDAO();
              en.create(new Enseignement("EC1", pan.find(1)));

            //Console.WriteLine(maitreDeConference);
            //Console.ReadLine();
            DAO<Categorie> categorie = factoSQL.getCategorieDAO();
            categorie.create(new Categorie("maitre de conference", 130));


            DAO<Enseignant> ens = factoSQL.getEnseignantDAO();
            ens.create(new Enseignant("John", "Bob", categorie.find(1)));
            ens.create(new Enseignant("Bobby", "Malik", categorie.find(1)));
            ens.create(new Enseignant("Benoit", "Martin", categorie.find(1)));
            ens.create(new Enseignant("Make", "Crelo", categorie.find(1)));*/

            Connexion.getInstance().Close();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }


        /// <summary>
        /// Invoqué lorsque l'application est lancée normalement par l'utilisateur final.  D'autres points d'entrée
        /// seront utilisés par exemple au moment du lancement de l'application pour l'ouverture d'un fichier spécifique.
        /// </summary>
        /// <param name="e">Détails concernant la requête et le processus de lancement.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Ne répétez pas l'initialisation de l'application lorsque la fenêtre comporte déjà du contenu,
            // assurez-vous juste que la fenêtre est active
            if (rootFrame == null)
            {
                // Créez un Frame utilisable comme contexte de navigation et naviguez jusqu'à la première page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: chargez l'état de l'application précédemment suspendue
                }

                // Placez le frame dans la fenêtre active
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Quand la pile de navigation n'est pas restaurée, accédez à la première page,
                    // puis configurez la nouvelle page en transmettant les informations requises en tant que
                    // paramètre
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Vérifiez que la fenêtre actuelle est active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Appelé lorsque la navigation vers une page donnée échoue
        /// </summary>
        /// <param name="sender">Frame à l'origine de l'échec de navigation.</param>
        /// <param name="e">Détails relatifs à l'échec de navigation</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Appelé lorsque l'exécution de l'application est suspendue.  L'état de l'application est enregistré
        /// sans savoir si l'application pourra se fermer ou reprendre sans endommager
        /// le contenu de la mémoire.
        /// </summary>
        /// <param name="sender">Source de la requête de suspension.</param>
        /// <param name="e">Détails de la requête de suspension.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: enregistrez l'état de l'application et arrêtez toute activité en arrière-plan
            deferral.Complete();
        }
    }
}