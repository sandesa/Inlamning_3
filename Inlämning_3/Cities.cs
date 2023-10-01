using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ImperativeToFunctional
{
    public class City
    {
        public string Name;
        public int Population;
        public double Area;
        public int Founded;
    }

    public class Cities
    {
        public static void Run()
        {
            // No main method for this program. Check the rest of the code for the relevant parts.
        }

        public static int HighestPopulation(City[] cities)
        {
            int highestPopulation = cities[0].Population;
            foreach (City city in cities)
            {
                if (city.Population > highestPopulation)
                {
                    highestPopulation = city.Population;
                }
            }
            return highestPopulation;
        }

        public static int EarliestFounding(City[] cities)
        {
            int earliestFounding = cities[0].Founded;
            foreach (City city in cities)
            {
                if (city.Founded < earliestFounding)
                {
                    earliestFounding = city.Founded;
                }
            }
            return earliestFounding;
        }

        public static double AveragePopulationDensity(City[] cities)
        {
            double totalPopulationDensity = 0;
            foreach (City city in cities)
            {
                totalPopulationDensity += city.Population / city.Area;
            }
            double averagePopulationDensity = Math.Round(totalPopulationDensity / cities.Length);
            return averagePopulationDensity;
        }

        public static string[] LongCityNames(City[] cities)
        {
            List<City> longNameCities = new List<City>();
            foreach (City city in cities)
            {
                if (city.Name.Length > 6)
                {
                    longNameCities.Add(city);
                }
            }

            List<string> longNames = new List<string>();
            foreach (City city in longNameCities)
            {
                longNames.Add(city.Name);
            }

            return longNames.ToArray();
        }

        // NOTE: This method still needs to be implemented.
        // It should return the list of cities sorted by age (newest first).
        // If multiple cities have the same age, those should be sorted alphabetically by name (A-Z).
        public static City[] CitiesSortedByByAgeAndName(City[] cities)
        {
            // Put your own code here instead of the following line.
            return cities;
        }
    }

    [TestClass]
    public class CitiesTest
    {
        // This variable will contain example cities that are created before each test in the BeforeEachTest method.
        public City[] exampleCities;

        // This method is automatically run before each test to create the example data.
        [TestInitialize]
        public void BeforeEachTest()
        {
            exampleCities = new[]
            {
                new City
                {
                    Name = "Göteborg",
                    Area = 215.13,
                    Population = 607882,
                    Founded = 1621
                },
                new City
                {
                    Name = "Stockholm",
                    Area = 381.63,
                    Population = 1515017,
                    Founded = 1252
                },
                new City
                {
                    Name = "Malmö",
                    Area = 77.06,
                    Population = 325069,
                    Founded = 1250
                },
                new City
                {
                    Name = "Västerås",
                    Area = 50.43,
                    Population = 128660,
                    Founded = 990
                },
                new City
                {
                    Name = "Piteå",
                    Area = 23.85,
                    Population = 23934,
                    Founded = 1621
                },
                new City
                {
                    Name = "Luleå",
                    Area = 28.45,
                    Population = 49123,
                    Founded = 1621
                }
            };
        }

        [TestMethod]
        public void HighestPopulationTest()
        {
            int result = Cities.HighestPopulation(exampleCities);
            Assert.AreEqual(1515017, result);
        }

        [TestMethod]
        public void EarliestFoundingTest()
        {
            int result = Cities.EarliestFounding(exampleCities);
            Assert.AreEqual(990, result);
        }

        [TestMethod]
        public void AveragePopulationDensityTest()
        {
            double result = Cities.AveragePopulationDensity(exampleCities);
            Assert.AreEqual(2716, result, 0.1);
        }

        [TestMethod]
        public void LongCityNamesTest()
        {
            string[] result = Cities.LongCityNames(exampleCities);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("Göteborg", result[0]);
            Assert.AreEqual("Stockholm", result[1]);
            Assert.AreEqual("Västerås", result[2]);
        }

        [TestMethod]
        public void CitiesSortedByByAgeAndNameTest()
        {
            City[] result = Cities.CitiesSortedByByAgeAndName(exampleCities);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual("Göteborg", result[0].Name);
            Assert.AreEqual("Luleå", result[1].Name);
            Assert.AreEqual("Piteå", result[2].Name);
            Assert.AreEqual("Stockholm", result[3].Name);
            Assert.AreEqual("Malmö", result[4].Name);
            Assert.AreEqual("Västerås", result[5].Name);
        }
    }
}