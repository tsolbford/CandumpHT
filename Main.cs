using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

public class CandumpHT
{
    struct PositionValue
    {
        public String value;
        public Int32 position;
    }

    static private Dictionary<String, Color> mKeys = new Dictionary<String, Color>();
    static private Dictionary<PositionValue, Color> mHighlights = new Dictionary<PositionValue, Color>();

    static private String modify(String line)
    {
        String modified = String.Empty;
        String[] parts = line.Trim().Split(' ');

        Int32 ndx = 0;
        foreach(var part in parts)
        {
            PositionValue pv;
            pv.value = part;
            pv.position = ndx;

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
            highlights = File.ReadAllLines(Args[1]);
            if(Args.Length > 2)
            {
                filters = File.ReadAllLines(Args[2]);
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
            if(highlight.StartsWith("@"))
            {
                // Comment
                continue;
            }

            if(highlight.StartsWith("#"))
            {
                var key = highlight.Split(' ');
                mKeys.Add(key[1], Color.FromName(key[2]));
                continue;
            }

            var config = highlight.Split(' ');

            PositionValue pv;
            pv.position = Int32.Parse(config[0]);
            pv.value = config[1];
            mHighlights.Add(pv, Color.FromName(config[2]));
        }

        // Start file output
        const String file = "output.html";
        File.WriteAllText(file, "<html>\n");

        foreach(var key in mKeys)
        {
            File.AppendAllText(file, key.Key);
            File.AppendAllText(file, "<mark style=\"background-color: " + key.Value.Name +"\">_</mark>&nbsp;");
        }

        File.AppendAllText(file, "<br><br>");

        ArrayList modified = new ArrayList();
        foreach(String line in log)
        {
            modified.Add(modify(line));
        }

        File.AppendAllLines(file, (String[])modified.ToArray(typeof(String)));
        File.AppendAllText(file, "</html>");

        return;
    }
}
