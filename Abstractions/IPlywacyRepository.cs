using Domain;
using System.Collections.Generic;

namespace S3_Lab5.Abstractions
{
    public interface IPlywacyRepository
    {
        IEnumerable<Plywak> PobierzWszystkich();
        Plywak PobierzPoId(int id);
        void Dodaj(Plywak plywak);
        void Edytuj(Plywak plywak);
        void Usun(int id);
    }
}