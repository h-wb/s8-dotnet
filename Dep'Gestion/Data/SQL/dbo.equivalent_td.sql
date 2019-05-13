CREATE TABLE [dbo].[equivalent_td] (
    [id]                      INT  NOT NULL,
    [id_categorie_enseignant] INT  NOT NULL,
    [id_type_cours]           INT  NOT NULL,
    [ratio_cours_td]          REAL NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC, [id_type_cours] ASC, [id_categorie_enseignant] ASC)
);

