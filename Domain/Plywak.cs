using System;

namespace Domain
{
    public class Plywak
    {
        public int Id { get; set; }
        public string ImieNazwisko { get; set; } = string.Empty;
        public int RokUrodzenia { get; set; }
        public double NajlepszyCzas { get; set; }
        public bool CzyAktywnyZawodnik { get; set; }
        public int? IloscZlotychMedali { get; set; }

        public string Wizytowka
        {
            get
            {
                string medale = IloscZlotychMedali.HasValue ? IloscZlotychMedali.Value.ToString() : "Brak";
                return $"{ImieNazwisko} ({RokUrodzenia}) - {NajlepszyCzas}s; Medale: {medale}";
            }
        }
    }
}