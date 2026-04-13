using Domain;
using S3_Lab5.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace S3_Lab5.Tests
{
    public class FakePlywacyRepository : IPlywacyRepository
    {
        public event Action<string> Sukces;
        private readonly List<Plywak> _plywacy = new List<Plywak>();

        private void Powiadom(string komunikat) => Sukces?.Invoke(komunikat);

        public IEnumerable<Plywak> PobierzWszystkich()
        {             
            Powiadom("Pobrano wszystkich pływaków");
            return _plywacy; 
        }

        public Plywak PobierzPoId(int id) { return _plywacy.FirstOrDefault(p => p.Id == id); }

        public void Dodaj(Plywak plywak)
        {
            if (_plywacy.Any()) { plywak.Id = _plywacy.Max(p => p.Id) + 1; }
            else { plywak.Id = 1; }

            _plywacy.Add(plywak);

            Powiadom($"Dodano zawodnika {plywak.ImieNazwisko}");
        }

        public void Edytuj(Plywak plywak)
        {
            var istniejacyPlywak = PobierzPoId(plywak.Id);
            
            if (istniejacyPlywak != null)
            {
                istniejacyPlywak.ImieNazwisko = plywak.ImieNazwisko;
                istniejacyPlywak.RokUrodzenia = plywak.RokUrodzenia;
                istniejacyPlywak.NajlepszyCzas = plywak.NajlepszyCzas;
                istniejacyPlywak.CzyAktywnyZawodnik = plywak.CzyAktywnyZawodnik;
                istniejacyPlywak.IloscZlotychMedali = plywak.IloscZlotychMedali;
            }

            Powiadom($"Zaktualizowano dane zawodnika o ID {plywak.Id}");
        }

        public void Usun(int id)
        {
            var plywakDoUsuniecia = PobierzPoId(id);
            if (plywakDoUsuniecia != null) { _plywacy.Remove(plywakDoUsuniecia); }

            Powiadom($"Usunięto zawodnika o ID {id}");
        }
    }
}