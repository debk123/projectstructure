using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
namespace automapapp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClass dc = new DataClass(100, "naresh");
            UserDataClass uc = new UserDataClass();

            int a = 10;
            int b;

            b = a;

           //copy a data from one class to another class

            uc.did = dc.did;
            uc.dname = dc.dname;

            // copy a data from one object to another object thru automapper

            // create a map
            //var cf = new MapperConfiguration(cd => cd.CreateMap<DataClass, UserDataClass>());
            //var map = new Mapper(cf);

            //uc = Mapper.Map<UserDataClass>(dc);



            UserDataClass c=getdata(uc);

            Console.WriteLine(c.did);
            Console.WriteLine(c.dname);




        }

        static UserDataClass getdata(UserDataClass d)
        {
            return d;
        }
    }

    public class DataClass
    {
        public int did { get; set; }
        public string dname { get; set; }


        public DataClass()
        {

        }

        public DataClass(int d,string n)
        {
            did = d;
            dname = n;
        }
    }


    public class UserDataClass
    {
        public int did { get; set; }
        public string dname { get; set; }
    }
}
