using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;



namespace _8
{
    interface IEnum<T> //обобщенный интерфейс
    {
        void Push(T value, MyList<T> list);
        void Dell(T value, MyList<T> list); 
        void Show(MyList<T> list);
    }
   
    public class MyList<T> : List<T>, IEnum<T>
    {

        public void Push(T value, MyList<T> list)
        {
            list.Add(value);
        }

        public void Dell(T el, MyList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (el.Equals(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }

        public void Show(MyList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].ToString());
            }
            Console.WriteLine();
        }
        public void Show<U>() where U : Isp //ограничение
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is U)
                Console.WriteLine(this[i].ToString());
            }
            Console.WriteLine();
        }

    }
    
    public static class MathOperation
    {
        public static bool IsLetter(this String str, char value)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Exam
    {
        public string name;
        public int GradExam;
        public int kolvo;
        virtual public void Show()
        {
            Console.WriteLine($"Name : {name}    GradExam mark  : {GradExam}    Kolvo: {kolvo}");
        }
        public override string ToString()
        {
            string text = $"Type: Exam , Name: {name} , GradExam mark  : {GradExam}   Kolvo: {kolvo}";
            return text;
        }
    }

    public class Test : Exam
    {
        public override string ToString()
        {
            string text = $"Type: Test , Name: {name} , GradExam mark  : {GradExam}   Kolvo: {kolvo}";
            return text;
        }
    }

    public class Isp : Exam
    {
        public override string ToString()
        {
            string text = $"Type: Isp , Name: {name} , GradExam mark  : {GradExam}   Kolvo: {kolvo}";
            return text;
        }
    }

    class Question : Isp
    {
        new readonly string name = "Question 1";
        public override string ToString()
        {
            return $"Type: Question, Name: {name}, Kolvo: {kolvo}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> List1 = new MyList<int>(); //Создаём новый список

            for (int i = 1; i < 11; i++)
            {
                List1.Push(i, List1);
            }
            Console.Write("List1:   ");
            List1.Show(List1);
            Console.WriteLine();

            MyList<int> List2 = new MyList<int>(); //Создаём новый список

            for (int i = 12; i < 22; i++)
            {
                List2.Push(i, List2);
            }
            Console.Write("List2:   ");
            List2.Show(List2);
            Console.WriteLine();


            Exam test = new Exam() { name = "Math", GradExam = 9, kolvo = 4 };
            Isp quest = new Isp() { name = "Graphic", GradExam = 7, kolvo = 6 };
            Test task = new Test() { name = "Grammar", GradExam = 10, kolvo = 7 };
            Question exam = new Question() { name = "Litersture", kolvo = 3 };

            MyList<Exam> List3 = new MyList<Exam>();
            List3.Push(quest, List3);
            List3.Push(test, List3);
            List3.Push(task, List3);
            List3.Push(exam, List3);
            Console.WriteLine("Третий список:");
            List3.Show(List3);
           // используемый тип T обязательно должен быть классом Isp или его наследником
            Console.WriteLine("Список элементов типа Question:");
            List3.Show<Question>();

            Console.WriteLine("Список элементов типа Isp:");
            List3.Show<Isp>();
            //Ограничение, где U : Isp.
            //Exam не наследуется от Isp, поэтому строка ниже будет выдавать ошибку
            //List3.Show<Exam>();


            //обработка исключений
            Console.WriteLine("Extension method IsLetter : ");
            String str = "Work with extension method IsLetter";
            Console.WriteLine("String:   " + str);
            char w;
            Console.Write("Input a char : ");
            try
            {
                w = Convert.ToChar(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : " + ex.Message);
            }
            finally
            {
                Console.Write("Input a char : ");
                w = Convert.ToChar(Console.ReadLine());
            }

            if (str.IsLetter(w))
            {
                Console.WriteLine($"Char {w} in string has been");
            }
            else
            {
                Console.WriteLine($"Char {w} hasn't been in string");
            }


            //запись и считывание в файл
            string writeFile = " D:\\2 курс\\00P\\8\\List.txt";
            string text = "";

            using (StreamWriter sw = new StreamWriter(writeFile, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("List1 : ");
                for (int i = 0; i < List1.Count; i++)
                {
                    sw.Write(List1[i] + "     ");
                }
                sw.WriteLine();
            }

            using (StreamWriter sw = new StreamWriter(writeFile, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Дозапись : ");
                sw.WriteLine("List2 : ");
                for (int i = 0; i < List2.Count; i++)
                {
                    sw.Write(List2[i] + "     ");
                }
            }

            using (StreamReader sw = new StreamReader(writeFile, System.Text.Encoding.Default))
            {
                text = sw.ReadToEnd();
            }
            Console.WriteLine("Текст из файла: ");
            Console.Write(text);

            Console.ReadLine();
        }
    }
}