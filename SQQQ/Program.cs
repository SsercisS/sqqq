using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQQQ
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Имя таблицы");
            string Name = Console.ReadLine();
            Console.WriteLine("Имя таблицы- {0}", Name);

            List<string> Params = new List<string>();
            List<string> Types = new List<string>();

            Params.Add("id");
            Types.Add("int");

            string param;
            string type;
            for(int i=0; i<100; i++)
            {
                Console.WriteLine("Введите параметр");
                param = Console.ReadLine();

                if (param == "0") { Console.WriteLine("Конец"); break; }

                Console.WriteLine("Вы ввели параметр {0}", param);
                Params.Add(param);

                Console.WriteLine("Введите тип данных");
                type = Console.ReadLine();
                Console.WriteLine("Вы ввели тип данных {0}", type);
                Types.Add(type);

               
            }

            string code = "create table " + Name + "\n(\n"+ Params[0] + " " + Types[0] + " identity (1,1),\n";
            
            for (int i=1; i < Params.Count; i++)
            {
                code += Params[i] + " " + Types[i] + ",\n";
            }

            code += "constraint [PK_"+ Name +"] primary key clustered (["+ Params[0] +"]ASC) on [PRIMARY] \n)\n";


            string proc_ins = "create procedure [" + Name + "_ins]\n";

            for (int i=1; i<Params.Count; i++)
            {
                proc_ins += "@" + Params[i] + " " + Types[i] + ",\n";
            }
            proc_ins += "as\n insert into " + Name + " values (";
            for (int i = 1; i < Params.Count; i++)
            {
                proc_ins += " @" + Params[i] +",";
            }
            proc_ins += ")\n select SCOPE_IDENTITY()\n go";

            string proc_del = "create procedure [" + Name + "_del]\n @"+ Params[0] + " " + Types[0] +"\n as" +
                "\n delete from "+ Name + "where " + Params[0] + "= @" + Params[0] + "\n select SCOPE_IDENTITY()\n go";


            string proc_upd = "create procedure [" + Name + "_upd]\n";
            for (int i = 0; i < Params.Count; i++)
            {
                proc_upd += "@" + Params[i] + " " + Types[i] + ",\n";
            }
            proc_upd += "as\n update " + Name + " set ";
            for (int i = 1; i < Params.Count; i++)
            {
                proc_upd += " [" + Params[i] + "]= @" + Params[i] + ",";
            }
            proc_upd += " where [" + Params[0] + "]= @" + Params[0] + "\n select SCOPE_IDENTITY() \n go";
            Console.WriteLine(code);
            Console.WriteLine(proc_ins);
            Console.WriteLine(proc_del);
            Console.WriteLine(proc_upd);
            Console.ReadKey();

        }
    }
}
