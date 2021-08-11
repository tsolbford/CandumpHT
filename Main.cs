using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

public class CandumpHT
{
    struct PositionValue
    {
        public Int32 value;
        public Int32 position;
    }

    static private Dictionary<PositionValue, Color> mHighlights = new Dictionary<PositionValue, Color>();

    static private String modify(String line)
    {
        String modified = String.Empty;
        String[] parts = line.Trim().Split(' ');

        Int32 ndx = 0;
        foreach(var part in parts)
        {
            PositionValue pv;
            pv.value = -1;
            pv.position = ndx;
            try {
                pv.value = Int32.Parse(part);
            } catch(Exception e)
            {
                e.ToString();
            }

            if(mHighlights.ContainsKey(pv))
            {
                modified += "<mark style=\"background-color: " + mHighlights[pv].Name +"\">";
                modified += part;
                modified += "</mark>";
            }
            else
            {
                modified += part;
                modified += " ";
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

        foreach(String highlight in highlights)
        {
            var config = highlight.Split(' ');

            PositionValue pv;
            pv.position = Int32.Parse(config[0]);
            pv.value = Int32.Parse(config[1]);
            mHighlights.Add(pv, Color.FromName(config[2]));
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
