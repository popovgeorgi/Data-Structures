namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Microsystems : IMicrosystem
    {
        private IDictionary<int, Computer> numberForComputer;

        private IDictionary<Brand, Dictionary<int, Computer>> brands;

        private HashSet<Computer> computers;

        public Microsystems()
        {
            this.numberForComputer = new Dictionary<int, Computer>();
            this.brands = new Dictionary<Brand, Dictionary<int, Computer>>();
            this.computers = new HashSet<Computer>();
        }

        public void CreateComputer(Computer computer)
        {
            if (this.numberForComputer.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            if (!this.brands.ContainsKey(computer.Brand))
            {
                this.brands.Add(computer.Brand, new Dictionary<int, Computer>());
            }

            this.computers.Add(computer);
            this.numberForComputer.Add(computer.Number, computer);
            this.brands[computer.Brand].Add(computer.Number, computer);
        }

        public bool Contains(int number)
        {
            return this.numberForComputer.ContainsKey(number);
        }

        public int Count()
        {
            return this.numberForComputer.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.numberForComputer.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            return this.numberForComputer[number];
        }

        public void Remove(int number)
        {
            if (!this.numberForComputer.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            //not removing from computers collection
            this.numberForComputer.Remove(number);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!this.brands.ContainsKey(brand))
            {
                throw new ArgumentException();
            }

            if (this.brands[brand].Count == 0)
            {
                throw new ArgumentException();
            }

            var computerInBrand = this.brands[brand];

            foreach (var computer in computerInBrand)
            {
                this.Remove(computer.Key);
            }
            this.brands[brand].Clear();
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!this.numberForComputer.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            // we do not add to the brand collection
            if (this.numberForComputer[number].RAM < ram)
            {
                this.numberForComputer[number].RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!this.brands.ContainsKey(brand) || this.brands[brand].Count == 0)
            {
                return new List<Computer>();
            }

            return this.brands[brand].Values.OrderByDescending(x => x.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            var computersWithGivenSize = this.computers.Where(x => x.ScreenSize == screenSize);
            if (computersWithGivenSize.Count() == 0)
            {
                return new List<Computer>();
            }

            return computersWithGivenSize;
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            return this.computers.Where(x => x.Color == color);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            var computersWithMathchingPrices = this.computers.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            if (computersWithMathchingPrices.Count() == 0)
            {
                return new List<Computer>();
            }

            return this.computers.Where(x => x.Price >= minPrice && x.Price <= maxPrice).OrderByDescending(x => x.Price);
        }
    }
}
