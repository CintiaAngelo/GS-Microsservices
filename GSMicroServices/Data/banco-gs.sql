use gs;


CREATE DATABASE IF NOT EXISTS promptdb;
USE promptdb;

CREATE TABLE Prompt (
    IdPrompt INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao TEXT,
    Versao INT NOT NULL,
    DataCriacao DATETIME NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    TipoModelo ENUM('Texto', 'Imagem', 'Audio', 'Video') NOT NULL,
    StatusPrompt ENUM('Ativo', 'Inativo', 'Arquivado') NOT NULL
);

select * from Prompt;