namespace Exam.Doodle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DoodleSearch : IDoodleSearch
    {
        private IDictionary<string, Doodle> byId;

        private HashSet<Doodle> doodles;

        public DoodleSearch()
        {
            this.byId = new Dictionary<string, Doodle>();
            this.doodles = new HashSet<Doodle>();
        }

        public int Count => this.byId.Count;

        public void AddDoodle(Doodle doodle)
        {
            this.doodles.Add(doodle);
            this.byId.Add(doodle.Id, doodle);
        }

        public bool Contains(Doodle doodle)
        {
            return this.doodles.Contains(doodle);
        }

        public Doodle GetDoodle(string id)
        {
            if (!this.byId.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            return this.byId[id];
        }

        public IEnumerable<Doodle> GetDoodleAds()
        {
            // may create another hashset
            var doodles = this.doodles.Where(x => x.IsAd == true).OrderByDescending(x => x.Revenue)
                .ThenByDescending(x => x.Visits).ToList();

            if (doodles.Count == 0)
            {
                return new List<Doodle>();
            }

            return this.doodles;
        }

        public IEnumerable<Doodle> GetTop3DoodlesByRevenueThenByVisits()
        {
            var doodles = this.doodles.OrderByDescending(x => x.Revenue).ThenByDescending(x => x.Visits).Take(3).ToList();
            if (doodles.Count == 0)
            {
                return new List<Doodle>();
            }

            return this.doodles;
        }

        public double GetTotalRevenueFromDoodleAds()
        {
            double revenue = 0;
            var ads = this.doodles.Where(x => x.IsAd == true).ToList();
            foreach (var ad in ads)
            {
                revenue += ad.Visits * ad.Revenue;
            }

            return revenue;
        }

        public void RemoveDoodle(string doodleId)
        {
            if (!this.byId.ContainsKey(doodleId))
            {
                throw new ArgumentException();
            }

            //does not remove from doodles
            this.byId.Remove(doodleId);
        }

        public IEnumerable<Doodle> SearchDoodles(string searchQuery)
        {
            var finalResult = new List<Doodle>();
            var matches = this.doodles.Where(x => x.Title.Contains(searchQuery)).Reverse().OrderByDescending(x => x.Visits).ToList();
            if (matches.Count == 0)
            {
                return new List<Doodle>();
            }

            foreach (var match in matches)
            {
                if (match.IsAd)
                {
                    finalResult.Add(match);
                }
            }

            foreach (var match in matches)
            {
                if (!match.IsAd)
                {
                    finalResult.Add(match);
                }
            }

            return finalResult;
        }

        public void VisitDoodle(string title)
        {
            var doodle = this.doodles.Where(x => x.Title == title).FirstOrDefault();
            if (doodle == null)
            {
                throw new ArgumentException();
            }

            doodle.Visits = doodle.Visits + 1;
        }
    }
}
