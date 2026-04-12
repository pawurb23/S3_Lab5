CREATE TABLE Plywacy (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ImieNazwisko NVARCHAR(50) NOT NULL,
    RokUrodzenia INT NOT NULL,
    NajlepszyCzas FLOAT NOT NULL,
    CzyAktywnyZawodnik BIT NOT NULL,
    IloscZlotychMedali INT NULL 
);