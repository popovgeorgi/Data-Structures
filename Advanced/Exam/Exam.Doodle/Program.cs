using System;

namespace Exam.Doodle
{
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var doodleSearch = new DoodleSearch();

            Doodle Doodle = new Doodle("asd", "bbbsd", 4000, true, 5.5);
            Doodle Doodle2 = new Doodle("nsd", "eesd", 4000, false, 5.6);
            Doodle Doodle3 = new Doodle("dsd", "ddsd", 5000, false, 5.7);
            Doodle Doodle4 = new Doodle("hsd", "zsd", 4000, true, 4.8);
            Doodle Doodle5 = new Doodle("qsd", "qsd", 4001, true, 4.8);
            Doodle Doodle6 = new Doodle("zsd", "ds", 5000, false, 5.7);

            doodleSearch.AddDoodle(Doodle);
            doodleSearch.AddDoodle(Doodle2);
            doodleSearch.AddDoodle(Doodle3);
            doodleSearch.AddDoodle(Doodle4);
            doodleSearch.AddDoodle(Doodle5);
            doodleSearch.AddDoodle(Doodle6);

            List<Doodle> Doodles = new List<Doodle>(doodleSearch.SearchDoodles("sd"));

            Console.WriteLine();
        }
    }
}
