

-- Categories for vocabulary files
CREATE TABLE Categories (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(16) UNIQUE
);

-- Insert allowed categories
INSERT INTO Categories (name) VALUES ('Nouns'), ('Verbs'), ('Grammar'), ('Miscellaneous'), ('Other');

CREATE TABLE Files (
    id Int IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    FOREIGN KEY (id) REFERENCES Categories(id)
);

CREATE TABLE Translations (
    id INT IDENTITY(1,1) PRIMARY KEY,
    file_id INT FOREIGN KEY REFERENCES Files(id),
    word NVARCHAR(50),
    translation NVARCHAR(50),
    date_last_practiced DATETIME NULL,
    practiced_correctly_last_month INT DEFAULT 0,  -- Amount of times it has been translated correctly in the last month
    suspended BIT DEFAULT 0, -- Whether or not the translation has been suspended and should be practiced
);