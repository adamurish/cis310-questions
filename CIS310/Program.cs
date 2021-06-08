using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS310
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PubsContext())
            {
                //Question Set 1
                //1.
                var query11 =
                    from author in db.authors
                    where author.state != "CA"
                    select author.au_id;
                Console.WriteLine("Question 1.1");
                foreach (var au_id in query11) Console.WriteLine("\t{0}", au_id);

                //2.
                var query12 =
                    from author in db.authors
                    where author.au_lname.StartsWith("D")
                    select author.au_lname;
                Console.WriteLine("Question 1.2");
                foreach (var lname in query12) Console.WriteLine("\t{0}", lname);

                //3.
                var query131 =
                    from author in db.authors
                    group author by author.state;
                Console.WriteLine("Question 1.3");
                foreach (var key in query131)
                {
                    if (key.Count() == 2) Console.WriteLine("\t{0}", key.Key);
                }

                //Question Set 2
                //1.
                var query21 =
                    from title in db.titles
                    orderby title.price descending
                    select title.title1;
                Console.WriteLine("Question 2.1");
                Console.WriteLine("\t{0}", query21.First());

                //2.
                var query22 =
                    from title in db.titles
                    orderby title.pubdate descending
                    select title.title1;
                Console.WriteLine("Question 2.2");
                Console.WriteLine("\t{0}", query22.First());

                //3.
                var query23 =
                    from title in db.titles
                    select new { title.title1, title.publisher.pub_name };
                Console.WriteLine("Question 2.3");
                foreach (var pair in query23) Console.WriteLine("\t{0}", pair);

                //4.
                var query24 =
                    from title in db.titles
                    group title by title.publisher;
                Console.WriteLine("Question 2.4");
                foreach (var pub in query24)
                {
                    Console.WriteLine("\tTitles under publisher {0}", pub.Key.pub_name);
                    foreach (var title in pub)
                    {
                        Console.WriteLine("\t\t{0}", title.title1);
                    }
                }

                //5.
                var query25 =
                    from sale in db.sales
                    where sale.title_id == "PC1035"
                    select sale.store;
                var query251 =
                    from store in db.stores
                    where !query25.Contains(store)
                    select store;
                Console.WriteLine("Question 2.5");
                foreach (var store in query251) Console.WriteLine("\t{0}", store.stor_name);

                //Question Set 3
                //1.
                var query31 =
                    from emp in db.employees
                    where emp.pub_id == "0877"
                    select emp.fname;
                Console.WriteLine("Question 3.1");
                foreach (var name in query31) Console.WriteLine(name);

                //2.
                var query32 =
                    from pr in db.pub_info
                    from pub in db.publishers
                    where pr.pub_id == "0736"
                    where pub.pub_id == "0736"
                    select new { pr.pr_info, pub.pub_name };
                Console.WriteLine("Question 3.2");
                Console.WriteLine(query32.First());

                //3.
                var query33 =
                    from auth in db.authors
                    where auth.state == "TN" || auth.state == "UT"
                    select auth;
                Console.WriteLine("Question 3.3");
                foreach (var author in query33) Console.WriteLine("{0} {1}", author.au_fname, author.au_lname);

                //4.
                var query34 =
                    from auth in db.authors
                    group auth by auth.address into g
                    where g.Count() >= 2
                    select g;
                Console.WriteLine("Question 3.4");
                foreach (var addr in query34)
                {
                    Console.WriteLine("Authors at {0}", addr.Key);
                    foreach (var auth in addr) Console.WriteLine("{0} {1}", auth.au_fname, auth.au_lname);
                }

                //5.
                var query35 =
                    from pub in db.publishers
                    where pub.state != "DC"
                    select new { pub.pub_name, pub.state };
                Console.WriteLine("Question 3.5");
                foreach (var pub in query35) Console.WriteLine("Pub: {0} State: {1}", pub.pub_name, pub.state);

                //Question Set 4
                //1.
                var query41 =
                    from title in db.titles
                    group title by title.publisher into g
                    orderby g.Count() descending
                    select g;
                Console.WriteLine("Question 4.1");
                Console.WriteLine(query41.First().Key.pub_name);

                //2.
                var query42 =
                    from title in db.titles
                    where title.publisher.pub_name == "Algodata Infosystems" ||
                        title.publisher.pub_name == "Binnet & Hardley" ||
                        title.publisher.pub_name == "New Moon Books"
                    group title by title.publisher;
                Console.WriteLine("Question 4.2");
                foreach (var pub in query42)
                {
                    Console.WriteLine("{0} published {1} books", pub.Key.pub_name, pub.Count());
                }

                //Question Set 5
                //1.

            }
            Console.ReadKey();
        }
    }
}
