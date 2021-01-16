using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.Entities.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjetTNAI.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjetTNAI.Entities.AppDbContext context)
        {
            if (!context.Plany.Any())
            {
                var plan = new Plan()
                {
                    Nazwa = "Testowy",
                    Rok = "Drugi",
                    Grupa = "Niebieska",
                    KluczEdycji = "superTajne"
                };

                context.Plany.Add(plan);
                context.SaveChanges();

                // Przykladowe zajecia i prowadzacy
                if (!context.Zajecia.Any())
                {
                    var prowadzacy1 = new Prowadzacy
                    {
                        Imie = "Grzesiu",
                        Nazwisko = "Dzban",
                        Email = "GD@example.com",
                    };

                    var prowadzacy2 = new Prowadzacy
                    {
                        Imie = "Joanna",
                        Nazwisko = "Durszlak",
                        Email = "JoDu@example.com",
                    };

                    context.Prowadzacy.Add(prowadzacy1);
                    context.Prowadzacy.Add(prowadzacy2);
                    context.SaveChanges();

                    var zajecia = new Zajecia()
                    {
                        Nazwa = "Testowe Zajecia",
                        PlanId = plan.Id,
                        Godzina = 10,
                        CzasTrwania = 2,
                        DzienTygodnia = 3,
                        LinkDoZajec = "www.test.pl",
                        Prowadzacy = new List<Prowadzacy>
                        {
                            prowadzacy1,
                        },
                    };

                    var zajecia2 = new Zajecia()
                    {
                        Nazwa = "Inne Testowe Zajecia",
                        PlanId = plan.Id,
                        Godzina = 12,
                        CzasTrwania = 1,
                        DzienTygodnia = 3,
                        LinkDoZajec = "www.testoweInne.pl",
                        Prowadzacy = new List<Prowadzacy>
                        {
                            prowadzacy2,
                            prowadzacy1,
                        },
                    };

                    context.Zajecia.Add(zajecia);
                    context.Zajecia.Add(zajecia2);
                    context.SaveChanges();
                }

            }
        }
    }
}
