using System;
using System.IO;
using System.Collections;

public class CandumpHT
{
    static private String modify(String line)
    {
        String modified = String.Empty;
        String[] parts = line.Trim().Split(' ');

        Int32 ndx = 0;
        foreach(var part in parts)
        {
            switch(ndx)
            {
/*
                case 2:
                    modified += "<mark style=\"background-color: blue\">";
                    modified += part;
                    modified += "</mark>";
                    break;
*/
                default:
                    modified += part;
                    modified += " ";
                    break;
            }

            ndx++;
        };

        modified += "<br>";

        return modified;
    }

    static void Main(String[] Args)
    {
        if(Args.Length < 1)
        {
            Console.WriteLine("Usage: <candump>");
            Console.WriteLine("Options:          <filters> <highlights>");
            return;
        }

        ArrayList log = new ArrayList();
        log.AddRange(File.ReadAllLines(Args[0]));

        var filters = new String[0];
        var highlights = new String[0];
        if(Args.Length > 1)
        {
            filters = File.ReadAllLines(Args[1]);
            if(Args.Length > 2)
            {
                highlights = File.ReadAllLines(Args[2]);
            }
        }

        foreach(String filter in filters)
        {
            while(log.Contains(filter))
            {
                log.Remove(filter);
            }
        }

        ArrayList modified = new ArrayList();
        foreach(String line in log)
        {
            
            modified.Add(modify(line));
        }

        const String file = "output.html";

        File.WriteAllText(file, "<html>\n");
        File.AppendAllLines(file, (String[])modified.ToArray(typeof(String)));
        File.AppendAllText(file, "</html>");

        return;
    }
}
