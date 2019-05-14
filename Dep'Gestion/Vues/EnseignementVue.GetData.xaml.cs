using DAO;
using Metier;
using Model;
using System.Linq;

namespace AppGestion
{
    public partial class EnseignementVue
    {
        private DAO<EC> EC = factoSQL.getECDAO();
        private DAO<InfosAssignation> InfosAssignation = factoSQL.getInfosAssignationDAO();
        private DAO<TypeCours> TypeCours = factoSQL.getTypeCoursDao();

        private ObservableCollectionExt<EC> GetECs(Enseignement enseignementSelectionne)
        {
            ObservableCollectionExt<EC> ECs = new ObservableCollectionExt<EC>();
            foreach (EC ec in EC.findAll())
            {
                if (enseignementSelectionne.Id == ec.Enseignement.Id)
                {
                    EC nouveauEC = new EC { Id = ec.Id, Nom = ec.Nom.TrimEnd(), Enseignement = ec.Enseignement, Children = new ObservableCollectionExt<ObjetBase>(), Visibility = true };
                    ECs.Add(nouveauEC);
                    foreach (InfosAssignation ia in InfosAssignation.findAll())
                    {
                        if (ec.Id == ia.EC.Id)
                        {
                            Enseignant enseignant = null;
                            if (!(ia.Enseignant is null))
                            {
                                enseignant = enseignementSelect.ListView.Where(x => x.Id == ia.Enseignant.Id).FirstOrDefault();
                            }
                                

                            nouveauEC.Children.Add(new InfosAssignation { Id = ia.Id, Nom = ia.Nom.TrimEnd(), EC = ia.EC, Enseignant = enseignant, TypeCours = ia.TypeCours, NbHeures = ia.NbHeures, Children = tCs, Enseignants = enseignementSelect.ListView, Parent = nouveauEC });
                        }
                    }
                }
            }
            return ECs;
        }


        private ObservableCollectionExt<ObjetBase> GetTypeCours()
        {
            ObservableCollectionExt<ObjetBase> tCs = new ObservableCollectionExt<ObjetBase>();
            foreach (TypeCours tC in TypeCours.findAll())
            {
                tCs.Add(new TypeCours { Id = tC.Id, Nom = tC.Nom.TrimEnd(), Groupes = tC.Groupes });
            }
            tCs.Add(new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 });
            return tCs;
        }

        private ObservableCollectionExt<ObjetBase> GetEnseignants()
        {
            ObservableCollectionExt<ObjetBase> Enseignants = new ObservableCollectionExt<ObjetBase>();
            foreach (Enseignant enseignant in enseignant.findAll())
            {
                Enseignants.Add(new Enseignant { Id = enseignant.Id, Nom = enseignant.Nom.TrimEnd(), Prenom = enseignant.Prenom.TrimEnd(), nbHeuresTravaillees = enseignant.nbHeuresTravaillees, Categorie = enseignant.Categorie });
            }
            return Enseignants;
        }

        private ObservableCollectionExt<Enseignant> GetEnseignantsAttribues(Enseignement enseignementSelectionne)
        {
            ObservableCollectionExt<Enseignant> Enseignants = new ObservableCollectionExt<Enseignant>();
            foreach (EC ec in EC.findAll())
            {
                if (enseignementSelectionne.Id == ec.Enseignement.Id)
                {
                    foreach (InfosAssignation ia in InfosAssignation.findAll())
                    {                
                        if (ec.Id == ia.EC.Id)
                        {
                            foreach(Enseignant enseignant in enseignant.findAll())
                            {
                                if (!(ia.Enseignant is null) && ia.Enseignant.Id == enseignant.Id && !Enseignants.Any(a => a.Id == enseignant.Id))
                                {
                                    Enseignants.Add(new Enseignant { Id = enseignant.Id, Nom = enseignant.Nom, Prenom = enseignant.Prenom, nbHeuresTravaillees = enseignant.nbHeuresTravaillees, Categorie = enseignant.Categorie });
                                }
                            }
                            }

                           
                    }
                }
            }
            return Enseignants;
        }

        
    }
}
