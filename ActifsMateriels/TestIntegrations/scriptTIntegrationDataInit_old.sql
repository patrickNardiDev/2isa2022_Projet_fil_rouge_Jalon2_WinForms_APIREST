-- SCRIPT FOR CREATE TABLE TO INTEGRATION TEST

-- USE GESTION_TICKETS_TEST_INTEGRATION; -- Erreur : Nom de la base non cohérente. Nom issu phase 1. => Dette technique TODO

-- désactivation des contraintes
-- SET foreign_key_checks = 0;

-- supression de toutes les tables
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


-- Création des tables
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

-- numéro de marquage = Id
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

-- activation des contraintes

-- INSERT DATA

-- D_DROITCRUD
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD
(Id, Droit, Nom)
VALUES(1, 0x30303030, 'no');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD
(Id, Droit, Nom)
VALUES(2, 0x30303031, 'read');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD
(Id, Droit, Nom)
VALUES(3, 0x30303130, 'create');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD
(Id, Droit, Nom)
VALUES(4, 0x30313030, 'update');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD
(Id, Droit, Nom)
VALUES(5, 0x31303030, 'delete');

-- D_SIROLE
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_SIROLE
(Id, Nom, Droit)
VALUES(1, 'consultation', 0x30303031);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_SIROLE
(Id, Nom, Droit)
VALUES(2, 'gestion', 0x31313131);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_SIROLE
(Id, Nom, Droit)
VALUES(3, 'norole', 0x30303030);

-- D_DROITCRUD_SIROLE
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(1, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(1, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(2, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(2, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(3, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(4, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_DROITCRUD_SIROLE
(IdDroitCrud, IdSIRole)
VALUES(5, 2);

-- INSERT USER
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(82001, 'Vincent', '111@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, '2023-05-26 08:59:45.000', '2023-05-26 08:59:45.000', 'Abel', 240555599, 3);

INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96101, 'Patterson', '115@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, NULL, NULL, 'Cooper', 871285928, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96128, 'DUNORD', '555@amio.com', NULL, '$2y$10$QWWl6dATEV19JOhPwBssauu3I2FRpVREuCZ0601WFf8apn3GHSj9e', NULL, NULL, NULL, '2023-06-16 07:04:51.000', '2023-06-16 07:04:51.000', 'Dunord', NULL, 3);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96129, 'PARIS', '999@amio.com', NULL, '$2y$10$Z4liTBoaFyeaJsfbyU4UsuuSOsKSajtZ7hk/LErLcWNPA.i3XUR4.', NULL, NULL, NULL, '2023-06-16 07:45:28.000', '2023-06-16 07:45:28.000', 'Paris', NULL, 3);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96130, 'LAVAL', '399@amio.com', NULL, '$2y$10$VfccdsXotJf4DI/ILRpwlOW91tJ0pfjUaowuTO6vXVu1x/C0DWHn2', NULL, NULL, NULL, '2023-06-16 08:39:44.000', '2023-06-16 08:39:44.000', 'Laval', NULL, 3);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96106, 'Marc', '120@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, '2023-05-26 20:03:24.000', '2023-05-26 20:03:24.000', 'Dimars', NULL, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96111, 'Stéphanie', '125@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, '2023-05-31 14:19:48.000', '2023-05-31 14:19:48.000', 'Petit', NULL, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96118, 'Janne', '132@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, '2023-05-31 14:55:18.000', '2023-05-31 14:55:18.000', 'Morel', NULL, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96102, 'Wolfe', '116@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, NULL, NULL, 'Prescott', 533066766, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.users
(id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, firstname, tel, IdSIRole)
VALUES(96103, 'Wallace', '117@amio.com', NULL, '$2y$10$n5PEaXWAD44qqW3vE.7s9OdCTVR/.3VYj55DYWGIawyCuj.TNVbBK', NULL, NULL, NULL, NULL, NULL, 'Odysseus', 122046634, 2);

-- D_ENTREPRISE
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE
(Id, Nom, Tel, Email, Archive, LastModif)
VALUES(1, 'LDLV', '534231265', 'j.jean@ldlv.fr', 0, '2023-12-11 13:48:30.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE
(Id, Nom, Tel, Email, Archive, LastModif)
VALUES(2, 'materiel.web', '512986753', 'info@matweb.fr', 0, '2023-12-11 13:48:30.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE
(Id, Nom, Tel, Email, Archive, LastModif)
VALUES(3, 'Dadaweb', '0564123765', 'allo@dada.com', 0, '2023-12-11 13:48:30.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE
(Id, Nom, Tel, Email, Archive, LastModif)
VALUES(4, 'MillauInfo', '0543218769', 'informatique@millau.fr', 0, '2023-12-11 13:48:30.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_ENTREPRISE
(Id, Nom, Tel, Email, Archive, LastModif)
VALUES(5, 'iciPc', '0543217654', 'icipc@gmou.fr', 0, '2023-12-11 13:48:30.000');

-- D_MCONTRAT
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(1, 'port_G67_2022', '12 portables 17p MSI G67-E-M867', '2022-01-03 00:00:00.000', '2025-03-01 00:00:00.000', '2023-10-11 00:00:00.000', '2024-01-04 00:00:00.000', 1, 0, '2023-12-11 13:46:08.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(2, 'ecr_27p_2021', '34 écrans 27p ViewSonic A27g4563', '2021-02-04 00:00:00.000', '2025-04-02 00:00:00.000', '2023-02-04 00:00:00.000', '2024-10-04 00:00:00.000', 2, 0, '2023-12-11 13:46:08.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(3, 'cls_bur_G800', '64 souris et claviers Logiteck G800', '2022-01-05 00:00:00.000', '2026-01-05 00:00:00.000', '2023-01-05 00:00:00.000', '2024-01-05 00:00:00.000', 3, 0, '2023-12-11 13:46:08.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(4, 'mtpc_cpu_inter_iç', '32 cpu intel i9-G678432', '2022-02-05 00:00:00.000', '2026-01-05 00:00:00.000', NULL, NULL, 4, 0, '2023-12-11 13:46:08.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(5, 'bur_chais_ergo', '43 chaîses ergo Dossier Haut BDT-5-330', '2021-04-05 00:00:00.000', '2026-04-05 00:00:00.000', NULL, NULL, 5, 0, '2023-12-11 13:46:08.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MCONTRAT
(Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif)
VALUES(6, 'ContratTest2', 'ContratTest2 infos', '2024-01-03 11:20:13.000', '2024-01-03 11:20:13.000', NULL, NULL, 5, 0, '2024-01-03 11:20:13.000');

-- D_MATERIEL
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL
(Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif)
VALUES(1, 'port_G67', '2022-01-03 00:00:00.000', '2024-01-03 00:00:00.000', 82001, 1, 1, '2023-12-11 13:48:07.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL
(Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif)
VALUES(2, 'ecr_27VS', '2021-02-04 00:00:00.000', '2024-02-04 00:00:00.000', 96101, 2, 1, '2023-12-11 13:48:07.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL
(Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif)
VALUES(3, 'imprimante multifonction', NULL, NULL, 82001, 6, 0, '2023-12-21 14:43:07.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL
(Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif)
VALUES(4, 'port_G67', '2022-01-03 00:00:00.000', '2024-01-03 00:00:00.000', NULL, 1, 0, '2023-12-26 16:17:51.000');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL
(Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif)
VALUES(29, 'imprimante multifonction', '2023-12-27 20:44:10.000', NULL, 82001, 6, 0, '2023-12-28 14:18:08.000');

-- D_MATERIEL_DETRUIT
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_DETRUIT
(Id, Nom, OldNumMarquage, DateMiseEnService, DateDestruction, IdMContrat, ListCategories)
VALUES(8, 'imprimante multifonction', 53, NULL, '2024-01-01 17:00:58.000', NULL, '9,10');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_DETRUIT
(Id, Nom, OldNumMarquage, DateMiseEnService, DateDestruction, IdMContrat, ListCategories)
VALUES(11, 'imprimante multifonction', 51, NULL, '2024-01-01 17:12:15.000', 2, '9,10');

-- D_CATEGORIE
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(1, 'laptop');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(2, 'écran PC');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(3, 'chaise de bureau');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(4, 'unité centrale');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(5, 'souris');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(6, 'clavier');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(7, 'CPU');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(8, 'GPU');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(9, 'imprimante');
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_CATEGORIE
(Id, Nom)
VALUES(10, 'scanner');

-- D_MATERIEL_CATEGORIE
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(1, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(1, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(2, 2);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(3, 9);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(3, 10);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(4, 1);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(29, 9);
INSERT INTO GESTION_TICKETS_TEST_INTEGRATION.D_MATERIEL_CATEGORIE
(IdMateriel, IdCategorie)
VALUES(29, 10);


-- SET foreign_key_checks = 1;

-- VueSQL D_MaterielsInfos
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