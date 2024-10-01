using CsvHelper;
using System.Globalization;

namespace LINQqq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"googleplaystore1.csv");
            var googleApps = LoadGoogleAps(csvPath);

            //Display(googleApps);
            //GetData(googleApps);
            //ProjectData(googleApps);
            //DivideData(googleApps);
            OrderData(googleApps);
        }

        private static void OrderData(List<GoogleApp> googleApps)
        {
            var hightRatedBeautyApps = googleApps.Where(app => app.Rating > 4.6 & app.Category == Category.BEAUTY)
                .OrderByDescending(app=> app.Rating).ThenByDescending(app=> app.Name);

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

            var genres = hightRatedBeautyApps.SelectMany(app=> app.Genres);
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
