USE [master]
GO
/****** Object:  Database [ProjetDotnet]    Script Date: 12/05/2019 19:17:20 ******/
CREATE DATABASE [ProjetDotnet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjetDotnet', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProjetDotnet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjetDotnet_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProjetDotnet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProjetDotnet] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjetDotnet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjetDotnet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjetDotnet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjetDotnet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjetDotnet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjetDotnet] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjetDotnet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjetDotnet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjetDotnet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjetDotnet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjetDotnet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjetDotnet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjetDotnet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjetDotnet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjetDotnet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjetDotnet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjetDotnet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjetDotnet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjetDotnet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjetDotnet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjetDotnet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjetDotnet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjetDotnet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjetDotnet] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjetDotnet] SET  MULTI_USER 
GO
ALTER DATABASE [ProjetDotnet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjetDotnet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjetDotnet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjetDotnet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjetDotnet] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjetDotnet', N'ON'
GO
ALTER DATABASE [ProjetDotnet] SET QUERY_STORE = OFF
GO
USE [ProjetDotnet]
GO
/****** Object:  Table [dbo].[annee]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[annee](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[id_departement] [int] NOT NULL,
	[description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categorie_enseignant]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categorie_enseignant](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[heures_a_travailler] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departement]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departement](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ec]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ec](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[id_enseignement] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[enseignant]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enseignant](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[prenom] [char](250) NOT NULL,
	[nb_heures_assignees] [float] NOT NULL,
	[id_categorie_enseignant] [int] NOT NULL,
	[image] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[enseignement]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enseignement](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[id_partie_annee] [int] NOT NULL,
	[description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[enseignement_enseignant]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enseignement_enseignant](
	[id] [int] NOT NULL,
	[id_enseignement] [int] NOT NULL,
	[id_enseignant] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equivalent_td]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equivalent_td](
	[id] [int] NOT NULL,
	[id_categorie_enseignant] [int] NOT NULL,
	[id_type_cours] [int] NULL,
	[ratio_cours_td] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[infos_assignation]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[infos_assignation](
	[id] [int] NOT NULL,
	[nom] [char](250) NULL,
	[id_ec] [int] NULL,
	[id_typecours] [int] NULL,
	[id_enseignant] [int] NULL,
	[nb_heures] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[partie_annee]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partie_annee](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[id_annee] [int] NOT NULL,
	[description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[type_cours]    Script Date: 12/05/2019 19:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type_cours](
	[id] [int] NOT NULL,
	[nom] [char](250) NOT NULL,
	[has_groups] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[annee]  WITH CHECK ADD FOREIGN KEY([id_departement])
REFERENCES [dbo].[departement] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ec]  WITH CHECK ADD FOREIGN KEY([id_enseignement])
REFERENCES [dbo].[enseignement] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[enseignant]  WITH CHECK ADD FOREIGN KEY([id_categorie_enseignant])
REFERENCES [dbo].[categorie_enseignant] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[enseignement]  WITH CHECK ADD FOREIGN KEY([id_partie_annee])
REFERENCES [dbo].[partie_annee] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[enseignement_enseignant]  WITH CHECK ADD FOREIGN KEY([id_enseignement])
REFERENCES [dbo].[enseignement] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[enseignement_enseignant]  WITH CHECK ADD FOREIGN KEY([id_enseignant])
REFERENCES [dbo].[enseignant] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[equivalent_td]  WITH CHECK ADD FOREIGN KEY([id_categorie_enseignant])
REFERENCES [dbo].[categorie_enseignant] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[equivalent_td]  WITH CHECK ADD FOREIGN KEY([id_type_cours])
REFERENCES [dbo].[type_cours] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[infos_assignation]  WITH CHECK ADD FOREIGN KEY([id_ec])
REFERENCES [dbo].[ec] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[infos_assignation]  WITH CHECK ADD FOREIGN KEY([id_enseignant])
REFERENCES [dbo].[enseignant] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[infos_assignation]  WITH CHECK ADD FOREIGN KEY([id_typecours])
REFERENCES [dbo].[type_cours] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[partie_annee]  WITH CHECK ADD FOREIGN KEY([id_annee])
REFERENCES [dbo].[annee] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [ProjetDotnet] SET  READ_WRITE 
GO
