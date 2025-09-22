namespace 属性的由来
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human man  = new Human();
            man.Age = 20;

            Console.WriteLine("human age:{0}", man.Age);
        }
    }

    class Human {

        private int age;

        public int Age
        {
            get { return age; }
            
            set {
                if (value >= 0 && value <= 100)
                {
                    age = value;
                }
                else { 
                    throw new OverflowException("Age overflow!");
                }
            }
        }
    
    
    
    }
}
