# CandumpHT
Simple text highlighting application for candump output. Output file is in html.

With this tool, you can highlight columns from previously piped to file candump output.
<br>
Optionally you can also filter out lines entirely.
<br>
Usage: CandumpHT (candump file)
<br>
Options: (highlights file) (filters file)
<br>

A typical log will look something like this
<br>
  can0  (id)   (size)  (data)(data)...

A candump log file in this format is the only required input.
<br>
Optionally, you can include highlights and filters. 
<br>
Each of these must be in the specified command line order.
<br>
Filters are expect to have lines verbatim from the log that the user does not want output to html.
<br>
Highlights have a unique format.
<br>

Lines that start with '@' are comments
<br>
@ Comment
<br>
<br>
Lines that start with '#' are used to build a key for a user's highlights
<br>
\# (name) (color)
<br>
\# Test blue
<br>
<br>
All html colors should be supported: red, blue, green, purple, fuchsia, cyan, yellow, etc...
<br>
<br>
Other lines are expected to be of the format
<br>
(column) (value) (color)
<br>2 4000 red
