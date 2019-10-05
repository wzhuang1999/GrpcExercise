-- https://sqlitebrowser.org/dl/

DROP TABLE IF EXISTS directory;

CREATE TABLE directory
(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	firstname VARCHAR(100) NOT NULL,
	lastname VARCHAR(100) NOT NULL,
	UNIQUE(firstname, lastname)
);