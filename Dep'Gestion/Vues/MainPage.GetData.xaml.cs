using Metier;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestion
{
    public partial class MainPage
    {

        private ObservableCollectionExt<Departement> GetDepartements()
        {
            ObservableCollectionExt<Departement> departements = new ObservableCollectionExt<Departement>();
            foreach (Departement dpt in depart.findAll())
            {
                departements.Add(new Departement { Visibility = true, Id = dpt.Id, Nom = dpt.Nom.TrimEnd() });
            }
            return departements;
        }


        private ObservableCollectionExt<Enseignant> GetEnseignants()
        {
            ObservableCollectionExt<Enseignant> enseignants = new ObservableCollectionExt<Enseignant>();
            foreach (Enseignant ens in enseignant.findAll())
            {
                enseignants.Add(new Enseignant { Id = ens.Id, Categorie = ens.Categorie, Prenom = ens.Prenom.TrimEnd(), Nom = ens.Nom.TrimEnd(), NavigationDestination = typeof(EnseignantVue), TreeView = annees });
            }
            return enseignants;
        }



        private ObservableCollectionExt<ObjetBase> GetAnnees(int idDepartement)
        {
            ObservableCollectionExt<ObjetBase> annees = new ObservableCollectionExt<ObjetBase>();

            foreach (Annee annee in annee.findAll())
            {
                if (idDepartement == annee._departement.Id)
                {
                    Annee nodeAnnee = new Annee { Visibility = true, Id = annee.Id, Nom = annee.Nom.TrimEnd(), Description = annee._description, NavigationDestination = typeof(AnneeVue), Departement = annee.Departement, ListView = enseignants };
                    annees.Add(nodeAnnee);
                    foreach (PartieAnnee partieAnnee in partieAnnee.findAll())
                    {
                        if (annee.Id == partieAnnee.Annee.Id)
                        {
                            PartieAnnee nodePartieAnnee = new PartieAnnee { Visibility = true, Id = partieAnnee.Id, Nom = partieAnnee.Nom.TrimEnd(), Description = partieAnnee.Description, Annee = annee, Parent = nodeAnnee, NavigationDestination = typeof(PartieAnneeVue), ListView = enseignants };
                            nodeAnnee.Children.Add(nodePartieAnnee);

                            foreach (Enseignement enseignement in enseignement.findAll())
                            {
                                if (partieAnnee.Id == enseignement.PartieAnnee.Id)
                                {
                                    Enseignement nodeEnseignement = new Enseignement { Visibility = true, Id = enseignement.Id, Nom = enseignement.Nom.TrimEnd(), PartieAnnee = partieAnnee, Description = enseignement.Description, Parent = nodePartieAnnee, NavigationDestination = typeof(EnseignementVue), ListView = enseignants };
                                    nodePartieAnnee.Children.Add(nodeEnseignement);
                                }
                            }
                        }

                    }
                }
            }
            return annees;
        }

        private ObservableCollectionExt<Categorie> GetCategories()
        {
            ObservableCollectionExt<Categorie> categories = new ObservableCollectionExt<Categorie>();
            foreach (Categorie categorie in categ.findAll())
            {
                categories.Add(new Categorie { Id = categorie.Id, Nom = categorie.Nom.TrimEnd(), Heures = categorie.Heures });
            }
            return categories;
        }

        private ObservableCollectionExt<TypeCours> GetTypeCours()
        {
            ObservableCollectionExt<TypeCours> tCs = new ObservableCollectionExt<TypeCours>();
            foreach (TypeCours tC in tps.findAll())
            {
                tCs.Add(new TypeCours { Id = tC.Id, Nom = tC.Nom.TrimEnd(), Groupes = tC.Groupes });
            }
            return tCs;
        }
    }
}
