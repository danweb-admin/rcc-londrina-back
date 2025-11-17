-- =============================
-- TABELA PRINCIPAL: EVENTO
-- =============================
CREATE TABLE Eventos (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NOT NULL,
    BannerImagem NVARCHAR(MAX) NULL,
    DataInicio DATETIME NOT NULL,
    DataFim DATETIME NOT NULL,
    OrganizadorNome NVARCHAR(200) NULL,
    OrganizadorEmail NVARCHAR(200) NULL,
    OrganizadorContato NVARCHAR(50) NULL,
    Status NVARCHAR(50) NULL,
    ExibirPregadores BIT NOT NULL DEFAULT 1,
    ExibirProgramacao BIT NOT NULL DEFAULT 1,
    ExibirInformacoesAdicionais BIT NOT NULL DEFAULT 1
);
GO

-- =============================
-- TABELA: SOBRE
-- =============================
CREATE TABLE Sobre (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    Titulo NVARCHAR(200) NULL,
    Conteudo NVARCHAR(MAX) NULL,

    CONSTRAINT FK_SOBRE_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: LOCAL
-- =============================
CREATE TABLE Locais (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    ImagemMapa NVARCHAR(MAX) NULL,
    Latitude FLOAT NULL,
    Longitude FLOAT NULL,
    Endereco NVARCHAR(300) NULL,
    Complemento NVARCHAR(200) NULL,
    Bairro NVARCHAR(100) NULL,
    Cidade NVARCHAR(100) NULL,
    Estado NVARCHAR(50) NULL,

    CONSTRAINT FK_LOCAL_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: INFORMACOES_ADICIONAIS
-- =============================
CREATE TABLE InformacoesAdicionais (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    Texto NVARCHAR(MAX) NULL,

    CONSTRAINT FK_INFO_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: PREGADORES
-- =============================
CREATE TABLE Participacoes (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    Nome NVARCHAR(200) NOT NULL,
    Foto NVARCHAR(MAX) NULL,
    Descricao NVARCHAR(MAX) NULL,

    CONSTRAINT FK_PREGADOR_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: PROGRAMACAO
-- =============================
CREATE TABLE Progamacoes (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    Dia NVARCHAR(50) NULL,
    Descricao NVARCHAR(MAX) NULL,

    CONSTRAINT FK_PROGRAMACAO_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: INSCRICOES
-- =============================
CREATE TABLE Inscricoes (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    CodigoInscricao varchar(20),
    Nome NVARCHAR(200) NOT NULL,
    Cpf NVARCHAR(20) NULL,
    Telefone NVARCHAR(20) NULL,
    GrupoOracao NVARCHAR(200) NULL,
    Decanato NVARCHAR(100) NULL,
    TipoPagamento NVARCHAR(50) NULL,
    Status NVARCHAR(50) NULL,

    CONSTRAINT FK_INSCRICAO_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO

-- =============================
-- TABELA: LOTES_INSCRICAO
-- =============================
CREATE TABLE LotesInscricao (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EventoId UNIQUEIDENTIFIER NOT NULL,
    Nome NVARCHAR(100) NOT NULL,              -- Exemplo: '1º Lote', '2º Lote'
    DataInicio DATETIME NOT NULL,
    DataFim DATETIME NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_LOTES_EVENTO FOREIGN KEY (EventoId)
        REFERENCES EVENTOS (Id) ON DELETE CASCADE
);
GO


ALTER TABLE Inscricoes
ADD 
GrupoOracaoId UNIQUEIDENTIFIER,
DecanatoId UNIQUEIDENTIFIER,
ServoId UNIQUEIDENTIFIER,
Email varchar(50),
CreatedAt datetime,
UpdatedAt datetime,
Active bit,
ValorInscricao decimal(10,2),
DataPagamento datetime
GO