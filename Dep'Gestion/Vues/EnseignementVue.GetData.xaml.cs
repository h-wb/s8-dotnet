using DAO;
using Metier;
using Model;
using System.Diagnostics;
using System.Linq;

namespace AppGestion
{
    public partial class EnseignementVue
    {
        private DAO<EC> EC = factoSQL.getECDAO();
        private DAO<InfosAssignation> InfosAssignation = factoSQL.getInfosAssignationDAO();
        private DAO<TypeCours> TypeCours = factoSQL.getTypeCoursDao();
        //private static DAO<Enseignant> enseignant = factoSQL.getEnseignantDAO();

        private ObservableCollectionExt<EC> GetECs(Enseignement enseignementSelectionne)
        {
            ObservableCollectionExt<EC> ECs = new ObservableCollectionExt<EC>();
            foreach (EC ec in EC.findAll())
            {
                if (enseignementSelectionne.Id == ec.Enseignement.Id)
                {
                    EC nouveauEC = new EC { Id = ec.Id, Nom = ec.Nom, Enseignement = ec.Enseignement, Children = new ObservableCollectionExt<ObjetBase>() };
                    ECs.Add(nouveauEC);
                    foreach (InfosAssignation ia in InfosAssignation.findAll())
                    {
                        if (ec.Id == ia.EC.Id)
                        {
                            InfosAssignation nouvelleInfoAssignation = new InfosAssignation { Id = ia.Id, Nom = ia.Nom, EC = ia.EC, Enseignant = ia.Enseignant, TypeCours = ia.TypeCours, NbHeures = ia.NbHeures, Children = new ObservableCollectionExt<ObjetBase>() };
                            nouveauEC.Children.Add(nouvelleInfoAssignation);
                            foreach (TypeCours tC in TypeCours.findAll())
                            {
                                nouvelleInfoAssignation.Children.Add(new TypeCours { Id = tC.Id, Nom = tC.Nom.TrimEnd(), Groupes = tC.Groupes });
                            }

                            //foreach (Enseignant enseignant in enseignant.findAll())
                            //{
                            //    //  nouvelleInfoAssignation.Enseignants.Add(new Enseignant {Id = enseignant.Id, Nom = enseignant.Nom, Prenom = enseignant.Prenom, nbHeuresTravaillees = enseignant.nbHeuresTravaillees, Categorie = enseignant.Categorie });
                            //}
                        }
                    }
                }
            }
            return ECs;
        }
    }
}
