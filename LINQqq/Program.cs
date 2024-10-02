using CsvHelper;
using LINQqq.Person;
using System.Globalization;

namespace LINQqq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "googleplaystore1.csv");
            var googleApps = LoadGoogleAps(csvPath);

            //Display(googleApps);
            //GetData(googleApps);
            //ProjectData(googleApps);
            //DivideData(googleApps);
            //OrderData(googleApps);
            //DataSetOperation(googleApps);
            //DataVerification(googleApps);
            //GroupData(googleApps);
            //GroupDataOperations(googleApps);




            //Exercise();
        }

        private static void Exercise()
        {
            var peoples = new List<People>()
            {
                new People
                {
                    Id = 1,
                    Name = "Jakub"
                },
                new People
                {
                    Id= 2,
                    Name="John"
                },
                new People
                {
                    Id=3,
                    Name="Will"
                },
                new People
                {
                    Id=4,
                    Name="Bob"
                }
            };
            var addresses = new List<Address>()
            {
                new Address
                {
                    Id = 1,
                    PersonId = 1,
                    Street = "Street1",
                    City = "City1"
                },
                new Address
                {
                    Id = 2,
                    PersonId = 2,
                    Street = "Street2",
                    City = "City2"
                },
                new Address
                {
                    Id = 3,
                    PersonId = 4,
                    Street = "Street4a",
                    City = "City4"
                },
                new Address
                {
                    Id = 4,
                    PersonId = 4,
                    Street = "Street4b",
                    City = "City4"
                }
            };
        }

        private static void GroupDataOperations(List<GoogleApp> googleApps)
        {
            var categoryGroup = googleApps
                .GroupBy(g => g.Category);

            foreach (var group in categoryGroup)
            {
                var averageReviews = group.Average(g => g.Reviews);
                var minReviews = group.Min(g => g.Reviews);
                var maxReviews = group.Max(g => g.Reviews);
                var reviewsCount = group.Sum(g => g.Reviews);

                var allAppsFromGroupHaveRatingOfThree = group.All(g => g.Rating > 3.0);

                Console.WriteLine($"Category {group.Key}");
                Console.WriteLine($"Avrage reviews: {averageReviews}");
                Console.WriteLine($"Min reviews: {minReviews}");
                Console.WriteLine($"Max reviews: {maxReviews}");
                Console.WriteLine($"Counted reviews: {reviewsCount}");
                Console.WriteLine($"allAppsFromGroupHaveRatingOfThree {allAppsFromGroupHaveRatingOfThree}");
                Console.WriteLine("\n");
            }
        }

        private static void GroupData(List<GoogleApp> googleApps)
        {
            var categoryGroup = googleApps.GroupBy(a => a.Category);

            var group = categoryGroup.First(g => g.Key == Category.ART_AND_DESIGN).ToList();
            Display(group);
        }

        private static void DataVerification(List<GoogleApp> googleApps)
        {
            var dataSet = googleApps.Where(a => a.Category == Category.WEATHER)
                .All(a => a.Reviews > 20);
            Console.WriteLine(dataSet);

            var anyOperatorResult = googleApps.Where(a => a.Category == Category.WEATHER)
                .Any(a => a.Reviews > 4_000_000);
            Console.WriteLine(anyOperatorResult);
        }

        private static void DataSetOperation(List<GoogleApp> googleApps)
        {
            var paidAppsCategories = googleApps.Where(a => a.Type == Type.Paid)
                .Select(a => a.Category)
                .Distinct();

            //Console.WriteLine($"Paid apps categories: {string.Join(",", paidAppsCategories)}");

            var setA = googleApps.Where(a => a.Rating > 4.7 && a.Type == Type.Paid && a.Reviews > 1000);
            var setB = googleApps.Where(a => a.Name.Contains("Pro") && a.Rating > 4.6 && a.Reviews > 10000);

            //Display(setA);
            //Console.WriteLine("\n");
            //Display(setB);

            //scalamy dwa zbiory bez duplikatów
            //var setAB = setA.Union(setB);
            //Display(setAB);

            //z dwóch zbiorów dostajemy tylko wspólne dane
            //var setAB = setA.Intersect(setB);
            //Display(setAB);

            //z dwóch zbiorów dostajemy tylko dane które znajdują się z zbiorze A i nie występują w zbiorze B
            //var setAB = setA.Except(setB);
            //Display(setAB);
        }

        private static void OrderData(List<GoogleApp> googleApps)
        {
            var hightRatedBeautyApps = googleApps.Where(app => app.Rating > 4.6 & app.Category == Category.BEAUTY)
                .OrderByDescending(app => app.Rating).ThenByDescending(app => app.Name);

            Display(hightRatedBeautyApps);
        }

        private static void DivideData(List<GoogleApp> googleApps)
        {
            var hightRatedBeautyApps = googleApps.Where(app => app.Rating > 4.6 & app.Category == Category.BEAUTY);

            Display(hightRatedBeautyApps);

            Console.WriteLine("\n");

            //var firstHightRatedBeautyApps = hightRatedBeautyApps.TakeWhile(app=> app.Reviews > 1000);
            //Display(firstHightRatedBeautyApps);

            var skippedResults = hightRatedBeautyApps.Skip(5);
            Display(skippedResults);

        }

        private static void ProjectData(List<GoogleApp> googleApps)
        {
            var hightRatedBeautyApps = googleApps.Where(app => app.Rating > 4.6 & app.Category == Category.BEAUTY);
            var hightRatedBeautyAppsNames = hightRatedBeautyApps.Select(app => app.Name);


            var dtos = hightRatedBeautyApps.Select(app => new GoogleAppDto()
            {
                Name = app.Name,
                Reviews = app.Reviews
            });

            var anonymousDtos = hightRatedBeautyApps.Select(app => new
            {
                Name = app.Name,
                Reviews = app.Reviews
            });

            foreach (var dto in anonymousDtos)
            {
                Console.WriteLine($"{dto.Name}: {dto.Reviews}");
            }

            var genres = hightRatedBeautyApps.SelectMany(app => app.Genres);
            //Console.WriteLine(string.Join(":", genres));

            //Console.WriteLine(string.Join(", ",hightRatedBeautyAppsNames));
        }

        private static void GetData(List<GoogleApp> googleApps)
        {
            var hightRatedApps = googleApps.Where(app => app.Rating > 4.6);
            var hightRatedBeautyApps = googleApps.Where(app => app.Rating > 4.6 & app.Category == Category.BEAUTY);
            Display(hightRatedBeautyApps);
            Console.WriteLine("\n");
            var firstHightRatedBeautyApp = hightRatedBeautyApps.Last(app => app.Reviews < 500);
            Console.WriteLine(firstHightRatedBeautyApp);
        }

        static void Display(IEnumerable<GoogleApp> googleApps)
        {
            foreach (var googleApp in googleApps)
            {
                Console.WriteLine(googleApp);
            }
        }

        static List<GoogleApp> LoadGoogleAps(string csvPath)
        {
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GoogleAppMap>();
                var records = csv.GetRecords<GoogleApp>().ToList();
                return records;
            }

        }
    }
}
