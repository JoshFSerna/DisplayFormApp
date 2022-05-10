namespace DisplayFormApp
{
    public partial class Form1 : Form
    {
        public List<Class> Classes { get; set; }
        public List<Class> UnfilteredClasses { get; set; }
        public Form1()
        {
            Classes = getClasses();
            InitializeComponent();
            
        }

        private List<Class> getClasses()
        {

            // Get system directory and direct to csv file
            //string path = Directory.GetCurrentDirectory();
            //path = path.Remove(43, 16);
            //path += @"Schedule\csv.txt";
            // C:\Users\jfser\source\repos\DisplayFormApp\Schedule\csv.txt
            string path = @"C:\Users\jfser\source\repos\DisplayFormApp\Schedule\csv.txt";

            //String filePath = @"C:\Users\jfser\source\repos\DisplayFormApp\Schedule\csv.txt";

            // Create Stream Reader
            StreamReader reader = null;


            List<Class> classes = new List<Class>();
            List<Class> unfilteredClasses = new List<Class>();
        
            reader = new StreamReader(File.OpenRead(path));

            DateOnly now = DateOnly.Parse(DateTime.Now.ToString("MM-dd-yyyy"));
            // This DateOnly now override is for development testing only. 
            now = DateOnly.Parse("01-27-2022");

            List<string> list = new List<string>();

            // Reads in header 
            var line = reader.ReadLine();

            while (!reader.EndOfStream)
            {

                line = reader.ReadLine();

                if (!reader.EndOfStream)
                {
                    line += reader.ReadLine();
                }

                if (!reader.EndOfStream)
                {
                    line += reader.ReadLine();
                }

                var records = line.Split(',');

                if (records.Length == 0 || records[0] != "\"Subject\"")
                {
                    foreach (var item in records)
                    {
                        if (item.Length > 2)
                        {
                            list.Add(item);
                        }
                    }
                }

                unfilteredClasses.Add(new Class
                {
                    Subject = list[0],
                    RoomNumber = "106",
                    Date = DateOnly.Parse(list[1].Substring(1, list[1].Length - 2)),
                    StartTime = list[2],
                    EndTime = list[4],
                    InstructorName = list[10]
                }
                );

                list.Clear();
            }

            classes = unfilteredClasses.OrderBy(o => o.Date).ToList();
            //classes = classes.Where(o=>o.Date == now).ToList();
            
            return classes;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Classes;
            label1.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }

       
    }

    
}