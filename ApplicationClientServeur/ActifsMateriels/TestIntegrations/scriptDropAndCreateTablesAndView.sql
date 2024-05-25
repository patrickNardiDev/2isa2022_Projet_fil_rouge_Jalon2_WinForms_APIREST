DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_DETRUIT;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.users;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_SIROLE;
DROP table IF EXISTS GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD;

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD (
    Id bigint(20) unsigned not null unique auto_increment,
    Droit varbinary(8) not null unique CHECK (Nom <> ''),
    Nom varchar(10) not null CHECK (Nom <> ''),
    PRIMARY KEY (Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_SIROLE (
    Id bigint(20) unsigned not null unique auto_increment,
    Nom varchar(50) not null unique CHECK (Nom <> ''),
    Droit varbinary(8) not null unique CHECK (Nom <> ''),
    PRIMARY KEY (Id)
);

CREATE TABLE GESTION_TICKETS_TEST_INTEGRATION.users (
  id bigint(20) unsigned NOT NULL auto_increment,
  name varchar(255) NOT NULL,
  email varchar(255) NOT NULL,
  email_verified_at timestamp NULL DEFAULT NULL,
  password varchar(255) NOT NULL,
  two_factor_secret text DEFAULT NULL,
  two_factor_recovery_codes text DEFAULT NULL,
  remember_token varchar(100) DEFAULT NULL,
  created_at timestamp NULL DEFAULT NULL,
  updated_at timestamp NULL DEFAULT NULL,
  firstname varchar(255) NOT NULL,
  tel int(11) DEFAULT NULL,
  IdSIRole bigint(20) unsigned,
  PRIMARY KEY (id),
  UNIQUE KEY users_email_uniqu (email),
  KEY FK_SIRole_Id__Users (IdSIRole),
  CONSTRAINT FK_SIRole_Id__Users FOREIGN KEY (IdSIRole) REFERENCES D_SIROLE (Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE (
    Id bigint(20) unsigned not null unique auto_increment,
	Nom varchar(50) not null CHECK (Nom <> ''),
    Tel varchar(10),
    Email varchar(50) not null CHECK (Email <> ''),
    Archive boolean not null default false,
    LastModif timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT (
    Id bigint(20) unsigned not null unique auto_increment,
	Nom varchar(50) not null CHECK (Nom <> ''),
	Info varchar(100),
	DateDebut Datetime not null default sysdate(),
	DateFin Datetime not null default sysdate() CHECK (DateFin >= DateDebut) ,
	DateDerniereIntervention Datetime CHECK (DateDerniereIntervention >= DateDebut),
	DateProfaineIntervention Datetime CHECK (DateProfaineIntervention >= DateDerniereIntervention),
    IdEntreprise bigint(20) unsigned  not null,
    Archive boolean not null default false,
    LastModif timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id),
    FOREIGN KEY (IdEntreprise) REFERENCES D_ENTREPRISE(Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL (
    Id bigint(20) unsigned not null unique auto_increment,
	Nom varchar(50) not null CHECK (Nom <> ''),
	DateMiseEnService Datetime,
	DateFinGarantie Datetime,
    IdUser bigint(20) unsigned,
    IdMContrat bigint(20) unsigned,
    Archive boolean not null default false,
    LastModif timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id),
    FOREIGN KEY (IdUser) REFERENCES users(id),
    FOREIGN KEY (IdMContrat) REFERENCES D_MCONTRAT(Id)
    
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_DETRUIT (
    Id bigint(20) unsigned not null unique auto_increment,
	Nom varchar(50) not null CHECK (Nom <> ''),
    OldNumMarquage bigint(20) unsigned not null,
	DateMiseEnService Datetime,
	DateDestruction Datetime default sysdate(),
    IdMContrat bigint(20) unsigned,
    IdCategorie bigint(20) unsigned,
    ListCategories varchar(50),
    PRIMARY KEY (Id),
    FOREIGN KEY (IdMContrat) REFERENCES D_MCONTRAT(Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE (
    Id bigint(20) unsigned not null unique auto_increment,
	Nom varchar(50) not null unique CHECK (Nom <> ''),
    PRIMARY KEY (Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE (
    IdMateriel bigint(20) unsigned not null,
    IdCategorie bigint(20) unsigned not null,
    PRIMARY KEY (IdMateriel, IdCategorie),
    FOREIGN KEY (IdMateriel) REFERENCES D_MATERIEL(Id),
    FOREIGN KEY (IdCategorie) REFERENCES D_CATEGORIE(Id)
);

CREATE table GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE (
    IdDroitCrud bigint(20) unsigned not null,
    IdSIRole bigint(20) unsigned not null,
    PRIMARY KEY (IdDroitCrud, IdSIRole),
    FOREIGN KEY (IdDroitCrud) REFERENCES D_DROITCRUD(Id),
    FOREIGN KEY (IdSIRole) REFERENCES D_SIROLE(Id)
);

CREATE OR REPLACE VIEW GESTION_TICKETS_TEST_INTEGRATION.D_MaterielsInfos
AS 
SELECT 
	GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.Id AS "IdMat",
	GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.Nom AS "NomMat",
	GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.DateMiseEnService AS "DateMiseEnService",
    GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.DateFinGarantie AS "DateFinGarantie",
    GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.IdUser AS "IdUser",
    CONCAT(LEFT(GESTION_TICKETS_TEST_INTEGRATION.users.firstname  , 1),'.', ' ', GESTION_TICKETS_TEST_INTEGRATION.users.name) AS "NomUser",
    GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.IdMContrat AS "IdMContrat",
    GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT.Nom AS "NomContrat",
    REPLACE(GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT.Nom, GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT.Nom, 'Maintenance') AS "Contrat",
    GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.Archive AS "Archive"

FROM GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL left JOIN GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
    ON GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.IdMContrat = GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT.Id
    left JOIN GESTION_TICKETS_TEST_INTEGRATION.users
    ON GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.IdUser = GESTION_TICKETS_TEST_INTEGRATION.users.id
ORDER BY GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL.Nom ASC; 