namespace NTTWorkFlow.Models
{
    public class Step
    {
        internal static readonly Step Begin = new Step { ID = 1, Name = "BEGIN" };
        internal static readonly Step End = new Step { ID = 9999, Name = "END" };

        public int ID { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }

        public Step Next { get; set; }

        public static Step Load(int pID)
        {
            return new Step()
            {
                ID = pID
            };
        }

    }
}
