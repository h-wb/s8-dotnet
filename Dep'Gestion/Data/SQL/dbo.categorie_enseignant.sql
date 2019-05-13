CREATE TABLE [dbo].[categorie_enseignant] (
    [id]                  INT IDENTITY (1, 1) NOT NULL,
    [id_eqtd]             INT NOT NULL,
    [id_enseignant]       INT NOT NULL,
    [heures_a_travailler] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

