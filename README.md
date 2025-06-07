README.md Projet : "Gestionnaire de budget étudiant"

#  Gestionnaire de Budget Étudiant

Application de bureau développée en **C# avec WinForms** permettant aux étudiants de mieux gérer leurs revenus, dépenses et objectifs financiers mensuels.



## Objectifs du projet

- Sensibiliser à la gestion budgétaire
- Visualiser les flux financiers
- Aider à atteindre des objectifs d’épargne



## Public cible

Étudiants universitaires ou toute personne souhaitant suivre un budget personnel de manière simple et intuitive.



## Fonctionnalités principales

-  Authentification sécurisée
-  Catégorisation des revenus et dépenses
-  Alertes en cas de dépassement de budget
-  Graphiques dynamiques des flux financiers (via LiveCharts)
-  Notifications pour échéances financières   


## Fonctionnalités Ajouter


-  Exportation en PDF/EXCEL
-  Création d’objectif personnelles et suivie via le diagramme 


## Modules

- **Module Authentification**
- **Module Budgets**
- **Module Transactions**
- **Module Notifications**
- **Module Statistiques**

---

## Technologies utilisées

- Langage : `C# (.NET)`
- UI : `WinForms` (interface glisser-déposer)
- Base de données : `PostgreSQL`
- Connexion DB : `ODBC` avec PostgreSQL (psqlodbc_x86.msi pour les app en 32bit psqlodbc-setup.exe pour les app en 64bit)
- Graphiques : `LiveCharts`



##Prérequis

- Windows + Visual Studio 2019 ou 
- PostgreSQL installé
- ODBC Driver PostgreSQL configuré
- `.NET Framework` installé (4.7.2 minimum recommandé)

## Installation

1. **Cloner le projet**
  
   git clone https://github.com/AdrienTHEO/MonBudgetPlus#
   cd gestionnaire-budget-etudiant
2.	Ouvrir le projet
o	Ouvrir BudgetManager.sln dans Visual Studio
3.	Configurer la base de données
o	Suivez les étapes de l’article1 pour exporter la bd depuis le fichier  backup_budget.backup a mettre dans ton disque system( C)  et créer les tables une fois avec les donneés 
4.	Mettre à jour la chaîne de connexion ODBC
o	Expliquer dans l’article 2
5.	Exécuter l’application
o	Build et lancer avec Visual Studio (F5)
o	Si lorsque tu lance le Projet tu as une erreur :  Erreur [IM014] [Microsoft][Gestionnaire de pilotes ODBC] La source de données (DSN) spécifiée présente une incompatibilité d'architecture entre le pilote et l'application
Ça veut dire en résumé :
Ton application et ton pilote ODBC PostgreSQL n'ont pas la même "architecture" (32 bits vs 64 bits).
o	Tu dois télécharger et reconfigurer ton driver en fonction de ton App

 Sécurité & confidentialité
•	Authentification avec mot de passe hashé (ex : j’ai créé ma propre fonction de hachage pour la securisation des mots de passes )
•	Connexion sécurisée à la base de données via ODBC



 Fonctionnalités à venir (suggestions)

•	Comptes multi-utilisateurs avec rôles
•	Objectifs d’épargne automatiques

 Auteur
•	Nom : TAMBA FOKA Yde Adrien, MESSANGE CHARLES PERIG, ONGMOBE EMBUAL CATHY MALINKA 
•	Étudiant en Génie Logiciel 4 à l’ENSP de Douala
•	Contact : 696838039

 Licence
Ce projet est open-source.
Vous pouvez l’utiliser, le modifier ou le redistribuer librement.

Article 1 : importation de la BD sur ta machine 
Tu dois restaurer ta base Budget :
a) Avec pg_restore
•	Ouvre une console cmd (ou terminal en mode admin).
•	Lance cette commande :
•	Rends-toi dans le dossier bin de ton postgreSQL déjà installer dans ta machine : C:\Program Files\PostgreSQL\17\bin\
•	TAPE LA COMMANDE :
>> pg_restore -U postgres -d postgres -1 C:\backup_budget.backup
Explication :
•	-U postgres : utilisateur pour se connecter.
•	-d postgres : base de destination temporaire pour restaurer (on injecte dedans, ou tu peux créer une base vide "Budget" avant).
•	-1 : exécuter toute la restauration dans une transaction unique (pratique).
Remarque :
•	Tu peux d'abord créer la base vide Budget dans ton PostgreSQL, puis faire :

>>createdb -U postgres Budget
>>pg_restore -U postgres -d Budget C:\backup_budget.backup


Article 2 : Procédure pour établir la connexion DSN avec PostgreSQL 


Installation du pilote ODBC PostgreSQL (psqlODBC)
•	Télécharger le pilote officiel ici : https://odbc.postgresql.org/
•	Installer psqlODBC (version 32 bits ou 64 bits selon ton projet WinForms).
o	(Si ton application C# est en 32 bits, prends aussi ODBC 32 bits même si ton WApp est 64 bits.)

2. Configuration de la Source DSN
•	Ouvre Sources de données ODBC :
o	Soit via le Panneau de configuration → Outils d'administration → Sources de données ODBC.
o	Soit en tapant ODBC dans le menu Démarrer.
•	Choisis DSN Système (ou DSN Utilisateur si c'est juste pour ton compte).
•	Clique sur Ajouter (Add).
•	Choisis PostgreSQL Unicode(x64) ou PostgreSQL Unicode(x86(32bit)) selon ton installation.

3. Remplir les paramètres de connexion
Dans la fenêtre de configuration du DSN, tu dois rempli :
Champ	Valeur
Data Source Name	PostgreLocal
Description	(optionnel)
Server	localhost
Database	budget
Port	5432 (port par défaut PostgreSQL)
Username	Ton utilisateur PostgreSQL (ex: postgres)
Password	Ton mot de passe PostgreSQL
•	Tu dois cocher Save Password pour éviter de devoir la remettre.
•	Tu dois cliquer sur Test pour tester la connexion → Résultat : Connexion réussie.
•	Tu dois cliquer sur OK pour enregistrer.

