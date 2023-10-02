﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        
        public static int HighestPopulation(City[] cities)
        {
            var highestPopulation = cities.Max(c => c.Population);
            return highestPopulation;
        }

        public static int EarliestFounding(City[] cities)
        {
            var earliestFounding = cities.Min(c =>  c.Founded);
            return earliestFounding;
        }

        public static double AveragePopulationDensity(City[] cities)
        {
            var averagePopulationDensity = Math.Round(cities.Average(c => c.Population / c.Area));
            return averagePopulationDensity;
        }

        public static string[] LongCityNames(City[] cities)
        {
            var longCityNames = cities.Where(c => c.Name.Length > 6).Select(c => c.Name).ToArray();
            return longCityNames;
        }

        // NOTE: This method still needs to be implemented.
        // It should return the list of cities sorted by age (newest first).
        // If multiple cities have the same age, those should be sorted alphabetically by name (A-Z).
        public static City[] CitiesSortedByByAgeAndName(City[] cities)
        {
            var citiesSortedByAgeAndName = cities.OrderByDescending(c => c.Founded).ThenBy(c => c.Name[0]).ToArray();
            return citiesSortedByAgeAndName;
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