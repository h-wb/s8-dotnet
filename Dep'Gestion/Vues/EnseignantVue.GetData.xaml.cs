using Metier;
using Model;
using System.Linq;

namespace AppGestion
{
    public partial class EnseignantVue
    {

        private ObservableCollectionExt<Categorie> GetCategories()
        {
            ObservableCollectionExt<Categorie> categories = new ObservableCollectionExt<Categorie>();
            foreach (Categorie categorie in categorie.findAll())
            {
                categories.Add(new Categorie { Id = categorie.Id, Nom = categorie.Nom.TrimEnd(), Heures = categorie.Heures });
            }
            categories.Add(new Categorie { Nom = "Créer une catégorie...", Heures = 0 });
            return categories;
        }

        private ObservableCollectionExt<ObjetBase> GetTypeCours()
        {
            ObservableCollectionExt<ObjetBase> tCs = new ObservableCollectionExt<ObjetBase>();
            foreach (TypeCours tC in typeCours.findAll())
            {
                tCs.Add(new TypeCours { Id = tC.Id, Nom = tC.Nom.TrimEnd(), Groupes = tC.Groupes });
            }
            tCs.Add(new TypeCours { Nom = "Créer un type de cours...", Groupes = 1 });
            return tCs;
        }

        private ObservableCollectionExt<EquivalentTD> GetEquivalentTDs(Categorie categorieSelectionnee)
        {
            ObservableCollectionExt<EquivalentTD> equivalentTDs = new ObservableCollectionExt<EquivalentTD>();

            foreach (EquivalentTD eqTD in equivalentTD.findAll())

            {
                if (eqTD.Categorie.Id == categorieSelectionnee.Id)
                {
                    EquivalentTD equivalent = new EquivalentTD { Id = eqTD.Id, Categorie = categorieSelectionnee, TypeCours = eqTD.TypeCours, tCs = tCs, Nom = "", Ratio = eqTD.Ratio };
                    equivalentTDs.Add(equivalent);

                }
            }
            return equivalentTDs;
        }

        private double GetHeures()
        {
            double nbHeures = 0;
            foreach (InfosAssignation infoAssignation in infoAssignations.findAll())
            {
                if (!(infoAssignation.Enseignant is null) & !(infoAssignation.TypeCours is null))
                {
                    foreach (EquivalentTD eqTD in equivalentTDs)
                    {
                        if (!(eqTD.TypeCours is null))
                        {
                            if (infoAssignation.TypeCours.Id == eqTD.TypeCours.Id && infoAssignation.Enseignant.Id == enseignantSelectionne.Id)
                            {
                                nbHeures += infoAssignation.NbHeures * eqTD.Ratio;
                            }
                        }
                    }
                }
            }
            return nbHeures;
        }

        private ObservableCollectionExt<Enseignement> GetEnseignementsAssignes()
        {
            ObservableCollectionExt<Enseignement> enseignementsAssignes = new ObservableCollectionExt<Enseignement>();
            foreach (InfosAssignation infoAssignation in infoAssignations.findAll())
            {
                if (!(infoAssignation.Enseignant is null) && infoAssignation.Enseignant.Id == enseignantSelectionne.Id)
                {
                    foreach(EC ec in eC.findAll())
                    {
                        if(infoAssignation.EC.Id == ec.Id && !enseignementsAssignes.Any(a => a.Id == ec.Enseignement.Id))
                        {
                            ObjetBase Annee = enseignantSelectionne.TreeView.Where(x => x.Id == annee.find(ec.Enseignement.PartieAnnee.Annee.Id).Id).FirstOrDefault();
                            ObjetBase PartieAnnee = Annee.Children.Where(x => x.Id == partieAnnee.find(ec.Enseignement.PartieAnnee.Id).Id).FirstOrDefault();
                            ObjetBase Enseignement = PartieAnnee.Children.Where(x => x.Id == ec.Enseignement.Id).FirstOrDefault();
                            enseignementsAssignes.Add((Enseignement)Enseignement);
                        }
                    }
                }
            }


            return enseignementsAssignes;
        }


        private string GetInformation(double heures)
        {

            string information = "Service complet";
            if (heures > enseignantSelectionne.Categorie.Heures)
            {
                information = "Sur-service de " + (heures - enseignantSelectionne.Categorie.Heures) + " heures";
            }
            else if (heures < enseignantSelectionne.Categorie.Heures)
            {
                information = "Sous-service de " + (enseignantSelectionne.Categorie.Heures - heures) + " heures";
            }
            return information;
        }

        private Service GetService()
        {
            Service service = new Service();
            service.Heures = GetHeures();
            service.Information = GetInformation(service.Heures);
            return service;
        }
    }
}
