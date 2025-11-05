using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LibraryCheckIn.Domain;
using LibraryCheckIn.Extensions;

namespace LibraryCheckIn.IO
{
    public sealed class XmlReportWriter : IReportWriter<Book>
    {
        public void Write(IEnumerable<Book> books, string path)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var total = books.Count();
            var conditionCounts = books.ToConditionCounts();
            var topBooks = books.TopBy(b => b.Condition, 5);

            var root = new XElement("DailySummary",
                new XElement("Processed", now),
                new XElement("TotalReturns", total),
                new XElement("ConditionCounts",
                    conditionCounts.Select(kv =>
                        new XElement("Condition",
                            new XAttribute("Name", kv.Key),
                            new XAttribute("Count", kv.Value)))),
                new XElement("TopBooks",
                    topBooks.Select(b =>
                        new XElement("Book",
                            new XAttribute("Id", b.Id),
                            new XElement("Title", b.Title),
                            new XElement("Author", b.Author),
                            new XElement("Penalty", (int)b.Condition))))
            );

            var doc = new XDocument(root);
            doc.Save(path);
        }
    }
}
