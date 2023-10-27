using Newtonsoft.Json;

namespace Serialized
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<string> names = new List<string>();
            //names.Add("Azer");
            //names.Add("Amin");
            //names.Add("Nicat");
            //names.Add("Nurlan");

            //string result = JsonConvert.SerializeObject(names);

            //using (StreamWriter sw = new StreamWriter(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            //{
            //    sw.Write(result);
            //};

            string result;
            using (StreamReader sr = new StreamReader(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            {
                result = sr.ReadToEnd();
            }
            List<string> list = new List<string>();

            list = JsonConvert.DeserializeObject<List<string>>(result);

            string name = "Amin";
            Console.WriteLine(Search(x=>x.Length>3,list));
        }

        public static void Add(string name)
        {
            string result;
            using (StreamReader sr = new StreamReader(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            {
                result = sr.ReadToEnd();
            }
            List<string> list = new List<string>();

            list = JsonConvert.DeserializeObject<List<string>>(result);

            list.Add(name);

            string result2 = JsonConvert.SerializeObject(list);

            using (StreamWriter sw = new StreamWriter(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            {
                sw.Write(result2);
            };


        }

        public static void Remove(string name)
        {
            string result;
            using (StreamReader sr = new StreamReader(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            {
                result = sr.ReadToEnd();
            }
            List<string> list = new List<string>();

            list = JsonConvert.DeserializeObject<List<string>>(result);

            list.Remove(name);

            string result2 = JsonConvert.SerializeObject(list);

            using (StreamWriter sw = new StreamWriter(@"C:\Users\lenovo\OneDrive\Masaüstü\AB202\Serialized\Serialized\Files\names.json"))
            {
                sw.Write(result2);
            };



        }

        public static bool Search(Predicate<string> func,List<string> list)
        {
            if (list.Find(func) == null)
            {
                return false;
            }

            return true;
        }

    }
}