using Domain;
using S3_Lab5.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace S3_Lab5
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IPlywacyRepository _repository;
        private Plywak _wybranyPlywak;
        private Plywak _formularz = new Plywak(); 

        public ObservableCollection<Plywak> Plywacy { get; set; }

        public Plywak Formularz
        {
            get => _formularz;
            set { _formularz = value; OnPropertyChanged(); }
        }

        public Plywak WybranyPlywak
        {
            get => _wybranyPlywak;
            set
            {
                _wybranyPlywak = value;
                OnPropertyChanged();

                if (_wybranyPlywak != null)
                {
                    Formularz = new Plywak
                    {
                        Id = _wybranyPlywak.Id,
                        ImieNazwisko = _wybranyPlywak.ImieNazwisko,
                        RokUrodzenia = _wybranyPlywak.RokUrodzenia,
                        NajlepszyCzas = _wybranyPlywak.NajlepszyCzas,
                        CzyAktywnyZawodnik = _wybranyPlywak.CzyAktywnyZawodnik,
                        IloscZlotychMedali = _wybranyPlywak.IloscZlotychMedali
                    };
                }
            }
        }

        public MainWindowViewModel(IPlywacyRepository repository)
        {
            _repository = repository;
            Plywacy = new ObservableCollection<Plywak>();
            OdswiezListe();
        }

        public void DodajNowego()
        {
            _repository.Dodaj(Formularz);
            OdswiezListe();
            WyczyscFormularz();
        }

        public void ZapiszZmiany()
        {
            if (Formularz.Id > 0)
            {
                _repository.Edytuj(Formularz);
                OdswiezListe();
            }
        }

        public void UsunWybranego()
        {
            if (WybranyPlywak != null)
            {
                _repository.Usun(WybranyPlywak.Id);
                OdswiezListe();
                WyczyscFormularz();
            }
        }

        public void WyczyscFormularz()
        {
            WybranyPlywak = null;
            Formularz = new Plywak();
        }

        private void OdswiezListe()
        {
            Plywacy.Clear();
            foreach (var plywak in _repository.PobierzWszystkich())
            {
                Plywacy.Add(plywak);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}